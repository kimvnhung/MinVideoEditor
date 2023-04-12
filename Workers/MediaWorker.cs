using MediaToolkit.Model;
using MediaToolkit.Options;
using MediaToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MinVideoEditor.Workers
{
    public class MediaWorker
    {
        public event EventHandler<int> UpdateProcessChanged;
        protected virtual void OnUpdateProcessChanged(int percentage)
        {
            UpdateProcessChanged?.Invoke(this, percentage);
        }

        private Config mConfig = null;
        private bool mIsRunning = false;
        private int mPercentage = 0;
        public MediaWorker(Config config) {
            mConfig = config;
        }

        public void SetConfig(Config config)
        {
            mConfig = config;
        }

        public void StartGenerate()
        {
            Task.Run(async () =>
            {
                mIsRunning = true;
                mPercentage = 0;
                UpdatePerentage();
                await ConcatVideo();
                mIsRunning = false;
            });
        }

        private async Task UpdatePerentage()
        {
            
            while(mIsRunning)
            {
                await Task.Delay(500);
                OnUpdateProcessChanged(mPercentage);
            }
        }

        public async Task<bool> ConcatVideo()
        {
            if (mConfig != null)
            {
                ConcatConfig concatcf = mConfig.ConcatConfig;
                if (concatcf != null)
                {
                    string ouputPath = Path.GetDirectoryName(concatcf.StartVideos[0]) + "/output_" + DateTime.Now.ToString("hhmm00yyMMdd");
                    if (concatcf.StartVideos.Count > 0 && !Directory.Exists(ouputPath)) {
                        Directory.CreateDirectory(ouputPath);
                    }else if(concatcf.StartVideos.Count == 0 || concatcf.EndVideos.Count == 0)
                    {
                        return false;
                    }
                    int step = 100 / (concatcf.StartVideos.Count * concatcf.EndVideos.Count);
                    var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
                    NReco.VideoConverter.ConcatSettings set = new NReco.VideoConverter.ConcatSettings();
                    NReco.VideoConverter.ConvertSettings convertSettings = new NReco.VideoConverter.ConvertSettings { 
                        VideoFrameRate = 30
                    };
                    try
                    {
                        string startOutTemp = Path.GetDirectoryName(concatcf.StartVideos[0])+"/startTemp";
                        string endOutTemp = Path.GetDirectoryName(concatcf.EndVideos[0]) + "/endTemp";
                        if(Directory.Exists(startOutTemp))
                        {
                            Directory.Delete(startOutTemp);   
                        }

                        Directory.CreateDirectory(startOutTemp);
                        if (Directory.Exists(endOutTemp))
                        {
                            Directory.Delete(endOutTemp);
                        }
                        Directory.CreateDirectory(endOutTemp);

                        for (int i = 0; i < concatcf.StartVideos.Count; i++)
                        {
                            ffMpeg.ConvertMedia(
                                concatcf.StartVideos[i],
                                NReco.VideoConverter.Format.mp4,
                                startOutTemp+"/"+Path.GetFileName(concatcf.StartVideos[i]),
                                NReco.VideoConverter.Format.mp4,
                                convertSettings
                            );
                            concatcf.StartVideos[i] = startOutTemp + "/" + Path.GetFileName(concatcf.StartVideos[i]);
                            for (int j = 0; j < concatcf.EndVideos.Count; j++)
                            {
                                mPercentage += step;
                                if(i==0)
                                {
                                    ffMpeg.ConvertMedia(
                                        concatcf.EndVideos[j],
                                        NReco.VideoConverter.Format.mp4,
                                        endOutTemp + "/" + Path.GetFileName(concatcf.EndVideos[j]),
                                        NReco.VideoConverter.Format.mp4,
                                        convertSettings
                                    );
                                    concatcf.EndVideos[j] = endOutTemp + "/" + Path.GetFileName(concatcf.EndVideos[j]);
                                }
                                await Task.Run(() =>
                                {
                                    try
                                    {
                                        ffMpeg.LogReceived += FfMpeg_LogReceived;
                                        ffMpeg.ConcatMedia(
                                            new string[] { concatcf.StartVideos[i], concatcf.EndVideos[j] },
                                            ouputPath + "/" + Path.GetFileNameWithoutExtension(concatcf.StartVideos[i]) + "_" + Path.GetFileNameWithoutExtension(concatcf.EndVideos[j]) + ".mp4",
                                            NReco.VideoConverter.Format.mp4,
                                            set);
                                        
                                        ffMpeg.Abort();
                                    }catch(Exception ex)
                                    {
                                        Utils.Log(ex.Message);
                                    }

                                });
                            }
                        }
                        Directory.Delete(startOutTemp);
                        Directory.Delete(endOutTemp);
                    }catch(Exception ex)
                    {
                        Utils.Log(ex.Message);
                    }
                    mPercentage = 100;
                    return true;
                }
            }

            return false;
        }

        private void FfMpeg_LogReceived(object? sender, NReco.VideoConverter.FFMpegLogEventArgs e)
        {
            Console.WriteLine(e.Data.ToString());
        }
    }
}
