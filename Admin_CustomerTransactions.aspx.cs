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
    public partial class Admin_CustomerTransactions : System.Web.UI.Page
    {
        Admin_Class obj = new Admin_Class();
        Visitor vobj = new Visitor();
        static DataTable tab_order = null;
        static int i = 0;
        static int j = 15;
        TableCell h4, Ch4, Ch5, h6, Ch6, h7, C24, Ch14, Ch15, C26, Ch16, C27, h8, C28, Ch17, Ch7, celldel;
        string tno = null;

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
                    if (this.IsPostBack == false)
                    {
                        i = 0;
                        j = 15;
                        DDListO_Status.SelectedIndex = 0;
                    }

                    view_orderdetails();
                }
            }
            catch
            {

            }
        }

        protected void DDListO_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            view_orderdetails();
        }

        private void view_orderdetails()
        {
            MultiView1.SetActiveView(View1);

            if (DDListO_Status.SelectedIndex == 0)

                view_All();

            else

                view_Status();
        }

        private void view_All()
        {
            TableOr.Rows.Clear();
            //Lbl_msg.Visible = false;
            TableCell Ch1 = new TableCell();
            Ch1.Text = "|";
            TableCell Ch2 = new TableCell();
            Ch2.Text = "|";
            TableCell Ch3 = new TableCell();
            Ch3.Text = "|";
            TableCell h1 = new TableCell();
            h1.Text = "Transaction ID";
            TableCell h2 = new TableCell();
            h2.Text = "Customer ID";
            TableCell h3 = new TableCell();
            h3.Text = "Transaction Date";
            TableCell h4 = new TableCell();
            h4.Text = "Status";
            TableCell h6 = new TableCell();
            h6.Text = "View_Details";
            
            TableRow R1 = new TableRow();
            R1.Font.Size = 10;
            R1.Font.Bold = true;
            R1.Controls.Add(h1);
            R1.Controls.Add(Ch1);
            R1.Controls.Add(h2);
            R1.Controls.Add(Ch2);
            R1.Controls.Add(h3);
            R1.Controls.Add(Ch3);
            R1.Controls.Add(h4);

            TableOr.Controls.Add(R1);
            TableCell Chr = new TableCell();
            Chr.ColumnSpan = 12;
            Chr.Text = "<hr/>";
            TableRow Rhr = new TableRow();
            Rhr.Controls.Add(Chr);
            TableOr.Controls.Add(Rhr);
            tab_order = obj.GetAllCustomerTransactions();

            if (tab_order.Rows.Count > 0)
            {
                for (int cnt = i; cnt < j; cnt++)
                {
                    if (cnt < tab_order.Rows.Count)
                    {
                        string msg;
                        PnlOrBtns.Visible = true;
                        if (j < tab_order.Rows.Count)
                        {
                            msg = string.Format("{0} - {1} Records out of {2}", i, j, tab_order.Rows.Count);

                        }
                        else
                        {
                            msg = string.Format("{0} - {1} Records out of {2}", i, tab_order.Rows.Count, tab_order.Rows.Count);
                        }
                        LblMsg_order.Text = msg;
                        TableCell Ch11 = new TableCell();
                        Ch11.Text = "|";
                        TableCell Ch12 = new TableCell();
                        Ch12.Text = "|";
                        TableCell Ch13 = new TableCell();
                        Ch13.Text = "|";

                        TableCell C21 = new TableCell();
                        C21.Text = tab_order.Rows[cnt]["Transaction_ID"].ToString();
                        TableCell C22 = new TableCell();
                        C22.Width = 200;
                        DataTable tab = vobj.GetCustomerDetails(tab_order.Rows[cnt]["Email_ID"].ToString());
                        C22.Text = "<a href='#'>" + tab.Rows[0]["Email_ID"].ToString() + "<span>Name: " + tab.Rows[0]["Name"].ToString() + ".<br/>Address : " + tab.Rows[0]["Address"].ToString() + ".<br/> Country: " + tab.Rows[0]["Country"].ToString() + ".</br>Phone Number: " + tab.Rows[0]["ContactNo"].ToString() + "</span></a>";
                        TableCell C23 = new TableCell();
                        C23.Text = tab_order.Rows[cnt]["Transaction_Date"].ToString();

                        TableCell C24 = new TableCell();
                        C24.Text = "<font color='red'>" + tab_order.Rows[cnt]["Status"].ToString() + "</font>";
                                                                       
                        TableRow R2 = new TableRow();
                        R2.Controls.Add(C21);
                        R2.Controls.Add(Ch11);
                        R2.Controls.Add(C22);
                        R2.Controls.Add(Ch12);
                        R2.Controls.Add(C23);
                        R2.Controls.Add(Ch13);
                        R2.Controls.Add(C24);
                        TableOr.Controls.Add(R2);
                    }
                }
            }
            else
            {
                PnlOrBtns.Visible = false;
                TableRow r3 = new TableRow();
                TableCell cellno = new TableCell();
                cellno.HorizontalAlign = HorizontalAlign.Center;
                cellno.ForeColor = System.Drawing.Color.Red;
                cellno.Font.Size = 10;
                cellno.ColumnSpan = 12;
                cellno.Text = "No Customer Transactions Found";
                r3.Controls.Add(cellno);
                TableOr.Controls.Add(r3);
            }
        }

        private void view_Status()
        {
            TableOr.Rows.Clear();
            TableCell Ch1 = new TableCell();
            Ch1.Text = "|";
            TableCell Ch2 = new TableCell();
            Ch2.Text = "|";
            TableCell Ch3 = new TableCell();
            Ch3.Text = "|";
            Ch4 = new TableCell();
            Ch4.Text = "|";
            Ch5 = new TableCell();
            Ch5.Text = "|";
            Ch6 = new TableCell();
            Ch6.Text = "|";
            Ch7 = new TableCell();
            Ch7.Text = "|";
            TableCell h1 = new TableCell();
            h1.Text = "Transaction ID";
            TableCell h2 = new TableCell();
            h2.Text = "Customer ID";
            TableCell h3 = new TableCell();
            h3.Text = "Transaction Date";
            h4 = new TableCell();
            h4.Text = "Dispatched_Date";
            TableCell h5 = new TableCell();
            h5.Text = "View_Details";
            h6 = new TableCell();
            h6.Text = "Dispatch";

            TableCell h7 = new TableCell();
            h7.Text = "Delete";

            TableRow R1 = new TableRow();
            R1.Font.Size = 10;
            R1.Font.Bold = true;
            R1.Controls.Add(h1);
            R1.Controls.Add(Ch1);
            R1.Controls.Add(h2);
            R1.Controls.Add(Ch2);
            R1.Controls.Add(h3);
            R1.Controls.Add(Ch3);
            R1.Controls.Add(h4);
            R1.Controls.Add(Ch4);
            R1.Controls.Add(h5);
            R1.Controls.Add(Ch5);
            R1.Controls.Add(h6);
            R1.Controls.Add(Ch6);
            //R1.Controls.Add(h8);
            R1.Controls.Add(Ch7);
            R1.Controls.Add(h7);

            TableOr.Controls.Add(R1);
            TableCell Chr = new TableCell();
            Chr.ColumnSpan = 16;
            Chr.Text = "<hr/>";
            TableRow Rhr = new TableRow();
            Rhr.Controls.Add(Chr);
            TableOr.Controls.Add(Rhr);
            tab_order = obj.GetTransactionsBasedonStatus(DDListO_Status.SelectedValue.ToString());

            if (tab_order.Rows.Count > 0)
            {
                for (int cnt = i; cnt < j; cnt++)
                {
                    if (cnt < tab_order.Rows.Count)
                    {
                        string msg;
                        PnlOrBtns.Visible = true;
                        if (j < tab_order.Rows.Count)
                        {
                            msg = string.Format("{0} - {1} Records out of {2}", i, j, tab_order.Rows.Count);

                        }
                        else
                        {
                            msg = string.Format("{0} - {1} Records out of {2}", i, tab_order.Rows.Count, tab_order.Rows.Count);
                        }
                        LblMsg_order.Text = msg;
                        TableCell Ch11 = new TableCell();
                        Ch11.Text = "|";
                        TableCell Ch12 = new TableCell();
                        Ch12.Text = "|";
                        TableCell Ch13 = new TableCell();
                        Ch13.Text = "|";
                        Ch14 = new TableCell();
                        Ch14.Text = "|";
                        Ch15 = new TableCell();
                        Ch15.Text = "|";
                        Ch16 = new TableCell();
                        Ch16.Text = "|";
                        Ch17 = new TableCell();
                        Ch17.Text = "|";
                        TableCell C21 = new TableCell();
                        C21.Text = tab_order.Rows[cnt]["Transaction_ID"].ToString();
                        TableCell C22 = new TableCell();
                        C22.Width = 200;
                        C22.HorizontalAlign = HorizontalAlign.Center;
                        DataTable tab = vobj.GetCustomerDetails(tab_order.Rows[cnt]["Email_ID"].ToString());
                        C22.Text = "<a href='#'>" + tab.Rows[0]["Email_ID"].ToString() + "<span>Name: " + tab.Rows[0]["Name"].ToString() + ".<br/>Address : " + tab.Rows[0]["Address"].ToString() + ".<br/> Country: " + tab.Rows[0]["Country"].ToString() + ".</br>Phone Number: " + tab.Rows[0]["ContactNo"].ToString() + "</span></a>";
                        TableCell C23 = new TableCell();
                        C23.Text = tab_order.Rows[cnt]["Transaction_Date"].ToString();
                        C24 = new TableCell();
                        C24.Text = tab_order.Rows[cnt]["Dispatched_Date"].ToString();
                        TableCell C25 = new TableCell();
                        Button view = new Button();
                        view.ToolTip = "Click here to view the order detail";
                        view.Text = "ViewDetails";
                        view.ID = tab_order.Rows[cnt]["Transaction_ID"].ToString() + "_00" + cnt;
                        view.Click += new EventHandler(view_Click);
                        C25.Controls.Add(view);

                        C26 = new TableCell();
                        Button dispatch = new Button();
                        dispatch.ToolTip = "Click here to dispatch the order";
                        dispatch.Text = "Dispatch";
                        dispatch.OnClientClick = "return confirm('Are you sure do you want to dispatch the order')";
                        dispatch.ID = tab_order.Rows[cnt]["Transaction_ID"].ToString() + "_01" + cnt;
                        dispatch.Click += new EventHandler(dispatch_Click);
                        C26.Controls.Add(dispatch);

                        celldel = new TableCell();
                        Button btndel = new Button();
                        btndel.Text = "Delete";
                        btndel.OnClientClick = "return confirm('Are you sure want to delete?')";
                        btndel.ID = "Delete~" + tab_order.Rows[cnt]["Transaction_ID"].ToString();
                        btndel.Click += new EventHandler(btndel_Click);
                        celldel.Controls.Add(btndel);
                                                
                        TableRow R2 = new TableRow();
                        R2.Controls.Add(C21);
                        R2.Controls.Add(Ch11);
                        R2.Controls.Add(C22);
                        R2.Controls.Add(Ch12);
                        R2.Controls.Add(C23);
                        R2.Controls.Add(Ch13);
                        R2.Controls.Add(C24);
                        R2.Controls.Add(Ch14);
                        R2.Controls.Add(C25);
                        R2.Controls.Add(Ch15);
                        R2.Controls.Add(C26);
                        R2.Controls.Add(Ch16);
                        //R2.Controls.Add(C28);
                        R2.Controls.Add(Ch17);
                        R2.Controls.Add(celldel);
                        //R2.Controls.Add(C27);
                        TableOr.Controls.Add(R2);

                    }
                    if (DDListO_Status.SelectedIndex == 1)
                    {
                        //Lbl_msg.Visible = true;
                        h4.Visible = false;
                        Ch4.Visible = false;
                        Ch5.Visible = true;
                        h6.Visible = true;
                        Ch6.Visible = true;
                        //h7.Visible = true;
                        C24.Visible = false;
                        Ch14.Visible = false;
                        Ch15.Visible = true;
                        C26.Visible = true;
                        Ch16.Visible = true;
                        //C27.Visible = true;
                        //h8.Visible = false;
                        Ch7.Visible = false;
                        Ch17.Visible = false;
                        //C28.Visible = false;
                    }
                    else
                    {
                        //Lbl_msg.Visible = false;
                        h4.Visible = true;
                        Ch4.Visible = true;
                        Ch5.Visible = false;
                        h6.Visible = false;
                        Ch6.Visible = false;
                        //h7.Visible = false;
                        C24.Visible = true;
                        Ch14.Visible = true;
                        Ch15.Visible = false;
                        C26.Visible = false;
                        Ch16.Visible = false;
                        //C27.Visible = false;
                        //h8.Visible = true;
                        Ch7.Visible = true;
                        Ch17.Visible = true;
                        //C28.Visible = true;
                    }
                }

            }
            else
            {
                PnlOrBtns.Visible = false;
                TableRow r3 = new TableRow();
                TableCell cellno = new TableCell();
                cellno.HorizontalAlign = HorizontalAlign.Center;
                cellno.ForeColor = System.Drawing.Color.Red;
                cellno.Font.Size = 10;
                cellno.ColumnSpan = 12;
                cellno.Text = "No Records Found";
                r3.Controls.Add(cellno);
                TableOr.Controls.Add(r3);
            }
        }

        void btndel_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string[] s = btn.ID.Split('~');

            obj.DeleteTransactionItems(int.Parse(s[1].ToString()));

            obj.DeleteTransaction(int.Parse(s[1].ToString()));
            ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('Transaction Deleted Successfully')</script>");

            DDListO_Status_SelectedIndexChanged(sender, e);
        }
                
        void dispatch_Click(object sender, EventArgs e)
        {
            Button d = (Button)sender;
            string[] id = d.ID.ToString().Split('_');

            try
            {
                obj.UpdateCustomerTransaction(DateTime.Now.ToString(), int.Parse(id[0].ToString()));
                DDListO_Status_SelectedIndexChanged(sender, e);
                ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('Items Dispatched Successfully')</script>");
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Server Error!')</script>");
            }


        }

        void view_Click(object sender, EventArgs e)
        {
            Button link = (Button)sender;
            string[] id = link.ID.ToString().Split('_');
            view_details(int.Parse(id[0].ToString()));
            MultiView1.SetActiveView(View2);

        }

        private void view_details(int id)
        {
            TableOrD.Rows.Clear();
            TableRow r1 = new TableRow();
            r1.Font.Size = 11;
            r1.Font.Bold = true;
            Label l11 = new Label();
            l11.Text = "|";
            Label l12 = new Label();
            l12.Text = "|";
            Label l13 = new Label();
            l13.Text = "|";
            Label l14 = new Label();
            l14.Text = "|";

            TableCell Ch1 = new TableCell();
            Ch1.Controls.Add(l11);
            TableCell Ch2 = new TableCell();
            Ch2.Controls.Add(l12);
            TableCell Ch3 = new TableCell();
            Ch3.Controls.Add(l13);
            TableCell Ch4 = new TableCell();
            Ch4.Controls.Add(l14);

            TableCell h1 = new TableCell();
            h1.Text = "Image";
            TableCell h2 = new TableCell();
            h2.Width = 100;
            h2.Text = "Quantity";
            TableCell h3 = new TableCell();
            h3.Width = 100;
            h3.Text = "Total Amount";
            TableCell h4 = new TableCell();
            h4.Width = 100;
            h4.Text = "Category";
            TableCell h5 = new TableCell();
            h5.Width = 100;
            h5.Text = "Item";
            r1.Controls.Add(h4);
            r1.Controls.Add(Ch1);
            r1.Controls.Add(h1);
            r1.Controls.Add(Ch2);
            r1.Controls.Add(h5);
            r1.Controls.Add(Ch4);
            r1.Controls.Add(h2);
            r1.Controls.Add(Ch3);
            r1.Controls.Add(h3);

            TableOrD.Controls.Add(r1);
            TableCell Chr = new TableCell();
            Chr.ColumnSpan = 12;
            Chr.Text = "<hr/>";
            TableRow Rhr = new TableRow();
            Rhr.Controls.Add(Chr);
            TableOrD.Controls.Add(Rhr);
            DataTable details = new DataTable();
            details = obj.GetTransactionDetails_ID(id);

            if (details.Rows.Count > 0)
            {
                double total = 0.0;

                for (int i = 0; i < details.Rows.Count; i++)
                {

                    Label l21 = new Label();
                    l21.Text = "|";
                    Label l22 = new Label();
                    l22.Text = "|";
                    Label l23 = new Label();
                    l23.Text = "|";
                    Label l24 = new Label();
                    l24.Text = "|";


                    TableCell Ch11 = new TableCell();
                    Ch11.Controls.Add(l21);
                    TableCell Ch12 = new TableCell();
                    Ch12.Controls.Add(l22);
                    TableCell Ch13 = new TableCell();
                    Ch13.Controls.Add(l23);
                    TableCell Ch14 = new TableCell();
                    Ch14.Controls.Add(l24);
                    DataTable tab = vobj.GetItemDetails(int.Parse(details.Rows[i]["Item_ID"].ToString()));
                    TableCell c26 = new TableCell();
                    DataTable cat = vobj.GetSubCategoryDetails(int.Parse(tab.Rows[0]["SubCategory_ID"].ToString()));
                    c26.Text = cat.Rows[0]["SubCategory_Name"].ToString();
                    TableCell c24 = new TableCell();
                    c24.Text = "<img src='" + tab.Rows[0]["Item_Image"].ToString() + "'width=25px height=30px border=0 />";
                    TableCell c21 = new TableCell();
                    c21.Text = "<a href='#'>" + tab.Rows[0]["Item_Name"].ToString() + "<span>Item Name: " + tab.Rows[0]["Item_Name"].ToString() + ".</br>Price : " + tab.Rows[0]["Item_Cost"].ToString() + ".<br/>Quantity Available: " + tab.Rows[0]["Quantity"].ToString() + "</span></a>";
                    Label l2 = new Label();
                    l2.Width = 100;
                    l2.Font.Size = 10;
                    l2.Text = details.Rows[i]["Quantity"].ToString();
                    TableCell c22 = new TableCell();
                    c22.Controls.Add(l2);
                    Label l3 = new Label();
                    l3.Width = 100;
                    l3.Font.Size = 10;
                    total += int.Parse(details.Rows[i]["Quantity"].ToString()) * double.Parse(tab.Rows[0]["Item_Cost"].ToString());
                    double t = int.Parse(details.Rows[i]["Quantity"].ToString()) * double.Parse(tab.Rows[0]["Item_Cost"].ToString());
                    l3.Text = t.ToString();
                    TableCell c23 = new TableCell();
                    c23.Controls.Add(l3);

                    TableRow r2 = new TableRow();
                    r2.Controls.Add(c26);
                    r2.Controls.Add(Ch13);
                    r2.Controls.Add(c24);

                    r2.Controls.Add(Ch11);
                    r2.Controls.Add(c21);
                    r2.Controls.Add(Ch14);
                    r2.Controls.Add(c22);
                    r2.Controls.Add(Ch12);
                    r2.Controls.Add(c23);
                    TableOrD.Controls.Add(r2);

                }
                TableCell c51 = new TableCell();
                c51.Text = "<hr>";
                c51.ColumnSpan = 10;
                TableRow r5 = new TableRow();
                r5.Controls.Add(c51);
                TableOrD.Controls.Add(r5);
                TableCell c31 = new TableCell();
                c31.ColumnSpan = 7;
                c31.HorizontalAlign = HorizontalAlign.Right;
                c31.Text = "<b>Shipping Cost</b>";
                double Scost;
                DataTable user = obj.GetTransactionDetails(id);
                DataTable tab_mem = vobj.GetCustomerDetails(user.Rows[0]["Email_ID"].ToString());
                if (tab_mem.Rows[0]["Country"].ToString().ToLower() == "india")
                    Scost = 50.00;
                else
                    Scost = 500.00;
                total += Scost;
                TableCell C32 = new TableCell();
                C32.Text = "|";
                TableCell c33 = new TableCell();
                c33.Text = Scost.ToString();
                TableRow r3 = new TableRow();
                r3.Controls.Add(c31);
                r3.Controls.Add(C32);
                r3.Controls.Add(c33);
                TableOrD.Controls.Add(r3);
                TableCell c41 = new TableCell();
                c41.ColumnSpan = 7;
                c41.HorizontalAlign = HorizontalAlign.Right;
                c41.Text = "<b>Total</b>";
                TableCell C42 = new TableCell();
                C42.Text = "|";
                TableCell c43 = new TableCell();
                c43.Text = total.ToString();
                TableRow r4 = new TableRow();
                r4.Controls.Add(c41);
                r4.Controls.Add(C42);
                r4.Controls.Add(c43);
                TableOrD.Controls.Add(r4);
                TableCell c61 = new TableCell();
                c61.Text = "<br/><br/><b><font color='#FF9900'> </font></b><br/><br/>";
                c61.ColumnSpan = 8;
                TableRow r6 = new TableRow();
                r6.Font.Size = 12;
                r6.Controls.Add(c61);
                TableOrD.Controls.Add(r6);
                TableCell c81 = new TableCell();
                c81.Text = "<font color='#FF9900'></font>";
                c81.ColumnSpan = 8;
                TableRow r8 = new TableRow();
                r8.Font.Size = 11;
                r8.Controls.Add(c81);
                //TableOrD.Controls.Add(r8);
                //Member.Member_Tasks mobj = new Publisher.Member.Member_Tasks();
                //DataTable pay = mobj.ViewPayment(id);
                //if (pay.Rows.Count > 0)
                //{
                //    TableRow r7 = new TableRow();

                //    TableCell c71 = new TableCell();
                //    c71.ColumnSpan = 8;
                //    c71.Text = "<b>Rs: " + total.ToString() + "/- </b>" + "</b> was Paid on <b>" + pay.Rows[0]["PaidDate"].ToString() + "</b>";
                //    r7.Controls.Add(c71);
                //    TableOrD.Controls.Add(r7);
                //}



            }
            else
            {
                TableRow r3 = new TableRow();
                TableCell cellno = new TableCell();
                cellno.HorizontalAlign = HorizontalAlign.Center;
                cellno.ForeColor = System.Drawing.Color.Red;
                cellno.Font.Size = 10;
                cellno.ColumnSpan = 12;
                cellno.Text = "No Records Found";
                r3.Controls.Add(cellno);
                TableOrD.Controls.Add(r3);
            }
        }

        protected void LBtnOPrev_Click(object sender, EventArgs e)
        {
            i = i - 15;
            j = j - 15;
            if (i >= 0)
            {
                TableOr.Rows.Clear();
                view_orderdetails();
            }
            else
            {
                i = 0;
                j = 15;

            }
        }

        protected void LBtnNext_Click(object sender, EventArgs e)
        {
            i = j;
            j = j + 15;
            if (i < tab_order.Rows.Count)
            {
                TableOr.Rows.Clear();
                view_orderdetails();

            }
            else
            {
                i = i - 15;
                j = j - 15;
                ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('No more records found')</script>");
            }

        }

        //protected void BtnSubmit_Click(object sender, EventArgs e)
        //{
        //    DataTable tab2 = vobj.GetUserDetails_Id(Lbl_Uid.Text);
        //    try
        //    {
        //        Emails.MailSender.SendEmail("ramya.ra87@gmail.com", "ramya1987", tab2.Rows[0]["EmailId"].ToString(), "Subject", Txt_Msg.Text, "");
        //        Txt_Msg.Text = "";
        //        MultiView1.SetActiveView(View1);
        //        ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('Reply has been send successfully')</script>");
        //    }
        //    catch
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('Failed send reply')</script>");
        //    }
        //}

        protected void LBtn_Back_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View1);
        }

    }
}
