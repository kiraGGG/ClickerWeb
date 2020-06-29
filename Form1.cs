using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickerWeb
{
    public partial class Form1 : Form
    {
        string fileName = "";
        string butid = "";
        public Form1()
        {
            InitializeComponent();
            Opacity = 0;
            Timer timer = new Timer();
            timer.Tick += new EventHandler((sender, e) =>
            {
                if ((Opacity += 0.02) >= 0.98) timer.Stop();
            });
            timer.Interval = 30;
            timer.Start();
            openFileDialog1.InitialDirectory = Application.StartupPath.ToString();
            butid = "d_l";
        }

        async private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                int i = 0;
                string[] lines = File.ReadAllLines(fileName);
                i = File.ReadAllLines(fileName).Length;
                listBox1.Items.AddRange(lines);
                label1.Text = $@"Уйдет времени: {(i * 4.2).ToString()} сек.    Ссылок: {i.ToString()}";
                webBrowser1.ScriptErrorsSuppressed = true;
                if (radioButton3.Checked)
                    butid = textBox1.Text;
                try
                {
                    while (i > 0)
                    {
                        webBrowser1.Navigate(lines[i - 1]);
                        await Task.Delay(2000);
                        i--;
                        webBrowser1.Document.GetElementById(butid).InvokeMember("Click");
                        await Task.Delay(2000);
                        System.Diagnostics.Process.Start("cmd.exe", "/c taskkill /IM iexplore.exe");
                    }
                    MessageBox.Show("Работа завершена!");
                    label1.Text = "";
                }
                catch
                {
                    MessageBox.Show("Указаны неверные ссылки или id кнопки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Указан неверный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string[] lines = File.ReadAllLines(fileName);
            listBox1.Items.AddRange(lines);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            try
            {
                int i = 0;
                fileName = openFileDialog1.FileName;
                listBox1.Items.Clear();
                string[] lines = File.ReadAllLines(fileName);
                i = File.ReadAllLines(fileName).Length;
                listBox1.Items.AddRange(lines);
                label1.Text = $@"Уйдет времени: {(i * 4.2).ToString()} сек.    Ссылок: {i.ToString()}";
            }
            catch { }

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            butid = "d_l";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            butid = "dlbutton";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            butid = "btn_download";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/kiraGGG/");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://t.me/FRAMEDEV/");
        }
    }
}
