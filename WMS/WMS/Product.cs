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
         
            data = DBUtility.GetData($"SELECT Product_ID 编号,Product_Name 名称,Product_tally 单位 , Product_Weight 重量 , Product_Long 长度 ,Product_Hgih 高度 , Product_Wide 宽度 , Product_Temperature 保存温度 , Product_humidity 保存湿度 FROM [WMS].[dbo].[Product] where Product_ID like '%{textBox1.Text}%' and Product_Name like '%{textBox2.Text}%' and Product_tally like '%{textBox3.Text}%' and Product_Weight like '%{textBox4.Text}%' and Product_Long like '%{textBox5.Text}%' and Product_Hgih like '%{textBox6.Text}%' and  Product_Wide like '%{textBox7.Text}%'  ");
            dataGridView1.DataSource = data;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DBUtility.ExecuteSQL($"INSERT INTO [WMS].[dbo].[Product] select right('00000000000000000000'+cast(max(Product_ID)+1 as varchar),20),'{textBox17.Text}','{textBox16.Text}','{textBox15.Text}','{textBox14.Text}','{textBox13.Text}','{textBox12.Text}','{textBox11.Text}','{textBox10.Text}' from [WMS].[dbo].[Product]");
            data = DBUtility.GetData("SELECT Product_ID 编号,Product_Name 名称,Product_tally 单位 , Product_Weight 重量 , Product_Long 长度 ,Product_Hgih 高度 , Product_Wide 宽度 , Product_Temperature 保存温度 , Product_humidity 保存湿度 FROM [WMS].[dbo].[Product]");
            dataGridView1.DataSource = data;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //delete
            DBUtility.ExecuteSQL($"delete from  [WMS].[dbo].[Product] where Product_ID ='{textBox18.Text}' ");
            data = DBUtility.GetData("SELECT Product_ID 编号,Product_Name 名称,Product_tally 单位 , Product_Weight 重量 , Product_Long 长度 ,Product_Hgih 高度 , Product_Wide 宽度 , Product_Temperature 保存温度 , Product_humidity 保存湿度 FROM [WMS].[dbo].[Product]");
            dataGridView1.DataSource = data;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DBUtility.ExecuteSQL($"UPDATE [WMS].[dbo].[Product] SET Product_Name = '{textBox17.Text}',Product_tally = '{textBox16.Text}',Product_Weight ='{textBox15.Text}',Product_Long ='{textBox14.Text}',Product_Hgih='{textBox13.Text}',Product_Wide = '{textBox12.Text}',Product_Temperature='{textBox11.Text}',Product_humidity='{textBox10.Text}'  where Product_ID ='{textBox18.Text}' ");
            data = DBUtility.GetData("SELECT Product_ID 编号,Product_Name 名称,Product_tally 单位 , Product_Weight 重量 , Product_Long 长度 ,Product_Hgih 高度 , Product_Wide 宽度 , Product_Temperature 保存温度 , Product_humidity 保存湿度 FROM [WMS].[dbo].[Product]");
            dataGridView1.DataSource = data;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedRows[0].Index;
            textBox18.Text = data.Rows[index][0].ToString();
            textBox17.Text = data.Rows[index][1].ToString();
            textBox16.Text = data.Rows[index][2].ToString();
            textBox15.Text = data.Rows[index][3].ToString();
            textBox14.Text = data.Rows[index][4].ToString();
            textBox13.Text = data.Rows[index][5].ToString();
            textBox12.Text = data.Rows[index][6].ToString();
            textBox11.Text = data.Rows[index][7].ToString();
            textBox10.Text = data.Rows[index][8].ToString();

        }
    }
}
