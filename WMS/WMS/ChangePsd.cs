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
    public partial class ChangePsd : Form
    {
        string ID;
        int type;
        public ChangePsd(string ID, int type)
        {
            this.ID = ID;
            this.type = type;
            InitializeComponent();
        }

        private void ChangePsd_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (type)
            {
                case 1:
                    {
                        string oldPsd = DBUtility.GetData($"select customer_psd from customer where customer_email = '{ID}'").Rows[0][0].ToString();
                        if (oldPsd != textBox1.Text)
                        {
                            MessageBox.Show("原密码错误");
                            return;
                        }
                        if (textBox2.Text != textBox3.Text)
                        {
                            MessageBox.Show("两次密码不同");
                            return;
                        }
                        DBUtility.ExecuteSQL($"update  customer set customer_psd ='{textBox2.Text}' ");
                        break;
                    }
                case 2:
                    {
                        string oldPsd = DBUtility.GetData($"select staff_psd from staff where staff_id = '{ID}'").Rows[0][0].ToString();
                        if (oldPsd != textBox1.Text)
                        {
                            MessageBox.Show("原密码错误");
                            return;
                        }
                        if (textBox2.Text != textBox3.Text)
                        {
                            MessageBox.Show("两次密码不同");
                            return;
                        }
                        DBUtility.ExecuteSQL($"update  staff set staff_psd ='{textBox2.Text}' ");
                        break;
                    }
                default:
                    break;
            }
            MessageBox.Show("修改密码成功。");
            this.Close();
            this.Dispose();
        }
    }
}
