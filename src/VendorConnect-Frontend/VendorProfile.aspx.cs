using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendorConnect_Frontend.ServiceReference1;

namespace VendorConnect_Frontend
{
    public partial class VendorProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Service1Client client = new Service1Client();
            if (!IsPostBack)
            {
               

                int userID = Convert.ToInt32(Session["UserID"]);
                var u = client.GetUser(userID);
                if (u != null)
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

                var ven = client.GetVendor(VendorID);
               
                if(ven!=null)
                {
                    FirstName.Value = u.FirstName;
                    LastName.Value = u.LastName;
                    password.Value = u.Password;
                    email.Value = u.Username;
                    CompanyName.Value = ven.BusinessName;
                    businessEmail.Value = ven.ContactEmail;
                    categorySelect.Value = ven.Category;
                    businessPhone.Value = ven.phone;
                }  
            }
            client.Close();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Service1Client client = new Service1Client();

            int userID = Convert.ToInt32(Session["UserID"]);
            var info = client.updateVendorProfile(userID, FirstName.Value, LastName.Value, password.Value, email.Value, CompanyName.Value, categorySelect.Value, businessEmail.Value, businessPhone.Value);
            if(info==true)
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