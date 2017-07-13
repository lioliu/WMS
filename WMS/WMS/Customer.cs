using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace WMS
{
    class Customer
    {
        string Name { set; get; }
        string PSD { set; get; }
        string Email { set; get; }
        public Customer(string Email,string PSD,string Name)
        {
            this.Email = Email;
            this.PSD = PSD;
            this.Name = Name;
        }

        public Boolean SignUp()
        {
            //check the email is unique
            if(DBUtility.GetData($"select * from dbo.customer where customer_email  ='{Email}'").Rows.Count != 0)
            {
                System.Windows.Forms.MessageBox.Show("该邮箱已被注册，请更换邮箱");
                return false;
            }
            try
            {
                DBUtility.ExecuteSQL($"Insert into dbo.customer select right('000000000'+cast(max(customer_id)+1 as varchar),10),'{Name}','{PSD}','{Email}' from dbo.customer");
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
