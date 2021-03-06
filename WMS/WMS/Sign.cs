﻿using System;
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
        string Captcha = string.Empty;
        const int CaptchaLength = 6;
        int timecount = 0;
        public Sign()
        {
            InitializeComponent();
            button1.Enabled = false;
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
          
            //check CAPTCHA
            if (!textBox2.Text.Equals(Captcha))
            {
                MessageBox.Show("验证码错误");
                return;
            }
            //check PassWord
            if (!textBox3.Text.Equals(textBox4.Text))
            {
                MessageBox.Show("两次密码不同");
                return;
            }
            Customer customer = new Customer(textBox1.Text,textBox3.Text,textBox5.Text);
            if (customer.SignUp())
            {
                MessageBox.Show("注册成功");
                Close();
                Dispose();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (!Regex.IsMatch(textBox1.Text, @"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$"))
            {
                MessageBox.Show("邮箱格式错误！");
                return;
            }
            else
            {
                button1.Enabled = true;
                Captcha = CAPTCHA.Number(CaptchaLength);
                Email.Sent(textBox1.Text, Captcha);
                button3.Enabled = false;
                timecount = 60;
                button3.Text = timecount.ToString();
                timer1.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timecount>0)
            {
                timecount--;
                button3.Text = timecount.ToString();
            }
            else
            {
                button3.Text = "重新发送";
                button3.Enabled = true;
                timer1.Enabled = false;
            }
        }
    }
}
