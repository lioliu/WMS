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
    public partial class RoutingInspection : Form
    {
        public RoutingInspection()
        {
            InitializeComponent();
            DataTable data = DBUtility.GetData("select WareArea_ID from wareArea");
            for (int i = 0; i < data.Rows.Count; i++)
            {
                comboBox1.Items.Add(data.Rows[i][0].ToString());
            }
        }

        private void RoutingInspection_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBUtility.ExecuteSQL($"Update warearea set last_check_date = getdate() where wareArea_id ='{comboBox1.SelectedItem.ToString()}' ");
            MessageBox.Show("巡检成功");
        }
    }
}
