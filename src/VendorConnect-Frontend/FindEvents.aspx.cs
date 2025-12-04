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
    public partial class FindEvents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                 Service1Client client = new Service1Client();
                int userID = Convert.ToInt32(Session["UserID"]);
                var u = client.GetUser(userID);
                if (u != null)
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

                dynamic AllEvents = client.GetEvents();
                EventsRepeater.DataSource = AllEvents;
                EventsRepeater.DataBind();

                client.Close();
            }
        }

        protected void EventsRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if( e.CommandName=="Apply")
            {
                int eventId = Convert.ToInt32(e.CommandArgument);
                Service1Client client = new Service1Client();
                int userId = Convert.ToInt32(Session["UserID"]);
                var vendorId = client.GetVendorByUserId(userId);

                var applied = client.EventApplication(vendorId.VendorID, eventId);
                if(applied==1)
                {
                   ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "successAlert",
                        "alert('Successfully applied for the event!');",
                        true
                    );
                }
                else if(applied==0)
                {
                    ScriptManager.RegisterStartupScript(
                       this, this.GetType(),
                       "failAlert",
                       "alert('You have already applied for this event.');",
                       true
                   );
                }
            }
        }
    }
}