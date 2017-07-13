using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS
{
    class Staff
    {
        public static Boolean Login(string ID, string PSD)
        {
            ID = ("000000000" + ID);
            ID = ID.Substring(ID.Length - 10);
            if (DBUtility.GetData($"select * from dbo.staff where staff_id = '{ID}' and staff_psd = '{PSD}'").Rows.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("错误的用户名或密码。");
                return false;
            }
            else
            {
             
                return true;

            }
        }
    }
}
