using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace CRM.Reports
{
    public partial class DownloadExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ExcelData"] != null)
            {
                DataTable dt = (DataTable)(Session["ExcelData"]);
                GridView gv = new GridView();
                gv.DataSource = dt;
                gv.DataBind();
                //CommonOperations.Export("DspHandover Export.xls", gv);
            }
            else if (Session["ExcelDataForwardStatus"] != null)
            {
                DataTable dt = (DataTable)(Session["ExcelDataForwardStatus"]);
                GridView gv = new GridView();
                gv.DataSource = dt;
                gv.DataBind();
                //CommonOperations.Export("ForwardStatus Export.xls", gv);
            }
            else if (Session["ExcelDataReturnStatus"] != null)
            {
                DataTable dt = (DataTable)(Session["ExcelDataReturnStatus"]);
                GridView gv = new GridView();
                gv.DataSource = dt;
                gv.DataBind();
                //CommonOperations.Export("ReturnStatus Export.xls", gv);
            }
            else if (Session["CustomReportData"] != null)
            {
                DataTable dt = (DataTable)(Session["CustomReportData"]);
                GridView gv = new GridView();
                gv.DataSource = dt;
                gv.DataBind();
                //CommonOperations.Export("Custom Report.xls", gv);
            }
            else if (Session["ExcelDataCuidPendingItems"] != null)
            {
                DataTable dt = (DataTable)(Session["ExcelDataCuidPendingItems"]);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["Web Order No"] = "'" + Convert.ToString(dt.Rows[i]["Web Order No"]);
                        dt.AcceptChanges();
                    }
                }
                StringBuilder sb = new StringBuilder();
                var columnNames = dt.Columns.Cast<DataColumn>().Select(column => "\"" + column.ColumnName.Replace("\"", "\"\"") + "\"").ToArray();
                sb.AppendLine(string.Join(",", columnNames));
                foreach (DataRow row in dt.Rows)
                {
                    var fields = row.ItemArray.Select(field => "\"" + field.ToString().Replace("\"", "\"\"") + "\"").ToArray();
                    sb.AppendLine(string.Join(",", fields));
                }
                string path = ConfigurationManager.AppSettings["CuidPendingItemsCsv"].ToString();
                path = string.Format("{0}{1}.csv", path, "CuidPendingItemsReport");
                if (File.Exists(path))
                    File.Delete(path);
                File.WriteAllText(path, sb.ToString(), Encoding.Default);
                Response.ContentType = "application/text";
                Response.AddHeader("content-disposition", "attachment;filename=CuidPendingReports.csv");
                Response.TransmitFile(path);
                Response.End();
            }
        }
    }
}