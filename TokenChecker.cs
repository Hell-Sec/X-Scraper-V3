using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Token_Scraper
{
    public partial class TokenChecker : Form
    {
        public TokenChecker()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        static void Error(string text)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            if (!File.Exists("tokens.txt"))
            {
                richTextBox1.SelectionColor = Color.OrangeRed;
                richTextBox1.AppendText("Error\r\n");
                return;
            }

            if (File.ReadAllText("tokens.txt").Length < 1)
            {
                richTextBox1.SelectionColor = Color.OrangeRed;
                richTextBox1.AppendText("tokens.txt does not contain any tokens.\r\n");
                return;
            }

            List<string> tokens = File.ReadAllLines("tokens.txt").ToList();
            richTextBox1.SelectionColor = Color.ForestGreen;
            richTextBox1.AppendText($"Loaded {tokens.Count} tokens\r\n");

            double mins = Math.Round((tokens.Count * 0.5) / 60, 1);
            richTextBox1.SelectionColor = Color.White;
            richTextBox1.AppendText($"Validating tokens checking will take approx ~{mins} minutes \r\n");

            int count = 0;
            int verified = 0;

            List<string> validT = new List<string>();
            List<string> verifiedT = new List<string>();

            foreach (string token in tokens)
            {
                if (Valid(token))
                {
                    count++;
                    validT.Add(token);

                    if (Verified(token))
                    {
                        verified++;
                        verifiedT.Add(token);

                        count++;
                        var lineCount = File.ReadLines(@"tokens.txt").Count();
                        label1.Text = $"{lineCount}";

                        richTextBox1.SelectionColor = Color.ForestGreen;
                        richTextBox1.AppendText($"{token} : Verified \r\n");
                    }
                }
                else
                {
                    label6.Text = ($"{count}");
                    richTextBox1.SelectionColor = Color.OrangeRed;
                    richTextBox1.AppendText($"{token} : Invalid \r\n");
                    Error(token);
                }
            }

            richTextBox1.AppendText($"{count}/{tokens.Count} tokens are valid. \r\n");

            if (validT.Count < 1)
            {
                Console.ReadLine();
                return;
            }

            string path = DateTime.Now.Ticks.ToString();
            Directory.CreateDirectory(path);

            string fullpath = Directory.GetCurrentDirectory() + $"\\{path}";
            richTextBox1.AppendText($"Open the folder {fullpath}  to view the results.\r\n");

            File.WriteAllText($"./{path}/valid.txt", string.Join("\n", validT));

            if (verifiedT.Count < 1)
            {
                Console.ReadLine();
                return;
            }

            File.WriteAllText($"./{path}/verified.txt", string.Join("\n", verifiedT));

            Console.ReadLine();
        }

        static bool Verified(string token)
        {
            try
            {
                string url = @"https://discordapp.com/api/v6/users/@me";
                string raw = string.Empty;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("Authorization", token);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    raw = reader.ReadToEnd();
                }

                if (string.IsNullOrEmpty(raw))
                    return false;

                if (raw.Contains("\"verified\": true,"))
                    return true;

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        static bool Valid(string token)
        {
            try
            {
                string url = @"https://discordapp.com/api/v6/users/@me";
                string raw = string.Empty;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("Authorization", token);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    raw = reader.ReadToEnd();
                }

                if (string.IsNullOrEmpty(raw))
                    return false;

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void TokenChecker_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var TokenScraper = new Home();
            TokenScraper.Show();
            this.Close();
        }
    }
}
