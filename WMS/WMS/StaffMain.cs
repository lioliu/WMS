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
        string ID = string.Empty;
        public StaffMain(string ID)
        {
            InitializeComponent();
            this.ID = ID;
        }

        private void 仓库信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //todo
            WareHouse wareHouse = new WareHouse();
            wareHouse.ShowDialog();
        }

        private void 区域种类信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WareAreaType wareAreaType = new WareAreaType();
            wareAreaType.ShowDialog();
        }

        private void 仓储区域信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WareArea wareArea = new WareArea();
            wareArea.ShowDialog();
        }

        private void 员工类型修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StaffType stafftype = new StaffType();
            stafftype.ShowDialog();
        }

        private void 密码修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePsd change = new ChangePsd(ID, 2);
            change.ShowDialog();
        }
    }
}
