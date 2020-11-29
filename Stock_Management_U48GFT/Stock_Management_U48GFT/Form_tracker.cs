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
using Stock_Management_U48GFT.Entities;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;

namespace Stock_Management_U48GFT
{
    public partial class Form_tracker : Form
    {
        StocksDBEntities context = new StocksDBEntities();
        List<Stocks_raw> NapiAdatok;
        List<Stocks_all> Stocks_lista;
        public static List<PortfolioItem> PortfolioLista = new List<PortfolioItem>();
        BindingList<PortfolioItem> source = new BindingList<PortfolioItem>(PortfolioLista);
        Random rnd = new Random();
        

        public Form_tracker()
        {
            InitializeComponent();
            label2.Text = Form_login.logolt_user;
            label4.Text = (DateTime.Today).ToString("dd/MM/yyy");
            dataGridView1.DataSource = source;
            ChartLoad();

        }

        private void btn_allstocks_Click(object sender, EventArgs e)
        {
            Form_all_stocks f3 = new Form_all_stocks();
            this.Hide();
            f3.ShowDialog();
            this.Show();
            source.ResetBindings();
            ChartLoad();

        }
        private void ChartLoad()
        {
            var date = DateTime.Parse("10/11/2020");
            var kezdo = date.Date;
            if (source.Count != 0)
            {
                //List<Stocks_raw> filternapi = NapiAdatok.Where(x => source.Contains(new PortfolioItem { symbol = x.OriginalFile }) && x.Date >= kezdo).ToList();
                chart1.DataSource = NapiAdatok;
                chart1.Series[0].XValueMember = "Date";
                chart1.Series[0].YValueMembers = "Fent";
                chart1.DataBind();
                MessageBox.Show("hiba");
            }
            for (int i = 0; i < source.Count; i++)
            {
                /*var series = chart1.Series[i];
                series.ChartType = SeriesChartType.Line;
                series.XValueMember = "Date";
                series.YValueMembers = "Price";
                series.BorderWidth = 2;
                var legend = chart1.Legends[0];
                legend.Enabled = false;
                var chartarea = chart1.ChartAreas[i];
                chartarea.AxisX.MajorGrid.Enabled = false;
                chartarea.AxisY.MajorGrid.Enabled = false;
                chartarea.AxisY.IsStartedFromZero = false;*/



                //DateTime kezdo = DateTime.Parse("2020/11/10");
                //List<Stocks_raw> filternapi = NapiAdatok.Where(x => x.OriginalFile == source[i].symbol && x.Date>=kezdo).OrderByDescending(r => r.Date).ToList();
                

                /*for (int j = 0; i < 5; i++)
                {
                    var avg = (filternapi[i].Fent + filternapi[i].Lent) / 2;
                    chart1.Series[item.symbol].Points.AddXY(filternapi[i].Date, avg);
                }
                Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                chart1.Series[item.symbol].Color = randomColor;*/
                


            }


        }
        public void AddPortfolioItem(string symbol1, string name1, decimal quantity1, decimal price1, decimal totalcost1)
        {
            source.Add(new PortfolioItem() { symbol = symbol1, name = name1, quantity = quantity1, price = price1, totalcost = totalcost1 });
            
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "csv";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Stream FileST = sfd.OpenFile();
                StreamWriter sw = new StreamWriter(FileST);
                sw.WriteLine("Symbol, Name, Quantity, Price, TotalCost");
                foreach (var x in source)
                {

                    sw.WriteLine(x.symbol + ", " + x.name + ", " + x.quantity + ", " + Convert.ToString(x.price).Replace(",", ".") + ", " + Convert.ToString(x.totalcost).Replace(",", "."));
                }

                sw.Close();
                FileST.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            var itemtoremove = PortfolioLista.Single(r => r.symbol == Convert.ToString(selectedRow.Cells["Symbol"].Value));
            source.Remove(itemtoremove);
            //PortfolioLista.Remove(new PortfolioItem() { symbol = Convert.ToString(selectedRow.Cells["Symbol"].Value), name = Convert.ToString(selectedRow.Cells["Name"].Value), quantity = Convert.ToDecimal(selectedRow.Cells["quantity"].Value), price = Convert.ToDecimal(selectedRow.Cells["Price"].Value), totalcost = Convert.ToDecimal(selectedRow.Cells["totalcost"].Value) });
            source.ResetBindings();
            ChartLoad();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            source.Clear();
            source.ResetBindings();
            ChartLoad();
        }

        private void Form_tracker_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            

        }
    }
    }


