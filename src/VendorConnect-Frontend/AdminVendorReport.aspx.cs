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
            if(!IsPostBack)
            {
                Service1Client client = new Service1Client();
                var getInfo = client.VendorReport();
                
                if (getInfo != null)
                {
                    
                    RepeaterReport.DataSource = getInfo;
                    RepeaterReport.DataBind();
                }
               
                client.Close();
            }
        }
        protected void RepeaterReport_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var data = (AdminVendorReportDTO)e.Item.DataItem;

                Literal litApproved = (Literal)e.Item.FindControl("litApproved");
                Literal litDeclined = (Literal)e.Item.FindControl("litDeclined");

                Service1Client client = new Service1Client();

                int approved = client.GetTotalApprovedApplicationPerVendor(data.VendorID);
                int declined = client.GetTotalDeclinedApplicationPerVendor(data.VendorID);

                litApproved.Text = approved.ToString();
                litDeclined.Text = declined.ToString();

                client.Close();
            }
        }

    }
}