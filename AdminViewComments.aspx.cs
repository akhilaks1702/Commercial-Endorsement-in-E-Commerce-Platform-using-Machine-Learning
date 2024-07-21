using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

namespace DATAMINING_ASSOCIATIONRULE
{
    public partial class AdminViewComments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                loadComments();
            }
            catch
            {

            }
        }

        //function to load comments
        private void loadComments()
        {
            DataTable tab = new DataTable();
            Admin_Class adminObj = new Admin_Class();

            tab.Rows.Clear();
            tab = adminObj.GetRatingByItem(int.Parse(Request.QueryString["ItemId"].ToString()));

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
                cell2.Text = "<b>Rating</b>";
                mainrow.Controls.Add(cell2);

                TableCell cell5 = new TableCell();
                cell5.Text = "<b>Comment</b>";
                mainrow.Controls.Add(cell5);

                TableCell cell6 = new TableCell();
                cell6.Text = "<b>PostedDate</b>";
                mainrow.Controls.Add(cell6);
                               
                TableCell cell9 = new TableCell();
                cell9.Text = "<b>Delete</b>";
                mainrow.Controls.Add(cell9);

                Table1.Controls.Add(mainrow);

                for (int cnt = 0; cnt < tab.Rows.Count; cnt++)
                {
                    TableRow row = new TableRow();
                                       
                    TableCell cell_details = new TableCell();
                    cell_details.Width = 250;
                    cell_details.Text = tab.Rows[cnt]["Email_ID"].ToString();
                    row.Controls.Add(cell_details);

                    TableCell cell_mobile = new TableCell();
                    cell_mobile.Width = 100;
                    cell_mobile.Text = tab.Rows[cnt]["Rating"].ToString();
                    row.Controls.Add(cell_mobile);

                    TableCell cell_email = new TableCell();
                    cell_email.Width = 200;
                    cell_email.Text = tab.Rows[cnt]["Comment"].ToString();
                    row.Controls.Add(cell_email);

                    TableCell cell_date = new TableCell();
                    cell_date.Width = 100;
                    cell_date.Text = tab.Rows[cnt]["PostedDate"].ToString();
                    row.Controls.Add(cell_date);

                    TableCell cell_Delete = new TableCell();

                    Button btn_del = new Button();
                    btn_del.Text = "Delete";
                    btn_del.ID = tab.Rows[cnt]["RatingId"].ToString();
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
                cellno.Text = "<b>No Comments Found!!!</b>";
                cellno.HorizontalAlign = HorizontalAlign.Center;
                rno.Controls.Add(cellno);
                Table1.Controls.Add(rno);
            }
        }

        void btn_del_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                Member_Class obj = new Member_Class();

                obj.DeleteRating(int.Parse(btn.ID));

                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<Script>alert('Rating Deleted Successfully')</script>");

                loadComments();

            }
            catch
            { }
        }


    }
}