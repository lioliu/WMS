using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace WMS
{
    public partial class Sign : Form
    {
        string CAPTCHA = string.Empty;
        public Sign()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var item in this.Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).Text = string.Empty;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            // check email format
            if (!Regex.IsMatch(textBox1.Text, @"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$"))
            {
                MessageBox.Show("邮箱格式错误！");
                return;
            }
            //check CAPTCHA
            if (!textBox2.Equals(CAPTCHA))
            {
                MessageBox.Show("验证码错误");
            }
            //check PassWord
            if (!textBox3.Text.Equals(textBox4.Text))
            {
                MessageBox.Show("两次密码不同");
            }
            //check     
        }

     
    }
}
