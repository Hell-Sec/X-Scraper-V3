using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections.Specialized;

namespace Token_Scraper
{
    public partial class Home : Form
    {
        private object sOutput;

       

        public Home()
        {
            InitializeComponent();
        }
        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Clear();


            var scraped = File.ReadLines("Scraped.txt");
            foreach (var line in scraped)
                try
                {
                        {
                            int count = 0;
                            List<string> Links = new List<string>();
                            using (WebClient wc = new WebClient())
                            {
                                {
                                    string s = wc.DownloadString(line);


                                    Regex r = new Regex(@"[\w-]{24}\.[\w-]{6}\.[\w-]{27}");
                                    foreach (Match m in r.Matches(s))
                                    {
                                        count++;
                                        var lineCount = File.ReadLines(@"tokens.txt").Count();
                                        label4.Text = $"{lineCount}";
                                        Links.Add(m.ToString());
                                        textBox2.AppendText($"{m}\r\n");

                                        using (TextWriter tokensfile = new StreamWriter($"tokens.txt", true))
                                            tokensfile.WriteLine($"{m}");
                                    }

                                }
                            }

                            {
                                foreach (string line1 in Links)
                                {

                                }
                            }
                            if (count == 0)
                            {
                            }
                        }
                      
                }
                catch
                {
                }

            System.Diagnostics.Process.Start("tokens.txt");

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == null)
            {
                textBox2.AppendText("Enter Keyword To Scrape");
            }
            else
            {
                {
                    int count = 0;
                    List<string> Links = new List<string>();
                    using (WebClient wc = new WebClient())
                    {
                        string s = wc.DownloadString("https://www.google.com/search?q=" + textBox3.Text.ToString() + "&as_qdr=y1");
                        Regex r = new Regex(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)");
                        foreach (Match m in r.Matches(s))
                        {
                            count++;
                            label5.Text = $"{count}";
                            Links.Add(m.ToString());
                        }
                    }

                    using (TextWriter tw = new StreamWriter($"scraped.txt"))
                    {
                        foreach (string line in Links)
                        {
                            tw.WriteLine(line.ToString());
                            textBox2.AppendText($"{line}\r\n");
                        }
                    }
                    textBox2.AppendText($"Found {count} Links");
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label7.Text = $"{textBox3.Text.Length}";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var TokenChecker = new TokenChecker();
            TokenChecker.Show();
            this.Hide();

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void label8_Click(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}