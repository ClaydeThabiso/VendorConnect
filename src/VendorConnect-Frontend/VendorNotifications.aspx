<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VendorNotifications.aspx.cs" Inherits="VendorConnect_Frontend.VendorNotifications" %>

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
                    <a href="VendorProfile.aspx" class="nav-link">
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

                <!-- Recent Activity Section -->
                <h3 class="section-title">Notifictions</h3>
                <div class="recent-activity">

                    <asp:Repeater ID="RepeaterNotifications"
                        runat="server"
                        OnItemCommand="RepeaterNotifications_ItemCommand">

                        <ItemTemplate>
                            <div class="card mb-2 notification-card <%# (bool)Eval("IsRead") ? "" : "border-primary" %>">
                                <div class="card-body d-flex align-items-start justify-content-between">

                                    <!-- LEFT -->
                                    <div class="d-flex">
                                        <div class="me-3">
                                            <i class="fas fa-bell fa-lg text-primary"></i>
                                        </div>

                                        <div>
                                            <div class="<%# (bool)Eval("IsRead") ? "" : "fw-bold" %>">
                                                <%# Eval("Title") %>
                                            </div>
                                            <small class="text-muted">
                                                <%# Eval("CreatedAt", "{0:dd MMM yyyy HH:mm}") %>
                                            </small>
                                        </div>
                                    </div>

                                    <!-- RIGHT -->
                                    <asp:LinkButton
                                        runat="server"
                                        CssClass="btn btn-outline-primary btn-sm"
                                        CommandName="Open"
                                        CommandArgument='<%# Eval("NotificationId") + "|" + Eval("RedirectUrl") %>'>
                        Open
                                    </asp:LinkButton>

                                </div>
                            </div>
                        </ItemTemplate>

                    </asp:Repeater>

                    <!-- EMPTY STATE -->
                    <asp:Panel ID="pnlNoNotifications" runat="server" Visible="false" CssClass="text-center text-muted mt-4">
                        <i class="fas fa-inbox fa-2x mb-2"></i>
                        <p>No notifications yet</p>
                    </asp:Panel>

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

