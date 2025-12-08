using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendorConnect_Frontend.ServiceReference1;

namespace VendorConnect_Frontend
{
    public partial class ManageEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Service1Client client = new Service1Client();

            int userID = Convert.ToInt32(Session["UserID"]);
            var u = client.GetUser(userID);
            if (u != null)
            {
                var vName = u.FirstName;
                var vLName = u.LastName;
                OrgaNames.InnerText = vName + " " + vLName;


                string intialN = "";
                string intialLN = "";
                for (int i = 0; i < 1; i++)
                {
                    intialN = Convert.ToString(vName[i]);
                    intialLN = Convert.ToString(vLName[i]);
                }
                initials.InnerText = intialN.ToUpper() + intialLN.ToUpper();
            }
            else
            {
                OrgaNames.InnerText = "Demo";
                initials.InnerText = "DD";
            }

            int eventId = Convert.ToInt32(Request.QueryString["EventId"]);
            var ev = client.GetEvent(eventId);

            if (ev != null)
            {
                lblEventName.Text = "Event: " + ev.EventName;
                lblEventDate.Text = "Date: " + ev.EventDate.ToString("d MMM yyyy").ToUpper();
                lblLocation.Text = "Location: " + ev.Location;
                lblDescription.Text = "Description: " + ev.Description;
                lblStatus.Text = "Status: " + ev.status;
                lblMaxVendors.Text = "Max Vendors: " + ev.MaxVendors;
            }
        }
        protected void btnCancelEvent_Click(object sender, EventArgs e)
        {
            Service1Client client = new Service1Client();
            int eventId = Convert.ToInt32(Request.QueryString["EventId"]);
            var result = client.CancelEvent(eventId);

            if (result == 1)
                lblStatus.Text = "Status: Cancelled";
        }
    }
}