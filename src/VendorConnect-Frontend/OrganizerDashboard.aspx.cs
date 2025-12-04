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

                var getTotalEvents = client.getTotalEventPerOrganizer(Convert.ToInt32(Session["OrganizerId"]));
                totalEvent.InnerText = Convert.ToString(getTotalEvents);

                dynamic ListEvents = client.GetEventPerOrganizer(Convert.ToInt32(Session["OrganizerId"]));
                StringBuilder display = new StringBuilder();
                if (ListEvents != null)
                {


                    foreach (Event eve in ListEvents)
                    {
                        display.Append("<tr>");
                        display.Append("<td>" + eve.EventName + "</td>");
                        display.Append("<td>" + eve.EventDate + "</td>");
                        display.Append("<td>" + eve.Location + "</td>");
                        display.Append("<td>" + eve.MaxVendors + "</td>");
                        display.Append("<td><span class=\"badge bg-success\">" + eve.status + "</span></td>");
                        display.Append("<a href=\"AboutEvent.aspx?Id=" + eve.EventId + "\">");
                        display.Append("<td><button class=\"btn btn-outline-primary btn-sm\">Manage</button></td>");
                        display.Append("</a>");
                        display.Append("</tr>");
                    }
                    TableData.InnerHtml = display.ToString();
                }
                else
                {
                    TableData.InnerText = "No Events created yet";
                }

                client.Close();
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateEvent.aspx");
        }
    }
}