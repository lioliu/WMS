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
    public partial class CustomerMain : Form
    {
        string Email;
        public CustomerMain(string email)
        {
            Email = email;
            InitializeComponent();
        }

        private void 商品ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.ShowDialog();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePsd change = new ChangePsd(Email, 1);
            change.ShowDialog();
        }

        private void 创建出库单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateStockOut form = new CreateStockOut(Email);
                form.ShowDialog();
        }

        private void 查询库存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stock form = new Stock(Email);
            form.ShowDialog();
        }
    }
}
