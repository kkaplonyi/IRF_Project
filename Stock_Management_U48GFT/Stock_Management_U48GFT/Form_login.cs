using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
//using Stock_Management_U48GFT.Entities;

namespace Stock_Management_U48GFT
{
    public partial class Form_login : Form
    {
        List<string> users = new List<string>();
        List<string> pass = new List<string>();
        public static string logolt_user = "";
        public Form_login()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            
            using (var sr = new StreamReader(@"Resources\password-hashed.csv"))
            {
                while(!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(',');
                    users.Add(line[0]);
                    pass.Add(line[1]);
                }
            }
            
        }
        private static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string password = textBox2.Text;
            string pw_hashed = (ComputeSha256Hash(password)).ToUpper();
            if (users.Contains(textBox1.Text) && pass.Contains(pw_hashed) && Array.IndexOf(users.ToArray(), textBox1.Text) == Array.IndexOf(pass.ToArray(), pw_hashed))
            {
                logolt_user = textBox1.Text;
                Form_tracker f2 = new Form_tracker();
                this.Hide();
                f2.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("A megadott username és/vagy jelszó hibás!");

                       
        }
    }
}
