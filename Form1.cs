using MinVideoEditor.Workers;

namespace MinVideoEditor
{
    public partial class Form1 : Form
    {
        private Config mConfig;
        private MediaWorker mWorker;

        public Form1()
        {
            InitializeComponent();

            InitView();
        }

        private void InitView()
        {
            videoListLv.HeaderStyle = ColumnHeaderStyle.None;
            videoListLv.View = View.Details;
            mConfig = new Config();
            mConfig.LoadFromFile();


        }

        private void openVideoBtn_Click(object sender, EventArgs e)
        {
            var openFile = new OpenFileDialog()
            {
                Title = "Mở file videos",
                Filter = "Video files (*.mp4;*.avi;*.mov;*.wmv;*.flv;*.mkv)|*.mp4;*.avi;*.mov;*.wmv;*.flv;*.mkv",
                Multiselect = true,
                InitialDirectory = mConfig.LastOpenPath,
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                mConfig.VideoPaths = openFile.FileNames;
                for (int i = 0; i < mConfig.VideoPaths.Length; i++)
                {
                    if (i == 0)
                    {
                        mConfig.LastOpenPath = mConfig.VideoPaths[i];
                    }
                    videoListLv.Items.Add(Path.GetFileName(mConfig.VideoPaths[i]));
                    string fileName = Path.GetFileName(mConfig.VideoPaths[i]);
                    if (fileName.ToLower().Contains("start"))
                    {
                        startBoxList.Items.Add(Path.GetFileName(mConfig.VideoPaths[i]));
                        startBoxList.SetItemChecked(startBoxList.Items.Count - 1, true);
                    }
                    else
                    {
                        endBoxList.Items.Add(Path.GetFileName(mConfig.VideoPaths[i]));
                        endBoxList.SetItemChecked(endBoxList.Items.Count - 1, true);
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mConfig != null)
            {
                mConfig.SaveToFile();
            }
        }

        private void amountNud_ValueChanged(object sender, EventArgs e)
        {
            mConfig.Amount = (int)amountNud.Value;
        }

        private void randomCb_CheckedChanged(object sender, EventArgs e)
        {
            mConfig.IsRandomConfig = randomCb.Checked;
        }

        private void speedVideoCbb_CheckedChanged(object sender, EventArgs e)
        {
            if (speedVideoCbb.Checked)
            {
                mConfig.SpeedConfig = new SpeedConfig((double)minSpeedNud.Value, (double)maxSpeedNud.Value);
            }
            else
            {
                mConfig.SpeedConfig = null;
            }
        }

        private void changeVoiceBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (changeVoiceBtn.Checked)
            {
                mConfig.SoundConfig = new SoundConfig(maleVoiceCbb.Checked, femaleVoiceCbb.Checked, insertVoiceCbb.Checked);
            }
            else
            {
                mConfig.SoundConfig = null;
            }
        }

        private void randomConcatCbb_CheckedChanged(object sender, EventArgs e)
        {
            onRandomConcatConfigChanged();
        }

        private void onRandomConcatConfigChanged()
        {
            if (randomConcatCbb.Checked)
            {
                var listStarts = ((CheckedListBox)videoStartGrp.Controls[0]).CheckedItems;
                var listEnds = ((CheckedListBox)videoEndGrp.Controls[0]).CheckedItems;
                List<string> startVideos = new List<string>();
                List<string> endVideos = new List<string>();

                for (int i = 0; i < listStarts.Count; i++)
                {
                    for (int j = 0; j < mConfig.VideoPaths.Length; j++)
                    {
                        if (mConfig.VideoPaths[j].Contains(listStarts[i].ToString()))
                        {
                            startVideos.Add(mConfig.VideoPaths[j]);
                            break;
                        }
                    }
                }

                for (int i = 0; i < listEnds.Count; i++)
                {
                    for (int j = 0; j < mConfig.VideoPaths.Length; j++)
                    {
                        if (mConfig.VideoPaths[j].Contains(listEnds[i].ToString()))
                        {
                            endVideos.Add(mConfig.VideoPaths[j]);
                            break;
                        }
                    }
                }

                mConfig.ConcatConfig = new ConcatConfig(startVideos, endVideos);
            }
            else
            {
                mConfig.ConcatConfig = null;
            }
        }

        private void startGenerateBtn_Click(object sender, EventArgs e)
        {

            if (mWorker == null)
            {
                mWorker = new MediaWorker(mConfig);
                mWorker.UpdateProcessChanged += MWorker_UpdateProcessChanged;
            }
            else
            {
                mWorker.SetConfig(mConfig);
            }

            mWorker.StartGenerate();
        }

        private void MWorker_UpdateProcessChanged(object? sender, int e)
        {
            processBar.Invoke((MethodInvoker)delegate
            {
                processBar.Value = e;
            });
        }

        private void startFilterTb_TextChanged(object sender, EventArgs e)
        {
            startBoxList.Items.Clear();
            for(int i = 0;i< mConfig.VideoPaths.Length;i++)
            {
                string fileName = Path.GetFileName(mConfig.VideoPaths[i]);
                if (fileName.ToLower().Contains(startFilterTb.Text.ToLower())){
                    startBoxList.Items.Add(fileName);
                    startBoxList.SetItemChecked(startBoxList.Items.Count - 1, true);
                }
            }
        }

        private void endFilterTb_TextChanged(object sender, EventArgs e)
        {
            endBoxList.Items.Clear();
            for (int i = 0; i < mConfig.VideoPaths.Length; i++)
            {
                string fileName = Path.GetFileName(mConfig.VideoPaths[i]);
                if (fileName.ToLower().Contains(endFilterTb.Text.ToLower()))
                {
                    endBoxList.Items.Add(fileName);
                    endBoxList.SetItemChecked(endBoxList.Items.Count - 1, true);
                }
            }
        }
    }
}