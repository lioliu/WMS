using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WMS
{
    public partial class CreateStockin : Form
    {
        DataTable data = null;
        public CreateStockin()
        {
            data.Columns.Add(new DataColumn("编号"));
            data.Columns.Add(new DataColumn("商品编号"));
            data.Columns.Add(new DataColumn("商品名称"));
            data.Columns.Add(new DataColumn("数量"));
            InitializeComponent();
            dataGridView1.DataSource = data;

            DataTable temp = DBUtility.GetData("select Product_id from product");
            for (int i = 0; i < temp.Rows.Count; i++)
            {
                comboBox1.Items.Add(temp.Rows[i][0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data.Rows.Add();
            data.Rows[data.Rows.Count - 1][0] = (data.Rows.Count + 1).ToString();
            data.Rows[data.Rows.Count - 1][1] = comboBox1.SelectedItem.ToString();
            data.Rows[data.Rows.Count - 1][2] = DBUtility.GetData($"select product_name from product where product_id ='{comboBox1.SelectedItem.ToString()}'").Rows[0][0].ToString();
            data.Rows[data.Rows.Count - 1][3] = textBox1.Text;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedCells[0].RowIndex;
            data.Rows[index].Delete();

            //rewrite sn
            for (int i = 0; i < data.Rows.Count; i++)
            {
                data.Rows[i][0] = (i + 1).ToString();
            }
            dataGridView1.DataSource = data;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        /// <summary>
        /// submit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            //todo

        }
    }
}
