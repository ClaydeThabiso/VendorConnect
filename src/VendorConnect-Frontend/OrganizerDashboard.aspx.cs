using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendorConnect_Frontend.ServiceReference1;

namespace VendorConnect_Frontend
{
    public partial class OrganizerDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || (char)Session["LoggedIn"] != 'O')
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
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
                var getTotPending = client.CountPendingApplicationPerOrganizer(Convert.ToInt32(Session["OrganizerId"]));
                displayPendingAppl.InnerText = Convert.ToString(getTotPending);

                var getRevnue = client.GetOrganizerRevenue(Convert.ToInt32(Session["OrganizerId"]));
                lblRevenue.InnerText = "R" + getRevnue;

                var getTotalUpcomingEevnts = client.getTotalUpcomingEvents(Convert.ToInt32(Session["OrganizerId"]));
                displayUpcomingEve.InnerText = Convert.ToString(getTotalUpcomingEevnts);

                var getTotalEvents = client.getTotalEventPerOrganizer(Convert.ToInt32(Session["OrganizerId"]));
                totalEvent.InnerText = Convert.ToString(getTotalEvents);

                dynamic ListEvents = client.GetEventPerOrganizer(Convert.ToInt32(Session["OrganizerId"]));
                if(ListEvents!=null)
                {
                    RepeaterEvents.DataSource = ListEvents;
                    RepeaterEvents.DataBind();
                }
                else
                {
                    RepeaterEvents = null;
                }
                LoadNotifications();
                client.Close();
            }
        }
        private void LoadNotifications()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (Service1Client client = new Service1Client())
            {

                var unreadCount = client.GetUnreadNotificationCount(userId);

                if (unreadCount > 0)
                {
                    notifCount.Visible = true;
                    notifCount.InnerText = unreadCount.ToString();
                }
                else
                {
                    notifCount.Visible = false;
                }
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateEvent.aspx");
        }
        protected void RepeaterEvents_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Service1Client client = new Service1Client();

            string[] args = e.CommandArgument.ToString().Split('|');
            int eventId = Convert.ToInt32(args[0]);
           
            if (e.CommandName == "Manage")
            {
                Response.Redirect("ManageEvent.aspx?EventId=" + eventId);
            }

            client.Close();
        }
        protected void RepeaterEvents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Service1Client client = new Service1Client();

                // Get the current row data
                var data = (dynamic)e.Item.DataItem;

                int eventId = Convert.ToInt32(data.EventId);
                int maxVendors = Convert.ToInt32(data.MaxVendors);

                // Get approved vendor count
                int approved = client.getApprovedApplication(eventId);

                // Find literal
                Literal maxVendorLiteral = (Literal)e.Item.FindControl("MaxVendorLiteral");

                if (maxVendorLiteral != null)
                {
                    maxVendorLiteral.Text = approved + "/" + maxVendors;
                }

                client.Close();
            }
        }
    }
}