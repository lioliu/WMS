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

    public partial class WareAreaType : Form
    {
        DataTable data;
        public WareAreaType()
        {
            InitializeComponent();
            ShowData();
        }



        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedRows[0].Index;

            textBox1.Text = data.Rows[index][0].ToString();
            textBox2.Text = data.Rows[index][1].ToString();
            checkBox1.Checked = (Boolean)data.Rows[index][2];
            checkBox2.Checked = (Boolean)data.Rows[index][3];
            textBox3.Text = data.Rows[index][4].ToString();
            textBox4.Text = data.Rows[index][5].ToString();
            textBox5.Text = data.Rows[index][6].ToString();
            textBox6.Text = data.Rows[index][7].ToString();
        }

        /// <summary>
        /// add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // slove the null value
            string Waretype_Max_tem = checkBox1.Checked ? $"'{textBox3.Text}'" : "NULL";
            string Waretype_Min_tem = checkBox1.Checked ? $"'{textBox4.Text}'" : "NULL";
            string Waretype_Max_hum = checkBox2.Checked ? $"'{textBox5.Text}'" : "NULL";
            string Waretype_Min_hum = checkBox2.Checked ? $"'{textBox6.Text}'" : "NULL";

            DBUtility.ExecuteSQL($"INSERT INTO [WMS].[dbo].[WareAreaType] select right('00000000000000000000'+cast(max([WareType_ID])+1 as varchar),10),'{textBox2.Text}',{checkBox1.Checked},{checkBox2.Checked},{Waretype_Max_tem},{Waretype_Min_tem},{Waretype_Max_hum},{Waretype_Min_hum} from [WMS].[dbo].[WareType_ID]");
            ShowData();
        }
        /// <summary>
        /// mod
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string Waretype_Max_tem = checkBox1.Checked ? $"'{textBox3.Text}'" : "NULL";
            string Waretype_Min_tem = checkBox1.Checked ? $"'{textBox4.Text}'" : "NULL";
            string Waretype_Max_hum = checkBox2.Checked ? $"'{textBox5.Text}'" : "NULL";
            string Waretype_Min_hum = checkBox2.Checked ? $"'{textBox6.Text}'" : "NULL";

            DBUtility.ExecuteSQL($"update  [WMS].[dbo].[WareAreaType] set Waretype_Name = '{textBox2.Text}', Waretype_Tem_controlable ={checkBox1.Checked},Waretype_Hum_controlable = {checkBox2.Checked},Waretype_Max_tem = {Waretype_Max_tem},Waretype_Min_tem = {Waretype_Min_tem},Waretype_Max_hum = {Waretype_Max_hum},Waretype_Min_hum = {Waretype_Min_hum} where WareType_ID = '{textBox1.Text}'");
            ShowData();
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
                DBUtility.ExecuteSQL($"delete from  [WMS].[dbo].[WareAreaType]  where WareType_ID = '{textBox1.Text}'");
                ShowData();

            }
            catch (Exception)
            {
                MessageBox.Show("有存储区域正使用此分类，不能删除。");
            }
        }
        private void ShowData()
        {
            data = DBUtility.GetData("SELECT [WareType_ID] 种类编号 ,[Waretype_Name] 种类名称 ,[Waretype_Tem_controlable] 可控温 ,[Waretype_Hum_controlable] 可控湿 ,[Waretype_Max_tem] 最高温度 ,[Waretype_Min_tem] 最低温度 ,[Waretype_Max_hum] 最高湿度 ,[Waretype_Min_hum] 最低湿度  FROM [WMS].[dbo].[WareAreaType]");
            dataGridView1.DataSource = data;
        }
    }
}
