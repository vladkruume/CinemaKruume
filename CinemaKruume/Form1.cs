using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Drawing;
using System.Net.Mail;
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
        string email;
        public string text;
        public List<string> attachments = new List<string>();
        public string imagge = "";
        Image red = Image.FromFile("red.png");
        Image yellow = Image.FromFile("yellow.png");
        Image green = Image.FromFile("green.png");

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
                to_file = new StreamWriter("Kino.txt", false);
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
                rida[i].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                rida[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                rida[i].Location = new Point(1, i * 50);
                this.Controls.Add(rida[i]);
            
            for(int j = 0; j < 4; j++)
            {
                    _arr[i, j] = new Label();
                    string[] arv = arr[i].Split(';');
                    string[] ardNum = arv[j].Split(',');
                    if (ardNum[2] =="true")
                    {
                        _arr[i, j].Image = red;
                    }
                    else
                    {
                        _arr[i, j].Image = green;
                    }
                    _arr[i, j].Text = "" + (j + 1);
                    _arr[i, j].TextAlign= 

                    _arr[i, j].TextAlign = System.Drawing.ContentAlignment.MiddleCenter; ;
                    _arr[i, j].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            kinni.Text = "Kinni";
            kinni.Location = new Point(150, 200);
            kinni.Click += Kinni_Click;
            this.Controls.Add(btnosta);
            this.Controls.Add(kinni);

        }

        private void Form1_Click1(object sender, EventArgs e)
        {
            var label = (Label)sender;
            var tag = (int[])label.Tag;
            if (_arr[tag[0], tag[1]].Image == green)
            {
                _arr[tag[0], tag[1]].Text = "kinni";
                _arr[tag[0], tag[1]].Image = yellow; ;
                ost = true;
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
                    if (_arr[i, j].Image == red)
                    {
                        Osta_Clik_Func();
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (_arr[i, j].Image ==red)
                    {
                        text += i + "," + j + ",true;";
                    }
                    else
                    {
                        text += i + "," + j + ",false;";
                    }
                }
                text += "\n";
            }
            to_file.Write(text);
            to_file.Close();
            this.Close();

        }

        private void Btnosta_Click(object sender, EventArgs e)
        {
            Osta_Clik_Func();
            sendemail();
    }
        void Osta_Clik_Func()
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
                        if (_arr[i, j].Image == yellow)
                        {
                                _arr[i, j].Image = red ;


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
                        if (_arr[i, j].Image ==yellow)
                        {
                                _arr[i, j].Image = green;
                                _arr[i, j].Text = "" + (j + 1);
                        }

                    }
                }
            }
            }
        }

       public void sendemail()
        {


            imagge = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (_arr[i, j].Text == "kinni")
                    {

                        switch (i+1)
                        {
                            case 1: imagge = "1k"; break;
                            case 2: imagge = "2k"; break;
                            case 3: imagge = "3k"; break;
                            case 4: imagge = "4k"; break;
                        }
                        switch (j+1)
                        {
                            case 1: imagge += "1.png"; break;
                            case 2: imagge += "2.png"; break;
                            case 3: imagge += "3.png"; break;
                            case 4: imagge += "4.png"; break;
                        }

                        text += "<ul>" + "<li>" + " Ряд:" + Convert.ToString(i + 1) + " Место:" + Convert.ToString(j + 1) + "</li>" + "</ul>";
                        attachments.Add(imagge);
                    }
                }
            }

            string emaill = "";
            ShowInputDialog(ref emaill);
            MailAddress from = new MailAddress("vkruume@gmail.com", "Tom");
            MailAddress to = new MailAddress(emaill);
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Билеты";
            // текст письма
            m.Body = "<h1>Здраствуйте уважаемый клиент</h1>"+"<h2>Ваш заказ:</h2>"+text;
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("vkruume@gmail.com", "vladik21473147");
            smtp.EnableSsl = true;
            Attachment Attachment = null;
            try
            {
                foreach (string attachment in attachments)
                {
                    Attachment = new Attachment(attachment);
                    m.Attachments.Add(Attachment);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            smtp.Send(m);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }
        private static DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Email";

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }
    }
}
