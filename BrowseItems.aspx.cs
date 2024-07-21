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
    public partial class BrowseItems : System.Web.UI.Page
    {
        DataTable tab = new DataTable();
        Visitor obj = new Visitor();
        TableCell[] c;
        static int i = 0;
        static int j = 15;
        static string records = "All";
        static int Category_ID = 0;
        static int SubCategID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                records = "All";
                i = 0;
                j = 15;

                LoadItemCategories();
            }

            try
            {
                if (records.Equals("All"))

                    GetAllItems();

                else if (records.Equals("Categorywise"))

                    GetCategoryItems(Category_ID);

                else if (records.Equals("SubCategory"))

                    GetItems_Categ_SubCategID(Category_ID, SubCategID);
            }
            catch
            {

            }

        }

        private void LoadItemCategories()
        {
            tab.Rows.Clear();
            tab = obj.GetAllCategories();

            DropDownList1.Items.Clear();

            if (tab.Rows.Count > 0)
            {
                DropDownList1.DataSource = tab.DefaultView;

                DropDownList1.DataTextField = "Category_Name";
                DropDownList1.DataValueField = "Category_ID";

                DropDownList1.DataBind();

                DropDownList1.Items.Insert(0, "- All -");


            }
            else
            {
                DropDownList1.DataSource = null;
                DropDownList1.DataBind();

                DropDownList2.DataSource = null;
                DropDownList2.DataBind();

                DropDownList1.Items.Insert(0, "- All -");
                DropDownList2.Items.Insert(0, "- All -");

            }
        }

        private void LoadItemCategorySubCategories(int categID)
        {
            tab.Rows.Clear();
            tab = obj.GetCategorySubCategories(categID);

            if (tab.Rows.Count > 0)
            {
                DropDownList2.Items.Clear();

                DropDownList2.DataSource = tab.DefaultView;

                DropDownList2.DataTextField = "SubCategory_Name";
                DropDownList2.DataValueField = "SubCategory_ID";

                DropDownList2.DataBind();

                DropDownList2.Items.Insert(0, "- All -");


            }
            else
            {
                DropDownList2.Items.Clear();

                DropDownList2.DataSource = null;
                DropDownList2.DataBind();

                DropDownList2.Items.Insert(0, "- All -");
            }


        }

        private void GetAllItems()
        {
            tab.Rows.Clear();
            tab = obj.GetAllItems();

            if (tab.Rows.Count > 0)
            {
                for (int cnt = i; cnt < j; cnt += 3)
                {
                    if (cnt < tab.Rows.Count)
                    {
                        //PanelBtns.Visible = true;
                        string msg;
                        if (tab.Rows.Count > j)
                        {
                            msg = string.Format("{0} - {1} Records out of {2}", i + 1, j, tab.Rows.Count);
                        }
                        else
                        {
                            msg = string.Format("{0} - {1} Records out of {2}", i + 1, tab.Rows.Count, tab.Rows.Count);
                        }
                        LblMsg.Text = msg;
                        int count = 0;
                        int k = 0;
                        c = new TableCell[3];
                        try
                        {
                            while (count < 3)
                            {
                                c[count] = new TableCell();
                                if (k + cnt < tab.Rows.Count)
                                {
                                    Table t = new Table();
                                    t.Width = 250;
                                    TableCell c1 = new TableCell();
                                    c1.ColumnSpan = 3;
                                    c1.Height = 30;
                                    c1.Text = "<b><font color='#006699' size='2px'>" + tab.Rows[k + cnt]["Item_Name"].ToString() + "</font></b>";
                                    TableRow R1 = new TableRow();
                                    R1.Controls.Add(c1);
                                    t.Controls.Add(R1);
                                    TableCell c2 = new TableCell();

                                    c2.RowSpan = 3;
                                    c2.Width = 70;
                                    c2.Text = "<img src='" + tab.Rows[k + cnt]["Item_Image"].ToString() + "' width=75px height=100px border=0 />";
                                    TableCell c3 = new TableCell();
                                    c3.HorizontalAlign = HorizontalAlign.Left;
                                    c3.ColumnSpan = 2;

                                    c3.Text = "<b><font color='#993366' size='2px'> Rs. " + tab.Rows[k + cnt]["Item_Cost"].ToString() + " /- </font></b>";
                                    ImageButton imgA = new ImageButton();
                                    imgA.Width = 50;
                                    imgA.Height = 50;
                                    imgA.ImageUrl = "../images/cart6.jpg";

                                    imgA.ToolTip = "Click to add the Item into the cart";
                                    imgA.ID = tab.Rows[k + cnt]["Item_ID"].ToString() + "_10" + k + cnt;
                                    imgA.Click += new ImageClickEventHandler(imgA_Click);
                                    TableCell txt = new TableCell();
                                    txt.HorizontalAlign = HorizontalAlign.Left;

                                    int totalquantity = 0;
                                    //totalquantity = obj.ItemSaledQuantity(int.Parse(tab.Rows[k + cnt]["Item_ID"].ToString()));

                                    totalquantity = int.Parse(tab.Rows[k + cnt]["Quantity"].ToString());

                                    LinkButton li = new LinkButton();
                                    if (totalquantity > 0)
                                    {
                                        li.Enabled = true;
                                        imgA.Enabled = true;
                                    }
                                    else
                                    {
                                        li.Enabled = false;
                                        imgA.Enabled = false;
                                    }

                                    li.Font.Underline = false;
                                    li.Text = "<font size='2px'>Add To Cart</font>";
                                    li.ToolTip = "Click here to add to Cart";
                                    li.ID = tab.Rows[k + cnt]["Item_ID"].ToString() + "_101" + k + cnt;
                                    li.Click += new EventHandler(li_Click);
                                    txt.Controls.Add(li);

                                    TableCell cartimg = new TableCell();
                                    cartimg.Width = 25;
                                    cartimg.HorizontalAlign = HorizontalAlign.Left;
                                    cartimg.Controls.Add(imgA);


                                    LinkButton l = new LinkButton();
                                    l.Font.Underline = false;
                                    l.Text = "<font size='1px' color='#006699'>More Details</font>";
                                    l.ToolTip = "Click here to view the details";
                                    l.Font.Underline = true;
                                    l.ID = tab.Rows[k + cnt]["Item_ID"].ToString() + "_0" + k + cnt;
                                    l.Click += new EventHandler(l_Click);
                                    TableCell c5 = new TableCell();
                                    c5.ColumnSpan = 2;
                                    //c5.Width = 100;
                                    c5.HorizontalAlign = HorizontalAlign.Left;
                                    c5.Controls.Add(l);
                                    TableRow R2 = new TableRow();
                                    R2.Controls.Add(c2);
                                    R2.Controls.Add(c3);
                                    TableRow R3 = new TableRow();
                                    R3.Controls.Add(cartimg);
                                    R3.Controls.Add(txt);
                                    TableRow R4 = new TableRow();

                                    R4.Controls.Add(c5);

                                    t.Controls.Add(R2);
                                    t.Controls.Add(R3);
                                    t.Controls.Add(R4);
                                    c[count].Controls.Add(t);

                                }
                                else
                                {

                                }
                                k++;
                                count++;
                            }
                            TableRow row = new TableRow();
                            row.Controls.Add(c[0]);
                            row.Controls.Add(c[1]);
                            row.Controls.Add(c[2]);
                            Table1.Controls.Add(row);
                            TableCell cell = new TableCell();
                            cell.ColumnSpan = 3;
                            cell.Text = "<br/><br/>------------------------------------------------------------------------------------------------------------------------<br/>";
                            TableRow row1 = new TableRow();
                            row1.Controls.Add(cell);
                            Table1.Controls.Add(row1);

                            if (tab.Rows.Count > 15)
                            {
                                if (cnt < 15)
                                {
                                    ImageButton2.Visible = true;
                                    ImageButton1.Visible = false;
                                    ImageButton3.Visible = false;
                                }
                                else
                                {
                                    if (j >= tab.Rows.Count)
                                    {
                                        ImageButton3.Visible = true;
                                        ImageButton2.Visible = false;
                                        ImageButton1.Visible = true;
                                    }
                                    else
                                    {
                                        ImageButton3.Visible = false;
                                        ImageButton2.Visible = true;
                                        ImageButton1.Visible = true;
                                    }
                                }
                            }
                            else
                            {
                                ImageButton1.Visible = false;
                                ImageButton2.Visible = false;
                                ImageButton3.Visible = false;
                            }
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        if (tab.Rows.Count < 15)
                        {
                            ImageButton1.Visible = false;
                            ImageButton2.Visible = false;
                            ImageButton3.Visible = false;
                        }
                        else
                        {
                            ImageButton1.Visible = true;
                            ImageButton2.Visible = false;
                            ImageButton3.Visible = true;
                        }
                    }

                }
            }
            else
            {
                Table1.Rows.Clear();
                Table1.GridLines = GridLines.None;

                TableHeaderRow row = new TableHeaderRow();
                TableHeaderCell cell = new TableHeaderCell();
                cell.Font.Bold = true;
                cell.ForeColor = System.Drawing.Color.Red;
                cell.ColumnSpan = 5;
                cell.Text = "No Items Found";
                row.Controls.Add(cell);

                Table1.Controls.Add(row);

                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = false;
                LblMsg.Text = " ";
            }
        }

        private void GetCategoryItems(int categ_ID)
        {
            tab.Rows.Clear();
            tab = obj.GetCategoryItems(categ_ID);

            if (tab.Rows.Count > 0)
            {
                Table1.Rows.Clear();

                for (int cnt = i; cnt < j; cnt += 3)
                {
                    if (cnt < tab.Rows.Count)
                    {
                        //PanelBtns.Visible = true;
                        string msg;
                        if (tab.Rows.Count > j)
                        {
                            msg = string.Format("{0} - {1} Records out of {2}", i + 1, j, tab.Rows.Count);
                        }
                        else
                        {
                            msg = string.Format("{0} - {1} Records out of {2}", i + 1, tab.Rows.Count, tab.Rows.Count);
                        }
                        LblMsg.Text = msg;
                        int count = 0;
                        int k = 0;
                        c = new TableCell[3];
                        try
                        {
                            while (count < 3)
                            {
                                c[count] = new TableCell();
                                if (k + cnt < tab.Rows.Count)
                                {
                                    Table t = new Table();
                                    t.Width = 250;
                                    TableCell c1 = new TableCell();
                                    c1.ColumnSpan = 3;
                                    c1.Height = 30;
                                    c1.Text = "<b><font color='#006699' size='2px'>" + tab.Rows[k + cnt]["Item_Name"].ToString() + "</font></b>";
                                    TableRow R1 = new TableRow();
                                    R1.Controls.Add(c1);
                                    t.Controls.Add(R1);
                                    TableCell c2 = new TableCell();

                                    c2.RowSpan = 3;
                                    c2.Width = 70;
                                    c2.Text = "<img src='" + tab.Rows[k + cnt]["Item_Image"].ToString() + "' width=75px height=100px border=0 />";
                                    TableCell c3 = new TableCell();
                                    c3.HorizontalAlign = HorizontalAlign.Left;
                                    c3.ColumnSpan = 2;

                                    c3.Text = "<b><font color='#993366' size='2px'> Rs. " + tab.Rows[k + cnt]["Item_Cost"].ToString() + " /- </font></b>";
                                    ImageButton imgA1 = new ImageButton();
                                    imgA1.Width = 50;
                                    imgA1.Height = 50;
                                    imgA1.ImageUrl = "../images/cart6.jpg";

                                    imgA1.ToolTip = "Click to add the Item into the cart";
                                    imgA1.ID = tab.Rows[k + cnt]["Item_ID"].ToString() + "_10" + k + cnt;
                                    imgA1.Click += new ImageClickEventHandler(imgA1_Click);
                                    TableCell txt = new TableCell();
                                    txt.HorizontalAlign = HorizontalAlign.Left;
                                    int totalquantity = 0;
                                    //totalquantity = obj.ItemSaledQuantity(int.Parse(tab.Rows[k + cnt]["Item_ID"].ToString()));

                                    totalquantity = int.Parse(tab.Rows[k + cnt]["Quantity"].ToString());

                                    LinkButton li1 = new LinkButton();
                                    if (totalquantity > 0)
                                    {
                                        li1.Enabled = true;
                                        imgA1.Enabled = true;
                                    }
                                    else
                                    {
                                        li1.Enabled = false;
                                        imgA1.Enabled = false;
                                    }

                                    li1.Font.Underline = false;
                                    li1.Text = "<font size='2px'>Add To Cart</font>";
                                    li1.ToolTip = "Click here to add to Cart";
                                    li1.ID = tab.Rows[k + cnt]["Item_ID"].ToString() + "_101" + k + cnt;
                                    li1.Click += new EventHandler(li1_Click);
                                    txt.Controls.Add(li1);

                                    TableCell cartimg = new TableCell();
                                    cartimg.Width = 25;
                                    cartimg.HorizontalAlign = HorizontalAlign.Left;
                                    cartimg.Controls.Add(imgA1);


                                    LinkButton l1 = new LinkButton();
                                    l1.Font.Underline = false;
                                    l1.Text = "<font size='1px' color='#006699'>More Details</font>";
                                    l1.ToolTip = "Click here to view the details";
                                    l1.Font.Underline = true;
                                    l1.ID = tab.Rows[k + cnt]["Item_ID"].ToString() + "_0" + k + cnt;
                                    l1.Click += new EventHandler(l1_Click);
                                    TableCell c5 = new TableCell();
                                    c5.ColumnSpan = 2;
                                    //c5.Width = 100;
                                    c5.HorizontalAlign = HorizontalAlign.Left;
                                    c5.Controls.Add(l1);
                                    TableRow R2 = new TableRow();
                                    R2.Controls.Add(c2);
                                    R2.Controls.Add(c3);
                                    TableRow R3 = new TableRow();
                                    R3.Controls.Add(cartimg);
                                    R3.Controls.Add(txt);
                                    TableRow R4 = new TableRow();

                                    R4.Controls.Add(c5);

                                    t.Controls.Add(R2);
                                    t.Controls.Add(R3);
                                    t.Controls.Add(R4);
                                    c[count].Controls.Add(t);

                                }
                                else
                                {

                                }
                                k++;
                                count++;
                            }
                            TableRow row = new TableRow();
                            row.Controls.Add(c[0]);
                            row.Controls.Add(c[1]);
                            row.Controls.Add(c[2]);
                            Table1.Controls.Add(row);
                            TableCell cell = new TableCell();
                            cell.ColumnSpan = 3;
                            cell.Text = "<br/><br/>------------------------------------------------------------------------------------------------------------------------<br/>";
                            TableRow row1 = new TableRow();
                            row1.Controls.Add(cell);
                            Table1.Controls.Add(row1);

                            if (tab.Rows.Count > 15)
                            {
                                if (cnt < 15)
                                {
                                    ImageButton2.Visible = true;
                                    ImageButton1.Visible = false;
                                    ImageButton3.Visible = false;
                                }
                                else
                                {
                                    if (j >= tab.Rows.Count)
                                    {
                                        ImageButton3.Visible = true;
                                        ImageButton2.Visible = false;
                                        ImageButton1.Visible = true;
                                    }
                                    else
                                    {
                                        ImageButton3.Visible = false;
                                        ImageButton2.Visible = true;
                                        ImageButton1.Visible = true;
                                    }
                                }
                            }
                            else
                            {
                                ImageButton1.Visible = false;
                                ImageButton2.Visible = false;
                                ImageButton3.Visible = false;
                            }
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        if (tab.Rows.Count < 15)
                        {
                            ImageButton1.Visible = false;
                            ImageButton2.Visible = false;
                            ImageButton3.Visible = false;
                        }
                        else
                        {
                            ImageButton1.Visible = true;
                            ImageButton2.Visible = false;
                            ImageButton3.Visible = true;
                        }
                    }



                }
            }
            else
            {
                Table1.Rows.Clear();
                Table1.GridLines = GridLines.None;

                TableHeaderRow row = new TableHeaderRow();
                TableHeaderCell cell = new TableHeaderCell();
                cell.Font.Bold = true;
                cell.ForeColor = System.Drawing.Color.Red;
                cell.ColumnSpan = 5;
                cell.Text = "No Items Found";
                row.Controls.Add(cell);

                Table1.Controls.Add(row);

                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = false;
                LblMsg.Text = " ";
            }
        }

        void l1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            LinkButton link = (LinkButton)sender;
            string[] id = link.ID.ToString().Split('_');
            Session["Item_ID"] = id[0];
            Response.Redirect("MemberViewDetails.aspx");
        }

        void li1_Click(object sender, EventArgs e)
        {
            LinkButton Or = (LinkButton)sender;
            string[] id = Or.ID.ToString().Split('_');
            Session["Item_ID"] = id[0];

            if (obj.CountItem(int.Parse(id[0]), Session["Customer_ID"].ToString()))
            {
                if (obj.AddCartDetails(Session["Customer_ID"].ToString(), int.Parse(id[0]), 1))
                {
                    Response.Redirect("MemberCart.aspx");
                }
            }
            else
                Response.Redirect("MemberCart.aspx");
        }

        void imgA1_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton Or = (ImageButton)sender;
            string[] id = Or.ID.ToString().Split('_');
            Session["Item_ID"] = id[0];

            if (obj.CountItem(int.Parse(id[0]), Session["Customer_ID"].ToString()))
            {
                if (obj.AddCartDetails(Session["Customer_ID"].ToString(), int.Parse(id[0]), 1))
                {
                    Response.Redirect("MemberCart.aspx");
                }
            }
            else
                Response.Redirect("MemberCart.aspx");
        }

        private void GetItems_Categ_SubCategID(int catID, int subcateID)
        {
            tab.Rows.Clear();
            tab = obj.GetItems_Categ_SubCategID(catID, subcateID);

            if (tab.Rows.Count > 0)
            {
                Table1.Rows.Clear();


                for (int cnt = i; cnt < j; cnt += 3)
                {
                    if (cnt < tab.Rows.Count)
                    {
                        //PanelBtns.Visible = true;
                        string msg;
                        if (tab.Rows.Count > j)
                        {
                            msg = string.Format("{0} - {1} Records out of {2}", i + 1, j, tab.Rows.Count);
                        }
                        else
                        {
                            msg = string.Format("{0} - {1} Records out of {2}", i + 1, tab.Rows.Count, tab.Rows.Count);
                        }
                        LblMsg.Text = msg;
                        int count = 0;
                        int k = 0;
                        c = new TableCell[3];
                        try
                        {
                            while (count < 3)
                            {
                                c[count] = new TableCell();
                                if (k + cnt < tab.Rows.Count)
                                {
                                    Table t = new Table();
                                    t.Width = 250;
                                    TableCell c1 = new TableCell();
                                    c1.ColumnSpan = 3;
                                    c1.Height = 30;
                                    c1.Text = "<b><font color='#006699' size='2px'>" + tab.Rows[k + cnt]["Item_Name"].ToString() + "</font></b>";
                                    TableRow R1 = new TableRow();
                                    R1.Controls.Add(c1);
                                    t.Controls.Add(R1);
                                    TableCell c2 = new TableCell();

                                    c2.RowSpan = 3;
                                    c2.Width = 70;
                                    c2.Text = "<img src='" + tab.Rows[k + cnt]["Item_Image"].ToString() + "' width=75px height=100px border=0 />";
                                    TableCell c3 = new TableCell();
                                    c3.HorizontalAlign = HorizontalAlign.Left;
                                    c3.ColumnSpan = 2;

                                    c3.Text = "<b><font color='#993366' size='2px'> Rs. " + tab.Rows[k + cnt]["Item_Cost"].ToString() + " /- </font></b>";
                                    ImageButton imgA2 = new ImageButton();
                                    imgA2.Width = 50;
                                    imgA2.Height = 50;
                                    imgA2.ImageUrl = "../images/cart6.jpg";

                                    imgA2.ToolTip = "Click to add the Item into the cart";
                                    imgA2.ID = tab.Rows[k + cnt]["Item_ID"].ToString() + "_10" + k + cnt;
                                    imgA2.Click += new ImageClickEventHandler(imgA2_Click);
                                    TableCell txt = new TableCell();
                                    txt.HorizontalAlign = HorizontalAlign.Left;



                                    int totalquantity = 0;
                                    //totalquantity = obj.ItemSaledQuantity(int.Parse(tab.Rows[k + cnt]["Item_ID"].ToString()));

                                    totalquantity = int.Parse(tab.Rows[k + cnt]["Quantity"].ToString());

                                    LinkButton li2 = new LinkButton();
                                    if (totalquantity > 0)
                                    {
                                        li2.Enabled = true;
                                        imgA2.Enabled = true;
                                    }
                                    else
                                    {
                                        li2.Enabled = false;
                                        imgA2.Enabled = false;
                                    }

                                    li2.Font.Underline = false;
                                    li2.Text = "<font size='2px'>Add To Cart</font>";
                                    li2.ToolTip = "Click here to add to Cart";
                                    li2.ID = tab.Rows[k + cnt]["Item_ID"].ToString() + "_101" + k + cnt;
                                    li2.Click += new EventHandler(li2_Click);
                                    txt.Controls.Add(li2);

                                    TableCell cartimg = new TableCell();
                                    cartimg.Width = 25;
                                    cartimg.HorizontalAlign = HorizontalAlign.Left;
                                    cartimg.Controls.Add(imgA2);


                                    LinkButton l2 = new LinkButton();
                                    l2.Font.Underline = false;
                                    l2.Text = "<font size='1px' color='#006699'>More Details</font>";
                                    l2.ToolTip = "Click here to view the details";
                                    l2.Font.Underline = true;
                                    l2.ID = tab.Rows[k + cnt]["Item_ID"].ToString() + "_0" + k + cnt;
                                    l2.Click += new EventHandler(l2_Click);
                                    TableCell c5 = new TableCell();
                                    c5.ColumnSpan = 2;
                                    //c5.Width = 100;
                                    c5.HorizontalAlign = HorizontalAlign.Left;
                                    c5.Controls.Add(l2);
                                    TableRow R2 = new TableRow();
                                    R2.Controls.Add(c2);
                                    R2.Controls.Add(c3);
                                    TableRow R3 = new TableRow();
                                    R3.Controls.Add(cartimg);
                                    R3.Controls.Add(txt);
                                    TableRow R4 = new TableRow();

                                    R4.Controls.Add(c5);

                                    t.Controls.Add(R2);
                                    t.Controls.Add(R3);
                                    t.Controls.Add(R4);
                                    c[count].Controls.Add(t);

                                }
                                else
                                {

                                }
                                k++;
                                count++;
                            }
                            TableRow row = new TableRow();
                            row.Controls.Add(c[0]);
                            row.Controls.Add(c[1]);
                            row.Controls.Add(c[2]);
                            Table1.Controls.Add(row);
                            TableCell cell = new TableCell();
                            cell.ColumnSpan = 3;
                            cell.Text = "<br/><br/>------------------------------------------------------------------------------------------------------------------------<br/>";
                            TableRow row1 = new TableRow();
                            row1.Controls.Add(cell);
                            Table1.Controls.Add(row1);

                            if (tab.Rows.Count > 15)
                            {
                                if (cnt < 15)
                                {
                                    ImageButton2.Visible = true;
                                    ImageButton1.Visible = false;
                                    ImageButton3.Visible = false;
                                }
                                else
                                {
                                    if (j >= tab.Rows.Count)
                                    {
                                        ImageButton3.Visible = true;
                                        ImageButton2.Visible = false;
                                        ImageButton1.Visible = true;
                                    }
                                    else
                                    {
                                        ImageButton3.Visible = false;
                                        ImageButton2.Visible = true;
                                        ImageButton1.Visible = true;
                                    }
                                }
                            }
                            else
                            {
                                ImageButton1.Visible = false;
                                ImageButton2.Visible = false;
                                ImageButton3.Visible = false;
                            }
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        if (tab.Rows.Count < 15)
                        {
                            ImageButton1.Visible = false;
                            ImageButton2.Visible = false;
                            ImageButton3.Visible = false;
                        }
                        else
                        {
                            ImageButton1.Visible = true;
                            ImageButton2.Visible = false;
                            ImageButton3.Visible = true;
                        }
                    }




                }
            }
            else
            {
                Table1.Rows.Clear();
                Table1.GridLines = GridLines.None;

                TableHeaderRow row = new TableHeaderRow();
                TableHeaderCell cell = new TableHeaderCell();
                cell.Font.Bold = true;
                cell.ForeColor = System.Drawing.Color.Red;
                cell.ColumnSpan = 5;
                cell.Text = "No Items Found";
                row.Controls.Add(cell);

                Table1.Controls.Add(row);

                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = false;
                LblMsg.Text = " ";
            }
        }

        void l2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            LinkButton link = (LinkButton)sender;
            string[] id = link.ID.ToString().Split('_');
            Session["Item_ID"] = id[0];
            Response.Redirect("MemberViewDetails.aspx");
        }

        void li2_Click(object sender, EventArgs e)
        {
            LinkButton Or = (LinkButton)sender;
            string[] id = Or.ID.ToString().Split('_');
            Session["Item_ID"] = id[0];

            if (obj.CountItem(int.Parse(id[0]), Session["Customer_ID"].ToString()))
            {
                if (obj.AddCartDetails(Session["Customer_ID"].ToString(), int.Parse(id[0]), 1))
                {
                    Response.Redirect("MemberCart.aspx");
                }
            }
            else
                Response.Redirect("MemberCart.aspx");
        }

        void imgA2_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton Or = (ImageButton)sender;
            string[] id = Or.ID.ToString().Split('_');
            Session["Item_ID"] = id[0];

            if (obj.CountItem(int.Parse(id[0]), Session["Customer_ID"].ToString()))
            {
                if (obj.AddCartDetails(Session["Customer_ID"].ToString(), int.Parse(id[0]), 1))
                {
                    Response.Redirect("MemberCart.aspx");
                }
            }
            else
                Response.Redirect("MemberCart.aspx");
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DropDownList1.SelectedIndex > 0)
                {
                    i = 0; j = 15;
                    records = "Categorywise";
                    Category_ID = int.Parse(DropDownList1.SelectedValue.ToString());
                    LoadItemCategorySubCategories(int.Parse(DropDownList1.SelectedValue.ToString()));
                    GetCategoryItems(int.Parse(DropDownList1.SelectedValue.ToString()));

                }
                else
                {
                    GetAllItems();
                    DropDownList2.Items.Clear();
                    DropDownList2.Items.Insert(0, "- All -");
                }
            }
            catch
            {

            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (DropDownList1.SelectedIndex > 0)
                {
                    if (DropDownList2.SelectedIndex > 0)
                    {
                        i = 0; j = 15;
                        records = "SubCategory";
                        SubCategID = int.Parse(DropDownList2.SelectedValue.ToString());
                        Category_ID = int.Parse(DropDownList1.SelectedValue.ToString());
                        GetItems_Categ_SubCategID(int.Parse(DropDownList1.SelectedValue.ToString()), int.Parse(DropDownList2.SelectedValue.ToString()));
                    }
                    else
                    {
                        GetCategoryItems(int.Parse(DropDownList1.SelectedValue.ToString()));
                    }

                }
                else
                {
                    DropDownList2.Items.Clear();
                    DropDownList2.Items.Insert(0, "- All -");


                }
            }
            catch
            {

            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            i = i - 15;
            j = j - 15;

            if (i >= 0)
            {
                Table1.Rows.Clear();

                if (records.Equals("All"))

                    GetAllItems();

                else if (records.Equals("Categorywise"))

                    GetCategoryItems(Category_ID);

                else if (records.Equals("SubCategory"))

                    GetItems_Categ_SubCategID(Category_ID, SubCategID);


            }
            else
            {
                i = 0;
                j = 15;
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            i = j;
            j = j + 15;

            if (i < tab.Rows.Count)
            {
                Table1.Rows.Clear();

                if (records.Equals("All"))

                    GetAllItems();

                else if (records.Equals("Categorywise"))

                    GetCategoryItems(Category_ID);

                else if (records.Equals("SubCategory"))

                    GetItems_Categ_SubCategID(Category_ID, SubCategID);

            }
            else
            {
                i = i - 15;
                j = j - 15;

                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = true;

            }
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {

        }

        void li_Click(object sender, EventArgs e)
        {
            LinkButton Or = (LinkButton)sender;
            string[] id = Or.ID.ToString().Split('_');
            Session["Item_ID"] = id[0];

            if (obj.CountItem(int.Parse(id[0]), Session["Customer_ID"].ToString()))
            {
                if (obj.AddCartDetails(Session["Customer_ID"].ToString(), int.Parse(id[0]), 1))
                {
                    Response.Redirect("MemberCart.aspx");
                }
            }
            else
                Response.Redirect("MemberCart.aspx");
        }

        void imgA_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton Or = (ImageButton)sender;
            string[] id = Or.ID.ToString().Split('_');
            Session["Item_ID"] = id[0];

            if (obj.CountItem(int.Parse(id[0]), Session["Customer_ID"].ToString()))
            {
                if (obj.AddCartDetails(Session["Customer_ID"].ToString(), int.Parse(id[0]), 1))
                {
                    Response.Redirect("MemberCart.aspx");
                }
            }
            else
                Response.Redirect("MemberCart.aspx");
        }

        void l_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            string[] id = link.ID.ToString().Split('_');
            Session["Item_ID"] = id[0];
            Response.Redirect("MemberViewDetails.aspx");
        }

    }
}
