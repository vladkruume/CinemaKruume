using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaKruume
{
    public partial class Form1 : Form
    {
        Label[,] _arr = new Label[4, 4];
        Label[] rida = new Label[4];
        Button btnosta,kinni;
        StreamWriter to_file;
        bool ost = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string text = "";
            StreamWriter to_file;
            if (!File.Exists("Kino.txt"))
            {
                to_file = new StreamWriter("kino.txt", false);
                for(int i = 0; i < 4; i++)
                {
                    for(int j = 0; j < 4; j++)
                    {
                        text += i + "," + j + ",false;";
                    }
                    text += "\n";
                }
                to_file.Write(text);
                to_file.Close();
            }
            StreamReader from_file = new StreamReader("Kino.txt", false);
            string[] arr = from_file.ReadToEnd().Split('\n');
            from_file.Close();
            this.Size = new Size(300, 430);
            this.Text = "kino";


            for (int i = 0; i < 4; i++)
            {
                rida[i] = new Label();
                rida[i].Text = "Rida" + (i+1);
                rida[i].Size = new Size(50, 50);
                
                rida[i].Location = new Point(1, i * 50);
                this.Controls.Add(rida[i]);
            
            for(int j = 0; j < 4; j++)
            {
                _arr[i, j] = new Label();
                    string[] arv = arr[i].Split(';');
                    string[] ardNum = arr[i].Split(',');
                    if (ardNum[2] == "true")
                    {
                        _arr[i, j].BackColor = Color.Red;
                    }
                    else
                    {
                        _arr[i, j].BackColor = Color.Green;
                    }
                    _arr[i, j].Text = "Koht" + (j + 1);
                    _arr[i, j].Size = new Size(50, 50);
                    _arr[i, j].BorderStyle = BorderStyle.Fixed3D;
                    _arr[i, j].Location = new Point(j * 50 + 50, i * 50);
                    this.Controls.Add(_arr[i, j]);
                    _arr[i, j].Tag = new int[] { i, j };
                    _arr[i, j].Click += new System.EventHandler(Form1_Click1);

                }
        }



            btnosta = new Button();
            btnosta.Text = "Osta";
            btnosta.Location = new Point(50, 200);
            btnosta.Click +=Btnosta_Click;
            
            kinni = new Button();
            kinni.Text = "Osta";
            kinni.Location = new Point(150, 200);
            kinni.Click += Kinni_Click;
            this.Controls.Add(btnosta);
            this.Controls.Add(kinni);

        }

        private void Form1_Click1(object sender, EventArgs e)
        {
            var label = (Label)sender;
            var tag = (int[])label.Tag;
            if (_arr[tag[0], tag[1]].BackColor == Color.LightGreen)
            {
                _arr[tag[0], tag[1]].Text = "kinni";
                _arr[tag[0], tag[1]].BackColor = Color.LightYellow;
            }
        }

        private void Kinni_Click(object sender, EventArgs e)
        {
            string text = "";
            to_file = new StreamWriter("Kino.txt",false);
            for(int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (_arr[i, j].BackColor == Color.Yellow)
                    {
                        Osta_Clik_Func();
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (_arr[i, j].BackColor == Color.Yellow)
                    {
                        Osta_Clik_Func();
                    }
                }
                text += "\n";
                to_file.Write(text);
                this.Close();
            }

        }

        private void Btnosta_Click(object sender, EventArgs e)
        {

            Osta_Clik_Func();
    }
        private void Osta_Clik_Func()
        {
            if (ost == true) { 

            DialogResult result2 = MessageBox.Show("Вы точно хотите купить эти билеты?",
            "Покупка билета",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Question);
            if (result2 == DialogResult.Yes)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_arr[i, j].BackColor == Color.LightYellow)
                        {
                            _arr[i, j].BackColor = Color.Red;


                        }
                    }
                }
            }
            if (result2 == DialogResult.No)
            {

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_arr[i, j].BackColor == Color.LightYellow)
                        {
                            _arr[i, j].BackColor = Color.LightGreen;
                            _arr[i, j].Text = "Koht" + (j + 1);
                        }

                    }
                }
            }
            }
        }

       

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }
    }
}
