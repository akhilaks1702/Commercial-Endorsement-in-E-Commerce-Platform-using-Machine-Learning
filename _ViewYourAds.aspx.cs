using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DATAMINING_ASSOCIATIONRULE
{
    public partial class _ViewYourAds : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadOffers();
        }

        //function to load all offers
        private void LoadOffers()
        {
            DataTable tab = new DataTable();

            Admin_Class obj = new Admin_Class();

            tab.Rows.Clear();


            tab = obj.GetAdsBySubCId(int.Parse(Request.QueryString["SubCId"].ToString()));

            if (tab.Rows.Count > 0)
            {
                Table1.Rows.Clear();
                Table1.GridLines = GridLines.Both;

                TableHeaderRow main_row = new TableHeaderRow();
                main_row.ForeColor = System.Drawing.Color.Black;
                main_row.BackColor = System.Drawing.Color.Goldenrod;

                TableHeaderCell cell1 = new TableHeaderCell();
                cell1.Text = "SubCategory";
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
    }
}