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
    public partial class Admin_Items : System.Web.UI.Page
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
                if (!this.IsPostBack)
                {
                    i = 0;
                    j = 5;
                    txt_ItemName.Focus();
                    LoadItemCategories();
                }

                GetAllItems();

                try
                {
                    if (DropDownList3.SelectedIndex == 0)
                    {
                        GetAllItems();
                    }
                    else
                    {
                        if (DropDownList2.SelectedIndex == 0)
                        {
                            GetCategoryItems(int.Parse(DropDownList3.SelectedValue.ToString()));
                        }
                        else
                        {
                            GetItems_Categ_SubCategID(int.Parse(DropDownList3.SelectedValue.ToString()), int.Parse(DropDownList2.SelectedValue.ToString()));
                        }
                    }
                }
                catch
                {

                }
            }
        }

        private void ClearTextBoxes()
        {
            FileUpload1.Enabled = true;
            txt_Cost.Text = txt_ItemDetails.Text = txt_ItemName.Text = txt_quantity.Text = txtOtherDetails.Text = string.Empty;
        }

        private void LoadItemCategories()
        {
            tab1.Rows.Clear();
            tab1 = obj.GetAllCategories();

            DropDownList3.Items.Clear();

            if (tab1.Rows.Count > 0)
            {
                DropDownList3.DataSource = tab1.DefaultView;

                DropDownList3.DataTextField = "Category_Name";
                DropDownList3.DataValueField = "Category_ID";

                DropDownList3.DataBind();

                DropDownList3.Items.Insert(0, "- All -");
            }
            else
            {
                DropDownList3.DataSource = null;
                DropDownList3.DataBind();

                DropDownList3.Items.Insert(0, "- All -");
            }
        }

        #region ------------ MANAGE ITEMS ------------------

        private void LoadItemCategorySubCategories(int categID)
        {
            tab2.Rows.Clear();
            tab2 = obj.GetCategorySubCategories(categID);

            if (tab2.Rows.Count > 0)
            {
                DropDownList2.Items.Clear();

                DropDownList2.DataSource = tab2.DefaultView;

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
            tab2.Rows.Clear();
            tab2 = obj.GetAllItems();

            if (tab2.Rows.Count > 0)
            {
                Table3.Rows.Clear();
                Table3.GridLines = GridLines.Both;

                TableHeaderRow main_row = new TableHeaderRow();

                TableHeaderCell cell1 = new TableHeaderCell();
                cell1.Text = "Image";
                main_row.Controls.Add(cell1);

                TableHeaderCell cell2 = new TableHeaderCell();
                cell2.Text = "Item";
                main_row.Controls.Add(cell2);

                TableHeaderCell cell3 = new TableHeaderCell();
                cell3.Text = "Item Details";
                main_row.Controls.Add(cell3);

                TableHeaderCell cell4 = new TableHeaderCell();
                cell4.Text = "Cost";
                main_row.Controls.Add(cell4);

                TableHeaderCell cell5 = new TableHeaderCell();
                cell5.Text = "Quantity";
                main_row.Controls.Add(cell5);

                Table3.Controls.Add(main_row);

                for (int cnt = 0; cnt < tab2.Rows.Count; cnt++)
                {
                    TableRow row = new TableRow();

                    TableCell cell_img = new TableCell();
                    Image img = new Image();
                    img.Height = 50;
                    img.Width = 50;
                    img.ImageUrl = tab2.Rows[cnt]["Item_Image"].ToString();
                    cell_img.Controls.Add(img);
                    row.Controls.Add(cell_img);

                    TableCell cell_name = new TableCell();
                    cell_name.Width = 100;
                    cell_name.Text = tab2.Rows[cnt]["Item_Name"].ToString();
                    row.Controls.Add(cell_name);

                    TableCell cell_details = new TableCell();
                    cell_details.Width = 250;
                    cell_details.Text = tab2.Rows[cnt]["Item_Details"].ToString();
                    row.Controls.Add(cell_details);

                    TableCell cell_cost = new TableCell();
                    cell_cost.Width = 100;
                    cell_cost.Text = tab2.Rows[cnt]["Item_Cost"].ToString();
                    row.Controls.Add(cell_cost);

                    TableCell cell_quantity = new TableCell();
                    cell_quantity.Width = 100;
                    cell_quantity.Text = tab2.Rows[cnt]["Quantity"].ToString();
                    row.Controls.Add(cell_quantity);

                    Table3.Controls.Add(row);
                }
            }
            else
            {
                Table3.Rows.Clear();
                Table3.GridLines = GridLines.None;

                TableHeaderRow row = new TableHeaderRow();
                TableHeaderCell cell = new TableHeaderCell();
                cell.Font.Bold = true;
                cell.ForeColor = System.Drawing.Color.Red;
                cell.ColumnSpan = 5;
                cell.Text = "No Items Found";
                row.Controls.Add(cell);

                Table3.Controls.Add(row);
            }
        }

        private void GetCategoryItems(int categ_ID)
        {
            tab2.Rows.Clear();
            tab2 = obj.GetCategoryItems(categ_ID);

            if (tab.Rows.Count > 0)
            {
                Table3.Rows.Clear();
                Table3.GridLines = GridLines.Both;

                TableHeaderRow main_row = new TableHeaderRow();

                TableHeaderCell cell1 = new TableHeaderCell();
                cell1.Text = "Image";
                main_row.Controls.Add(cell1);

                TableHeaderCell cell2 = new TableHeaderCell();
                cell2.Text = "Item";
                main_row.Controls.Add(cell2);

                TableHeaderCell cell3 = new TableHeaderCell();
                cell3.Text = "Item Details";
                main_row.Controls.Add(cell3);

                TableHeaderCell cell4 = new TableHeaderCell();
                cell4.Text = "Cost";
                main_row.Controls.Add(cell4);

                TableHeaderCell cell5 = new TableHeaderCell();
                cell5.Text = "Quantity";
                main_row.Controls.Add(cell5);

                Table3.Controls.Add(main_row);

                for (int cnt = 0; cnt < tab2.Rows.Count; cnt++)
                {
                    TableRow row = new TableRow();

                    TableCell cell_img = new TableCell();
                    Image img = new Image();
                    img.Height = 50;
                    img.Width = 50;
                    img.ImageUrl = tab2.Rows[cnt]["Item_Image"].ToString();
                    cell_img.Controls.Add(img);
                    row.Controls.Add(cell_img);

                    TableCell cell_name = new TableCell();
                    cell_name.Width = 100;
                    cell_name.Text = tab2.Rows[cnt]["Item_Name"].ToString();
                    row.Controls.Add(cell_name);

                    TableCell cell_details = new TableCell();
                    cell_details.Width = 250;
                    cell_details.Text = tab2.Rows[cnt]["Item_Details"].ToString();
                    row.Controls.Add(cell_details);

                    TableCell cell_cost = new TableCell();
                    cell_cost.Width = 100;
                    cell_cost.Text = tab2.Rows[cnt]["Item_Cost"].ToString();
                    row.Controls.Add(cell_cost);

                    TableCell cell_quantity = new TableCell();
                    cell_quantity.Width = 100;
                    cell_quantity.Text = tab2.Rows[cnt]["Quantity"].ToString();
                    row.Controls.Add(cell_quantity);

                    Table3.Controls.Add(row);
                }
            }
            else
            {
                Table3.Rows.Clear();
                Table3.GridLines = GridLines.None;

                TableHeaderRow row = new TableHeaderRow();
                TableHeaderCell cell = new TableHeaderCell();
                cell.Font.Bold = true;
                cell.ForeColor = System.Drawing.Color.Red;
                cell.ColumnSpan = 5;
                cell.Text = "No Items Found for the selected Category";
                row.Controls.Add(cell);

                Table3.Controls.Add(row);
            }
        }

        private void GetItems_Categ_SubCategID(int catID, int subcateID)
        {
            tab2.Rows.Clear();
            tab2 = obj.GetItems_Categ_SubCategID(catID, subcateID);

            if (tab2.Rows.Count > 0)
            {
                Table3.Rows.Clear();
                Table3.GridLines = GridLines.Both;

                TableHeaderRow main_row = new TableHeaderRow();

                TableHeaderCell cell1 = new TableHeaderCell();
                cell1.Text = "Image";
                main_row.Controls.Add(cell1);

                TableHeaderCell cell2 = new TableHeaderCell();
                cell2.Text = "Item";
                main_row.Controls.Add(cell2);

                TableHeaderCell cell3 = new TableHeaderCell();
                cell3.Text = "Item Details";
                main_row.Controls.Add(cell3);

                TableHeaderCell cell4 = new TableHeaderCell();
                cell4.Text = "Cost";
                main_row.Controls.Add(cell4);

                TableHeaderCell cell5 = new TableHeaderCell();
                cell5.Text = "Quantity";
                main_row.Controls.Add(cell5);

                TableHeaderCell cell6 = new TableHeaderCell();
                cell6.Text = "Edit Details";
                main_row.Controls.Add(cell6);

                TableHeaderCell cell7 = new TableHeaderCell();
                cell7.Text = "Delete";
                main_row.Controls.Add(cell7);

                Table3.Controls.Add(main_row);

                for (int cnt = 0; cnt < tab2.Rows.Count; cnt++)
                {
                    TableRow row = new TableRow();

                    TableCell cell_img = new TableCell();
                    Image img = new Image();
                    img.Height = 50;
                    img.Width = 50;
                    img.ImageUrl = tab2.Rows[cnt]["Item_Image"].ToString();
                    cell_img.Controls.Add(img);
                    row.Controls.Add(cell_img);

                    TableCell cell_name = new TableCell();
                    cell_name.Width = 100;
                    cell_name.Text = tab2.Rows[cnt]["Item_Name"].ToString();
                    row.Controls.Add(cell_name);

                    TableCell cell_details = new TableCell();
                    cell_details.Width = 250;
                    cell_details.Text = tab2.Rows[cnt]["Item_Details"].ToString();
                    row.Controls.Add(cell_details);

                    TableCell cell_cost = new TableCell();
                    cell_cost.Width = 100;
                    cell_cost.Text = tab2.Rows[cnt]["Item_Cost"].ToString();
                    row.Controls.Add(cell_cost);

                    TableCell cell_quantity = new TableCell();
                    cell_quantity.Width = 100;
                    cell_quantity.Text = tab2.Rows[cnt]["Quantity"].ToString();
                    row.Controls.Add(cell_quantity);

                    TableCell cell_editItem = new TableCell();
                    Button btn_editItem = new Button();
                    btn_editItem.ID = "ItemEdit~" + tab2.Rows[cnt]["Item_ID"].ToString() + "~" + tab2.Rows[cnt]["Item_Image"].ToString();
                    btn_editItem.Text = "EditDetails";
                    btn_editItem.Click += new EventHandler(btn_editItem_Click);
                    cell_editItem.Controls.Add(btn_editItem);
                    row.Controls.Add(cell_editItem);

                    TableCell cell_ItemDelete = new TableCell();
                    Button btn_ItemDelete = new Button();
                    btn_ItemDelete.ID = "ItemDelete~" + tab2.Rows[cnt]["Item_ID"].ToString() + "~" + tab2.Rows[cnt]["Item_Image"].ToString();
                    btn_ItemDelete.Text = "Delete";
                    btn_ItemDelete.OnClientClick = "return confirm('Are you sure want to delete ?')";
                    btn_ItemDelete.Click += new EventHandler(btn_ItemDelete_Click);
                    cell_ItemDelete.Controls.Add(btn_ItemDelete);
                    row.Controls.Add(cell_ItemDelete);

                    Table3.Controls.Add(row);



                }
            }
            else
            {
                Table3.Rows.Clear();
                Table3.GridLines = GridLines.None;

                TableHeaderRow row = new TableHeaderRow();
                TableHeaderCell cell = new TableHeaderCell();
                cell.Font.Bold = true;
                cell.ForeColor = System.Drawing.Color.Red;
                cell.ColumnSpan = 5;
                cell.Text = "No Items Found for the selected SubCategory";
                row.Controls.Add(cell);

                Table3.Controls.Add(row);


            }
        }

        void btn_ItemDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] s = btn.ID.ToString().Split('~');

            try
            {
                obj.DeleteItem(int.Parse(s[1].ToString()));
                File.Delete(Server.MapPath(s[2].ToString()));
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Item Deleted Successfully')</script>");
                ClearTextBoxes();
                DropDownList2_SelectedIndexChanged(sender, e);
                btn_Items.Text = "Submit";
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Server Error!')</script>");
            }
        }

        void btn_editItem_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string[] s = btn.ID.ToString().Split('~');

            tab2.Rows.Clear();
            tab2 = obj.GetItemDetails(int.Parse(s[1].ToString()));

            txt_ItemName.Text = tab2.Rows[0]["Item_Name"].ToString();
            Item_name = tab2.Rows[0]["Item_Name"].ToString();
            txt_ItemDetails.Text = tab2.Rows[0]["Item_Details"].ToString();
            txt_Cost.Text = tab2.Rows[0]["Item_Cost"].ToString();

            txt_quantity.Text = tab2.Rows[0]["Quantity"].ToString();
            //txtKeywords.Text = tab2.Rows[0]["Keywords"].ToString();
            txtOtherDetails.Text = tab2.Rows[0]["OtherDetails"].ToString();
            ItemID = int.Parse(tab2.Rows[0]["Item_ID"].ToString());
            oldphoto_path = tab2.Rows[0]["Item_Image"].ToString();

            btn_Items.Text = "Update";
            FileUpload1.Enabled = false;
            LinkButton1.Visible = true;

            RequiredFieldValidator8.Enabled = false;
            RegularExpressionValidator1.Enabled = false;
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DropDownList3.SelectedIndex > 0)
                {
                    LoadItemCategorySubCategories(int.Parse(DropDownList3.SelectedValue.ToString()));
                    GetCategoryItems(int.Parse(DropDownList3.SelectedValue.ToString()));

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
                if (DropDownList3.SelectedIndex > 0)
                {
                    if (DropDownList2.SelectedIndex > 0)
                    {
                        GetItems_Categ_SubCategID(int.Parse(DropDownList3.SelectedValue.ToString()), int.Parse(DropDownList2.SelectedValue.ToString()));
                    }
                    else
                    {
                        GetCategoryItems(int.Parse(DropDownList3.SelectedValue.ToString()));
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

        protected void btn_Items_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_Items.Text.Equals("Submit"))
                {
                    if (obj.CheckItemName(txt_ItemName.Text))
                    {
                        string filename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);

                        int index = filename.LastIndexOf('.');
                        string ext = filename.Substring(index + 1);

                        string filepath = Server.MapPath("Item_Photos/" + txt_ItemName.Text + "." + ext);
                        FileUpload1.PostedFile.SaveAs(filepath);

                        string db_path1 = @"/Item_Photos/" + txt_ItemName.Text + "." + ext;

                        obj.NewItem(int.Parse(DropDownList2.SelectedValue.ToString()), txt_ItemName.Text, txt_ItemDetails.Text, double.Parse(txt_Cost.Text.ToString()), db_path1, int.Parse(txt_quantity.Text.ToString()), txtOtherDetails.Text);
                        ClearTextBoxes();
                        DropDownList2_SelectedIndexChanged(sender, e);
                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('New Item Added Successfully')</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('ItemName Exists!')</script>");
                    }

                }
                else if (btn_Items.Text.Equals("Update"))
                {
                    if (Item_name.Equals(txt_ItemName.Text))
                    {
                        if (FileUpload1.Enabled == true)
                        {
                            File.Delete(Server.MapPath(oldphoto_path));
                            string filename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);

                            int index = filename.LastIndexOf('.');
                            string ext = filename.Substring(index + 1);

                            string filepath = Server.MapPath("Item_Photos/" + txt_ItemName.Text + "." + ext);
                            FileUpload1.PostedFile.SaveAs(filepath);

                            string db_path = @"/Item_Photos/" + txt_ItemName.Text + "." + ext;

                            obj.UpdateItemDetails(int.Parse(DropDownList2.SelectedValue.ToString()), txt_ItemName.Text, txt_ItemDetails.Text, double.Parse(txt_Cost.Text.ToString()), db_path, int.Parse(txt_quantity.Text.ToString()), txtOtherDetails.Text, ItemID);
                            
                            ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Item Details Updated Successfully')</script>");
                            ClearTextBoxes();
                            DropDownList2_SelectedIndexChanged(sender, e);

                            btn_Items.Text = "Submit";
                            LinkButton1.Visible = false;
                            FileUpload1.Enabled = true;

                        }
                        else
                        {
                            obj.UpdateItemDetails(int.Parse(DropDownList2.SelectedValue.ToString()), txt_ItemName.Text, txt_ItemDetails.Text, double.Parse(txt_Cost.Text.ToString()), oldphoto_path, int.Parse(txt_quantity.Text.ToString()), txtOtherDetails.Text, ItemID);
                            ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Item Details Updated Successfully')</script>");
                            ClearTextBoxes();
                            DropDownList2_SelectedIndexChanged(sender, e);

                            btn_Items.Text = "Submit";
                            LinkButton1.Visible = false;
                            FileUpload1.Enabled = true;

                        }
                    }
                    else
                    {
                        if (obj.CheckSubCategoryItem(int.Parse(DropDownList2.SelectedValue.ToString()), txt_ItemName.Text))
                        {
                            if (FileUpload1.Enabled == true)
                            {
                                File.Delete(Server.MapPath(oldphoto_path));
                                string filename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);

                                int index = filename.LastIndexOf('.');
                                string ext = filename.Substring(index + 1);

                                string filepath = Server.MapPath("Item_Photos/" + txt_ItemName.Text + "." + ext);
                                FileUpload1.PostedFile.SaveAs(filepath);

                                string db_path = @"/Item_Photos/" + txt_ItemName.Text + "." + ext;

                                obj.UpdateItemDetails(int.Parse(DropDownList2.SelectedValue.ToString()), txt_ItemName.Text, txt_ItemDetails.Text, double.Parse(txt_Cost.Text.ToString()), db_path, int.Parse(txt_quantity.Text.ToString()), txtOtherDetails.Text, ItemID);
                                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Item Details Updated Successfully')</script>");
                                ClearTextBoxes();
                                DropDownList2_SelectedIndexChanged(sender, e);

                                btn_Items.Text = "Submit";
                                LinkButton1.Visible = false;

                            }
                            else
                            {
                                obj.UpdateItemDetails(int.Parse(DropDownList2.SelectedValue.ToString()), txt_ItemName.Text, txt_ItemDetails.Text, double.Parse(txt_Cost.Text.ToString()), oldphoto_path, int.Parse(txt_quantity.Text.ToString()), txtOtherDetails.Text, ItemID);
                                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Item Details Updated Successfully')</script>");
                                ClearTextBoxes();
                                DropDownList2_SelectedIndexChanged(sender, e);

                                btn_Items.Text = "Submit";
                                LinkButton1.Visible = false;

                            }
                        }
                    }

                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Server Error!')</script>");
            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            FileUpload1.Enabled = true;
            RequiredFieldValidator8.Enabled = true;
            RegularExpressionValidator1.Enabled = true;
        }

        #endregion




    }
}
