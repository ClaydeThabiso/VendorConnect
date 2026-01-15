using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VendorConnect_Frontend
{
    public partial class AdminReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Service1Client client = new Service1Client();
                var GetTotalVendors = client.totalVendors();
                var GetTotalOrga = client.totalOrganizers();
                var getUpcoming = client.getUpcomingEvents();

                DisplayVendors.InnerText = Convert.ToString(GetTotalVendors);
                DisplayOrga.InnerText = Convert.ToString(GetTotalOrga);
                displayUpcomingEvents.InnerText = Convert.ToString(getUpcoming);
                client.Close();


            }
        }
    }
}