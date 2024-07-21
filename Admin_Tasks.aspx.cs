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
    public partial class Admin_Tasks : System.Web.UI.Page
    {
        Admin_Class obj = new Admin_Class();
        DataTable tab = new DataTable();
        static int i = 0, j = 5, serial_no = 1, FAQ_ID = 0;
        string value = null;
        static string oldpassword = null;
        static int VQ_previous_i = 0, VQ_next_j = 5, QuestionID = 0;

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
                    value = Request.QueryString["value"].ToString();

                    if (value.Equals("Changepassword"))
                    {
                        MultiView1.SetActiveView(View3);
                    }

                    else if (value.Equals("Feedbacks"))
                    {
                        if (!this.IsPostBack)
                        {
                            i = 0; j = 5;
                        }

                        loadfeedbacks();
                        MultiView1.SetActiveView(View5);
                    }


                }
            }
            catch
            {

            }
        }

        #region ------- ADMIN CHANGE PASSWORD ---------

        protected void btn_changepassword_Click(object sender, EventArgs e)
        {
            tab.Rows.Clear();

            tab = obj.GetAdminDetails(Session["Admin_ID"].ToString());

            if (tab.Rows.Count > 0)
            {
                oldpassword = tab.Rows[0]["Password"].ToString();
            }
            if (oldpassword.Equals(txt_oldpassword.Text))
            {
                try
                {
                    obj.AdminChangepassword(txt_Newpassword.Text, Session["Admin_ID"].ToString());
                    ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Password changed successfully')</script>");

                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Server Error!')</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Old password incorrect')</script>");
            }
        }


        #endregion



        #region --------- FEEDBACK MANAGEMENT ---------

        public void loadfeedbacks()
        {
            tab.Rows.Clear();
            tab = obj.GetAllFeedbacks();

            if (tab.Rows.Count > 0)
            {
                Table3.Rows.Clear();

                Table3.BorderStyle = BorderStyle.Double;
                Table3.GridLines = GridLines.Both;
                Table3.BackImageUrl = "Images/Opera-Background-Light-Blue";
                Table3.BorderColor = System.Drawing.Color.SteelBlue;

                TableRow mainrow = new TableRow();
                mainrow.BorderStyle = BorderStyle.Double;
                mainrow.ForeColor = System.Drawing.Color.WhiteSmoke;
                mainrow.BackColor = System.Drawing.Color.Orange;



                TableCell cell2 = new TableCell();
                cell2.Text = "<b>Member ID</b>";
                mainrow.Controls.Add(cell2);

                TableCell cell3 = new TableCell();
                cell3.Text = "<b>Feedback</b>";
                mainrow.Controls.Add(cell3);

                TableCell cell31 = new TableCell();
                cell31.Text = "<b>Posted date</b>";
                mainrow.Controls.Add(cell31);

                TableCell cell4 = new TableCell();
                cell4.Text = "<b>Delete</b>";
                mainrow.Controls.Add(cell4);

                Table3.Controls.Add(mainrow);

                for (int cnt = i; cnt < j; cnt++)
                {
                    if (cnt < tab.Rows.Count)
                    {
                        string msg = null;

                        if (tab.Rows.Count > j)
                        {
                            msg = string.Format("{0} - {1} Records out of {2} Records", i + 1, j, tab.Rows.Count);
                        }
                        else
                        {
                            msg = string.Format("{0} - {1} Records out of {2} Records", i + 1, tab.Rows.Count, tab.Rows.Count);
                        }

                        lbl_paging.Text = msg;
                        TableRow row1 = new TableRow();

                        //TableCell cell_fid = new TableCell();
                        //cell_fid.Text = tab.Rows[i]["Feedback_ID"].ToString();
                        //row1.Controls.Add(cell_fid);

                        TableCell cell_compname = new TableCell();
                        cell_compname.Text = tab.Rows[cnt]["Email_ID"].ToString();
                        row1.Controls.Add(cell_compname);

                        TableCell cell_feedback = new TableCell();
                        cell_feedback.Width = 300;
                        cell_feedback.Text = tab.Rows[cnt]["Feedback"].ToString();
                        row1.Controls.Add(cell_feedback);

                        TableCell cell_date = new TableCell();
                        cell_date.Text = tab.Rows[cnt]["Posted_date"].ToString();
                        row1.Controls.Add(cell_date);

                        TableCell cell_delete = new TableCell();

                        Button btn_delete1 = new Button();
                        btn_delete1.Text = "Delete";
                        btn_delete1.ID = tab.Rows[cnt]["Feedback_ID"].ToString();
                        btn_delete1.OnClientClick = "return confirm('Are you sure want to delete this record')";
                        btn_delete1.Click += new EventHandler(btn_delete1_Click);

                        cell_delete.Controls.Add(btn_delete1);
                        row1.Controls.Add(cell_delete);

                        Table3.Controls.Add(row1);

                        if (tab.Rows.Count > 5)
                        {
                            if (cnt < 5)
                            {
                                ImageButton8.Visible = true;
                                ImageButton7.Visible = false;
                                ImageButton9.Visible = false;
                            }
                            else
                            {
                                if (j >= tab.Rows.Count)
                                {
                                    ImageButton9.Visible = true;
                                    ImageButton8.Visible = false;
                                    ImageButton7.Visible = true;
                                }
                                else
                                {
                                    ImageButton9.Visible = false;
                                    ImageButton8.Visible = true;
                                    ImageButton7.Visible = true;
                                }
                            }
                        }
                        else
                        {
                            ImageButton7.Visible = false;
                            ImageButton8.Visible = false;
                            ImageButton9.Visible = false;
                        }


                    }
                    else
                    {
                        if (tab.Rows.Count < 5)
                        {
                            ImageButton7.Visible = false;
                            ImageButton8.Visible = false;
                            ImageButton9.Visible = false;
                        }
                        else
                        {
                            ImageButton7.Visible = true;
                            ImageButton8.Visible = false;
                            ImageButton9.Visible = true;
                        }
                    }
                }

            }
            else
            {
                Table3.Rows.Clear();
                Table3.GridLines = GridLines.None;
                lbl_paging.Text = "";

                TableHeaderRow row = new TableHeaderRow();
                TableHeaderCell cell = new TableHeaderCell();
                cell.ForeColor = System.Drawing.Color.Red;
                cell.Text = "No Feedbacks Found";

                row.Controls.Add(cell);
                Table3.Controls.Add(row);

                ImageButton7.Visible = false;
                ImageButton8.Visible = false;
                ImageButton9.Visible = false;
            }
        }

        void btn_delete1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Button btn = (Button)sender;
            int id = int.Parse(btn.ID.ToString());

            try
            {
                obj.DeleteFeedback(id);
                ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('Record Deleted Successfully')</script>");
                loadfeedbacks();

            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('Server Error!')</script>");

            }
        }

        protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
        {
            i = i - 5;
            j = j - 5;

            if (i >= 0)
            {
                Table3.Rows.Clear();
                loadfeedbacks();

            }
            else
            {
                i = 0;
                j = 5;

            }
        }

        protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
        {
            i = j;
            j = j + 5;

            if (i < tab.Rows.Count)
            {
                Table3.Rows.Clear();
                loadfeedbacks();

            }
            else
            {
                i = i - 5;
                j = j - 5;
                ImageButton7.Visible = false;
                ImageButton8.Visible = false;
                ImageButton9.Visible = true;


            }
        }

        protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Admin_Tasks.aspx?value=feedbacks");
        }

        #endregion




    }
}
