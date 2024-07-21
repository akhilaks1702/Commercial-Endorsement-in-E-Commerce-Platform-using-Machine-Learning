using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;

namespace DATAMINING_ASSOCIATIONRULE
{
    public partial class _Compare : System.Web.UI.Page
    {
        Dictionary<string, double> testData = new Dictionary<string, double>();

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                _CompareAlgorithms();

                base.OnLoad(e);

                if (!IsPostBack)
                {
                    // bind chart type names to ddl
                    ddlChartType.DataSource = Enum.GetNames(typeof(SeriesChartType));
                    ddlChartType.DataBind();

                    cbUse3D.Checked = false;
                }

                DataBind();

            }
            catch
            {

            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            testData.Clear();

            testData.Add("Apriori", _TimeApriori);
            testData.Add("ECLAT", _TimeEclat);

            cTestChart.Series["Testing"].Points.DataBind(testData, "Key", "Value", string.Empty);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // update chart rendering           
            cTestChart.Series["Testing"].ChartTypeName = "Column";

            cTestChart.ChartAreas[0].Area3DStyle.Enable3D = cbUse3D.Checked;
            cTestChart.ChartAreas[0].Area3DStyle.Inclination = Convert.ToInt32(rblInclinationAngle.SelectedValue);

            cTestChart.Visible = true;
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            cTestChart.Visible = true;

            OnDataBinding(e);
            OnPreRender(e);
        }

        static double _TimeApriori, _TimeEclat;

        private void _CompareAlgorithms()
        {
            tableCompare.Rows.Clear();

            tableCompare.BorderStyle = BorderStyle.Double;
            tableCompare.GridLines = GridLines.Both;
            tableCompare.BorderColor = System.Drawing.Color.Black;

            TableRow mainrow = new TableRow();
            mainrow.HorizontalAlign = HorizontalAlign.Left;
            mainrow.Height = 30;
            mainrow.ForeColor = System.Drawing.Color.Black;
            mainrow.Font.Bold = true;
            mainrow.BackColor = System.Drawing.Color.Orange;

            TableCell cell11 = new TableCell();
            cell11.Text = "<b>Constraint</b>";
            mainrow.Controls.Add(cell11);

            TableCell cell1 = new TableCell();
            cell1.Text = "<b>Apriori</b>";
            mainrow.Controls.Add(cell1);                        

            TableCell cell3 = new TableCell();
            cell3.Text = "<b>ECLAT</b>";
            mainrow.Controls.Add(cell3);

            tableCompare.Controls.Add(mainrow);

            TableRow r2 = new TableRow();

            TableCell r2cell = new TableCell();
            r2cell.Text = "Time";
            r2.Controls.Add(r2cell);

            _TimeApriori = double.Parse(Session["A_Time"].ToString());

            TableCell r2c1 = new TableCell();
            r2c1.Text = Session["A_Time"].ToString() + " milli secs";
            r2.Controls.Add(r2c1);
                        
            TableCell r2c3 = new TableCell();
            r2c3.Text = Session["E_Time"].ToString() + " milli secs"; ;
            r2.Controls.Add(r2c3);

            _TimeEclat = double.Parse(Session["E_Time"].ToString());

            tableCompare.Controls.Add(r2);                      
        }

    }
}