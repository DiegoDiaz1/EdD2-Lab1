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
        

        #region Global variables and Form init.
        /// <summary>
        /// Variables Globales 
        /// newWaveList es el listado  de las canciones en el programa.
        /// Path es donde se almacena el lungar en donde se encuentra el archivo a reproducir.
        /// newPlaylist es el listado general de canciones que contiene la playlists del programa.
        /// dicPlaylistQueue es el listado de playlists del programa.
        /// </summary>
        List<Songs> newWaveList = new List<Songs>();
        string fileName, path;
        int contadorPlaylists = 0;
        Playlists newPlaylist = new Playlists();
        List<Playlists> playListsQueue = new List<Playlists>();
        Dictionary<int, Playlists> dicPlaylistQueue = new Dictionary<int, Playlists>();
        List<Songs> findList = new List<Songs>();
        public Form1()
        {
            InitializeComponent();
            ///Valor del Trackbar que actuara como el volumen default del reproductor.
            trackBar1.Value = 32;
            ///Volumen inicial del reproductor.
            media.settings.volume = trackBar1.Value;
            ///Inicializacion de los Textbox y valor que tomaran en caso de faltar un campo a la hora de agregar los valores
            textBox1.Text = "Desconocido";
            textBox2.Text = "Desconocido";
            textBox3.Text = "00";
            textBox4.Text = "00";
            textBox6.Text = "Desconocido";
            newPlaylist.name = "Main PlayList";
            comboBox3.Items.Add(newPlaylist.name);
            dicPlaylistQueue.Add(0,newPlaylist);
            for (int i = 0; i < dicPlaylistQueue.Count; i++)
            {
                listBox2.Items.Add(dicPlaylistQueue.ElementAt(i).Value.name);
            }
        
        }
        #endregion



        #region Media Controls
        private void button1_Click(object sender, EventArgs e) => media.Ctlcontrols.play();
        private void button3_Click(object sender, EventArgs e) => media.Ctlcontrols.stop();
        private void button2_Click(object sender, EventArgs e) => media.Ctlcontrols.pause();

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            media.settings.volume = trackBar1.Value;
        }

        #endregion


        //Manejador de archivos de Windows
        private void button4_Click(object sender, EventArgs e)
        {
            
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to load files. " + ex.Message);
                    throw;
                }

            }
        }

        //Metodo para reproducir la cancion luego de seleccionarla en la lista.
        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                media.URL = dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(listBox1.SelectedIndex).path;

            }
            catch (Exception ex)
            {
                MessageBox.Show("La Ruta del archivo no fue especificada o se cambio de lugar el archivo." + ex.Message);
                throw;
            }
        }

        //Mostrar elementos en el ListBox Principal
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.Count; i++)
            {
                listBox1.Items.Add(dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).songName + ".                             " + dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).artist + ".                             " + dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).duration + ".                             " + dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).album);
            }
        }




        #region Instancia de referencias Accidentales.
                //NADA>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
                {

                }
                //NADA>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
                {

                }
        #endregion

        #region List Methods

        //Metodo que compara si el nombre de la lista ya esta en uso y si no crea una nueva lista.
        private void button6_Click(object sender, EventArgs e)
        {
            bool playlistExist = false;
            int n = 0;
            while (n < dicPlaylistQueue.Count)
            {
                if (dicPlaylistQueue.ElementAt(n).Value.name != textBox5.Text)
                {
                    n++;
                }
                else
                {
                    n = dicPlaylistQueue.Count;
                    playlistExist = true;
                }
            }
            if (playlistExist)
            {
                MessageBox.Show("El nombre de la lista ya existe");
            }
            else
            {
                Playlists nuevaPlaylist = new Playlists();
                nuevaPlaylist.name = textBox5.Text;
                comboBox3.Items.Add(nuevaPlaylist.name);
                listBox2.Items.Add(nuevaPlaylist.name);
                contadorPlaylists = contadorPlaylists + 1;
                textBox5.Text = "";
                dicPlaylistQueue.Add(contadorPlaylists, nuevaPlaylist);
            }

        }

        //Metodo para mostrar los elementos de la lista en la ventanilla de agregar playlists o musica playlists.
        private void listBox2_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            for (int i = 0; i < dicPlaylistQueue.ElementAt(0).Value.newPlaylist.Count; i++)
            {
                listBox4.Items.Add(dicPlaylistQueue.ElementAt(0).Value.newPlaylist.ElementAt(i).songName.ToString() + "               " + dicPlaylistQueue.ElementAt(0).Value.newPlaylist.ElementAt(i).artist.ToString());
            }

            for (int i = 0; i < dicPlaylistQueue.ElementAt(listBox2.SelectedIndex).Value.newPlaylist.Count; i++)
            {
                listBox3.Items.Add(dicPlaylistQueue.ElementAt(listBox2.SelectedIndex).Value.newPlaylist.ElementAt(i).songName.ToString() + "               " + dicPlaylistQueue.ElementAt(listBox2.SelectedIndex).Value.newPlaylist.ElementAt(i).artist.ToString());
            }

        }

        //Metodo que agreaga canciones a las Listas
        private void listBox3_MouseDoubleClick(object sender, MouseEventArgs e)
                {
                    dicPlaylistQueue.ElementAt(listBox2.SelectedIndex).Value.newPlaylist.RemoveAt(listBox3.SelectedIndex);
                    listBox3.Items.Clear();
                    for (int i = 0; i < dicPlaylistQueue.ElementAt(listBox2.SelectedIndex).Value.newPlaylist.Count; i++)
                    {
                        listBox3.Items.Add(dicPlaylistQueue.ElementAt(listBox2.SelectedIndex).Value.newPlaylist.ElementAt(i).songName + ".                             " + dicPlaylistQueue.ElementAt(listBox2.SelectedIndex).Value.newPlaylist.ElementAt(i).artist + ".                             " + dicPlaylistQueue.ElementAt(listBox2.SelectedIndex).Value.newPlaylist.ElementAt(i).duration + ".                             " + dicPlaylistQueue.ElementAt(listBox2.SelectedIndex).Value.newPlaylist.ElementAt(i).album);
                    }
                }

        //Metodo que elimina canciones de las listas
        private void listBox4_DoubleClick(object sender, EventArgs e)
                {
                    dicPlaylistQueue.ElementAt(listBox2.SelectedIndex).Value.newPlaylist.Add(dicPlaylistQueue.ElementAt(0).Value.newPlaylist.ElementAt(listBox4.SelectedIndex));
                    listBox3.Items.Clear();

                    for (int i = 0; i < dicPlaylistQueue.ElementAt(listBox2.SelectedIndex).Value.newPlaylist.Count; i++)
                    {
                        listBox3.Items.Add(dicPlaylistQueue.ElementAt(listBox2.SelectedIndex).Value.newPlaylist.ElementAt(i).songName + ".                             " + dicPlaylistQueue.ElementAt(listBox2.SelectedIndex).Value.newPlaylist.ElementAt(i).artist + ".                             " + dicPlaylistQueue.ElementAt(listBox2.SelectedIndex).Value.newPlaylist.ElementAt(i).duration + ".                             " + dicPlaylistQueue.ElementAt(listBox2.SelectedIndex).Value.newPlaylist.ElementAt(i).album);
                    }

                }
        #endregion


        //Metodo de Ordenamiento
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    if (comboBox1.SelectedIndex == 0)
                    {
                        dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist = dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.OrderBy(x => x.songName).ToList();
                        listBox1.Items.Clear();
                        for (int i = 0; i < dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.Count; i++)
                        {
                            listBox1.Items.Add(dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).songName + ".                             " + dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).artist + ".                             " + dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).duration + ".                             " + dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).album);
                        }
                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                       dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist = dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.OrderByDescending(x => x.songName).ToList();
                        listBox1.Items.Clear();
                        for (int i = 0; i < dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.Count; i++)
                        {
                            listBox1.Items.Add(dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).songName + ".                             " + dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).artist + ".                             " + dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).duration + ".                             " + dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).album);
                        }
                    }
                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    if (comboBox1.SelectedIndex == 0)
                    {
                        dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist = dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.OrderBy(x => x.duration).ToList();
                        listBox1.Items.Clear();
                        for (int i = 0; i < dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.Count; i++)
                        {
                            listBox1.Items.Add(dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).songName + ".                             " + dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).artist + ".                             " + dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).duration + ".                             " + dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).album);
                        }
                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                        dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist = dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.OrderByDescending(x => x.duration).ToList();

                        listBox1.Items.Clear();
                        for (int i = 0; i < dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.Count; i++)
                        {
                            listBox1.Items.Add(dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).songName + ".                             " + dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).artist + ".                             " + dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).duration + ".                             " + dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.ElementAt(i).album);
                        }
                    }
                }   
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seleccione los campos del otrdenamiento deseado." + ex.Message);
                throw;
            }

        }

        //Metodo de Busqueda
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                
                findList.Add(dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist.Find(x => x.songName == textBox7.Text));
                if (findList != null)
                {
                    dicPlaylistQueue.ElementAt(comboBox3.SelectedIndex).Value.newPlaylist = findList;
                    listBox1.Items.Clear();
                    for (int i = 0; i < findList.Count; i++)
                    {
                        listBox1.Items.Add(findList.ElementAt(i).songName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ingrese el nombre de la cancion que desea buscar" + ex.Message);
                    throw;
            }
        }
       

        //Metodo Del boton para agregar canciones a waves audio.
        private void button5_Click(object sender, EventArgs e)
        {
            string duracion;
            bool nameExists = false;
            if (dicPlaylistQueue.ElementAt(0).Value != null)
            {
                int i = 0;
                while (i < dicPlaylistQueue.ElementAt(0).Value.newPlaylist.Count)
                {
                    if (dicPlaylistQueue.ElementAt(0).Value.newPlaylist.ElementAt(i).songName == textBox1.Text)
                    {
                        nameExists = true;
                        i = dicPlaylistQueue.ElementAt(0).Value.newPlaylist.Count;
                    }
                    else
                    {
                        i++;
                    }
                }
                if (nameExists != false)
                {
                    MessageBox.Show("La cancion que intenta agregar ya se encuntra en Waves Audio.");
                }
                else
                {
                    duracion = textBox3.Text + ":" + textBox6.Text;
                    Songs newSong = new Songs(textBox1.Text, textBox2.Text, duracion, textBox4.Text, path);
                    dicPlaylistQueue.Values.ElementAt(0).newPlaylist.Add(newSong);
                    textBox1.Text = "Desconocido";
                    textBox2.Text = "Desconocido";
                    textBox3.Text = "00";
                    textBox4.Text = "Desconocido";
                    textBox6.Text = "00";
                }
            }
            else
            {
                duracion = textBox3.Text + ":" + textBox6.Text;
                Songs newSong = new Songs(textBox1.Text, textBox2.Text, duracion, textBox4.Text, path);
                dicPlaylistQueue.Values.ElementAt(0).newPlaylist.Add(newSong);
                textBox1.Text = "Desconocido";
                textBox2.Text = "Desconocido";
                textBox3.Text = "00";
                textBox4.Text = "Desconocido";
                textBox6.Text = "00";
            }

        }
    }
}
