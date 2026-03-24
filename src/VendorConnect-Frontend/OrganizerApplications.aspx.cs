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
            if(events!=null)
            {
                ApplicationsData.DataSource = events;
                ApplicationsData.DataBind();
            }
            else
            {
                Display.Text = "No Applications yet";
            }
           

            client.Close();
        }


        protected void ApplicationsData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int eventId = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "EventId"));
                int maxVendors = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "MaxVendors"));
                Repeater nestedRepeater = (Repeater)e.Item.FindControl("VendorApplicationsRepeater");

                if (nestedRepeater != null)
                {
                    Service1Client client = new Service1Client();
                    try
                    {
                        var approvedVendors = client.getApprovedApplication(eventId);
                        Literal maxVendorLiteral = (Literal)e.Item.FindControl("MaxVendorLiteral");
                        if (maxVendorLiteral != null)
                        {
                            maxVendorLiteral.Text = $"Max Vendors: {approvedVendors}/{maxVendors}";
                        }
                        int organizerID = Convert.ToInt32(Session["OrganizerId"]);
                        dynamic allApplications = client.GetApplicationsPerOrganizer(organizerID);

                        var applicationsForEvent = ((IEnumerable<dynamic>)allApplications)
                                                   .Where(a => a.EventId == eventId)
                                                   .ToList();

                        if(applicationsForEvent!=null)
                        {
                            nestedRepeater.DataSource = applicationsForEvent;
                            nestedRepeater.DataBind();
                            pnlNoNotifications.Visible = false;
                        }
                        else
                        {
                            nestedRepeater.DataSource = null;
                            nestedRepeater.DataBind();
                            pnlNoNotifications.Visible = true;
                        }
                        
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
            using (Service1Client client = new Service1Client())
            {
                int applicationId = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Accept")
                {
                    var acceptApplication = client.AccepptApplication(applicationId);

                    if (acceptApplication != null)
                    {
                        int vendorId = acceptApplication.VendorId;

                        var eventId = acceptApplication.EventId;
                        var eventdetails = client.GetEvent(eventId);
                        var eventFee = Convert.ToDecimal(eventdetails.Fee);

                        client.CreatePayment(vendorId, eventId, eventFee);

                        var vendor = client.GetVendor(vendorId); 

                        if (vendor != null)
                        {
                            int vendorUserId = vendor.UserID;

                            client.SendNotification(
                                vendorUserId,
                                'V',
                                "Application Approved",
                                "Your application has been approved",
                                "VendorApplication.aspx"
                            );

                            ScriptManager.RegisterStartupScript(
                                this, this.GetType(),
                                "successAlert",
                                "alert('Successfully accepted the application!');",
                                true
                            );
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(
                            this, this.GetType(),
                            "technicalIssue",
                            "alert('Couldn't accept application!');",
                            true
                        );
                    }
                }
                else if (e.CommandName == "Decline")
                {
                    var declineApplication = client.DeclineApplication(applicationId);

                    if (declineApplication != null)
                    {
                        int vendorId = declineApplication.VendorId;

                        var vendor = client.GetVendor(vendorId);

                        if (vendor != null)
                        {
                            int vendorUserId = vendor.UserID;

                            client.SendNotification(
                                vendorUserId,
                                'V',
                                "Application Declined",
                                "Your application has been declined",
                                "VendorApplication.aspx"
                            );

                            ScriptManager.RegisterStartupScript(
                                this, this.GetType(),
                                "successAlert",
                                "alert('Successfully declined the application!');",
                                true
                            );
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(
                            this, this.GetType(),
                            "technicalIssue",
                            "alert('Couldn't decline application!');",
                            true
                        );
                    }
                }

                BindApplications();
            }
        }

    }
}