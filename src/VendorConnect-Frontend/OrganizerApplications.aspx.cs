using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using VendorConnect_Frontend.ServiceReference1;

namespace VendorConnect_Frontend
{
    public partial class OrganizerApplications : System.Web.UI.Page
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

                BindApplications();

                client.Close();
            }
        
        }
        private void BindApplications()
        {
            Service1Client client = new Service1Client();

            int organizerID = Convert.ToInt32(Session["OrganizerId"]);
            dynamic allApplications = client.GetApplicationsPerOrganizer(organizerID);
            var events = ((IEnumerable<dynamic>)allApplications)
                         .GroupBy(a => a.EventId)
                         .Select(g => g.First())
                         .ToList();

            ApplicationsData.DataSource = events;
            ApplicationsData.DataBind();

            client.Close();
        }


        protected void ApplicationsData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int eventId = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "EventId"));
                Repeater nestedRepeater = (Repeater)e.Item.FindControl("VendorApplicationsRepeater");

                if (nestedRepeater != null)
                {
                    Service1Client client = new Service1Client();
                    try
                    {
                        int organizerID = Convert.ToInt32(Session["OrganizerId"]);
                        dynamic allApplications = client.GetApplicationsPerOrganizer(organizerID);

                        var applicationsForEvent = ((IEnumerable<dynamic>)allApplications)
                                                   .Where(a => a.EventId == eventId)
                                                   .ToList();

                        nestedRepeater.DataSource = applicationsForEvent;
                        nestedRepeater.DataBind();
                    }
                    finally
                    {
                        client.Close();
                    }
                }
            }
        }


        protected void VendorApplicationsRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Service1Client client = new Service1Client();
            var applicationId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName=="Accept")
            {
               
                var acceptApplication = client.AccepptApplication(applicationId);

                if(acceptApplication !=null)
                {
                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "successAlert",
                        "alert('Successfully accepted the application!');",
                        true
                    );
                }
                else {
                    ScriptManager.RegisterStartupScript(
                        this, this.GetType(),
                        "technicalIssue",
                        "alert('Couldnt accept application!');",
                        true
                    );
                }

                
            }
            else if(e.CommandName=="Decline")
            {
               
                var declineApplication = client.DeclineApplication(applicationId);
                if(declineApplication !=null)
                {
                    ScriptManager.RegisterStartupScript(
                       this, this.GetType(),
                       "successAlert",
                       "alert('Successfully declined the application!');",
                       true
                   );
                }
                else
                {
                    ScriptManager.RegisterStartupScript(
                       this, this.GetType(),
                       "technicalIssue",
                       "alert('Couldnt accept application!');",
                       true
                   );
                }
            }

            BindApplications();
            client.Close();
        }
    }
}