using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.IO;

namespace DATAMINING_ASSOCIATIONRULE
{
    public partial class Admin_Loginpage : System.Web.UI.Page
    {
        Visitor obj = new Visitor();

        protected void Page_Load(object sender, EventArgs e)
        {
            tb_uname.Focus();
        }

        protected void btn_submit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (obj.Check_Adminlogin(tb_uname.Text, tb_pass.Text))
                {
                    //Session["Login"] = "Admin";
                    Session["Admin_ID"] = tb_uname.Text;
                    //Session["Admin_PWD"] = tb_pass.Text;

                    Response.Redirect("Admin_Homepage.aspx");
                }
                else
                {
                    Label1.Text = "Invalid AdminID/password";
                }
            }
            catch
            {

            }
        }


    }
}
