using MinVideoEditor.Workers;

namespace MinVideoEditor
{
    public partial class Form1 : Form
    {
        private Config mConfig;

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
    }
}