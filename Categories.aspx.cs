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
    public partial class Categories : System.Web.UI.Page
    {
        Admin_Class obj = new Admin_Class();
        static DataTable tab = new DataTable();
        DataTable tab1 = new DataTable();
        DataTable tab2 = new DataTable();
        static int serial_no = 1;
        static int i = 0, j = 5;
        static int subcategory_ID = 0, ItemID = 0;
        static string subcategoryname = null, Item_name = null;
        static string records = "All", oldphoto_path = null, oldAttachment_path = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin_ID"] == null)
            {
                Session.Abandon();
                Response.Redirect("Home_page.aspx");
            }
            else
            {
                GetAllCategories();

            }
        }



        #region -------- MANAGE ITEM CATEGORIES ------------

        private void GetAllCategories()
        {
            tab.Rows.Clear();
            tab = obj.GetAllCategories();

            if (tab.Rows.Count > 0)
            {
                Table1.Rows.Clear();

                for (int cnt = i; cnt < j; cnt++)
                {
                    if (cnt < tab.Rows.Count)
                    {
                        TableRow row1 = new TableRow();

                        TableCell row1_cell1 = new TableCell();
                        row1_cell1.Width = 10;
                        row1_cell1.Font.Size = 10;
                        row1_cell1.Text = serial_no + cnt + ".";
                        row1.Controls.Add(row1_cell1);

                        TableCell row1_cell2 = new TableCell();
                        row1_cell2.HorizontalAlign = HorizontalAlign.Left;
                        row1_cell2.Width = 400;
                        row1_cell2.Text = tab.Rows[cnt]["Category_Name"].ToString();
                        row1.Controls.Add(row1_cell2);

                        TableRow row2 = new TableRow();

                        TableCell row2_cell1 = new TableCell();
                        row2_cell1.Width = 10;
                        row2_cell1.Text = " ";
                        row2.Controls.Add(row2_cell1);

                        TableCell row2_Cell2 = new TableCell();
                        row2_Cell2.Width = 400;
                        row2_Cell2.HorizontalAlign = HorizontalAlign.Right;
                        Button btn_deletecategory = new Button();
                        btn_deletecategory.ID = "Category~" + tab.Rows[cnt]["Category_ID"].ToString();
                        btn_deletecategory.Text = "Delete";
                        btn_deletecategory.OnClientClick = "return confirm('Are you sure want to delete?')";
                        btn_deletecategory.Click += new EventHandler(btn_deletecategory_Click);
                        row2_Cell2.Controls.Add(btn_deletecategory);
                        row2.Controls.Add(row2_Cell2);

                        TableRow row3 = new TableRow();
                        TableCell row3_cell1 = new TableCell();
                        row3_cell1.Width = 10;
                        row3_cell1.Text = " ";
                        row3.Controls.Add(row3_cell1);

                        TableCell row3_cell2 = new TableCell();
                        row3_cell2.Width = 400;
                        row3_cell2.Text = "<hr/>";
                        row3.Controls.Add(row3_cell2);

                        Table1.Controls.Add(row1);
                        Table1.Controls.Add(row2);
                        Table1.Controls.Add(row3);

                        if (tab.Rows.Count > 5)
                        {
                            if (cnt < 5)
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
                    else
                    {
                        if (tab.Rows.Count < 5)
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
                cell.ColumnSpan = 5;
                cell.Font.Bold = true;
                cell.ForeColor = System.Drawing.Color.Red;
                cell.Text = "No ItemCategories Found";
                row.Controls.Add(cell);

                Table1.Controls.Add(row);

                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = false;

            }

        }

        void btn_deletecategory_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string[] s = btn.ID.ToString().Split('~');

            try
            {
                int subcategoryID = 0, ItemID = 0;
                DataTable t1 = new DataTable();
                t1.Rows.Clear();
                t1 = obj.GetCategorySubCategories(int.Parse(s[1].ToString()));

                if (t1.Rows.Count > 0)
                {
                    for (int x = 0; x < t1.Rows.Count; x++)
                    {
                        subcategoryID = int.Parse(t1.Rows[x]["SubCategory_ID"].ToString());

                        DataTable t2 = new DataTable();
                        t2.Rows.Clear();
                        t2 = obj.GetSubcategoryItems(subcategoryID);

                        if (t2.Rows.Count > 0)
                        {
                            for (int y = 0; y < t2.Rows.Count; y++)
                            {
                                ItemID = int.Parse(t2.Rows[y]["Item_ID"].ToString());
                                string path = t2.Rows[y]["Item_Image"].ToString();
                                File.Delete(path);
                                obj.DeleteItem(ItemID);
                            }

                            obj.DeleteItemSubCategory(subcategoryID);
                        }
                        else
                        {
                            obj.DeleteItemSubCategory(subcategoryID);
                        }

                    }

                    obj.DeleteItemCategory(int.Parse(s[1].ToString()));
                }
                else
                {
                    obj.DeleteItemCategory(int.Parse(s[1].ToString()));
                }

                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('ItemCategory Deleted Successfully (With SubCategories & Items)')</script>");
                GetAllCategories();
                ClearTextBoxes();
                btn_newcategories.Text = "Submit";

            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Server Error!')</script>");
            }

        }

        protected void btn_newcategories_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_newcategories.Text.Equals("Submit"))
                {
                    if (obj.CheckItemCategory(txt_categoryname.Text))
                    {
                        obj.NewItemCategory(txt_categoryname.Text);
                        ClearTextBoxes();
                        GetAllCategories();
                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('New ItemCategory Added Successfully')</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('ItemCategory Exists')</script>");
                    }
                }

            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Server Error!')</script>");
            }
        }

        private void ClearTextBoxes()
        {
            txt_categoryname.Text = string.Empty;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            i = i - 5;
            j = j - 5;
            if (i >= 0)
            {
                Table1.Rows.Clear();
                GetAllCategories();
            }
            else
            {
                i = 0;
                j = 5;
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            i = j;
            j = j + 5;

            if (i < tab.Rows.Count)
            {
                Table1.Rows.Clear();
                GetAllCategories();
            }
            else
            {
                i = i - 5;
                j = j - 5;
                ImageButton1.Visible = false;
                ImageButton2.Visible = false;
                ImageButton3.Visible = true;

            }
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Categories.aspx");
        }

        #endregion

    }
}