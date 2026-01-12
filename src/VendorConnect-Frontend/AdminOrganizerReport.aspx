<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AdminOrganizerReport.aspx.cs" Inherits="VendorConnect_Frontend.AdminOrganizerReport" %>
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
    <style type="text/css">
        .auto-style1 {
            height: 25px;
        }
        .auto-style2 {
            height: 36px;
        }
    </style>
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
                    <a href="AdminDashoard.aspx" class="nav-link active">
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
                <!-- Recent Activity Section -->
                <h3 class="section-title">Vendor Report</h3>
                <div class="recent-activity">
                    <table class="table table-hover ">
                        <thead>
                            <tr>
                                <th scope="col" class="auto-style1">Organization Name</th>
                                <th scope="col" class="auto-style1">Email</th>
                                <th scope="col" class="auto-style1">Joined</th>
                                <th scope="col" class="auto-style1">Total Events</th>
                                 <th scope="col" class="auto-style1">Completed</th>
                                 <th scope="col" class="auto-style1">Upcoming</th>
                                 <th scope="col" class="auto-style1">Vendors Approved</th>
                                 <th scope="col" class="auto-style1">Status</th>
                                <th scope="col" class="auto-style1">Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterReport" runat="server"
                                OnItemCommand="RepeaterReport_ItemCommand"  OnItemDataBound="RepeaterReport_ItemDataBound">
                                <itemtemplate>
                                    <tr>
                                        <td><%# Eval("OrganizationName") %></td>
                                        <td><%# Eval("Email") %></td>
                                        <td>
                                            <%# Convert.ToDateTime(Eval("CreatedAt")).ToString("d MMM yyyy").ToUpper() %>
                                        </td>
                                        <td><%# Eval("TotalEvents") %></td>
                                        <td><%# Eval("CompletedEvents") %></td>
                                        <td><%# Eval("UpcomingEvents") %></td>
                                        <td><%# Eval("ApprovedVendors") %></td>
                                        <td>
                                            <asp:Literal runat="server" ID="Status"></asp:Literal>
                                        </td>
                                        <td>
                                             <asp:Button ID="btnDeactivate"
                                                runat="server"
                                                CssClass="btn btn-danger btn-sm ms-1"
                                                Text="Deactivate"
                                                CommandName="Deactivate"
                                                CommandArgument='<%# Eval("UserID") %>'
                                                  Visible='<%# Eval("IsActive").ToString() == "True" %>' />

                                            <asp:Button ID="btnActivate"
                                                runat="server"
                                                CssClass="btn btn-primary btn-sm ms-1"
                                                Text="Activate"
                                                CommandName="Activate"
                                                CommandArgument='<%# Eval("UserID") %>'
                                                  Visible='<%# Eval("IsActive").ToString() == "False" %>' />
                                                
                                        </td>
                                    </tr>
                                </itemtemplate>
                            </asp:Repeater>

                        </tbody>

                    </table>
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