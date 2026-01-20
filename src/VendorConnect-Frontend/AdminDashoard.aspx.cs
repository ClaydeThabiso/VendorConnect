using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendorConnect_Frontend.ServiceReference1;

namespace VendorConnect_Frontend
{
    public partial class AdminDashoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || (char)Session["LoggedIn"] != 'A')
            {
                Response.Redirect("Login.aspx");
                return;
            }

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