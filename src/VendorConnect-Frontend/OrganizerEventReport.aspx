<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="OrganizerEventReport.aspx.cs" Inherits="VendorConnect_Frontend.OrganizerEventReport" %>
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
                    <a href="AdminDashoard.aspx" class="nav-link active">
                        <i class="fas fa-tachometer-alt"></i>
                        <span>Dashboard</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="Events.aspx" class="nav-link">
                        <i class="bi bi-calendar"></i>
                        <span>My Events</span>
                    </a>
                </div>
                <div class="nav-item">
                    <a href="OrganizerApplications.aspx" class="nav-link">
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
                    <a href="OrganizerProfile.aspx" class="nav-link">
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
                <!-- Dashboard Cards -->
                <h3 class="section-title">Event Report</h3>
                <div class="recent-activity">
                     <table class="table table-hover ">
                        <thead>
                            <tr>
                                <th scope="col" class="auto-style1">Event Name</th>
                                <th scope="col" class="auto-style1">Event Date</th>
                                <th scope="col" class="auto-style1">Event Location</th>
                                <th scope="col" class="auto-style1">Event Status</th>
                                <th scope="col" class="auto-style1">Total Applied Vendors</th>
                                <th scope="col" class="auto-style1">Total Approved</th>
                                <th scope="col" class="auto-style1">Total Declined</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="RepeaterReport" runat="server">
                                <itemtemplate>
                                    <tr>
                                        <td><%# Eval("EventName") %></td>
                                        <td>
                                            <%# Convert.ToDateTime(Eval("EventDate")).ToString("d MMM yyyy").ToUpper() %>
                                        </td>
                                        <td><%# Eval("EventLocation") %></td>
                                       
                                        <td>
                                            <span class='badge 
                                            <%# Eval("EventStatus").ToString() == "Completed" ? "bg-sucess" :
                                                Eval("EventStatus").ToString() == "Active" ? "bg-primary" :
                                                Eval("EventStatus").ToString() == "Cancelled" ? "bg-danger" :
                                                "bg-warning" %>'>
                                                <%# Eval("EventStatus") %>
                                            </span>
                                        </td>

                                        <td><%# Eval("TotalApplied") %></td>
                                        <td><%# Eval("TotalApproved") %></td>
                                        <td><%# Eval("TotalDeclined") %></td>
                                        <td>
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

