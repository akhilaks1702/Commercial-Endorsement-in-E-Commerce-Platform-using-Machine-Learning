using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DATAMINING_ASSOCIATIONRULE
{
    public partial class _postAds : System.Web.UI.Page
    {
        static int ad_ID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadCategories();
            }

            LoadOffers();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Admin_Class obj = new Admin_Class();

            if (btnSubmit.Text.Equals("Submit"))
            {
                obj.InsertAd(int.Parse(DropDownListCateg.SelectedValue), txtOffer.Text, txtDetails.Text, DateTime.Now, DropDownList1.SelectedItem.Text);

               

                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Offer Added Successfully')</script>");
            }
            else if (btnSubmit.Text.Equals("Update"))
            {
                obj.UpdateAd(int.Parse(DropDownListCateg.SelectedValue), txtOffer.Text, txtDetails.Text, DateTime.Now, DropDownList1.SelectedItem.Text, ad_ID);


                btnSubmit.Text = "Submit";

                ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Offer Updated Successfully')</script>");
            }

            txtDetails.Text = txtOffer.Text = string.Empty;

            LoadOffers();
        }

        //function to load categories
        private void LoadCategories()
        {
            DataTable tab2 = new DataTable();
            Admin_Class obj = new Admin_Class();

            tab2 = obj.GetAllSubCategories();

            if (tab2.Rows.Count > 0)
            {
                DropDownListCateg.Items.Clear();

                DropDownListCateg.DataSource = tab2.DefaultView;

                DropDownListCateg.DataTextField = "SubCategory_Name";
                DropDownListCateg.DataValueField = "SubCategory_ID";

                DropDownListCateg.DataBind();

                DropDownListCateg.Items.Insert(0, "- All -");
            }
            else
            {
                DropDownListCateg.Items.Clear();

                DropDownListCateg.DataSource = null;
                DropDownListCateg.DataBind();

                DropDownListCateg.Items.Insert(0, "- Input Sub Categories -");
            }
        }

        //function to load all offers
        private void LoadOffers()
        {
            DataTable tab = new DataTable();

            Admin_Class obj = new Admin_Class();

            tab.Rows.Clear();


            tab = obj.GetAllAds();

            if (tab.Rows.Count > 0)
            {
                Table1.Rows.Clear();
                Table1.GridLines = GridLines.Both;

                TableHeaderRow main_row = new TableHeaderRow();
                main_row.ForeColor = System.Drawing.Color.Black;
                main_row.BackColor = System.Drawing.Color.Goldenrod;

                TableHeaderCell cell1 = new TableHeaderCell();
                cell1.Text = "Category";
                main_row.Controls.Add(cell1);

                TableHeaderCell cell2 = new TableHeaderCell();
                cell2.Text = "Offer";
                main_row.Controls.Add(cell2);

                TableHeaderCell cell3 = new TableHeaderCell();
                cell3.Text = "Details";
                main_row.Controls.Add(cell3);

               
                TableHeaderCell cell5 = new TableHeaderCell();
                cell5.Text = "PostedDate";
                main_row.Controls.Add(cell5);

                TableHeaderCell cell51 = new TableHeaderCell();
                cell51.Text = "Status";
                main_row.Controls.Add(cell51);

                TableHeaderCell cell61 = new TableHeaderCell();
                cell61.Text = "Edit";
                main_row.Controls.Add(cell61);

                TableHeaderCell cell6 = new TableHeaderCell();
                cell6.Text = "Delete";
                main_row.Controls.Add(cell6);

                Table1.Controls.Add(main_row);

                for (int cnt = 0; cnt < tab.Rows.Count; cnt++)
                {
                    TableRow row = new TableRow();

                    TableCell cellCateg = new TableCell();
                    cellCateg.Width = 150;
                    DataTable tabSub = new DataTable();
                    Admin_Class obj1 = new Admin_Class();
                    tabSub = obj1.GetSubCategoryDetails(int.Parse(tab.Rows[cnt]["SubCategory_ID"].ToString()));
                    cellCateg.Text = tabSub.Rows[0]["SubCategory_Name"].ToString();
                    row.Controls.Add(cellCateg);

                    TableCell cellOffer = new TableCell();
                    cellOffer.Width = 350;
                    cellOffer.Text = tab.Rows[cnt]["Offer"].ToString();
                    row.Controls.Add(cellOffer);

                    TableCell cellStartDate = new TableCell();
                    cellStartDate.Width = 150;
                    cellStartDate.Text = tab.Rows[cnt]["Details"].ToString();
                    row.Controls.Add(cellStartDate);

                   
                    TableCell cellPostedDate = new TableCell();
                    cellPostedDate.Width = 150;
                    cellPostedDate.Text = tab.Rows[cnt]["PostedDate"].ToString();
                    row.Controls.Add(cellPostedDate);

                    TableCell cellStatus = new TableCell();
                    cellStatus.Width = 150;
                    cellStatus.Text = tab.Rows[cnt]["Status"].ToString();
                    row.Controls.Add(cellStatus);

                    TableCell celledit = new TableCell();
                   

                    Button btnEdit = new Button();
                    btnEdit.Text = "Edit";
                   
                    btnEdit.ID = "edit!" + tab.Rows[cnt]["AdId"].ToString();
                    btnEdit.Click += new EventHandler(btnEdit_Click);
                    celledit.Controls.Add(btnEdit);

                    row.Controls.Add(celledit);

                    TableCell cellDelete = new TableCell();
                    

                    Button btnDelete = new Button();
                    btnDelete.Text = "Delete";
                    btnDelete.OnClientClick = "confirm('are u sure want to delete?')";
                    btnDelete.ID ="del!" + tab.Rows[cnt]["AdId"].ToString();
                    btnDelete.Click += new EventHandler(btnDelete_Click);
                    cellDelete.Controls.Add(btnDelete);

                    row.Controls.Add(cellDelete);

                    Table1.Controls.Add(row);
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

        void btnEdit_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Button btn = (Button)sender;
            Admin_Class obj = new Admin_Class();
            string[] s = btn.ID.Split('!');

            DataTable tab1 = new DataTable();
            tab1.Rows.Clear();
            tab1 = obj.GetAdById(int.Parse(s[1].ToString()));

            txtOffer.Text = tab1.Rows[0]["Offer"].ToString();
            txtDetails.Text = tab1.Rows[0]["Details"].ToString();

            int subcategoryID = int.Parse(tab1.Rows[0]["SubCategory_ID"].ToString());

            string datatextfield = DropDownListCateg.Items.FindByValue(subcategoryID.ToString()).ToString();

            ListItem item = new ListItem(datatextfield, subcategoryID.ToString());
            int index = DropDownListCateg.Items.IndexOf(item);

            if (index != -1)

                DropDownListCateg.SelectedIndex = index;


            
            ListItem item1 = new ListItem(tab1.Rows[0]["Status"].ToString(), tab1.Rows[0]["Status"].ToString());
            int index1 = DropDownList1.Items.IndexOf(item1);

            if (index1 != -1)

                DropDownList1.SelectedIndex = index1;


            ad_ID = int.Parse(s[1]);
            btnSubmit.Text = "Update";

        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Admin_Class obj = new Admin_Class();
            string[] s = btn.ID.Split('!');
            obj.DeleteAd(int.Parse(s[1]));

            ClientScript.RegisterStartupScript(this.GetType(), "Key", "<script>alert('Ad Deleted Successfully')</script>");
            LoadOffers();
        }
    }
}