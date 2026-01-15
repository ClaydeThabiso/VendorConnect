using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendorConnect_Frontend.ServiceReference1;

namespace VendorConnect_Frontend
{
    public partial class AdminReport : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEventKPI();
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

    }
}