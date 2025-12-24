using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendorConnect_Frontend.ServiceReference1;

namespace VendorConnect_Frontend
{
    public partial class OrganizerProfile : System.Web.UI.Page
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

                
                var org = client.GetOrganizer(Convert.ToInt32(Session["OrganizerId"]));
                if(org!=null)
                {
                    FirstName.Value = u.FirstName;
                    LastName.Value = u.LastName;
                    password.Value = u.Password;
                    email.Value = u.Username;
                    OrgaEmail.Value = org.ContactEmail;
                    OrgaName.Value = org.OrganizationName;
                    OrgaPhone.Value = org.Phone;
                }
               


                client.Close();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Service1Client client = new Service1Client();

            int userID = Convert.ToInt32(Session["UserID"]);
            var organizer = client.updateOrganizerProfile(userID, FirstName.Value, LastName.Value, password.Value, email.Value, OrgaName.Value, OrgaEmail.Value, OrgaPhone.Value);
            if(organizer==true)
            {
                ScriptManager.RegisterStartupScript(
                     this, this.GetType(),
                     "successAlert",
                     "alert('Successfully updated the profile');",
                     true
                 );
            }
            else
            {
                ScriptManager.RegisterStartupScript(
                      this, this.GetType(),
                      "successAlert",
                      "alert('Unsuccessfully!!');",
                      true
                  );
            }
            client.Close();
        }
    }
}