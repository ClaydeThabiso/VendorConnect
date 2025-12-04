<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CreateEvent.aspx.cs" Inherits="VendorConnect_Frontend.CreateEvent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
          .login-container {
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            background: linear-gradient(135deg, var(--primary), var(--dark));
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
                <small class="text-light">Event Organizer Panel</small>
            </div>
            <div class="sidebar-menu">
                <div class="nav-item">
                    <a href="OrganizerDashboard.aspx" class="nav-link active">
                        <i class="fas fa-tachometer-alt"></i>
                        <span>Dashboard</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="#" class="nav-link">
                        <i class="bi bi-calendar"></i>
                        <span>My Events</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="#" class="nav-link">
                        <i class="bi bi-journal"></i>
                        <span>Vendor Applications</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="#" class="nav-link">
                        <i class="bi bi-clipboard-data"></i>
                        <span>Event reports</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="#" class="nav-link">
                        <i class="fas fa-credit-card"></i>
                        <span>Tickets sales</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="#" class="nav-link">
                        <i class="bi bi-person-lines-fill"></i>
                        <span>profile</span>
                    </a>
                </div>
                <div class="nav-item mt-4">
                    <a href="Login.aspx" class="nav-link">
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
                <h1>Event Organizer Dashboard — VendorConnect</h1>
                <div class="header-actions">
                     <div class="user-info">
                        <div class="user-avatar" runat="server" id="initials"></div>
                        <div>
                            <div class="fw-bold" runat="server" id="OrgaNames"></div>
                            <small class="text-muted">Event Organizer</small>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Content Area -->
            <div class="content-area">
                <div id="login-page" class="login-container">
                    <div class="login-card">
                        <div class="card-body p-4">
                            <h4 class="text-center mb-4">Create Event</h4>
                            <div class="mb-3 input-icon">
                                <i class="fas fa-user"></i>
                                <input type="text" class="form-control ps-5" id="EventName" placeholder="Event Name" runat="server">
                            </div>
                            <div class="mb-3 input-icon">
                                <i class="bi bi-calendar"></i>
                                <input type="date" class="form-control ps-5" id="EventDate" placeholder="Event date" runat="server">
                            </div>
                             <div class="mb-3 input-icon">
                                <i class="bi bi-pencil-square"></i>
                                <input type="text" class="form-control ps-5" id="EventDescription" placeholder="Event Description" runat="server">
                            </div>
                            <div class="mb-3 input-icon">
                                <i class="bi bi-geo-alt"></i>
                                <input type="text" class="form-control ps-5" id="EventLocation" placeholder="Event Location" runat="server">
                            </div>
                            <div class="mb-3 input-icon">
                                <i class="fas fa-users"></i>
                                <input type="text" class="form-control ps-5" id="NumVendors"
                                    placeholder="Number of vendors" runat="server">
                            </div>
                             <asp:Label runat="server" ID="lblMsg" Text=" " class="m-1"></asp:Label>
                            <asp:Button id="btnCreateEvent" class="btn btn-primary w-100 mt-4" runat="server" Text="+ Create" OnClick="btnCreateEvent_Click"/> 
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
