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
    public partial class StockStaff : Form
    {
        DataTable data;
        public StockStaff()
        {
            InitializeComponent();
            data = DBUtility.GetData("  select WareArea_ID 仓储区域号,a.Ware_detail_owner 货主,a.Product_ID 商品编号 ,b.Product_Name 商品名,a.Ware_detail_number 数量 from Ware_Detail a , Product b where a.Product_ID = b.Product_ID ");
            dataGridView1.DataSource = data;
        }

        private void StockStaff_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data = DBUtility.GetData($"  select WareArea_ID 仓储区域号,a.Ware_detail_owner 货主,a.Product_ID 商品编号 ,b.Product_Name 商品名,a.Ware_detail_number 数量 from Ware_Detail a , Product b where a.Product_ID = b.Product_ID  and b.Product_Name like '%{textBox1.Text}%' and a.Ware_detail_owner like '%{textBox2.Text}%' and WareArea_ID like '%{textBox3.Text}%'");
            dataGridView1.DataSource = data;
        }
    }
}
