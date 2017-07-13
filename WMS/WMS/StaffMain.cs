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
    public partial class StaffMain : Form
    {
        string ID = string.Empty;
        public StaffMain(string ID)
        {
            InitializeComponent();
            this.ID = ID;
        }

        private void 仓库信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            WareHouse wareHouse = new WareHouse();
            wareHouse.ShowDialog();
        }

        private void 区域种类信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WareAreaType wareAreaType = new WareAreaType();
            wareAreaType.ShowDialog();
        }

        private void 仓储区域信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WareArea wareArea = new WareArea();
            wareArea.ShowDialog();
        }

        private void 员工类型修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StaffType stafftype = new StaffType();
            stafftype.ShowDialog();
        }

        private void 密码修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePsd change = new ChangePsd(ID, 2);
            change.ShowDialog();
        }

        private void 巡检ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoutingInspection form = new RoutingInspection();
            form.ShowDialog();
        }

        private void 库存查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StockStaff form = new StockStaff();
            form.ShowDialog();
        }

        private void StaffMain_Load(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBUtility.ExecuteSQL($"update stock_in set stock_in_checked = 1,stock_in_pay = {double.Parse(textBox1.Text) * double.Parse(textBox2.Text)} where stock_in_id = '{comboBox2.SelectedItem.ToString()}'");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DBUtility.ExecuteSQL($"update stock_out set stock_out_checked = 1,stock_out_pay = {double.Parse(textBox3.Text) * double.Parse(textBox4.Text)} where stock_in_id2 = '{comboBox2.SelectedItem.ToString()}'");

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Text = "100";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "100";
        }

        private void 入库单处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 出库单处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            #region set pie pic
            List<string> xData = new List<string>() { "未用", "已用", };
            double total = double.Parse(DBUtility.GetData("select Convert(decimal(18,2),(sum(WareArea_High*WareArea_Long*WareArea_Wide)/1000000),2) from WareArea").Rows[0][0].ToString());
            double used = double.Parse(DBUtility.GetData("select Convert(decimal(18,2),isnull(sum(a.Ware_detail_number*b.Product_Hgih*b.Product_Long*b.Product_Wide),0),2) from Ware_Detail a left join Product b on a.Product_ID = b.Product_ID").Rows[0][0].ToString());
            List<double> yData = new List<double>() { total - used, used };
            chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
            chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
            chart1.Series[0].Points.DataBindXY(xData, yData);
            #endregion

            #region load stock list
            DataTable temp = DBUtility.GetData($"select stock_in_id from stock_in where stock_in_checked = 0 and staff_id ='{ID}'");
            for (int i = 0; i < temp.Rows.Count; i++)
            {
                comboBox1.Items.Add(temp.Rows[i][0].ToString());
            }
            temp = DBUtility.GetData($"select stock_in_id2 from stock_out where stock_out_checked = 0 and staff_id ='{ID}'");
            for (int i = 0; i < temp.Rows.Count; i++)
            {
                comboBox2.Items.Add(temp.Rows[i][0].ToString());
            }
            #endregion

            #region load routing list
            temp = DBUtility.GetData("select wareArea_id from wareArea where GETDATE()-30 > Last_check_date and WareArea_ID in (select WareArea_ID from Ware_Detail)");
            for (int i = 0; i < temp.Rows.Count; i++)
            {
                listBox1.Items.Add(temp.Rows[i][0].ToString());
            }
            temp = DBUtility.GetData("select wareArea_id from wareArea where GETDATE()-30 < Last_check_date or WareArea_ID not in (select WareArea_ID from Ware_Detail)");
            for (int i = 0; i < temp.Rows.Count; i++)
            {
                listBox2.Items.Add(temp.Rows[i][0].ToString());
            }
            #endregion
        }
    }
}
