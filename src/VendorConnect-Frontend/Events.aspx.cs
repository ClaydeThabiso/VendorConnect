using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendorConnect_Frontend.ServiceReference1;

namespace VendorConnect_Frontend
{
    public partial class Events : System.Web.UI.Page
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

                dynamic ListEvents = client.GetEventPerOrganizer(Convert.ToInt32(Session["OrganizerId"]));
                if (ListEvents != null)
                {
                    EventsRepeater.DataSource = ListEvents;
                    EventsRepeater.DataBind();
                }
                else
                {
                    EventsRepeater = null;
                }

                client.Close();
            }
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateEvent.aspx");
        }
        protected void EventsRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Manage")
            {
                int eventId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("ManageEvent.aspx?EventId=" + eventId);
            }
        }
        protected void EventsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                dynamic dataItem = e.Item.DataItem;

                Label lbl = (Label)e.Item.FindControl("lblCompleted");
                Button btn = (Button)e.Item.FindControl("btnManage");

                if (dataItem.status.ToString() == "Completed" || dataItem.status.ToString()== "Cancelled")
                {
                    lbl.Text = dataItem.status;
                    lbl.Visible = true;
                    btn.Visible = false;
                }
                else
                {
                    lbl.Visible = false;
                    btn.Visible = true;
                }
            }
        }

    }
}