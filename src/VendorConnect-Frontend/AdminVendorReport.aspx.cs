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
    }
}