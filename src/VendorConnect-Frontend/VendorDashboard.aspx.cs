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
            if (!IsPostBack)
            {
                Service1Client client = new Service1Client();

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

                client.Close();
            }

        }
    }
}