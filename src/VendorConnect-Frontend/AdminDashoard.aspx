<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AdminDashoard.aspx.cs" Inherits="VendorConnect_Frontend.AdminDashoard" %>
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
                    <a href="#" class="nav-link">
                        <i class="fas fa-store"></i>
                        <span>Vendors</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="#" class="nav-link">
                        <i class="fas fa-calendar-alt"></i>
                        <span>All Events</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="#" class="nav-link">
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
                    <div class="card">
                        <div class="card-body">
                            <div class="card-icon vendors">
                                <i class="fas fa-store"></i>
                            </div>
                            <div class="card-title">Total Vendors</div>
                            <div class="card-value" runat="server" id="DisplayVendors"></div>
                            <div class="card-change positive">
                                <i class="fas fa-arrow-up me-1"></i> 12% increase
                            </div>
                        </div>
                    </div>
                    
                    <div class="card">
                        <div class="card-body">
                            <div class="card-icon events">
                                <i class="fas fa-calendar-alt"></i>
                            </div>
                            <div class="card-title">Active Events</div>
                            <div class="card-value">28</div>
                            <div class="card-change positive">
                                <i class="fas fa-arrow-up me-1"></i> 5% increase
                            </div>
                        </div>
                    </div>
                    
                    <div class="card">
                        <div class="card-body">
                            <div class="card-icon organizers">
                                <i class="fas fa-users"></i>
                            </div>
                            <div class="card-title">Organizers</div>
                            <div class="card-value" runat="server" id="DisplayOrga"></div>
                            <div class="card-change positive">
                                <i class="fas fa-arrow-up me-1"></i> 8% increase
                            </div>
                        </div>
                    </div>
                    
                    <div class="card">
                        <div class="card-body">
                            <div class="card-icon payments">
                                <i class="fas fa-credit-card"></i>
                            </div>
                            <div class="card-title">Revenue</div>
                            <div class="card-value">$24,580</div>
                            <div class="card-change positive">
                                <i class="fas fa-arrow-up me-1"></i> 15% increase
                            </div>
                        </div>
                    </div>
                </div>
                
                <!-- Recent Activity Section -->
                <h3 class="section-title">Recent Activity</h3>
                <div class="recent-activity">
                    <div class="activity-item">
                        <div class="activity-icon" style="background-color: var(--primary);">
                            <i class="fas fa-user-plus"></i>
                        </div>
                        <div class="activity-details">
                            <div class="activity-title">New vendor registered</div>
                            <div class="activity-time">5 minutes ago</div>
                        </div>
                        <span class="badge bg-success">New</span>
                    </div>
                    
                    <div class="activity-item">
                        <div class="activity-icon" style="background-color: var(--secondary);">
                            <i class="fas fa-calendar-check"></i>
                        </div>
                        <div class="activity-details">
                            <div class="activity-title">Event "Tech Conference 2023" approved</div>
                            <div class="activity-time">1 hour ago</div>
                        </div>
                    </div>
                    
                    <div class="activity-item">
                        <div class="activity-icon" style="background-color: #ff9800;">
                            <i class="fas fa-money-bill-wave"></i>
                        </div>
                        <div class="activity-details">
                            <div class="activity-title">Payment received from XYZ Corp</div>
                            <div class="activity-time">2 hours ago</div>
                        </div>
                    </div>
                    
                    <div class="activity-item">
                        <div class="activity-icon" style="background-color: #9c27b0;">
                            <i class="fas fa-file-invoice"></i>
                        </div>
                        <div class="activity-details">
                            <div class="activity-title">Monthly report generated</div>
                            <div class="activity-time">5 hours ago</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        // Toggle sidebar on mobile
        document.querySelector('.toggle-sidebar').addEventListener('click', function() {
            document.querySelector('.sidebar').classList.toggle('active');
        });
        
        // Close sidebar when clicking outside on mobile
        document.addEventListener('click', function(event) {
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
            link.addEventListener('click', function() {
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
