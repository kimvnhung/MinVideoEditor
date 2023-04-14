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
                    ffMpeg.LogReceived += FfMpeg_LogReceived;
                    NReco.VideoConverter.ConcatSettings set = new NReco.VideoConverter.ConcatSettings {
                        VideoFrameRate = 30
                    };
                    
                    NReco.VideoConverter.ConvertSettings convertSettings = new NReco.VideoConverter.ConvertSettings { 
                        VideoFrameRate = 30,
                        VideoFrameSize = "2160x2700",
                    };
                    try
                    {
                        string startOutTemp = Path.GetDirectoryName(concatcf.StartVideos[0])+"/startTemp";
                        string endOutTemp = Path.GetDirectoryName(concatcf.EndVideos[0]) + "/endTemp";
                        try
                        {
                            if (Directory.Exists(startOutTemp))
                            {
                                Directory.Delete(startOutTemp,true);
                            }

                            Directory.CreateDirectory(startOutTemp);
                            if (Directory.Exists(endOutTemp))
                            {
                                Directory.Delete(endOutTemp,true);
                            }
                            Directory.CreateDirectory(endOutTemp);
                        }catch (Exception ex)
                        {
                            Utils.Log(ex.Message);
                        }
                        for (int i = 0; i < concatcf.StartVideos.Count; i++)
                        {
                            ffMpeg.ConvertMedia(
                                concatcf.StartVideos[i],
                                Path.GetExtension(concatcf.StartVideos[i]).Substring(1),
                                startOutTemp + "/" + Path.GetFileName(concatcf.StartVideos[i]),
                                Path.GetExtension(concatcf.StartVideos[i]).Substring(1),
                                convertSettings
                            );
                            concatcf.StartVideos[i] = startOutTemp + "/" + Path.GetFileName(concatcf.StartVideos[i]);
                            for (int j = 0; j < concatcf.EndVideos.Count; j++)
                            {
                                mPercentage += step;
                                if (i == 0)
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
                        Directory.Delete(startOutTemp, true);
                        Directory.Delete(endOutTemp, true);
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
            Utils.Log(e.Data.ToString());
        }
    }
}
