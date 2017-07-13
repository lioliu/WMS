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
    public partial class WareHouse : Form
    {
        DataTable data;
        public WareHouse()
        {
            InitializeComponent();
            ShowData();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int index = dataGridView1.SelectedRows[0].Index;
            textBox1.Text = data.Rows[index][0].ToString();
            textBox2.Text = data.Rows[index][1].ToString();

        }
        /// <summary>
        /// insert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DBUtility.ExecuteSQL($"insert into  [WMS].[dbo].[WareHouse] select  right('0000000000'+cast(max(Warehouse_ID)+1 as varchar),10),'{textBox2.Text}' from [WMS].[dbo].[WareHouse] ");
            ShowData();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            DBUtility.ExecuteSQL($"update [WMS].[dbo].[WareHouse] set Warehouse_Name = '{textBox2.Text}'  where Warehouse_ID = '{textBox1.Text}'  ");
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
                DBUtility.ExecuteSQL($"delete from [WMS].[dbo].[WareHouse] where Warehouse_ID = '{textBox1.Text}'  ");
                ShowData();
            }
            catch (Exception)
            {
                MessageBox.Show("有子存储仓库不能删除。");
            }
        }
            private void ShowData()
            {
                data = DBUtility.GetData("SELECT [Warehouse_ID] 仓库编号 ,[Warehouse_Name] 仓库名 FROM [WMS].[dbo].[WareHouse]");
                dataGridView1.DataSource = data;
            }
        }
    }
