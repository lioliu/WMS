using System;
using System.Windows.Forms;

namespace WMS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            cbxType.SelectedIndex = 0;
        }

        private void cbxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Reset();

            if (cbxType.SelectedIndex == 0)
            {
                lSign.Visible = false;
                lID.Text = "ID:";
            }
            else
            {
                lSign.Visible = !false;
                lID.Text = "邮箱:";
            }
        }

        private void bReset_Click(object sender, EventArgs e)
        {
            Reset();

        }

        private void Reset()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
        }

        private void bLogin_Click(object sender, EventArgs e)
        {
            Form main = null;
            switch (cbxType.SelectedIndex)
            {
                case 0:
                    {
                        if (Staff.Login(textBox1.Text, textBox2.Text))
                        {
                            main = new StaffMain(textBox1.Text);
                            Hide();
                            main.ShowDialog();
                            Close();
                            Dispose();
                        }
                        break;
                    }
                case 1:
                    {
                        if (Customer.Login(textBox1.Text, textBox2.Text))
                        {
                            main = new CustomerMain(textBox1.Text);
                            Hide();
                            main.ShowDialog();
                            Close();
                            Dispose();
                        }
                        break;
                    }
                default:
                    break;
            }
         
           
        }

        private void lSign_Click(object sender, EventArgs e)
        {
            Sign sign = new Sign();
            sign.ShowDialog();
        }
    }
}
