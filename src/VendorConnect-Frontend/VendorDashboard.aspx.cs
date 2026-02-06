using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendorConnect_Frontend.ServiceReference1;

namespace VendorConnect_Frontend
{
   

    public partial class VendorDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || (char)Session["LoggedIn"] != 'V')
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                Service1Client client = new Service1Client();
                LoadNotifications();
                int userID = Convert.ToInt32(Session["UserID"]);
                var u = client.GetUser(userID);
                if(u!=null)
                {
                    var vName = u.FirstName;
                    var vLName = u.LastName;
                    VendorNames.InnerText = vName + " " + vLName;


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
                    VendorNames.InnerText = Convert.ToString(u);
                    initials.InnerText = "DD";
                }

                int VendorID = Convert.ToInt32(Session["VendorID"]);
                var getApplication = client.GetApplicationPerVendor(VendorID);

                if (getApplication != null)
                {
                    UpcomingEvents.DataSource = getApplication;
                    UpcomingEvents.DataBind();

                }
                else
                {
                    UpcomingEvents.DataSource = null;
                }

                var totEvents = client.getTotAcceptVendorApplication(VendorID);
                displayTotEve.InnerText = Convert.ToString(totEvents);

                var totApp = client.getTotalVendorApplicationPerVendo(VendorID);
                displayTotAppli.InnerText = Convert.ToString(totApp);
                

                client.Close();
            }

        }
        private void LoadNotifications()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (Service1Client client = new Service1Client())
            {
                dynamic notifications = client.GetUserNotifications(userId, (char)Session["LoggedIn"]);
                var unreadCount = client.GetUnreadNotificationCount(userId);

                if (notifications == null)
                {
                    throw new Exception("Notifications returned NULL");
                }

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