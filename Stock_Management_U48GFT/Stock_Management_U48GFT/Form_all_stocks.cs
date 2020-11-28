using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_Management_U48GFT
{
    public partial class Form_all_stocks : Form
    {
        StocksDBEntities context = new StocksDBEntities();
        List<Stocks_raw> NapiAdatok;
        List<Stocks_all> Stocks_lista;
        decimal a;
        public Form_all_stocks()
        {
            InitializeComponent();
            Stocks_lista = context.Stocks_all.ToList();
            dataGridView1.DataSource = Stocks_lista;
            
        }
        private void datagridview1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                a = Convert.ToDecimal(selectedRow.Cells["Price"].Value);
            }
            Totalfrissit();
        }
        private void Totalfrissit()
        {
            if(textBox2.Text=="")
            {
                label4.Text = "0";
            }
            else
            {
                label4.Text = Convert.ToString((Convert.ToDecimal(textBox2.Text) * a));
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Totalfrissit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                dataGridView1.DataSource = Stocks_lista;
            }
            else
                {
                List<Stocks_all> filterList = Stocks_lista.Where(x => x.Name.ToLower().Contains(textBox1.Text.ToLower())).ToList();
                    dataGridView1.DataSource = filterList;
                }
            }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
    }

