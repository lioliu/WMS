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
    public partial class WareArea : Form
    {
        DataTable data;
        public WareArea()
        {
            InitializeComponent();
           
            ShowData();
            DataTable temp = DBUtility.GetData("select warehouse_name from dbo.warehouse");
            for (int i = 0; i < temp.Rows.Count; i++)
            {
                comboBox1.Items.Add(temp.Rows[i][0].ToString());
            }
            temp = DBUtility.GetData("select waretype_name from dbo.wareareatype");
            for (int i = 0; i < temp.Rows.Count; i++)
            {
                comboBox2.Items.Add(temp.Rows[i][0].ToString());
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

        }




        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedRows[0].Index;
            textBox1.Text = data.Rows[index][0].ToString();
            //set combobox
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if (comboBox1.Items[i].ToString().Equals(data.Rows[index][1].ToString()))
                {
                    comboBox1.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < comboBox2.Items.Count; i++)
            {

                if (comboBox2.Items[i].ToString().Equals(data.Rows[index][2].ToString()))
                {
                    comboBox2.SelectedIndex = i;
                    break;
                }
            }

            textBox2.Text = data.Rows[index][3].ToString();
            textBox3.Text = data.Rows[index][4].ToString();
            textBox4.Text = data.Rows[index][5].ToString();
            textBox5.Text = data.Rows[index][6].ToString();
            textBox6.Text = data.Rows[index][7].ToString();
            textBox7.Text = data.Rows[index][8].ToString();
            textBox8.Text = data.Rows[index][9].ToString();
            textBox9.Text = data.Rows[index][10].ToString();

        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DBUtility.ExecuteSQL($"delete From [WMS].[dbo].[WareArea] where  WareArea_ID = '{textBox1.Text}'");
                ShowData();
            }
            catch (Exception)
            {
                MessageBox.Show("当前区域仍有货物不可删除。");
            }
        }
        /// <summary>
        /// mod
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //获取 combobox 的值
            string warehouse_id = DBUtility.GetData($"select warehouse_ID from dbo.warehouse where warehouse_name = '{comboBox1.SelectedItem.ToString()}'").Rows[0][0].ToString();
            string waretype_id = DBUtility.GetData($"select waretype_ID from dbo.wareAreatype where waretype_name = '{comboBox2.SelectedItem.ToString()}'").Rows[0][0].ToString();
            DBUtility.ExecuteSQL($"UPDATE [WMS].[dbo].[WareArea] set  Warehouse_ID = '{warehouse_id}', waretype_id = '{waretype_id}',WareArea_Floor = '{textBox2.Text}', WareArea_High = '{textBox3.Text}',WareArea_Wide = '{textBox4.Text}',WareArea_Long = '{textBox5.Text}',WareArea_MaxWeight = '{textBox6.Text}',WareArea_Cost = '{textBox7.Text}',WareArea_NowTem = '{textBox8.Text}',WareArea_NowHum = '{textBox9.Text}' where WareArea_ID = '{textBox1.Text}' ");
            ShowData();
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //获取 combobox 的值
            string warehouse_id = DBUtility.GetData($"select warehouse_ID from dbo.warehouse where warehouse_name = '{comboBox1.SelectedItem.ToString()}'").Rows[0][0].ToString();
            string waretype_id = DBUtility.GetData($"select waretype_ID from dbo.wareAreatype where waretype_name = '{comboBox2.SelectedItem.ToString()}'").Rows[0][0].ToString();

            DBUtility.ExecuteSQL($"insert into [dbo].[WareArea] select right('00000000000000000000'+cast(max([WareArea_ID])+1 as varchar),10),'{warehouse_id}','{waretype_id}','{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{textBox5.Text}','{textBox6.Text}','{textBox7.Text}','{textBox8.Text}','{textBox9}',NULL from  [WMS].[dbo].[WareArea] ");
            ShowData();
        }
        private void ShowData()
        {
            data = DBUtility.GetData("SELECT [WareArea_ID] 区域编号,[Warehouse_Name] 主仓库名称 ,[Waretype_Name] 仓库类别 ,[WareArea_Floor] 层数 ,[WareArea_High] 高度 ,[WareArea_Wide] 宽度 ,[WareArea_Long] 长度 ,[WareArea_MaxWeight] 最大承重 ,[WareArea_Cost] 花费 ,[WareArea_NowTem] 当前温度 ,[WareArea_NowHum] 当前湿度 ,[Last_check_date] 最后检查时间 FROM [WMS].[dbo].[WareArea] a, WareAreaType b, WareHouse c  where a.Warehouse_ID = c.Warehouse_ID and a.WareType_ID = b.WareType_ID");
            dataGridView1.DataSource = data;
        }
    }
}
