using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendorConnect_Frontend.ServiceReference1;

namespace VendorConnect_Frontend
{
    public partial class CreateEvent : System.Web.UI.Page
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
                    OrgaNames.InnerText = vName + "" + vLName;


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


                client.Close();
            }
        }

        protected void btnCreateEvent_Click(object sender, EventArgs e)
        {
            Service1Client client = new Service1Client();
            var EName = EventName.Value;
            var EDate = EventDate.Value;
            var EDesc = EventDescription.Value;
            var numVendor = NumVendors.Value;
            var ELocation = EventLocation.Value;
            var orgaID=Convert.ToInt32(Session["OrganizerId"]);
            var createEvent = client.CreateEvent(EName, Convert.ToDateTime(EDate), ELocation, Convert.ToInt32(numVendor), EDesc, orgaID);
            if(createEvent== 1)
            {
                ScriptManager.RegisterStartupScript(
                      this, this.GetType(),
                      "successAlert",
                      "alert('Successfully created an event');",
                      true
                  );
            }
            else if(createEvent==0)
            {
                ScriptManager.RegisterStartupScript(
                      this, this.GetType(),
                      "successAlert",
                      "alert('Event already exists');",
                      true
                  );
            }
            else
            {
                ScriptManager.RegisterStartupScript(
                      this, this.GetType(),
                      "successAlert",
                      "alert('Unsuccessfully,could not create event');",
                      true
                  );
            }
            client.Close();
        }
    }
}