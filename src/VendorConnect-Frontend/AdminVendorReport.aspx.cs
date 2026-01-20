using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendorConnect_Frontend.ServiceReference1;

namespace VendorConnect_Frontend
{
    public partial class AdminVendorReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || (char)Session["LoggedIn"] != 'A')
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadVendorReport();
            }
        }
        private void LoadVendorReport()
        {
            using (Service1Client client = new Service1Client())
            {
                RepeaterReport.DataSource = client.VendorReport();
                RepeaterReport.DataBind();
            }
        }
        protected void RepeaterReport_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var data = (AdminVendorReportDTO)e.Item.DataItem;

                Literal litStatus = (Literal)e.Item.FindControl("Status");
                if (data.IsActive == true)
                {
                    litStatus.Text = "<span class='badge bg-success'>Active</span>"; ;
                }
                else if (data.IsActive == false)
                {
                    litStatus.Text = "<span class='badge bg-danger'>Inactive</span>";

                }
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
                        LoadVendorReport();
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
                        LoadVendorReport();
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