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
    public partial class StaffType : Form
    {
        public StaffType()
        {
            InitializeComponent();
            ShowData();

        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            string temp = listBox1.SelectedItem.ToString();
            DBUtility.ExecuteSQL($"DELETE FROM STAFF_TYPE where staff_type_name ='{temp}'");
            listBox1.Items.Remove(temp);
        }
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DBUtility.ExecuteSQL($"insert into STAFF_TYPE select  right('0'+cast(max([Staff_type_id])+1 as varchar),2),'{textBox1.Text}' from STAFF_TYPE ");
            ShowData();
        }
        private void ShowData()
        {
            listBox1.Items.Clear();

            DataTable data = DBUtility.GetData("select staff_type_name from staff_type");
            for (int i = 0; i < data.Rows.Count; i++)
            {
                listBox1.Items.Add(data.Rows[i][0].ToString());
            }

        }
    }
}
