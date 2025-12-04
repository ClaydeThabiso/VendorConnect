using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendorConnect_Frontend.ServiceReference1;

namespace VendorConnect_Frontend
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLgin_Click(object sender, EventArgs e)
        {
            Service1Client client = new Service1Client();
            var name = username.Value;
            var pass = password.Value;

            var loggedIn = client.login(name, pass);
            if(loggedIn!=null)
            {
                
                Session["LoggedIn"] = loggedIn.UserType;
                Session["UserID"] = loggedIn.Id;
                if (loggedIn.UserType == 'V')
                {
                    var vendorID = client.GetVendorByUserId(loggedIn.Id);
                    if(vendorID!=null)
                    {
                        Session["VendorID"] = vendorID.VendorID;
                        Response.Redirect("VendorDashboard.aspx");
                    }
                   
                }
                else if (loggedIn.UserType == 'A')
                {
                    Response.Redirect("AdminDashoard.aspx");
                }
                else if (loggedIn.UserType == 'O')
                {
                    var organizer = client.GetOrganizerByUserId(loggedIn.Id);

                    if (organizer != null)
                    {
                        Session["OrganizerId"] = organizer.OrganizerId;
                        Response.Redirect("OrganizerDashboard.aspx");
                    }
                    
                }
            }
            else
            {
                lblMsg.Text = "Incorrect Login details";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

            client.Close();
        }
    }
}