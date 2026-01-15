<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AdminReport.aspx.cs" Inherits="VendorConnect_Frontend.AdminReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                <small class="text-light">Admin Panel</small>
            </div>
            <div class="sidebar-menu">
                <div class="nav-item">
                    <a href="#" class="nav-link active">
                        <i class="fas fa-tachometer-alt"></i>
                        <span>Dashboard</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="AdminVendorReport.aspx" class="nav-link">
                        <i class="fas fa-store"></i>
                        <span>Vendors</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="AdminEventReport.aspx" class="nav-link">
                        <i class="fas fa-calendar-alt"></i>
                        <span>All Events</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="AdminOrganizerReport.aspx" class="nav-link">
                        <i class="fas fa-users"></i>
                        <span>Organizers</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="#" class="nav-link">
                        <i class="fas fa-credit-card"></i>
                        <span>Payments</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="#" class="nav-link">
                        <i class="fas fa-chart-bar"></i>
                        <span>Reports</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="#" class="nav-link">
                        <i class="fas fa-cog"></i>
                        <span>Settings</span>
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
                <h1>Admin Dashboard — VendorConnect</h1>
                <div class="header-actions">
                    <div class="user-info">
                        <div class="user-avatar">AD</div>
                        <div>
                            <div class="fw-bold">Admin User</div>
                            <small class="text-muted">Administrator</small>
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Content Area -->
            <div class="content-area">
                <!-- Dashboard Cards -->
               <div class="dashboard-cards">

    <!-- Total Events -->
    <div class="card">
        <div class="card-body">
            <div class="card-icon events">
                <i class="fas fa-calendar-alt"></i>
            </div>
            <div class="card-title">Total Events</div>
            <div class="card-value" runat="server" id="DisplayTotalEvents"></div>
            <div class="card-change positive">
                <i class="fas fa-arrow-up me-1"></i> Tracking live
            </div>
        </div>
    </div>

    <!-- Active Events -->
    <div class="card">
        <div class="card-body">
            <div class="card-icon events">
                <i class="fas fa-bolt"></i>
            </div>
            <div class="card-title">Active Events</div>
            <div class="card-value" runat="server" id="DisplayActiveEvents"></div>
            <div class="card-change positive">
                <i class="fas fa-arrow-up me-1"></i> Real-time
            </div>
        </div>
    </div>

    <!-- Completed Events -->
    <div class="card">
        <div class="card-body">
            <div class="card-icon organizers">
                <i class="fas fa-check-circle"></i>
            </div>
            <div class="card-title">Completed Events</div>
            <div class="card-value" runat="server" id="DisplayCompletedEvents"></div>
            <div class="card-change positive">
                <i class="fas fa-arrow-up me-1"></i> Updated
            </div>
        </div>
    </div>

    <!-- Total Vendor Applications -->
    <div class="card">
        <div class="card-body">
            <div class="card-icon payments">
                <i class="fas fa-file-alt"></i>
            </div>
            <div class="card-title">Vendor Applications</div>
            <div class="card-value" runat="server" id="DisplayApplications"></div>
            <div class="card-change positive">
                <i class="fas fa-arrow-up me-1"></i> Today’s stats
            </div>
        </div>
    </div>

</div>

                
                <!-- Recent Activity Section -->
                <h3 class="section-title">Recent Activity</h3>
                <div class="recent-activity">
              
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
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

</asp:Content>

