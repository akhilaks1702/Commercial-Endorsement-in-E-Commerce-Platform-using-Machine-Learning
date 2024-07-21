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
using System.Collections.Generic;

namespace DATAMINING_ASSOCIATIONRULE
{
    public partial class _EclatAlgorithm : System.Web.UI.Page
    {
        Visitor obj1 = new Visitor();
        DataTable tab = new DataTable();
        TableCell[] c;
        int z = 0;
        Member_Class memObj = new Member_Class();
        Admin_Class obj = new Admin_Class();
        DataTable tab1 = new DataTable();
        DataTable tab2 = new DataTable();
        Dictionary<string, string> DictionaryAllFrequentItems = new Dictionary<string, string>();
        Dictionary<string, string> _FrequentItems = new Dictionary<string, string>();
        List<ClassRules> StrongRules = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Customer_ID"] == null)
                {
                    Session.Abandon();
                    Response.Redirect("Home_page.aspx");
                }
                else
                {

                    GetDistinctItems();
                    GetDistinctTransactions();

                    string _time;

                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    EclatAlgorithm();
                    LoadOffers();

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    _time = elapsedMs.ToString();

                    lblTime.Text = string.Empty;
                    lblTime.ForeColor = System.Drawing.Color.Red;
                    lblTime.Font.Bold = true;
                    lblTime.Text = "Execution Time: " + _time + " milliseconds";

                    Session["E_Time"] = null;
                    Session["E_Time"] = _time;

                    GetAllItems();

                }
            }
            catch
            {
                Table4.Rows.Clear();
            }
        }

        private void LoadOffers()
        {
            DataTable tab = new DataTable();

            Admin_Class obj = new Admin_Class();

            tab.Rows.Clear();


            tab = obj.GetAllActiveAds();

            if (tab.Rows.Count > 0)
            {
                for (int cnt = 0; cnt < tab.Rows.Count; cnt++)
                {
                    int _slno = cnt + 1;
                    lblMsg.ForeColor = System.Drawing.Color.DarkGreen;
                    lblMsg.Font.Size = 14;
                    lblMsg.Font.Bold = true;
                    lblMsg.Text += "Ad-" + _slno + " " + tab.Rows[cnt]["Offer"].ToString() + ": " + tab.Rows[cnt]["Details"].ToString() + ".<br/><br/>";

                }
            }
            else
            {
                Table1.Rows.Clear();
                Table1.GridLines = GridLines.None;

                TableHeaderRow row = new TableHeaderRow();
                TableHeaderCell cell = new TableHeaderCell();
                cell.ColumnSpan = 5;
                cell.Font.Bold = true;
                cell.ForeColor = System.Drawing.Color.Red;
                cell.Text = "No Offers Found";
                row.Controls.Add(cell);

                Table1.Controls.Add(row);

            }
        }

        private void GetAllItems()
        {
            Table5.Rows.Clear();
            //first for loop           
            if (ListBox2.Items.Count > 0)
            {
                foreach (ListItem item in ListBox2.Items)
                {
                    string[] rule = item.Text.Split('>');

                    if (rule[0].Contains(','))
                    {
                        TableHeaderRow mainrow = new TableHeaderRow();
                        TableHeaderCell maincell = new TableHeaderCell();

                        Label lbl_rulex = new Label();
                        lbl_rulex.Text = "RULE X";
                        maincell.Controls.Add(lbl_rulex);
                        mainrow.Controls.Add(maincell);
                        Table5.Controls.Add(mainrow);

                        DataTable tab2 = new DataTable();
                        tab2.Rows.Clear();

                        string[] s = rule[0].Split(',');

                        int count = 0;
                        c = new TableCell[s.Length];
                        TableRow row = new TableRow();

                        //for loop to display items in rule X
                        for (int k = 0; k < s.Length; k++)
                        {
                            tab2 = obj1.GetItemDetails_ItemName(s[k].ToString());

                            c[count] = new TableCell();

                            Table t = new Table();
                            t.Width = 250;
                            TableCell c1 = new TableCell();
                            c1.ColumnSpan = 3;
                            c1.Height = 30;
                            c1.Text = "<b><font color='#006699' size='2px'>" + tab2.Rows[0]["Item_Name"].ToString() + "</font></b>";
                            TableRow R1 = new TableRow();
                            R1.Controls.Add(c1);
                            t.Controls.Add(R1);
                            TableCell c2 = new TableCell();

                            c2.RowSpan = 3;
                            c2.Width = 70;
                            c2.Text = "<img src='" + tab2.Rows[0]["Item_Image"].ToString() + "' width=75px height=100px border=0 />";
                            TableCell c3 = new TableCell();
                            c3.HorizontalAlign = HorizontalAlign.Left;
                            c3.ColumnSpan = 2;

                            c3.Text = "<b><font color='#993366' size='2px'> Rs. " + tab2.Rows[0]["Item_Cost"].ToString() + " /- </font></b>";
                            ImageButton imgA = new ImageButton();
                            imgA.Height = 50;
                            imgA.Width = 50;
                            imgA.ImageUrl = "../images/cart.gif";

                            imgA.ToolTip = "Click to add the Item into the cart";
                            imgA.ID = tab2.Rows[0]["Item_ID"].ToString() + "_" + z;
                            imgA.Click += new ImageClickEventHandler(imgA_Click);
                            TableCell txt = new TableCell();
                            txt.HorizontalAlign = HorizontalAlign.Left;
                            int totalquantity = 0;
                            //totalquantity = obj.ItemSaledQuantity(int.Parse(tab.Rows[k + cnt]["Item_ID"].ToString()));

                            totalquantity = int.Parse(tab2.Rows[0]["Quantity"].ToString());

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

                            ++z;

                            li.Font.Underline = false;
                            li.Text = "<font size='2px'>Add To Cart</font>";
                            li.ToolTip = "Click here to add to Cart";
                            li.ID = tab2.Rows[0]["Item_ID"].ToString() + "_" + z;
                            li.Click += new EventHandler(li_Click);
                            txt.Controls.Add(li);

                            TableCell cartimg = new TableCell();
                            cartimg.Width = 25;
                            cartimg.HorizontalAlign = HorizontalAlign.Left;
                            cartimg.Controls.Add(imgA);

                            ++z;

                            LinkButton l = new LinkButton();
                            l.Font.Underline = false;
                            l.Text = "<font size='1px' color='#006699'>More Details</font>";
                            l.ToolTip = "Click here to view the details";
                            l.Font.Underline = true;
                            l.ID = tab2.Rows[0]["Item_ID"].ToString() + "_" + z;
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

                            row.Controls.Add(c[count]);

                            count++;

                            ++z;
                        }

                        Table5.Controls.Add(row);
                    }
                    //rule x with out ,
                    else
                    {
                        TableHeaderRow mainrow = new TableHeaderRow();
                        TableHeaderCell maincell = new TableHeaderCell();

                        Label lbl_rulex = new Label();
                        lbl_rulex.Text = "RULE X";
                        maincell.Controls.Add(lbl_rulex);
                        mainrow.Controls.Add(maincell);
                        Table5.Controls.Add(mainrow);


                        DataTable tab1 = new DataTable();
                        tab1.Rows.Clear();

                        tab1 = obj1.GetItemDetails_ItemName(rule[0]);

                        Table t = new Table();
                        t.Width = 250;
                        TableCell c1 = new TableCell();
                        c1.ColumnSpan = 3;
                        c1.Height = 30;
                        c1.Text = "<b><font color='#006699' size='2px'>" + tab1.Rows[0]["Item_Name"].ToString() + "</font></b>";
                        TableRow R1 = new TableRow();
                        R1.Controls.Add(c1);
                        t.Controls.Add(R1);
                        TableCell c2 = new TableCell();

                        c2.RowSpan = 3;
                        c2.Width = 70;
                        c2.Text = "<img src='" + tab1.Rows[0]["Item_Image"].ToString() + "' width=75px height=100px border=0 />";
                        TableCell c3 = new TableCell();
                        c3.HorizontalAlign = HorizontalAlign.Left;
                        c3.ColumnSpan = 2;

                        c3.Text = "<b><font color='#993366' size='2px'> Rs. " + tab1.Rows[0]["Item_Cost"].ToString() + " /- </font></b>";
                        ImageButton imgA = new ImageButton();
                        imgA.Height = 50;
                        imgA.Width = 50;
                        imgA.ImageUrl = "../images/cart.gif";

                        imgA.ToolTip = "Click to add the Item into the cart";
                        imgA.ID = tab1.Rows[0]["Item_ID"].ToString() + "_" + z;
                        imgA.Click += new ImageClickEventHandler(imgA_Click);
                        TableCell txt = new TableCell();
                        txt.HorizontalAlign = HorizontalAlign.Left;
                        int totalquantity = 0;

                        totalquantity = int.Parse(tab1.Rows[0]["Quantity"].ToString());

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

                        ++z;

                        li.Font.Underline = false;
                        li.Text = "<font size='2px'>Add To Cart</font>";
                        li.ToolTip = "Click here to add to Cart";
                        li.ID = tab1.Rows[0]["Item_ID"].ToString() + "_" + z;
                        li.Click += new EventHandler(li_Click);
                        txt.Controls.Add(li);

                        TableCell cartimg = new TableCell();
                        cartimg.Width = 25;
                        cartimg.HorizontalAlign = HorizontalAlign.Left;
                        cartimg.Controls.Add(imgA);

                        ++z;

                        LinkButton l = new LinkButton();
                        l.Font.Underline = false;
                        l.Text = "<font size='1px' color='#006699'>More Details</font>";
                        l.ToolTip = "Click here to view the details";
                        l.Font.Underline = true;
                        l.ID = tab1.Rows[0]["Item_ID"].ToString() + "_" + z;
                        l.Click += new EventHandler(l_Click);
                        TableCell c5 = new TableCell();
                        c5.ColumnSpan = 2;
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

                        TableRow row = new TableRow();
                        TableCell cell0 = new TableCell();
                        cell0.Controls.Add(t);
                        row.Controls.Add(cell0);
                        Table5.Controls.Add(row);

                        ++z;
                    }

                    //check for rule y with ,
                    if (rule[1].Contains(','))
                    {
                        TableHeaderRow mainrow = new TableHeaderRow();
                        TableHeaderCell maincell = new TableHeaderCell();

                        Label lbl_ruley = new Label();
                        lbl_ruley.Text = "RULE Y";
                        maincell.Controls.Add(lbl_ruley);
                        mainrow.Controls.Add(maincell);
                        Table5.Controls.Add(mainrow);


                        DataTable tab3 = new DataTable();
                        tab3.Rows.Clear();

                        string[] s = rule[1].Split(',');

                        int count = 0;
                        c = new TableCell[s.Length];
                        TableRow row = new TableRow();

                        //for loop to display the items

                        for (int k = 0; k < s.Length; k++)
                        {
                            tab3 = obj1.GetItemDetails_ItemName(s[k].ToString());

                            c[count] = new TableCell();

                            Table t = new Table();
                            t.Width = 250;
                            TableCell c1 = new TableCell();
                            c1.ColumnSpan = 3;
                            c1.Height = 30;
                            c1.Text = "<b><font color='#006699' size='2px'>" + tab3.Rows[0]["Item_Name"].ToString() + "</font></b>";
                            TableRow R1 = new TableRow();
                            R1.Controls.Add(c1);
                            t.Controls.Add(R1);
                            TableCell c2 = new TableCell();

                            c2.RowSpan = 3;
                            c2.Width = 70;
                            c2.Text = "<img src='" + tab3.Rows[0]["Item_Image"].ToString() + "' width=75px height=100px border=0 />";
                            TableCell c3 = new TableCell();
                            c3.HorizontalAlign = HorizontalAlign.Left;
                            c3.ColumnSpan = 2;

                            c3.Text = "<b><font color='#993366' size='2px'> Rs. " + tab3.Rows[0]["Item_Cost"].ToString() + " /- </font></b>";
                            ImageButton imgA = new ImageButton();
                            imgA.Height = 50;
                            imgA.Width = 50;
                            imgA.ImageUrl = "../images/cart.gif";

                            imgA.ToolTip = "Click to add the Item into the cart";
                            imgA.ID = tab3.Rows[0]["Item_ID"].ToString() + "_" + z;
                            imgA.Click += new ImageClickEventHandler(imgA_Click);
                            TableCell txt = new TableCell();
                            txt.HorizontalAlign = HorizontalAlign.Left;
                            int totalquantity = 0;
                            //totalquantity = obj.ItemSaledQuantity(int.Parse(tab.Rows[k + cnt]["Item_ID"].ToString()));

                            totalquantity = int.Parse(tab3.Rows[0]["Quantity"].ToString());

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

                            ++z;

                            li.Font.Underline = false;
                            li.Text = "<font size='2px'>Add To Cart</font>";
                            li.ToolTip = "Click here to add to Cart";
                            li.ID = tab3.Rows[0]["Item_ID"].ToString() + "_" + z;
                            li.Click += new EventHandler(li_Click);
                            txt.Controls.Add(li);

                            TableCell cartimg = new TableCell();
                            cartimg.Width = 25;
                            cartimg.HorizontalAlign = HorizontalAlign.Left;
                            cartimg.Controls.Add(imgA);

                            ++z;

                            LinkButton l = new LinkButton();
                            l.Font.Underline = false;
                            l.Text = "<font size='1px' color='#006699'>More Details</font>";
                            l.ToolTip = "Click here to view the details";
                            l.Font.Underline = true;
                            l.ID = tab3.Rows[0]["Item_ID"].ToString() + "_" + z;
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

                            row.Controls.Add(c[count]);

                            count++;

                            ++z;
                        }

                        Table5.Controls.Add(row);

                        TableCell cell = new TableCell();
                        cell.ColumnSpan = 8;
                        cell.Text = "<br/><br/>------------------------------------------------------------------------------------------------------------------------<br/>";
                        TableRow row1 = new TableRow();
                        row1.Controls.Add(cell);
                        Table5.Controls.Add(row1);
                    }

                    //rule y without ,
                    else
                    {
                        TableHeaderRow mainrow = new TableHeaderRow();
                        TableHeaderCell maincell = new TableHeaderCell();

                        Label lbl_ruley = new Label();
                        lbl_ruley.Text = "RULE Y";
                        maincell.Controls.Add(lbl_ruley);
                        mainrow.Controls.Add(maincell);
                        Table5.Controls.Add(mainrow);


                        DataTable tab4 = new DataTable();
                        tab4.Rows.Clear();

                        tab4 = obj1.GetItemDetails_ItemName(rule[1]);

                        Table t = new Table();
                        t.Width = 250;
                        TableCell c1 = new TableCell();
                        c1.ColumnSpan = 3;
                        c1.Height = 30;
                        c1.Text = "<b><font color='#006699' size='2px'>" + tab4.Rows[0]["Item_Name"].ToString() + "</font></b>";
                        TableRow R1 = new TableRow();
                        R1.Controls.Add(c1);
                        t.Controls.Add(R1);
                        TableCell c2 = new TableCell();

                        c2.RowSpan = 3;
                        c2.Width = 70;
                        c2.Text = "<img src='" + tab4.Rows[0]["Item_Image"].ToString() + "' width=75px height=100px border=0 />";
                        TableCell c3 = new TableCell();
                        c3.HorizontalAlign = HorizontalAlign.Left;
                        c3.ColumnSpan = 2;

                        c3.Text = "<b><font color='#993366' size='2px'> Rs. " + tab4.Rows[0]["Item_Cost"].ToString() + " /- </font></b>";
                        ImageButton imgA = new ImageButton();
                        imgA.Height = 50;
                        imgA.Width = 50;
                        imgA.ImageUrl = "../images/cart.gif";

                        imgA.ToolTip = "Click to add the Item into the cart";
                        imgA.ID = tab4.Rows[0]["Item_ID"].ToString() + "_" + z;
                        imgA.Click += new ImageClickEventHandler(imgA_Click);
                        TableCell txt = new TableCell();
                        txt.HorizontalAlign = HorizontalAlign.Left;
                        int totalquantity = 0;
                        //totalquantity = obj.ItemSaledQuantity(int.Parse(tab.Rows[k + cnt]["Item_ID"].ToString()));

                        totalquantity = int.Parse(tab4.Rows[0]["Quantity"].ToString());

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

                        ++z;

                        li.Font.Underline = false;
                        li.Text = "<font size='2px'>Add To Cart</font>";
                        li.ToolTip = "Click here to add to Cart";
                        li.ID = tab4.Rows[0]["Item_ID"].ToString() + "_" + z;
                        li.Click += new EventHandler(li_Click);
                        txt.Controls.Add(li);

                        TableCell cartimg = new TableCell();
                        cartimg.Width = 25;
                        cartimg.HorizontalAlign = HorizontalAlign.Left;
                        cartimg.Controls.Add(imgA);

                        ++z;

                        LinkButton l = new LinkButton();
                        l.Font.Underline = false;
                        l.Text = "<font size='1px' color='#006699'>More Details</font>";
                        l.ToolTip = "Click here to view the details";
                        l.Font.Underline = true;
                        l.ID = tab4.Rows[0]["Item_ID"].ToString() + "_" + z;
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

                        TableRow row = new TableRow();
                        //row.Controls.Add(c[0]);
                        //row.Controls.Add(c[1]);
                        //row.Controls.Add(c[2]);
                        TableCell cell0 = new TableCell();
                        cell0.Controls.Add(t);
                        row.Controls.Add(cell0);
                        Table5.Controls.Add(row);

                        TableCell cell = new TableCell();
                        cell.ColumnSpan = 8;
                        cell.Text = "<br/><br/>------------------------------------------------------------------------------------------------------------------------<br/>";
                        TableRow row1 = new TableRow();
                        row1.Controls.Add(cell);
                        Table5.Controls.Add(row1);

                        ++z;
                    }

                }
            }
        }

        void li_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton Or = (LinkButton)sender;
                string[] id = Or.ID.ToString().Split('_');
                Session["Item_ID"] = id[0];
                if (obj1.CountItem(int.Parse(id[0]), Session["Customer_ID"].ToString()))
                {
                    if (obj1.AddCartDetails(Session["Customer_ID"].ToString(), int.Parse(id[0]), 1))
                    {
                        Response.Redirect("MemberCart.aspx");
                    }
                }
                else
                    Response.Redirect("MemberCart.aspx");
            }
            catch
            {

            }
        }

        void imgA_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton Or = (ImageButton)sender;
                string[] id = Or.ID.ToString().Split('_');
                Session["Item_ID"] = id[0];
                if (obj1.CountItem(int.Parse(id[0]), Session["Customer_ID"].ToString()))
                {
                    if (obj1.AddCartDetails(Session["Customer_ID"].ToString(), int.Parse(id[0]), 1))
                    {
                        Response.Redirect("MemberCart.aspx");
                    }
                }
                else
                    Response.Redirect("MemberCart.aspx");
            }
            catch
            {

            }
        }

        void l_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton link = (LinkButton)sender;
                string[] id = link.ID.ToString().Split('_');
                Session["Item_ID"] = id[0];
                Response.Redirect("MemberViewDetails.aspx");
            }
            catch
            {

            }
        }

        #region -- Algorithm Steps ---

        private void GetDistinctTransactions()
        {
            tab2.Rows.Clear();

            tab2 = memObj.GetCustomerTransactions(Session["Customer_ID"].ToString());

            if (tab2.Rows.Count > 0)
            {
                int _Ids = 100;
                Table2.Rows.Clear();
                lv_Transactions.Items.Clear();

                Table2.GridLines = GridLines.Both;

                TableHeaderRow mainrow2 = new TableHeaderRow();
                mainrow2.ForeColor = System.Drawing.Color.Black;
                mainrow2.BackColor = System.Drawing.Color.Goldenrod;

                TableHeaderCell cell2 = new TableHeaderCell();
                cell2.Text = "Transaction Dataset";
                mainrow2.Controls.Add(cell2);

                Table2.Controls.Add(mainrow2);

                for (int i = 0; i < tab2.Rows.Count; i++)
                {
                    string constraints = null;

                    TableRow row2 = new TableRow();

                    TableCell cell_transaction = new TableCell();
                    cell_transaction.HorizontalAlign = HorizontalAlign.Left;
                    DataTable tab4 = new DataTable();
                    tab4.Rows.Clear();
                    tab4 = obj.GetTransactionDetails_ID(int.Parse(tab2.Rows[i]["Transaction_ID"].ToString()));

                    if (tab4.Rows.Count > 0)
                    {

                        for (int k = 0; k < tab4.Rows.Count; k++)
                        {
                            DataTable tab5 = new DataTable();
                            tab5 = obj.GetItemDetails(int.Parse(tab4.Rows[k]["Item_ID"].ToString()));


                            constraints += tab5.Rows[0]["Item_Name"].ToString() + ",";
                        }

                        cell_transaction.Text = constraints.Substring(0, constraints.Length - 1).ToString();

                        row2.Controls.Add(cell_transaction);
                        Table2.Controls.Add(row2);

                        lv_Transactions.Items.Add(cell_transaction.Text.ToString());
                        lv_TransactionsId.Items.Add(_Ids.ToString());
                    }

                    ++_Ids;
                }

            }
            else
            {
                Table2.Rows.Clear();
                Table2.GridLines = GridLines.None;

                TableHeaderRow row = new TableHeaderRow();
                TableHeaderCell cell = new TableHeaderCell();
                cell.Font.Bold = true;
                cell.ForeColor = System.Drawing.Color.Red;
                cell.ColumnSpan = 5;
                cell.Text = "No Customer Transactions Found!";
                row.Controls.Add(cell);

                Table2.Controls.Add(row);

            }
        }

        private void GetDistinctItems()
        {
            tab1.Rows.Clear();

            tab1 = memObj.GetCustomerDistinctItems(Session["Customer_ID"].ToString());

            if (tab1.Rows.Count > 0)
            {
                Table1.Rows.Clear();
                lv_Items.Items.Clear();

                Table1.GridLines = GridLines.Both;

                TableHeaderRow mainrow1 = new TableHeaderRow();
                mainrow1.ForeColor = System.Drawing.Color.Black;
                mainrow1.BackColor = System.Drawing.Color.Goldenrod;

                TableHeaderCell cell1 = new TableHeaderCell();
                cell1.Text = "Item Set";
                mainrow1.Controls.Add(cell1);

                Table1.Controls.Add(mainrow1);

                for (int i = 0; i < tab1.Rows.Count; i++)
                {
                    TableRow row = new TableRow();

                    TableCell cell_item = new TableCell();
                    cell_item.HorizontalAlign = HorizontalAlign.Left;
                    cell_item.Width = 150;
                    DataTable tab3 = new DataTable();
                    tab3.Rows.Clear();

                    tab3 = obj.GetItemDetails(int.Parse(tab1.Rows[i]["Item_ID"].ToString()));
                    cell_item.Text = tab3.Rows[0]["Item_Name"].ToString();
                    row.Controls.Add(cell_item);

                    Table1.Controls.Add(row);

                    lv_Items.Items.Add(tab3.Rows[0]["Item_Name"].ToString());
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
                cell.Text = "No Transactions Found";
                row.Controls.Add(cell);

                Table1.Controls.Add(row);
            }
        }

        //main function which contains the main steps of the eclat algorithm
        private void EclatAlgorithm()
        {
            try
            {
                //double _X = 0;
                //_X = double.Parse(lv_Transactions.Items.Count.ToString()) / 20;

                double _supportCnt = (double.Parse(lv_Transactions.Items.Count.ToString()) / 100) * 2;
                double _confidence = 0.4;

                //function to calculate the L1 and store in buffer
                Dictionary<string, string> _FrequentItemsMain = L1(_supportCnt);
                Dictionary<string, string> _Candidates = new Dictionary<string, string>();

                do
                {
                    //function to calculate C2,C3.... and store in buffer
                    _Candidates = GenerateCandidates(_FrequentItemsMain);

                    //function to calculate L2,L3.... and store in buffer
                    _FrequentItemsMain = GetFrequentItems(_Candidates, _supportCnt);
                }

                while (_Candidates.Count != 0);

                //function to generate rules or patterns
                List<ClassRules> RulesList = GenerateRules();
                //compare with the confidence and find strong rules
                List<ClassRules> StrongRules = GetStrongRules(_confidence, RulesList);

                //function to display the final results (L and Patterns)
                Result(DictionaryAllFrequentItems, StrongRules);
            }
            catch
            {

            }
        }

        //function to find L1
        private Dictionary<string, string> L1(double MinSupport)
        {
            Dictionary<string, string> DictionaryFrequentItemsReturn = new Dictionary<string, string>();
            _FrequentItems.Clear();

            for (int i = 0; i < lv_Items.Items.Count; i++)
            {
                string _Support = GetSupport(lv_Items.Items[i].Text.ToString());

                string[] Id = _Support.Split(',');

                if ((int.Parse(Id.Length.ToString()) >= MinSupport))
                {
                    DictionaryFrequentItemsReturn.Add(lv_Items.Items[i].Text.ToString(), _Support);

                    //temp buffer                   
                    _FrequentItems.Add(lv_Items.Items[i].Text.ToString(), _Support);

                    DictionaryAllFrequentItems.Add(lv_Items.Items[i].Text.ToString(), _Support);
                }
            }
            return DictionaryFrequentItemsReturn;
        }

        //function to find the support of items
        private string GetSupport(string GeneratedCandidate)
        {
            string _SupportReturn = null;

            string[] AllTransactions = new string[lv_Transactions.Items.Count];
            string[] AllTransactionsId = new string[lv_TransactionsId.Items.Count];

            for (int i = 0; i < lv_Transactions.Items.Count; i++)
            {
                AllTransactions[i] = lv_Transactions.Items[i].Text.ToString();
                AllTransactionsId[i] = lv_TransactionsId.Items[i].Text.ToString();
            }

            for (int j = 0; j < lv_Transactions.Items.Count; j++)
            {
                if (IsSubstring(GeneratedCandidate, lv_Transactions.Items[j].Text.ToString()))
                {
                    _SupportReturn += lv_TransactionsId.Items[j].Text.ToString() + ",";
                }
            }

            if (_SupportReturn != null)
            {
                _SupportReturn = _SupportReturn.Substring(0, _SupportReturn.Length - 1);
            }

            return _SupportReturn;
        }

        //function to find the support of items(new)
        private string NewSupport(string GeneratedCandidate)
        {
            string _SupportReturn = null;

            if (_FrequentItems.Count > 0)
            {
                string[] _C = GeneratedCandidate.Split(',');

                string[] AllTransactionsId = new string[lv_TransactionsId.Items.Count];

                for (int j = 0; j < _FrequentItems.Count; j++)
                {
                    if (_C.Contains(_FrequentItems.ElementAt(j).Key))
                    {

                    }

                    if (IsSubstring(GeneratedCandidate, lv_Transactions.Items[j].Text.ToString()))
                    {
                        _SupportReturn += lv_TransactionsId.Items[j].Text.ToString() + ",";
                    }
                }

                if (_SupportReturn != null)
                {
                    _SupportReturn = _SupportReturn.Substring(0, _SupportReturn.Length - 1);
                }
            }

            return _SupportReturn;
        }

        //function to check if the item exisits in a given transaction
        private bool IsSubstring(string Child, string Parent)
        {
            string[] TransactionArray = Child.Split(',');
            //string value = null;
            foreach (string Item in TransactionArray)
            {
                if (!Parent.Contains(Item))
                    return false;
            }
            return true;
        }

        //function to find C2, C3, C4.....
        private Dictionary<string, string> GenerateCandidates(Dictionary<string, string> MainFrequentItems)
        {
            Dictionary<string, string> DictionaryCandidatesReturn = new Dictionary<string, string>();
            for (int i = 0; i < MainFrequentItems.Count - 1; i++)
            {
                string[] FirstItem = Alphabetize(MainFrequentItems.Keys.ElementAt(i));
                string FirstItemString = null;
                for (int k = 0; k < FirstItem.Length; k++)
                {
                    FirstItemString += FirstItem[k].ToString() + ",";
                }
                FirstItemString = FirstItemString.Remove(FirstItemString.Length - 1);
                for (int j = i + 1; j < MainFrequentItems.Count; j++)
                {
                    string[] SecondItem = Alphabetize(MainFrequentItems.Keys.ElementAt(j));
                    string SecondItemString = null;
                    for (int l = 0; l < SecondItem.Length; l++)
                    {
                        SecondItemString += SecondItem[l].ToString() + ",";
                    }
                    SecondItemString = SecondItemString.Remove(SecondItemString.Length - 1);
                    string GeneratedCandidate = GetCandidate(FirstItemString, SecondItemString);

                    if (GeneratedCandidate != string.Empty)
                    {
                        string[] CandidateArray = Alphabetize(GeneratedCandidate);
                        GeneratedCandidate = "";
                        for (int m = 0; m < CandidateArray.Length; m++)
                        {
                            GeneratedCandidate += CandidateArray[m].ToString() + ",";
                        }

                        GeneratedCandidate = GeneratedCandidate.Remove(GeneratedCandidate.Length - 1);
                        string Support = GetSupport(GeneratedCandidate);
                        DictionaryCandidatesReturn.Add(GeneratedCandidate, Support);
                    }
                }
            }
            return DictionaryCandidatesReturn;
        }

        //function to set in alphabetical order
        private string[] Alphabetize(string Token)
        {
            // Convert to char array, then sort and return
            string[] TokenArray = Token.Split(',');
            Array.Sort(TokenArray);
            return TokenArray;
        }

        //function to get the candidate excluding the similar items
        private string GetCandidate(string FirstItemString, string SecondItemString)
        {
            string CandidateJoin = null;
            if (FirstItemString.Contains(',') || SecondItemString.Contains(','))
            {
                string[] First = FirstItemString.Split(',');
                string[] Second = SecondItemString.Split(',');
                if (First[0] != Second[0])
                {
                    return string.Empty;
                }
                else
                {
                    string firstString = FirstItemString.Substring(0, FirstItemString.LastIndexOf(','));
                    string secondString = SecondItemString.Substring(0, SecondItemString.LastIndexOf(','));
                    if (firstString == secondString)
                    {
                        return FirstItemString + SecondItemString.Substring(SecondItemString.LastIndexOf(','));
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
            else
            {
                return FirstItemString + "," + SecondItemString;
            }
        }

        //Function to find L through given support
        private Dictionary<string, string> GetFrequentItems(Dictionary<string, string> CandidatesDictionary, double MinimumSupport)
        {
            Dictionary<string, string> FrequentReturn = new Dictionary<string, string>();
            for (int i = CandidatesDictionary.Count - 1; i >= 0; i--)
            {
                string Item = CandidatesDictionary.Keys.ElementAt(i);
                string Support = CandidatesDictionary[Item];

                if (Support != null)
                {
                    string[] Id = Support.Split(',');

                    if ((Id.Length >= MinimumSupport))
                    {
                        FrequentReturn.Add(Item, Support);
                        DictionaryAllFrequentItems.Add(Item, Support);
                    }
                }
            }
            return FrequentReturn;
        }

        //Generate Rules
        private List<ClassRules> GenerateRules()
        {
            List<ClassRules> RulesReturnList = new List<ClassRules>();
            foreach (string Item in DictionaryAllFrequentItems.Keys)
            {
                string[] ItemArray = Item.Split(',');
                if (ItemArray.Length > 1)
                {
                    int MaxCombinationLength = ItemArray.Length / 2;
                    GenerateCombination(Item, MaxCombinationLength, ref RulesReturnList);
                }
            }
            return RulesReturnList;
        }

        private void GenerateCombination(string Item, int CombinationLength, ref List<ClassRules> RulesReturnList)
        {
            string[] ItemArray = Item.Split(',');
            int ItemLength = ItemArray.Length;
            if (ItemLength == 2)
            {
                AddItem(ItemArray[0].ToString(), Item, ref RulesReturnList);
                return;
            }
            else if (ItemLength == 3)
            {
                for (int i = 0; i < ItemLength; i++)
                {
                    AddItem(ItemArray[i].ToString(), Item, ref RulesReturnList);
                }
                return;
            }
            else
            {
                for (int i = 0; i < ItemLength; i++)
                {
                    GetCombinationRecursive(ItemArray[i].ToString(), Item, CombinationLength, ref RulesReturnList);
                }
            }
        }

        private void AddItem(string Combination, string Item, ref List<ClassRules> RulesReturnList)
        {
            string Remaining = GetRemaining(Combination, Item);
            ClassRules Rule = new ClassRules(Combination, Remaining, 0);
            RulesReturnList.Add(Rule);
        }

        private string GetCombinationRecursive(string Combination, string Item, int CombinationLength, ref List<ClassRules> RulesReturnList)
        {
            AddItem(Combination, Item, ref RulesReturnList);
            string LastTokenItem = Combination;
            if (Combination.Contains(','))
                LastTokenItem = Combination.Substring(Combination.LastIndexOf(',') + 1);

            string NextItem = null; ;
            string LastItem = Item.Substring(Item.LastIndexOf(',') + 1);
            if (Combination.Split(',').Length == CombinationLength)
            {
                if (LastTokenItem != LastItem)
                {
                    string TempCombination = null;
                    foreach (string str in Combination.Split(','))
                    {
                        if (str != LastTokenItem)
                        {
                            TempCombination = TempCombination + "," + str;
                        }
                    }
                    Combination = TempCombination.Substring(1);
                    string[] strs = Item.Split(',');
                    for (int i = 0; i < strs.Length; i++)
                    {
                        if (strs[i] == LastTokenItem)
                        {
                            NextItem = strs[i + 1];
                        }
                    }
                    //Combination = Combination.Remove(nLastTokenCharcaterIndex, 1);
                    //NextItem = Item[nLastTokenCharcaterIndexInParent + 1];
                    string strNewToken = Combination + "," + NextItem;
                    return (GetCombinationRecursive(strNewToken, Item, CombinationLength, ref RulesReturnList));
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                if (Combination != LastItem.ToString())
                {
                    string[] strs = Item.Split(',');
                    for (int i = 0; i < strs.Length; i++)
                    {
                        if (strs[i] == LastTokenItem)
                        {
                            NextItem = strs[i + 1];
                        }
                    }
                    //NextItem = Item[nLastTokenCharcaterIndexInParent + 1];
                    string strNewToken = Combination + "," + NextItem;
                    return (GetCombinationRecursive(strNewToken, Item, CombinationLength, ref RulesReturnList));
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private string GetRemaining(string Child, string Parent)
        {
            string[] childArray = Child.Split(',');

            for (int i = 0; i < childArray.Length; i++)
            {
                string Remaining = null;
                string[] ParentArray = Parent.Split(',');
                for (int j = 0; j < ParentArray.Length; j++)
                {
                    if (childArray[i] != ParentArray[j])
                    {
                        Remaining = Remaining + "," + ParentArray[j];
                    }
                }

                if (Remaining.Contains(','))
                    Parent = Remaining.Substring(1);
                else
                    Parent = Remaining;
            }
            return Parent;
        }

        //funciton to generate strong rules
        private List<ClassRules> GetStrongRules(double MinConfidence, List<ClassRules> RulesList)
        {
            List<ClassRules> StrongRulesReturn = new List<ClassRules>();

            foreach (ClassRules Rule in RulesList)
            {
                string[] XY = Alphabetize(Rule.X + "," + Rule.Y);
                string XYString = null;
                for (int i = 0; i < XY.Length; i++)
                {
                    XYString += XY[i] + ",";
                }
                XYString = XYString.Remove(XYString.Length - 1);
                AddStrongRule(Rule, XYString, ref StrongRulesReturn, MinConfidence);
            }

            StrongRulesReturn.Sort();

            return StrongRulesReturn;
        }

        private void AddStrongRule(ClassRules Rule, string XY, ref List<ClassRules> StrongRulesReturn, double MinConfidence)
        {
            double Confidence = GetConfidence(Rule.X, XY);
            ClassRules NewRule;

            if (Confidence >= MinConfidence)
            {
                NewRule = new ClassRules(Rule.X, Rule.Y, Confidence);
                StrongRulesReturn.Add(NewRule);
            }

            Confidence = GetConfidence(Rule.Y, XY);

            if (Confidence >= MinConfidence)
            {
                NewRule = new ClassRules(Rule.Y, Rule.X, Confidence);
                StrongRulesReturn.Add(NewRule);
            }
        }

        private double GetConfidence(string X, string XY)
        {
            string Support_X, Support_XY;
            Support_X = DictionaryAllFrequentItems[X];
            Support_XY = DictionaryAllFrequentItems[XY];

            string[] Id1 = Support_X.Split(',');
            string[] Id2 = Support_XY.Split(',');

            return (double.Parse(Id2.Length.ToString()) / double.Parse(Id1.Length.ToString()));
        }

        //function to display the final output
        public void Result(Dictionary<string, string> AllFrequentItems, List<ClassRules> StrongRulesList)
        {
            LoadFrequentItems(AllFrequentItems);
            LoadRules(StrongRulesList);
        }

        private void LoadFrequentItems(Dictionary<string, string> AllFrequentItems)
        {
            foreach (string Item in AllFrequentItems.Keys)
            {
                ListItem items = new ListItem(Item);
                ListBox1.Items.Add(items);
            }
        }

        //private void LoadFrequentItems(Dictionary<string, string> AllFrequentItems)
        //{
        //    Table3.Rows.Clear();

        //    Table3.GridLines = GridLines.Both;

        //    TableHeaderRow mainrow1 = new TableHeaderRow();
        //    mainrow1.ForeColor = System.Drawing.Color.WhiteSmoke;
        //    mainrow1.BackColor = System.Drawing.Color.Red;

        //    TableHeaderCell cell1 = new TableHeaderCell();
        //    cell1.Text = "Frequent ItemSet";
        //    mainrow1.Controls.Add(cell1);

        //    TableHeaderCell cell2 = new TableHeaderCell();
        //    cell2.Text = "SC";
        //    mainrow1.Controls.Add(cell2);

        //    Table3.Controls.Add(mainrow1);


        //    foreach (string Item in AllFrequentItems.Keys)
        //    {
        //        //ListViewItem LVItem = new ListViewItem(Item);
        //        //LVItem.SubItems.Add(AllFrequentItems[Item].ToString());
        //        //lv_Frequent.Items.Add(LVItem);

        //        ListItem items = new ListItem(Item);
        //        ListBox1.Items.Add(items);

        //        TableRow row = new TableRow();

        //        TableCell cell_item = new TableCell();
        //        cell_item.HorizontalAlign = HorizontalAlign.Left;
        //        cell_item.Width = 500;
        //        cell_item.Text = Item.ToString();
        //        row.Controls.Add(cell_item);

        //        TableCell cell_support = new TableCell();
        //        cell_support.HorizontalAlign = HorizontalAlign.Left;
        //        cell_support.Width = 100;
        //        cell_support.Text = AllFrequentItems[Item].ToString();
        //        row.Controls.Add(cell_support);

        //        Table3.Controls.Add(row);

        //    }
        //}

        private void LoadRules(List<ClassRules> StrongRulesList)
        {
            Table4.Rows.Clear();
            Table4.GridLines = GridLines.Both;
            System.Threading.Thread.Sleep(100);
            TableHeaderRow mainrow1 = new TableHeaderRow();
            mainrow1.ForeColor = System.Drawing.Color.WhiteSmoke;
            mainrow1.BackColor = System.Drawing.Color.DarkRed;

            TableHeaderCell cell1 = new TableHeaderCell();
            cell1.Width = 450;
            cell1.Text = "Rule X";
            mainrow1.Controls.Add(cell1);

            TableHeaderCell cell3 = new TableHeaderCell();
            cell3.Text = "--->";
            mainrow1.Controls.Add(cell3);

            TableHeaderCell cell4 = new TableHeaderCell();
            cell4.Width = 450;
            cell4.Text = "Rule Y";
            mainrow1.Controls.Add(cell4);

            TableHeaderCell cell2 = new TableHeaderCell();
            cell2.Text = "Confidence";
            mainrow1.Controls.Add(cell2);

            Table4.Controls.Add(mainrow1);

            int i = 0;

            if (StrongRulesList.Count > 0)
            {
                foreach (ClassRules Rule in StrongRulesList)
                {
                    ListItem items = new ListItem(Rule.X + ">" + Rule.Y);
                    ListBox2.Items.Add(items);

                    TableRow row = new TableRow();

                    if (Rule.X.Contains(','))
                    {
                        TableCell cellX1 = new TableCell();

                        string[] s = Rule.X.Split(',');

                        for (int a = 0; a < s.Length; a++)
                        {
                            DataTable tab45 = new DataTable();
                            tab45 = obj1.GetItemDetails_ItemName(s[a]);

                            LinkButton linkX1 = new LinkButton();
                            linkX1.Text = s[a] + "(" + tab45.Rows[0]["SubCategory_ID"].ToString() + ")" + ",";
                            linkX1.Click += new EventHandler(linkX1_Click);
                            linkX1.ID = "Link1~" + tab45.Rows[0]["SubCategory_ID"].ToString() + "~" + i;
                            ++i;
                            cellX1.Controls.Add(linkX1);

                        }

                        row.Controls.Add(cellX1);

                    }
                    else
                    {

                        DataTable tab45 = new DataTable();
                        tab45 = obj1.GetItemDetails_ItemName(Rule.X);

                        TableCell cell_rule1 = new TableCell();
                        LinkButton link1 = new LinkButton();
                        link1.Text = Rule.X + "(" + tab45.Rows[0]["SubCategory_ID"].ToString() + ")";
                        link1.Click += new EventHandler(link1_Click);
                        link1.ID = "Link2~" + tab45.Rows[0]["SubCategory_ID"].ToString() + "~" + i;
                        ++i;
                        cell_rule1.Controls.Add(link1);
                        row.Controls.Add(cell_rule1);

                    }

                    TableCell cell_rule2 = new TableCell();
                    cell_rule2.HorizontalAlign = HorizontalAlign.Left;
                    cell_rule2.Width = 750;
                    cell_rule2.Text = " --> ";
                    row.Controls.Add(cell_rule2);

                    if (Rule.Y.Contains(','))
                    {
                        TableCell cellY1 = new TableCell();

                        string[] s = Rule.Y.Split(',');

                        for (int a = 0; a < s.Length; a++)
                        {
                            DataTable tab45 = new DataTable();
                            tab45 = obj1.GetItemDetails_ItemName(s[a]);

                            LinkButton linkY1 = new LinkButton();
                            linkY1.Text = s[a] + "(" + tab45.Rows[0]["SubCategory_ID"].ToString() + ")" + ",";
                            linkY1.Click += new EventHandler(linkY1_Click);
                            linkY1.ID = "Link3~" + tab45.Rows[0]["SubCategory_ID"].ToString() + "~" + i;
                            ++i;
                            cellY1.Controls.Add(linkY1);

                        }

                        row.Controls.Add(cellY1);
                    }
                    else
                    {
                        DataTable tab45 = new DataTable();
                        tab45 = obj1.GetItemDetails_ItemName(Rule.Y);

                        TableCell cell_rule3 = new TableCell();
                        LinkButton link2 = new LinkButton();
                        link2.Text = Rule.Y + "(" + tab45.Rows[0]["SubCategory_ID"].ToString() + ")";
                        link2.Click += new EventHandler(link2_Click);
                        link2.ID = "Link4~" + tab45.Rows[0]["SubCategory_ID"].ToString() + "~" + i;
                        ++i;
                        cell_rule3.Controls.Add(link2);
                        row.Controls.Add(cell_rule3);
                    }

                    TableCell cell_confidence = new TableCell();
                    cell_confidence.HorizontalAlign = HorizontalAlign.Left;
                    cell_confidence.Width = 100;
                    cell_confidence.Text = String.Format("{0:0.00}", (Rule.Confidence * 100)) + "%";
                    row.Controls.Add(cell_confidence);

                    Table4.Controls.Add(row);

                    ++i;
                }
            }
            else
            {
                Table4.Rows.Clear();
                Table4.GridLines = GridLines.None;

                TableHeaderRow row = new TableHeaderRow();
                TableHeaderCell cell = new TableHeaderCell();
                cell.Font.Bold = true;
                cell.ForeColor = System.Drawing.Color.Red;
                cell.ColumnSpan = 5;
                cell.Text = "No Pattrens Found for the Input!!!";
                row.Controls.Add(cell);

                Table4.Controls.Add(row);
            }


        }

        void linkY1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            LinkButton lbtn = (LinkButton)sender;

            string[] s = lbtn.ID.Split('~');

            Response.Redirect(string.Format("_ViewYourAds.aspx?SubCId={0}", s[1]));
        }

        void linkX1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            LinkButton lbtn = (LinkButton)sender;

            string[] s = lbtn.ID.Split('~');

            Response.Redirect(string.Format("_ViewYourAds.aspx?SubCId={0}", s[1]));
        }

        void link2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            LinkButton lbtn = (LinkButton)sender;

            string[] s = lbtn.ID.Split('~');

            Response.Redirect(string.Format("_ViewYourAds.aspx?SubCId={0}", s[1]));

        }

        void link1_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            LinkButton lbtn = (LinkButton)sender;

            string[] s = lbtn.ID.Split('~');

            Response.Redirect(string.Format("_ViewYourAds.aspx?SubCId={0}", s[1]));
        }



        #endregion

    }
}