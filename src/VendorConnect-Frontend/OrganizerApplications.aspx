<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="OrganizerApplications.aspx.cs" Inherits="VendorConnect_Frontend.OrganizerApplications" %>

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
                <!-- Recent Activity Section -->
                <h3 class="section-title">My Applications</h3>
                <div class="accordion" id="accordionContainer">
                    <asp:Repeater ID="ApplicationsData" runat="server"
                        OnItemDataBound="ApplicationsData_ItemDataBound"
                        OnItemCommand="ApplicationsData_ItemCommand">
                        <ItemTemplate>
                            <div class="accordion-item mb-2">
                                <!-- Accordion Header -->
                                <h2 class="accordion-header" id="heading<%# Container.ItemIndex %>">
                                    <button class="accordion-button collapsed" type="button"
                                        data-bs-toggle="collapse"
                                        data-bs-target="#collapse<%# Container.ItemIndex %>"
                                        aria-expanded="false"
                                        aria-controls="collapse<%# Container.ItemIndex %>">
                                        <div class="activity-item">
                                            <div class="activity-icon" style="background-color: var(--primary);">
                                                <i class="fas fa-calendar-check"></i>
                                            </div>
                                            <div class="activity-details">
                                                <div class="activity-title"><%# Eval("EventName") %></div>
                                                <div class="activity-time">
                                                    <i class="bi bi-geo-alt"></i>
                                                    <%# Eval("Location") %>
                                                </div>
                                                <div class="activity-time">
                                                    <i class="bi bi-calendar"></i>
                                                    <%# Convert.ToDateTime(Eval("EventDate")).ToString("d MMM yyyy").ToUpper() %>
                                                </div>
                                                <div class="activity-time">
                                                    <span class="badge <%# 
    Eval("status").ToString() == "Completed" ? "bg-warning" :
    Eval("status").ToString() == "Active" ? "bg-success" :
    "bg-secondary" %>">
                                                        <%# Eval("status") %>
                                                    </span>

                                                </div>
                                                <div class="activity-time">
                                                    <i class="fas fa-user"></i>Max Vendor: <%# Eval("MaxVendors") %>
                                                </div>
                                            </div>
                                        </div>
                                    </button>
                                </h2>
                                <!-- Accordion Body -->
                                <div id="collapse<%# Container.ItemIndex %>" class="accordion-collapse collapse"
                                    aria-labelledby="heading<%# Container.ItemIndex %>"
                                    data-bs-parent="#accordionContainer">
                                    <div class="accordion-body">
                                        <asp:Repeater ID="VendorApplicationsRepeater" runat="server">
                                            <ItemTemplate>
                                                <div class="activity-item mb-2">
                                                    <div class="activity-icon" style="background-color: red;">
                                                        <i class="fas fa-user"></i>
                                                    </div>
                                                    <div class="activity-details">
                                                        <div class="activity-title"><%# Eval("BusinessName") %></div>
                                                        <div class="activity-time">
                                                            <i class="bi bi-option"></i>
                                                            <%# Eval("Category") %>
                                                        </div>
                                                        <div class="activity-time">
                                                            <i class="bi bi-calendar"></i>
                                                            <%# Convert.ToDateTime(Eval("AppliedAt")).ToString("d MMM yyyy").ToUpper() %>
                                                        </div>
                                                    </div>
                                                    <asp:Button CssClass="btn btn-outline-primary btn-sm ms-2"
                                                        Text="Accept"
                                                        CommandName="Accept"
                                                        CommandArgument='<%# Eval("ApplicationId") %>'
                                                        runat="server" />
                                                    <asp:Button CssClass="btn btn-outline-danger btn-sm ms-2"
                                                        Text="Decline"
                                                        CommandName="Decline"
                                                        CommandArgument='<%# Eval("ApplicationId") %>'
                                                        runat="server" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

            </div>
        </div>
    </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

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
