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
    public partial class CreateStockOut : Form
    {
        string Customer_ID;
        DataTable data = null;
        public CreateStockOut(string customer_id)
        {
            this.Customer_ID = DBUtility.GetData($"select customer_id from customer where customer_email = '{customer_id}'").Rows[0][0].ToString();
            InitializeComponent();
            data = DBUtility.GetData("select CAST('false' as bit) 是否出库 ,a.Product_ID 商品编号,b.Product_Name 商品名称,sum(Ware_detail_number) 商品总数 ,'0'  出库数量 " +
                " from[WMS].[dbo].[Ware_Detail] a, Product b" +
                " where a.Product_ID = b.Product_ID " +
                $" group by a.Product_ID, b.Product_Name, a.Ware_detail_owner having a.Ware_detail_owner = '{Customer_ID}'");
            dataGridView1.DataSource = data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region stock out
            string freeStaff = DBUtility.GetData("  select a.staff_id ,isnull(innumber + outnumber,0) 处理数量 " +
  " from Staff a left join (select Staff_id, isnull(count(*), 0) innumber from stock_in where stock_in_Checked = 0 group by staff_ID) b on a.staff_ID = b.staff_ID " +
  " left join(select Staff_id, count(*) outnumber from Stock_out where stock_out_Checked = 0 group by staff_ID ) c on a.staff_ID = c.staff_ID " +
  "order by 处理数量").Rows[0][1].ToString();
            DBUtility.ExecuteSQL($"insert into stock_out select  right('00000000000000000000'+cast(max(stock_in_id2)+1 as varchar),20),'{freeStaff}','{Customer_ID}',getdate(),0,0,0,0 from stock_out   "); // pay price need to modi todo
            #endregion

            #region stock out detail
            string stock_out_id = DBUtility.GetData("select right('00000000000000000000'+cast(max(stock_in_id2) as varchar),20) from stock_out ").Rows[0][0].ToString();
            int sn = 1;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (int.Parse(data.Rows[i][0].ToString()) == 1) // need to stock out
                {
                    DBUtility.ExecuteSQL($"insert into stock_out_detail VALUES('{stock_out_id}','{data.Rows[i][1].ToString()}','{sn++}','{data.Rows[i][4].ToString()}')");
                }
            }
            #endregion
        }
    }
}
