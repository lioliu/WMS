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

        }



        private void ShowData()
        {
            data = DBUtility.GetData("SELECT [WareArea_ID] 区域编号,[Warehouse_Name] 主仓库名称 ,[Waretype_Name] 仓库类别 ,[WareArea_Floor] 层数 ,[WareArea_High] 高度 ,[WareArea_Wide] 宽度 ,[WareArea_Long] 长度 ,[WareArea_MaxWeight] 最大承重 ,[WareArea_Cost] 花费 ,[WareArea_NowTem] 当前温度 ,[WareArea_NowHum] 当前湿度 ,[Last_check_date] 最后检查时间 FROM[WMS].[dbo].[WareArea] a, WareAreaType b, WareHouse c  where a.Warehouse_ID = c.Warehouse_ID and a.WareType_ID = b.WareType_ID");
            dataGridView1.DataSource = data;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {

        }

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
    }
}
