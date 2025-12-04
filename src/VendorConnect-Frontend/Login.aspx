<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="VendorConnect_Frontend.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>VendorConnect - Login</title>
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
<asp:Content ID="Content2" ContentPlaceHolderID="DashboardStyling" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="login-page" class="login-container">
        <div class="login-card">
            <div class="login-header">
                <h2><i class="fas fa-handshake me-2"></i>VendorConnect</h2>
                <p class="mb-0">B2B Event Management Platform</p>
            </div>
            <div class="card-body p-4">
                <h4 class="text-center mb-4">Login Here</h4>
               <div class="mb-3 input-icon">
                    <i class="fas fa-user"></i>
                    <input type="text" class="form-control ps-5" id="username" placeholder="Username or Email" runat="server">
                </div>
                
                <div class="mb-3 input-icon">
                    <i class="fas fa-lock"></i>
                    <input type="password" class="form-control ps-5" id="password" placeholder="Password" runat="server">
                </div>
                <asp:Label runat="server" ID="lblMsg" Text=" " class="m-1"></asp:Label>
                <asp:Button class="btn btn-primary w-100 mt-4" Text="Login" runat="server" ID="btnLgin" OnClick="btnLgin_Click"></asp:Button>
                <div class="signup-link">
                    Don't have an account? <a href="Register.aspx">Sign up now</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
