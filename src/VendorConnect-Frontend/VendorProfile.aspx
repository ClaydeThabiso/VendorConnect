<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VendorProfile.aspx.cs" Inherits="VendorConnect_Frontend.VendorProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
     .login-container {
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 20px;
            width: 100%;
            border-radius: 10px;
        }
         .login-card {
            border-radius: 16px;
            box-shadow: 0 10px 30px rgba(0,0,0,0.2);
            overflow: hidden;
            width: 100%;
            max-width: 400px;
            background-color: var(--light);
        }
        
        .login-header {
            background: var(--primary);
            color: var(--light);
            padding: 2rem;
            text-align: center;
        }
        
        .login-header h2 {
            font-weight: 700;
            margin-bottom: 0.5rem;
        }
        
        .login-header p {
            opacity: 0.9;
            font-weight: 300;
            margin-bottom: 0;
        }
        
        .card-body {
            padding: 2rem;
        }
        
        .input-icon {
            position: relative;
        }
        
        .input-icon i {
            position: absolute;
            left: 1rem;
            top: 50%;
            transform: translateY(-50%);
            color: var(--primary);
            z-index: 2;
            width: 16px;

        }
        
        .form-control {
            padding: 0.75rem 1rem 0.75rem 3rem; 
            border-radius: 10px;
            border: 1px solid #e1e5ee;
            transition: all 0.3s;
            font-size: 0.95rem;
            background-color: var(--light);
        }

        .form-control:focus {
            border-color: var(--primary);
            box-shadow: 0 0 0 0.2rem rgba(75, 73, 172, 0.25);
        }
        .btn-primary {
            background: linear-gradient(135deg, var(--primary), var(--dark));
            border: none;
            padding: 0.75rem;
            border-radius: 10px;
            font-weight: 500;
            transition: all 0.3s;
            color: var(--light);
        }
        
        .btn-primary:hover {
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(75, 73, 172, 0.4);
            background: linear-gradient(135deg, var(--dark), var(--primary));
            color: var(--light);
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DashboardStyling" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.13.1/font/bootstrap-icons.min.css">
    <!-- Google Fonts -->
    <link
        href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&family=Roboto:wght@300;400;500&display=swap"
        rel="stylesheet">

    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <link href="css/Admin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard-container">
        <!-- Sidebar Navigation -->
        <div class="sidebar">
            <div class="sidebar-header">
                <h3><i class="fas fa-handshake me-2"></i>VendorConnect</h3>
                <small class="text-light">Vendor Panel</small>
            </div>
            <div class="sidebar-menu">
                <div class="nav-item">
                    <a href="VendorDashboard.aspx" class="nav-link active">
                        <i class="fas fa-tachometer-alt"></i>
                        <span>Dashboard</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="FindEvents.aspx" class="nav-link">
                        <i class="bi bi-search"></i>
                        <span>Find Events</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="VendorApplications.aspx" class="nav-link">
                        <i class="bi bi-journal"></i>
                        <span>My Applications</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="#" class="nav-link">
                        <i class="bi bi-clipboard-data"></i>
                        <span>Analytics</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="#" class="nav-link">
                        <i class="fas fa-credit-card"></i>
                        <span>Invoices</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="#" class="nav-link">
                        <i class="bi bi-person-lines-fill"></i>
                        <span>profile</span>
                    </a>
                </div>
                <div class="nav-item mt-4">
                    <a href="LogOut.aspx" class="nav-link">
                        <i class="fas fa-sign-out-alt"></i>
                        <span>Logout</span>
                    </a>
                </div>
            </div>
        </div>

        <!-- Main Content -->
        <div class="main-content">
            <!-- Header -->
            <div class="header">
                <button class="toggle-sidebar">
                    <i class="fas fa-bars"></i>
                </button>
                <h1>Vendor Dashboard — VendorConnect</h1>
                <div class="header-actions">
                    <div class="user-info">
                        <div class="user-avatar" runat="server" id="initials"></div>
                        <div>
                            <div class="fw-bold" runat="server" id="VendorNames"></div>
                            <small class="text-muted">Vendor</small>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Content Area -->
            <div class="content-area">
                <!-- Dashboard Cards -->
                <div class="recent-activity">
                    <div id="login-page" class="login-container">
                        <div class="login-card">
                            <div class="card-body p-4">
                                  <h6 class="text-center">Personal Information</h6>
                                <div class="mb-3 input-icon">
                                    <i class="fas fa-user"></i>
                                    <input type="text" class="form-control ps-5" id="FirstName" placeholder="First Name" runat="server" required>
                                </div>
                                <div class="mb-3 input-icon">
                                    <i class="fas fa-user"></i>
                                    <input type="text" class="form-control ps-5" id="LastName" placeholder="Last Name" runat="server" required>
                                </div>
                                <div class="mb-3 input-icon">
                                    <i class="bi bi-envelope-fill"></i>
                                    <input type="text" class="form-control ps-5" id="email" placeholder="Username or Email" runat="server" required>
                                </div>
                                 <div class="mb-3 input-icon">
                                    <i class="fas fa-lock"></i>
                                    <input type="password" class="form-control ps-5" id="password" placeholder="Password" runat="server" required>
                                </div>
                                <div id="additionalFields">
                                    <div class="additional-fields fade-in">
                                        <h6 class="text-center"><i class="fas fa-store role-icon pe-2"></i>Vendor Information</h6>
                                        <div class="mb-3 input-icon">
                                            <i class="fas fa-building"></i>
                                            <input type="text" class="form-control ps-5" placeholder="Company Name" id="CompanyName" runat="server">
                                        </div>
                                        <div class="mb-3 input-icon">
                                            <i class="fas fa-tags"></i>
                                            <input type="text" class="form-control ps-5" id="categorySelect" placeholder="business Category" runat="server">
                                        </div>
                                        <div class="mb-3 input-icon">
                                            <i class="bi bi-telephone-fill"></i>
                                            <input type="text" class="form-control ps-5" placeholder="Business Number" id="businessPhone" runat="server">
                                        </div>
                                        <div class="mb-3 input-icon">
                                            <i class="bi bi-envelope-fill"></i>
                                            <input type="email" class="form-control ps-5" placeholder="Business Email" id="businessEmail" runat="server">
                                        </div>
                                    </div>
                                </div>
                                <asp:Label runat="server" ID="lblMsg" Text=" " class="m-1"></asp:Label>
                                <asp:Button class="btn btn-primary w-100 mt-4" runat="server" Text="Save changes" ID="btnSave" OnClick="btnSave_Click"></asp:Button>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        </div>

        <script>
            // Toggle sidebar on mobile
            document.querySelector('.toggle-sidebar').addEventListener('click', function () {
                document.querySelector('.sidebar').classList.toggle('active');
            });

            // Close sidebar when clicking outside on mobile
            document.addEventListener('click', function (event) {
                const sidebar = document.querySelector('.sidebar');
                const toggleBtn = document.querySelector('.toggle-sidebar');

                if (window.innerWidth <= 992 &&
                    !sidebar.contains(event.target) &&
                    !toggleBtn.contains(event.target)) {
                    sidebar.classList.remove('active');
                }
            });

            // Update active nav link
            document.querySelectorAll('.nav-link').forEach(link => {
                link.addEventListener('click', function () {
                    document.querySelectorAll('.nav-link').forEach(l => l.classList.remove('active'));
                    this.classList.add('active');

                    // Close sidebar on mobile after selection
                    if (window.innerWidth <= 992) {
                        document.querySelector('.sidebar').classList.remove('active');
                    }
                });
            });
        </script>
</asp:Content>
