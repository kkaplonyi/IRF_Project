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


namespace Stock_Management_U48GFT
{
    public partial class Form_tracker : Form
    {
        StocksDBEntities context = new StocksDBEntities();
        List<Stocks_raw> NapiAdatok;
        List<Stocks_all> Stocks_lista;

        public Form_tracker()
        {
            InitializeComponent();
            label2.Text = Form_login.logolt_user;
            label4.Text = (DateTime.Today).ToString("dd/MM/yyy");
        }

        private void btn_allstocks_Click(object sender, EventArgs e)
        {
            Form_all_stocks f3 = new Form_all_stocks();
            this.Hide();
            f3.ShowDialog();
            this.Show();
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            /*SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "csv";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Stream FileST = sfd.OpenFile();
                StreamWriter sw = new StreamWriter(FileST);

                sw.WriteLine("Időszak, Nyereség");
                foreach (var x in nyereségekRendezve)
                {

                    sw.WriteLine();
                }

                sw.Close();
                FileST.Close();
            }*/
        }
    }
}

