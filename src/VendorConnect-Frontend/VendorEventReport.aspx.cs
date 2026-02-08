using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendorConnect_Frontend.ServiceReference1;

namespace VendorConnect_Frontend
{
    public partial class VendorEventReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAnalytics();
            }
        }

        private void LoadAnalytics()
        {
            int vendorId = Convert.ToInt32(Session["VendorId"]);

            using (Service1Client client = new Service1Client())
            {
                var analytics = client.GetVendorAnalytics(vendorId);

                displayTotAppli.InnerText = analytics.TotalApplications.ToString();

                // Send data to JS
                ViewState["Approved"] = analytics.Approved;
                ViewState["Pending"] = analytics.Pending;
                ViewState["Declined"] = analytics.Declined;
                ViewState["MonthlyStats"] = analytics.MonthlyStats;
            }
        }

    }
}