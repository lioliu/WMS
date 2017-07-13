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
    public partial class StaffMain : Form
    {
        public StaffMain(string ID)
        {
            InitializeComponent();
        }

        private void 仓库信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //todo
            WareHouse wareHouse = new WareHouse();
            wareHouse.ShowDialog();
        }
    }
}
