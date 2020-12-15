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
using System.Data.Sql;
using System.Data.SqlClient;

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

        bool toltve = false;

        SqlCommand cmd;
        SqlConnection con;
        SqlDataAdapter da;

        Timer t = new Timer();
        int WIDTH = 300 / 4, HEIGHT = 300 / 4, secHAND = 140 / 4, minHAND = 110 / 4, hrHAND = 80 / 4; 
        int cy, cx;
        Bitmap bmp;
        Graphics cg;


        public Form_tracker()
        {
            InitializeComponent();

        }
        private void Form_tracker_Load(object sender, EventArgs e)
        {
            source.Clear();
            label2.Text = Form_login.logolt_user;
            label4.Text = (DateTime.Today).ToString("dd/MM/yyy");
            switch (Form_login.logolt_user)
            {
                case "admin":
                    var admin_start = context.admins.ToList();
                    foreach (var x in admin_start)
                    {
                        source.Add(new PortfolioItem()
                        {
                            Symbol = x.Symbol,
                            Name = x.Name,
                            Quantity = Convert.ToDecimal(x.Quantity),
                            Price = Convert.ToDecimal(x.Price),
                            Totalcost = Convert.ToDecimal(x.Totalcost)
                        });

                    }
                    break;
                case "tanar":
                    var tanar_start = context.tanars.ToList();
                    foreach (var x in tanar_start)
                    {
                        source.Add(new PortfolioItem()
                        {
                            Symbol = x.Symbol,
                            Name = x.Name,
                            Quantity = Convert.ToDecimal(x.Quantity),
                            Price = Convert.ToDecimal(x.Price),
                            Totalcost = Convert.ToDecimal(x.Totalcost)
                        });

                    }
                    break;
                case "user1":
                    var user1_start = context.user1.ToList();
                    foreach (var x in user1_start)
                    {
                        source.Add(new PortfolioItem()
                        {
                            Symbol = x.Symbol,
                            Name = x.Name,
                            Quantity = Convert.ToDecimal(x.Quantity),
                            Price = Convert.ToDecimal(x.Price),
                            Totalcost = Convert.ToDecimal(x.Totalcost)
                        });

                    }
                    break;
                case "user2":
                    var user2_start = context.user2.ToList();
                    foreach (var x in user2_start)
                    {
                        source.Add(new PortfolioItem()
                        {
                            Symbol = x.Symbol,
                            Name = x.Name,
                            Quantity = Convert.ToDecimal(x.Quantity),
                            Price = Convert.ToDecimal(x.Price),
                            Totalcost = Convert.ToDecimal(x.Totalcost)
                        });

                    }
                    break;

            }
            dataGridView1.DataSource = source;
            toltve = true;
            ChartLoad();
            notifyIcon.Icon = SystemIcons.Application;

            bmp = new Bitmap(WIDTH + 1, HEIGHT + 1);
            // placing in center  
            cx = WIDTH / 2;
            cy = HEIGHT / 2;
            //backcolor  
            this.BackColor = Color.White;
            //timer  
            t.Interval = 1000; // i.e. tick in milisecond  
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }
        private void t_Tick(object sender, EventArgs e)
        {
            // create an image  
            cg = Graphics.FromImage(bmp);
            //get time  
            int ss = DateTime.Now.Second;
            int mm = DateTime.Now.Minute;
            int hh = DateTime.Now.Hour;
            int[] handCoord = new int[2];
            //get time  
            cg.Clear(SystemColors.Control);
            //draw a circle  
            cg.DrawEllipse(new Pen(Color.Black, 2), 0, 0, WIDTH, HEIGHT);
            //draw clock numbers  
            cg.DrawString("12", new Font("Ariel", 6), Brushes.Black, new PointF(140 / 4, 3 / 4));
            cg.DrawString("1", new Font("Ariel", 6), Brushes.Black, new PointF(218 / 4, 22 / 4));
            cg.DrawString("2", new Font("Ariel", 6), Brushes.Black, new PointF(263 / 4, 70 / 4));
            cg.DrawString("3", new Font("Ariel", 6), Brushes.Black, new PointF(285 / 4, 140 / 4));
            cg.DrawString("4", new Font("Ariel", 6), Brushes.Black, new PointF(263 / 4, 212 / 4));
            cg.DrawString("5", new Font("Ariel", 6), Brushes.Black, new PointF(218 / 4, 259 / 4));
            cg.DrawString("6", new Font("Ariel", 6), Brushes.Black, new PointF(142 / 4, 279 / 4));
            cg.DrawString("7", new Font("Ariel", 6), Brushes.Black, new PointF(70 / 4, 259 / 4));
            cg.DrawString("8", new Font("Ariel", 6), Brushes.Black, new PointF(22 / 4, 212 / 4));
            cg.DrawString("9", new Font("Ariel", 6), Brushes.Black, new PointF(1 / 4, 140 / 4));
            cg.DrawString("10", new Font("Ariel", 6), Brushes.Black, new PointF(22 / 4, 70 / 4));
            cg.DrawString("11", new Font("Ariel", 6), Brushes.Black, new PointF(70 / 4, 22 / 4));
            //draw seconds hand  
            handCoord = msCoord(ss, secHAND);
            cg.DrawLine(new Pen(Color.Red, 2), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));
            //draw minutes hand  
            handCoord = msCoord(mm, minHAND);
            cg.DrawLine(new Pen(Color.Black, 3), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));
            //draw hours hand  
            handCoord = hrCoord(hh % 12, mm, hrHAND);
            cg.DrawLine(new Pen(Color.Black, 3), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));
            //load the bitmap image  
            pictureBox1.Image = bmp;
            //display time in the heading  
            cg.Dispose();
        }
        //coord for minute and second  
        private int[] msCoord(int val, int hlen)
        {
            int[] coord = new int[2];
            val *= 6; // note: each minute and seconds make a 6 degree  
            if (val >= 0 && val <= 100)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }
        //coord for hour  
        private int[] hrCoord(int hval, int mval, int hlen)
        {
            int[] coord = new int[2];
            //each hour makes 60 degree with min making 0.5 degree  
            int val = (int)((hval * 30) + (mval * 0.5));
            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
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
            var objChart = chart1.ChartAreas[0];
            objChart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            objChart.AxisY.IsStartedFromZero = false;
            objChart.AxisX.Title = "Date";
            objChart.AxisY.Title = "Price";


            //datek

            var kezdo = DateTime.Parse("09/11/2020").Date;
            var zaro = DateTime.Parse("20/11/2020").Date;

            chart1.Series.Clear();
            foreach (PortfolioItem item in source)
            {
                chart1.Series.Add(item.Symbol);
                List<Stocks_raw> filternapi = new List<Stocks_raw>(context.Stocks_raw.Where(m => m.OriginalFile == item.Symbol && m.Date >= kezdo).OrderByDescending(r => r.Date).ToList());
                chart1.Series[item.Symbol].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series[item.Symbol].Color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                chart1.Series[item.Symbol].XValueMember = "Date";
                chart1.Series[item.Symbol].YValueMembers = "Price";
                chart1.Series[item.Symbol].BorderWidth = 3;

                for (int i = 0; i < 10; i++)
                {
                    chart1.Series[item.Symbol].Points.AddXY(zaro.AddDays(-i), Convert.ToInt32((filternapi[i].Fent+ filternapi[i].Lent)/2));

                }

            }
        }
        public void AddPortfolioItem(string symbol1, string name1, decimal quantity1, decimal price1, decimal totalcost1)
        {
            source.Add(new PortfolioItem() { Symbol = symbol1, Name = name1, Quantity = quantity1, Price = price1, Totalcost = totalcost1 });
            
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

                    sw.WriteLine(x.Symbol + ", " + x.Name + ", " + x.Quantity + ", " + Convert.ToString(x.Price).Replace(",", ".") + ", " + Convert.ToString(x.Totalcost).Replace(",", "."));
                }

                sw.Close();
                FileST.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            var itemtoremove = PortfolioLista.Single(r => r.Symbol == Convert.ToString(selectedRow.Cells["Symbol"].Value) && r.Quantity == Convert.ToDecimal(selectedRow.Cells["Quantity"].Value));
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

        private  void btn_sync_Click(object sender, EventArgs e)
        {
            var current_user = Form_login.logolt_user;
            con = new SqlConnection(@"Data Source = localhost; Initial Catalog = StocksDB; Integrated Security = True");
            con.Open();
            cmd = new SqlCommand("DELETE FROM " + current_user, con);
            cmd.ExecuteNonQuery();
            foreach (var sor in source)
            {
                cmd = new SqlCommand("INSERT INTO " + current_user + " (Symbol, Name, Quantity, Price, Totalcost) VALUES (@Symbol, @Name, @Quantity, @Price, @Totalcost)", con);
                cmd.Parameters.Add("@Symbol", sor.Symbol);
                cmd.Parameters.Add("@Name", sor.Name);
                cmd.Parameters.Add("@Quantity", sor.Quantity);
                cmd.Parameters.Add("@Price", sor.Price);
                cmd.Parameters.Add("@Totalcost", sor.Totalcost);
                cmd.ExecuteNonQuery();
            }
            notifyIcon.ShowBalloonTip(3000, "Üzenet", "Portfolió sikeresen szinkronizálva!", ToolTipIcon.Info);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (toltve == true)
            {
                var curSymbol = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                foreach (var item in chart1.Series)
                {
                    if (item.Name==curSymbol)
                    {
                        item.BorderWidth = 10;
                    }
                    else
                    {
                        item.BorderWidth = 3;
                    }
                }
                
            }
            
        }
    }
}


