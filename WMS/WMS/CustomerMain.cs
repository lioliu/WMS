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
    public partial class CustomerMain : Form
    {
        string Email;
        string ID;
        DataTable pay;
        public CustomerMain(string email)
        {
            ID = DBUtility.GetData($"select customer_id from customer where customer_email = '{email}'").Rows[0][0].ToString();
            Email = email;
            InitializeComponent();
        }

        private void 商品ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.ShowDialog();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePsd change = new ChangePsd(Email, 1);
            change.ShowDialog();
        }

        private void 创建出库单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateStockOut form = new CreateStockOut(Email);
            form.ShowDialog();
        }

        private void 查询库存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stock form = new Stock(Email);
            form.ShowDialog();
        }

        private void 入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateStockin form = new CreateStockin(Email);
            form.ShowDialog();
        }



        private void CustomerMain_Load(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = pay.Rows[comboBox1.SelectedIndex][1].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DBUtility.ExecuteSQL($"update stock_in set stock_in_payed = 1 where stock_in_id = '{comboBox1.SelectedItem.ToString()}'");
                MessageBox.Show("缴费成功");
                UpdateInfo();
                return;
            }
            catch (Exception)
            {


            }
            try
            {
                DBUtility.ExecuteSQL($"update stock_out set stock_out_payed = 1 where stock_in_id2 = '{comboBox1.SelectedItem.ToString()}'");
                MessageBox.Show("缴费成功");
                UpdateInfo();
                return;
            }
            catch (Exception)
            {


            }
            MessageBox.Show("缴费失败");


        }

        private void UpdateInfo()
        {
            DataTable data = DBUtility.GetData($"  select stock_in_ID 入库单号,stock_in_Checked 入库单确认,stock_in_Pay 金额,stock_in_Payed 是否支付,stock_in_Finished 是否完成 from stock_in where customer_id = '{ID}'");
            dataGridView1.DataSource = data;
            DataTable data1 = DBUtility.GetData($"  select stock_in_ID2 出库单号,stock_out_Checked 出库单确认,stock_out_Pay 金额,stock_out_Payed 是否支付,stock_out_Finished 是否完成 from stock_out where customer_id = '{ID}'");
            dataGridView2.DataSource = data1;

            pay = DBUtility.GetData($" select stock_in_id2, Stock_out_pay from stock_out where stock_out_Payed = 0 and Customer_ID = '{ID}' union select stock_in_id, Stock_in_pay from stock_in where stock_in_Payed = 0 and Customer_ID = '{ID}'");
            for (int i = 0; i < pay.Rows.Count; i++)
            {
                comboBox1.Items.Add(pay.Rows[i][0].ToString());
            }
            DataTable data2 = DBUtility.GetData($"select WareArea_ID 仓储区域号, a.Product_ID 商品编号, b.Product_Name 商品名, a.Ware_detail_number 数量 from Ware_Detail a , Product b where a.Product_ID = b.Product_ID and a.Ware_detail_owner = '{ID}'");
            dataGridView3.DataSource = data2;
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateInfo();
        }
    }
}
