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
                if(ListEvents!=null)
                {
                    RepeaterEvents.DataSource = ListEvents;
                    RepeaterEvents.DataBind();
                }
                else
                {
                    RepeaterEvents = null;
                }
               
                client.Close();
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateEvent.aspx");
        }
        protected void RepeaterEvents_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}