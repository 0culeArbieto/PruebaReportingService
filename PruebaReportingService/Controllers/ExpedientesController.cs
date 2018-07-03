using Microsoft.Reporting.WebForms;
using PruebaReportingService.Reportes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PruebaReportingService.Controllers
{
    public class ExpedientesController : Controller
    {
        // GET: Expedientes
        public ActionResult Index()
        {
            return View();
        }

        SisceDataSet ds = new SisceDataSet();
        public ActionResult ExpedientesReporte()
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);

            var connectionString = ConfigurationManager.ConnectionStrings["Oracle.SISCE2"].ConnectionString;


            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT * FROM SISCE.T_SCEM_EXPEDIENTES", conx);

             conx.Open();
             adp.Fill(ds, ds.T_SCEM_EXPEDIENTES.TableName);

            

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reportes\ReporteExpedientes.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("SisceDataSet", ds.Tables[0]));


            ViewBag.ReportViewer = reportViewer;

            return View();
        }
    }
}