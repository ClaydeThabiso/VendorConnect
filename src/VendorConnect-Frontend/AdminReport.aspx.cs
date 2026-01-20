using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using VendorConnect_Frontend.ServiceReference1;

namespace VendorConnect_Frontend
{
    public partial class AdminReport : System.Web.UI.Page
    {
        protected string EventStatusJson = "{}";
        protected string EventApplicationsJson = "[]";
        protected string TopEventsJson = "[]";
        protected string MonthlyEventsJson = "[]";
        protected string OrganizerEventsJson = "[]";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEventKPI();
                LoadCharts();
            }
        }

        private void LoadEventKPI()
        {
            using (Service1Client client = new Service1Client())
            {
                var data = client.GetEventReportsDashboard();

                DisplayTotalEvents.InnerText = data.TotalEvents.ToString();
                DisplayActiveEvents.InnerText = data.ActiveEvents.ToString();
                DisplayCompletedEvents.InnerText = data.CompletedEvents.ToString();
                DisplayApplications.InnerText = data.TotalApplications.ToString();
            }
        }
        private void LoadCharts()
        {
            using (Service1Client client = new Service1Client())
            {
                var status = client.GetEventStatusChart();
                var apps = client.GetEventApplicationsChart();
                var topEvents = client.GetTopAppliedEvents();
                var monthly = client.GetMonthlyEventsTrend();
                var organizers = client.GetTopOrganizersByEvents();
               

                JavaScriptSerializer js = new JavaScriptSerializer();
                EventStatusJson = js.Serialize(status);
                EventApplicationsJson = js.Serialize(apps);
                TopEventsJson = js.Serialize(topEvents);
                MonthlyEventsJson = js.Serialize(monthly);
                OrganizerEventsJson = js.Serialize(organizers);
            }
        }

    }
}