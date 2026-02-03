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

                LoadNotifications();

                DisplayVendors.InnerText = Convert.ToString(GetTotalVendors);
                DisplayOrga.InnerText = Convert.ToString(GetTotalOrga);
                displayUpcomingEvents.InnerText = Convert.ToString(getUpcoming);
                client.Close();
                
               
            }
        }
        private void LoadNotifications()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (Service1Client client = new Service1Client())
            {
                dynamic notifications = client.GetUserNotifications(userId);
                var unreadCount = client.GetUnreadNotificationCount(userId);

                RepeaterNotifications.DataSource = notifications;
                RepeaterNotifications.DataBind();

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
        protected void RepeaterNotifications_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Open")
            {
                string[] args = e.CommandArgument.ToString().Split('|');

                int notificationId = Convert.ToInt32(args[0]);
                string redirectUrl = args.Length > 1 ? args[1] : null;

                using (Service1Client client = new Service1Client())
                {
                    client.MarkNotificationAsRead(notificationId);
                }

                if (!string.IsNullOrEmpty(redirectUrl))
                    Response.Redirect(redirectUrl);
            }
        }
    }
}