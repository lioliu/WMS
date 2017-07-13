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
    public partial class Stock : Form
    {
        string Customer_id = string.Empty;
        DataTable data;
        public Stock(string email)
        {
            InitializeComponent();
            this.Customer_id = DBUtility.GetData($"select customer_id from customer where customer_email = '{email}'").Rows[0][0].ToString();
            //加载库存
            data = DBUtility.GetData($"select WareArea_ID 仓储区域号, a.Product_ID 商品编号, b.Product_Name 商品名, a.Ware_detail_number 数量 from Ware_Detail a , Product b where a.Product_ID = b.Product_ID and a.Ware_detail_owner = '{Customer_id}'");
            dataGridView1.DataSource = data;
        }

        private void Stock_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
