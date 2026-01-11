using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendorConnect_Frontend.ServiceReference1;

namespace VendorConnect_Frontend
{
    public partial class AdminOrganizerReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadOrganizerReport();
            }
        }
        private void LoadOrganizerReport()
        {
            using(Service1Client client = new Service1Client())
            {
                RepeaterReport.DataSource = client.OragnizerReport();
                RepeaterReport.DataBind();
            }
        }
        protected void RepeaterReport_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            using (Service1Client client = new Service1Client())
            {
                int userId = Convert.ToInt32(e.CommandArgument);
                if (e.CommandName == "Deactivate")
                {
                    int result = client.DeactivateUser(userId);
                    if (result == 1)
                    {
                        LoadOrganizerReport();
                        ScriptManager.RegisterStartupScript(
                           this, this.GetType(),
                           "successAlert",
                           "alert('Successfully deactivated !');",
                           true
                       );
                    }
                    else if (result == 0)
                    {
                        ScriptManager.RegisterStartupScript(
                           this, this.GetType(),
                           "Alert",
                           "alert('User doesnt exist!');",
                           true
                       );
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(
                           this, this.GetType(),
                           "Alert",
                           "alert('Unsuccessfully!');",
                           true
                       );
                    }

                }
                else if (e.CommandName == "Activate")
                {
                    int result = client.ActivateUser(userId);
                    if (result == 1)
                    {
                        LoadOrganizerReport();
                        ScriptManager.RegisterStartupScript(
                           this, this.GetType(),
                           "successAlert",
                           "alert('Successfully activated !');",
                           true
                       );
                    }
                    else if (result == 0)
                    {
                        ScriptManager.RegisterStartupScript(
                           this, this.GetType(),
                           "Alert",
                           "alert('User doesnt exist!');",
                           true
                       );
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(
                           this, this.GetType(),
                           "Alert",
                           "alert('Unsuccessfully!');",
                           true
                       );
                    }

                }
            }
        }
    }
}