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
            data = DBUtility.GetData("SELECT [WareArea_ID] 区域编号 ,[Warehouse_ID] 仓库编号 ,[WareType_ID] 仓库类别编号 ,[WareArea_Floor] 层数 ,[WareArea_High] 高度 ,[WareArea_Wide] 宽度 ,[WareArea_Long] 长度 ,[WareArea_MaxWeight] 最大承重 ,[WareArea_Cost] 花费 ,[WareArea_NowTem] 当前温度 ,[WareArea_NowHum] 当前湿度 ,[Last_check_date] 最后检查日期   FROM [WMS].[dbo].[WareArea]");
        }
    }
}
