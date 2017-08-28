using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace wavesAudio
{
    public partial class Form1 : Form
    {
        List<Songs> newWaveList = new List<Songs>();
        public Form1()
        {
            InitializeComponent();
            trackBar1.Value = 32;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            media.Ctlcontrols.play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            media.Ctlcontrols.stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            media.Ctlcontrols.pause();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string fileName, path;
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.Multiselect = false;
            FolderBrowserDialog directchoosedlg = new FolderBrowserDialog();
            oFD.Filter = "Mpr3 Files |*.mp3";

            if (oFD.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileName = oFD.SafeFileName;
                    path = oFD.FileName;

                        Songs newSong = new Songs(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, path);
                        newWaveList.Add(newSong);
                        listBox1.Items.Add(newSong.songName.ToString() + ".                             " + newSong.artist.ToString() + ".                             " + newSong.duration.ToString() + ".                             " + newSong.album.ToString());
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to load files. " + ex.Message);
                    throw;
                }

            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            media.settings.volume = trackBar1.Value;
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            media.URL = newWaveList.ElementAt(listBox1.SelectedIndex).path;
        }
    }
}
