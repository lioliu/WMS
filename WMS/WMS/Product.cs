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
    public partial class Product : Form
    {
        DataTable data;
        public Product()
        {
            InitializeComponent();
             data = DBUtility.GetData("SELECT Product_ID 编号,Product_Name 名称,Product_tally 单位 , Product_Weight 重量 , Product_Long 长度 ,Product_Hgih 高度 , Product_Wide 宽度 , Product_Temperature 保存温度 , Product_humidity 保存湿度 FROM[WMS].[dbo].[Product]");
            dataGridView1.DataSource = data;
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
         
            data = DBUtility.GetData($"SELECT Product_ID 编号,Product_Name 名称,Product_tally 单位 , Product_Weight 重量 , Product_Long 长度 ,Product_Hgih 高度 , Product_Wide 宽度 , Product_Temperature 保存温度 , Product_humidity 保存湿度 FROM[WMS].[dbo].[Product] where Product_ID like '%{textBox1.Text}%' and Product_Name like '%{textBox2.Text}%' and Product_tally like '%{textBox3.Text}%' and Product_Weight like '%{textBox4.Text}%' and Product_Long like '%{textBox5.Text}%' and Product_Hgih like '%{textBox6.Text}%' and  Product_Wide like '%{textBox7.Text}%'  ");
            dataGridView1.DataSource = data;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
