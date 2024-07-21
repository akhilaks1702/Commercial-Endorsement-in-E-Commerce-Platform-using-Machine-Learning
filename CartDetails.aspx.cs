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
    public partial class CartDetails : System.Web.UI.Page
    {
        DataTable tab = null;
        Visitor obj = new Visitor();
        static string status = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Visitor"] == null)
                {
                    Panelca2.Visible = false;
                }
                else
                {
                    ViewCartDetails();
                }
            }
            catch
            {

            }
        }

        private void ViewCartDetails()
        {
            Label1.Visible = false;
            Table1.Rows.Clear();

            tab = obj.GetCartDetails(Session["Visitor"].ToString());
            if (tab.Rows.Count > 0)
            {
                Panelca2.Visible = true;
                TableCell C11 = new TableCell();
                C11.Text = "Item";
                TableCell C11a = new TableCell();
                C11a.Text = "|";
                TableCell C12 = new TableCell();
                C12.Text = "Name";
                TableCell C12a = new TableCell();
                C12a.Text = "|";
                TableCell C13 = new TableCell();
                C13.Text = "Price";
                TableCell C13a = new TableCell();
                C13a.Text = "|";
                TableCell C14 = new TableCell();
                C14.Text = "Qty";
                TableCell C14a = new TableCell();
                C14a.Text = "|";
                TableCell C15 = new TableCell();
                C15.Text = "Update";
                TableCell C15a = new TableCell();
                C15a.Text = "|";
                TableCell C16 = new TableCell();
                C16.Text = "TotalPrice";
                TableCell C16a = new TableCell();
                C16a.Text = "|";
                TableCell C17 = new TableCell();
                C17.Text = "Delete";
                TableCell C17a = new TableCell();
                C17a.Text = "|";
                TableRow R1 = new TableRow();
                R1.Font.Size = 11;
                R1.Font.Bold = true;
                R1.Controls.Add(C11);
                //R1.Controls.Add(C11a);
                R1.Controls.Add(C12);
                //R1.Controls.Add(C12a);
                R1.Controls.Add(C13);
                //R1.Controls.Add(C13a);
                R1.Controls.Add(C14);
                //R1.Controls.Add(C14a);
                //R1.Controls.Add(C15);
                //R1.Controls.Add(C15a);
                R1.Controls.Add(C16);
                //R1.Controls.Add(C16a);
                R1.Controls.Add(C17);
                //R1.Controls.Add(C17a);
                R1.HorizontalAlign = HorizontalAlign.Center;
                Table1.Controls.Add(R1);
                double TCost = 0;
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    DataTable book = obj.GetItemDetails(int.Parse(tab.Rows[i]["Item_ID"].ToString()));
                    TableCell C21 = new TableCell();
                    C21.Text = "<center><img src='" + book.Rows[0]["Item_Image"].ToString() + "'width=25px height=35px border=0 /></center>";
                    TableCell C21a = new TableCell();
                    C21a.Text = "|";
                    TableCell C22 = new TableCell();
                    C22.Text = book.Rows[0]["Item_name"].ToString();
                    TableCell C22a = new TableCell();
                    C22a.Text = "|";
                    TableCell C23 = new TableCell();
                    C23.Text = book.Rows[0]["Item_Cost"].ToString();
                    TableCell C23a = new TableCell();
                    C23a.Text = "|";
                    TableCell C24 = new TableCell();
                    TextBox qt = new TextBox();
                    qt.Width = 30;
                    qt.Text = tab.Rows[i]["Quantity"].ToString();
                    qt.ID = tab.Rows[i]["Cart_ID"].ToString() + "_" + tab.Rows[i]["Item_ID"].ToString() + "_" + i;
                    LblQuan.Text = tab.Rows[i]["Quantity"].ToString();
                    qt.TextChanged += new EventHandler(qt_TextChanged);
                    
                    C24.Controls.Add(qt);

                    TableCell C24a = new TableCell();
                    C24a.Text = "|";
                    TableCell C25 = new TableCell();
                    LinkButton update = new LinkButton();
                    update.Text = "Update";
                    update.ToolTip = "Click here the update the quantity";
                    update.OnClientClick = "return confirm('Are you sure do you want to update the quantity')";
                    update.ID = tab.Rows[i]["Cart_ID"].ToString() + "_00" + i;
                    update.Click += new EventHandler(update_Click);
                    C25.Controls.Add(update);

                    TableCell C25a = new TableCell();
                    C25a.Text = "|";
                    TableCell C26 = new TableCell();
                    double total = int.Parse(tab.Rows[i]["Quantity"].ToString()) * double.Parse(book.Rows[0]["Item_Cost"].ToString());
                    TCost += total;
                    C26.Text = total.ToString();
                    TableCell C26a = new TableCell();
                    C26a.Text = "|";
                    TableCell C27 = new TableCell();
                    ImageButton del = new ImageButton();
                    del.Width = 15;
                    del.Height = 15;
                    del.ImageUrl = "../images/deletebtn.jpg";
                    del.ToolTip = "Click here to Delete the item from the cart";
                    del.ID = tab.Rows[i]["Cart_ID"].ToString() + "_001" + i;
                    del.OnClientClick = "return confirm('Are you sure do you want to Delete')";
                    del.Click += new ImageClickEventHandler(del_Click);
                    C27.Controls.Add(del);
                    TableCell C27a = new TableCell();
                    C27a.Text = "|";
                    TableRow R2 = new TableRow();
                    R2.Controls.Add(C21);
                    //R2.Controls.Add(C21a);
                    R2.Controls.Add(C22);
                    //R2.Controls.Add(C22a);
                    R2.Controls.Add(C23);
                    //R2.Controls.Add(C23a);
                    R2.Controls.Add(C24);
                    //R2.Controls.Add(C24a);
                    //R2.Controls.Add(C25);
                    //R2.Controls.Add(C25a);
                    R2.Controls.Add(C26);
                    //R2.Controls.Add(C26a);
                    R2.Controls.Add(C27);
                    //R2.Controls.Add(C27a);
                    Table1.Controls.Add(R2);


                }
                TableCell c51 = new TableCell();
                c51.Text = "<hr>";
                c51.ColumnSpan = 12;
                TableRow r5 = new TableRow();
                r5.Controls.Add(c51);
                //Table1.Controls.Add(r5);

                TableCell c41 = new TableCell();
                c41.ColumnSpan = 4;
                c41.HorizontalAlign = HorizontalAlign.Right;
                c41.Text = "<b>Total</b>";
                TableCell C42 = new TableCell();
                C42.Text = "|";
                TableCell c43 = new TableCell();
                c43.ColumnSpan = 2;
                c43.Text = TCost.ToString();
                TableRow r4 = new TableRow();
                r4.Controls.Add(c41);
                //r4.Controls.Add(C42);
                r4.Controls.Add(c43);
                Table1.Controls.Add(r4);

                //Session["Visitor"] = tab.Rows[0]["UserID"].ToString();
            }
            else
            {
                Panelca2.Visible = false;
                Table1.Rows.Clear();
                Label1.Text = "Your Shopping Cart is Empty";
                Label1.Visible = true;
                //Session["Visitor"] = null;

            }

        }

        void qt_TextChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TextBox t = (TextBox)sender;
            LblQuan.Text = t.Text;

            string[] id = t.ID.ToString().Split('_');
            LabelId.Text = id[0];

            DataTable tab50 = new DataTable();
            tab50 = obj.GetItemDetails(int.Parse(id[1].ToString()));

            int cnt = int.Parse(t.Text.ToString());


            int totalquantity = 0;

            totalquantity = int.Parse(tab50.Rows[0]["Quantity"].ToString());

            status = null;

            if (cnt <= totalquantity)
            {
                status = "Avail";
                if (obj.UpdateCartDetails(int.Parse(LblQuan.Text), int.Parse(LabelId.Text)))
                    ViewCartDetails();
            }
            else
            {
                status = "Exceeds";

            }
        }
               
        void del_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton delete = (ImageButton)sender;
            string[] id = delete.ID.ToString().Split('_');
            if (obj.DeleteCartDetails(int.Parse(id[0])))
                ViewCartDetails();

        }

        void update_Click(object sender, EventArgs e)
        {

        }

        protected void BtnShopMore_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewItems.aspx");
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (status == null)
                {
                    Session["Cart"] = "1";
                    Response.Redirect("Login_page.aspx");
                }
                else if (status.Equals("Exceeds"))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Availibility exceeds the Stock')</script>");
                }
                else
                {
                    Session["Cart"] = "1";
                    Response.Redirect("Login_page.aspx");
                }
                               
            }
            catch
            {

            }
            
        }


    }
}
