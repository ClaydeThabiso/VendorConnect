using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendorConnect_Frontend.ServiceReference1;

namespace VendorConnect_Frontend
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Service1Client client = new Service1Client();

            var name = FirstName.Value;
            var lastname = LastName.Value;
            var username = email.Value;
            var role = Convert.ToChar(roleSelect.Value);
            var pass = password.Value;

            var regU = client.RegisterUser(name,lastname,username, pass, role);
            int userID = regU;
            if (regU > 0)
            {
                if (role == 'A')
                {
                    lblMsg.Text = "Account created successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Response.Redirect("Login.aspx");
                }
                else if (role == 'V')
                {
                    
                    var Bname = Request.Form["CompanyName"];
                    var BEmail = Request.Form["businessEmail"];
                    var BPhone = Request.Form["businessPhone"];
                    var BCategory = Request.Form["businessCategory"];

                    var regV = client.registerVendor(Bname, BEmail, BCategory,BPhone,userID);
                   
                    if(regV==1)
                    {
                        lblMsg.Text = "Vendor account created successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        Response.Redirect("Login.aspx");
                    }
                    else if(regV==0)
                    {
                        
                        lblMsg.Text = "User already exist";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblMsg.Text = Bname + BEmail + BPhone + BCategory;
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else if (role == 'O')
                {
                    var Oname = Request.Form["OrgaName"];
                    var OEmail = Request.Form["OrgaEmail"];
                    var OPhone = Request.Form["OrgaPhone"];

                    var regO = client.registerOrganizer(Oname, OEmail, OPhone, userID );

                    if (regO == 1)
                    {
                        lblMsg.Text = "Organizer account created successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        Response.Redirect("Login.aspx");
                    }
                    else if (regO == 0)
                    {
                        lblMsg.Text = "User already exist";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblMsg.Text = " Oragnaizer account could not be created";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            else if(regU==0)
            {
                lblMsg.Text = "User already exist";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblMsg.Text = Convert.ToString( regU);
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }


            client.Close();
        }
    }
}