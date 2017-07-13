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
        public Product()
        {
            InitializeComponent();
            DataTable data = DBUtility.GetData("SELECT Product_ID 编号,Product_Name 名称,Product_tally 单位 , Product_Weight 重量 , Product_Long 长度 ,Product_Hgih 高度 , Product_Wide 宽度 , Product_Temperature 保存温度 , Product_humidity 保存湿度 FROM[WMS].[dbo].[Product]");
            dataGridView1.DataSource = data;
        }
    }
}
