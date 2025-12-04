<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="VendorConnect_Frontend.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>VendorConnect - Register</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.13.1/font/bootstrap-icons.min.css">
    <!-- Google Fonts -->
    <link
        href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&family=Roboto:wght@300;400;500&display=swap"
        rel="stylesheet">

    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
         <link rel="stylesheet" href="css/style.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="login-page" class="login-container">
        <div class="login-card">
            <div class="login-header">
                <h2><i class="fas fa-handshake me-2"></i>VendorConnect</h2>
                <p class="mb-0">B2B Event Management Platform</p>
            </div>
            <div class="card-body p-4">
                <h4 class="text-center mb-4">Register Here</h4>
                <div class="mb-3 input-icon">
                    <i class="fas fa-user"></i>
                    <input type="text" class="form-control ps-5" id="FirstName" placeholder="First Name" runat="server">
                </div>
                <div class="mb-3 input-icon">
                    <i class="fas fa-user"></i>
                    <input type="text" class="form-control ps-5" id="LastName" placeholder="Last Name" runat="server">
                </div>
                <div class="mb-3 input-icon">
                    <i class="bi bi-envelope-fill"></i>
                    <input type="text" class="form-control ps-5" id="email" placeholder="Username or Email" runat="server">
                </div>
                <div class="mb-3 input-icon">
                    <i class="bi bi-option"></i>
                    <select class="form-select ps-5" id="roleSelect" runat="server" ClientIDMode="Static">
                        <option value=" ">Select a Role</option>
                        <option value="A">Admin</option>
                        <option value="V">Vendor</option>
                        <option value="O">Organizer</option>
                    </select>
                </div>
                <div id="additionalFields"></div>
                <div class="mb-3 input-icon">
                    <i class="fas fa-lock"></i>
                    <input type="password" class="form-control ps-5" id="password" placeholder="Password" runat="server">
                </div> 
                 <asp:Label runat="server" ID="lblMsg" Text=" " class="m-1"></asp:Label>
                <asp:Button class="btn btn-primary w-100 mt-4" runat="server" Text="Register" ID="btnRegister" OnClick="btnRegister_Click"></asp:Button>
                <div class="signup-link">
                    Already have an account? <a href="Login.aspx">Login now</a>
                </div>
            </div>
        </div>
    </div>

    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const roleSelect = document.getElementById('roleSelect');
            const additionalFields = document.getElementById('additionalFields');

            roleSelect.addEventListener('change', function () {
                const selectedRole = this.value;

                additionalFields.innerHTML = '';

                if (selectedRole === "V") {
                    additionalFields.innerHTML = `
                        <div class="additional-fields fade-in">
                            <h6 class="text-center"><i class="fas fa-store role-icon pe-2"></i>Vendor Information</h6>
                            <div class="mb-3 input-icon">
                                <i class="fas fa-building"></i>
                                <input type="text" class="form-control ps-5" placeholder="Company Name"  name="CompanyName">
                            </div>
                            <div class="mb-3 input-icon">
                                <i class="fas fa-tags"></i>
                                <select class="form-select ps-5" id="categorySelect" name="businessCategory">
                                    <option selected>Vendor Category</option>
                                    <option>Catering</option>
                                    <option>Audio/Visual</option>
                                    <option>Decoration</option>
                                    <option>Entertainment</option>
                                    <option>Other</option>
                                </select>
                            </div>
                            <div class="mb-3 input-icon">
                                <i class="bi bi-telephone-fill"></i>
                                <input type="text" class="form-control ps-5" placeholder="Business Number" name="businessPhone">
                            </div>
                            <div class="mb-3 input-icon">
                                <i class="bi bi-envelope-fill"></i>
                                <input type="email" class="form-control ps-5" placeholder="Business Email" name="businessEmail">
                            </div>
                        </div>
                    `;
                } else if (selectedRole === "O") {
                    additionalFields.innerHTML = `
                        <div class="additional-fields fade-in">
                            <h6 class="text-center"><i class="fas fa-calendar-alt role-icon pe-2"></i>Organizer Information</h6>
                            <div class="mb-3 input-icon">
                                <i class="fas fa-building"></i>
                                <input type="text" class="form-control ps-5" placeholder="Organization Name" name="OrgaName">
                            </div>
                            <div class="mb-3 input-icon">
                                <i class="bi bi-telephone-fill"></i>
                                <input type="text" class="form-control ps-5" placeholder="Oragnization Number" name="OrgaPhone">
                            </div>
                            <div class="mb-3 input-icon">
                                <i class="bi bi-envelope-fill"></i>
                                <input type="email" class="form-control ps-5" placeholder="Oragnization Email" name="OrgaEmail">
                            </div>
                        </div>
                    `;
                }
            });
        });

    </script>
</asp:Content>
