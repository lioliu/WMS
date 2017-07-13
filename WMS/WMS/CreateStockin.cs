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
        string customerID;
        DataTable data = null;
        public CreateStockin(string email)
        {
            this.customerID = DBUtility.GetData($"select customer_id from customer where customer_email = '{email}'").Rows[0][0].ToString();
            InitializeComponent();
            data = new DataTable();
            data.Columns.Add(new DataColumn("编号"));
            data.Columns.Add(new DataColumn("商品编号"));
            data.Columns.Add(new DataColumn("商品名称"));
            data.Columns.Add(new DataColumn("数量"));
            dataGridView1.DataSource = data;
            DataTable temp = DBUtility.GetData("select Product_id from product");
            for (int i = 0; i < temp.Rows.Count; i++)
            {
                comboBox1.Items.Add(temp.Rows[i][0].ToString());
                Console.WriteLine(temp.Rows[i][0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data.Rows.Add();
            data.Rows[data.Rows.Count - 1][0] = (data.Rows.Count).ToString();
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
            #region create stock list
            string freeStaff = DBUtility.GetData("  select a.staff_id ,isnull(innumber + outnumber,0) 处理数量 " +
 " from Staff a left join (select Staff_id, isnull(count(*), 0) innumber from stock_in where stock_in_Checked = 0 group by staff_ID) b on a.staff_ID = b.staff_ID " +
 " left join(select Staff_id, count(*) outnumber from Stock_out where stock_out_Checked = 0 group by staff_ID ) c on a.staff_ID = c.staff_ID " +
 "order by 处理数量").Rows[0][0].ToString();
            DBUtility.ExecuteSQL($"insert into stock_in select  right('00000000000000000000'+cast(isnull(max(stock_in_id),0)+1 as varchar),20),'{customerID}','{freeStaff}',{dateTimePicker1.Value.ToShortDateString()},0,0,0,0 from stock_in   "); // pay price need to modi todo

            string stockInID = DBUtility.GetData("select right('00000000000000000000'+cast(max(stock_in_id) as varchar),20) from stock_in ").Rows[0][0].ToString();
            int sn = 1;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                DBUtility.ExecuteSQL($"insert into stock_in_detail VALUES('{stockInID}','{data.Rows[i][1].ToString()}','{sn++}','{data.Rows[i][3].ToString()}')");
            }
            #endregion
            MessageBox.Show("添加成功");
            this.Close();
            this.Dispose();
        }

        private void CreateStockin_Load(object sender, EventArgs e)
        {
          
        }
    }
}
