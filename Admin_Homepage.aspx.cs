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

namespace DATAMINING_ASSOCIATIONRULE
{
    public partial class Admin_Homepage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Admin_ID"] == null)
                {
                    Session.Abandon();
                    Response.Redirect("Homepage.aspx");
                }
                else
                {
                    //Label1.Text = "Welcome " + Session["Admin_ID"].ToString() + " Now the time is : " + DateTime.Now.ToString();
                    GetAllCustomers();
                }
            }
            catch
            {

            }
        }

        private void GetAllCustomers()
        {
            DataTable tab = new DataTable();
            Admin_Class adminObj = new Admin_Class();

            tab.Rows.Clear();
            tab = adminObj.GetAllCustomers();

            if (tab.Rows.Count > 0)
            {
                Table1.Rows.Clear();

                Table1.BorderStyle = BorderStyle.Double;
                Table1.GridLines = GridLines.Both;
                //Table1.BackImageUrl = "Images/Opera-Background-Light-Blue";
                Table1.BorderColor = System.Drawing.Color.Black;

                TableRow mainrow = new TableRow();
                mainrow.Height = 20;
                mainrow.ForeColor = System.Drawing.Color.WhiteSmoke;
                mainrow.BackColor = System.Drawing.Color.Orange;

                TableCell cell1 = new TableCell();
                cell1.Text = "<b>EmailId</b>";
                mainrow.Controls.Add(cell1);

                TableCell cell2 = new TableCell();
                cell2.Text = "<b>Name</b>";
                mainrow.Controls.Add(cell2);

                TableCell cell5 = new TableCell();
                cell5.Text = "<b>Gender</b>";
                mainrow.Controls.Add(cell5);

                TableCell cell6 = new TableCell();
                cell6.Text = "<b>DateOfBirth</b>";
                mainrow.Controls.Add(cell6);

                TableCell cell7 = new TableCell();
                cell7.Text = "<b>ContactNo</b>";
                mainrow.Controls.Add(cell7);

                TableCell cell9 = new TableCell();
                cell9.Text = "<b>Delete</b>";
                mainrow.Controls.Add(cell9);

                Table1.Controls.Add(mainrow);

                for (int cnt = 0; cnt < tab.Rows.Count; cnt++)
                {
                    TableRow row = new TableRow();

                    TableCell cell_name = new TableCell();
                    cell_name.Width = 100;
                    cell_name.Text = "<a href='#'>" + tab.Rows[cnt]["Email_ID"].ToString() + "<span>Name : " + tab.Rows[cnt]["Name"].ToString() + ".</br>Address : " + tab.Rows[cnt]["Address"].ToString() + ".</br>Email ID : " + tab.Rows[cnt]["Email_ID"].ToString() + "</span></a>";
                    row.Controls.Add(cell_name);

                    TableCell cell_details = new TableCell();
                    cell_details.Width = 250;
                    cell_details.Text = tab.Rows[cnt]["Name"].ToString();
                    row.Controls.Add(cell_details);

                    TableCell cell_mobile = new TableCell();
                    cell_mobile.Width = 100;
                    cell_mobile.Text = tab.Rows[cnt]["Gender"].ToString();
                    row.Controls.Add(cell_mobile);

                    TableCell cell_email = new TableCell();
                    cell_email.Width = 200;
                    cell_email.Text = tab.Rows[cnt]["DateOfBirth"].ToString();
                    row.Controls.Add(cell_email);

                    TableCell cell_date = new TableCell();
                    cell_date.Width = 100;
                    cell_date.Text = tab.Rows[cnt]["ContactNo"].ToString();
                    row.Controls.Add(cell_date);

                    TableCell cell_Delete = new TableCell();

                    Button btn_del = new Button();
                    btn_del.Text = "Delete";
                    btn_del.ID = tab.Rows[cnt]["Email_ID"].ToString();
                    btn_del.OnClientClick = "return confirm('Are you sure want to delete?')";
                    btn_del.Click += new EventHandler(btn_del_Click);
                    cell_Delete.Controls.Add(btn_del);
                    row.Controls.Add(cell_Delete);

                    Table1.Controls.Add(row);
                }
            }
            else
            {
                Table1.Rows.Clear();
                Table1.GridLines = GridLines.None;

                TableRow rno = new TableRow();

                TableCell cellno = new TableCell();
                cellno.ColumnSpan = 10;
                cellno.ForeColor = System.Drawing.Color.Red;
                cellno.Font.Size = 10;
                cellno.Text = "<b>No Registered Users Found</b>";
                cellno.HorizontalAlign = HorizontalAlign.Center;
                rno.Controls.Add(cellno);
                Table1.Controls.Add(rno);
            }
        }

        void btn_del_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            DataTable tab = new DataTable();
            Admin_Class adminObj = new Admin_Class();

            string emailId = btn.ID.ToString();

            tab.Rows.Clear();
            Member_Class memObj = new Member_Class();
            tab = memObj.GetCustomerTransactions(emailId);

            if (tab.Rows.Count > 0)
            {
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    DataTable tab1 = new DataTable();
                    tab1 = memObj.GetTransactionDetails(int.Parse(tab.Rows[i]["Transaction_Id"].ToString()));

                    for (int j = 0; j < tab1.Rows.Count; j++)
                    {
                        adminObj.DeleteTransactionDetails(int.Parse(tab1.Rows[j]["Details_ID"].ToString()));
                    }
                }

                adminObj.DeleteCustomerTransactions(emailId);
                //delete customer feedbacks
                //delete customer ratings
                adminObj.DeleteCustomer(emailId);

                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Customer Details Deleted Successfully')</script>");

                GetAllCustomers();

            }
            else
            {
                //delete customer feedbacks
                //delete customer ratings
                adminObj.DeleteCustomer(emailId);

                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Customer Details Deleted Successfully')</script>");

                GetAllCustomers();

            }
        }





        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Label1.Text = "Welcome " + Session["Admin_ID"].ToString() + " Now the time is : " + DateTime.Now.ToString();
        }





    }
}
