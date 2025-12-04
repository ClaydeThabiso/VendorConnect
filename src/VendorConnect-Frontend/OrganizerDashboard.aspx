<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="OrganizerDashboard.aspx.cs" Inherits="VendorConnect_Frontend.OrganizerDashboard" %>
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
                    <a href="#" class="nav-link active">
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
                <!-- Dashboard Cards -->
                <div class="dashboard-cards">
                    <div class="card">
                        <div class="card-body">
                            <div class="card-icon vendors">
                                <i class="fas fa-store"></i>
                            </div>
                            <div class="card-title">Total Events</div>
                            <div class="card-value" runat="server" id="totalEvent"></div>
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
                            <div class="card-title">Upcoming Events</div>
                            <div class="card-value"></div>
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
                            <div class="card-title">Active Vendors</div>
                            <div class="card-value">67</div>
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
                            <div class="card-title">Total Revenue</div>
                            <div class="card-value">$24,580</div>
                            <div class="card-change positive">
                                <i class="fas fa-arrow-up me-1"></i> 15% increase
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Recent Activity Section -->
                <h3 class="section-title">My events</h3>
                <div class="d-flex justify-content-end">
                    <asp:Button class="btn btn-outline-primary btn-sm mb-3" runat="server" Text="+ Create event" ID="btnCreate" OnClick="btnCreate_Click"> </asp:Button>
                </div>
                <div class="recent-activity">
                    <table class="table table-hover ">
                        <thead>
                            <tr>
                                <th scope="col">Event Name</th>
                                <th scope="col"> Event Date</th>
                                <th scope="col">Event Location</th>
                                <th scope="col"> Max Vendors</th>
                                <th scope="col">Status</th>
                                <th scope="col">Actions</th>
                            </tr>
                        </thead>
                        <tbody runat="server" id="TableData">
                           <asp:Repeater runat="server" ID="RepeaterEvents" OnItemCommand="RepeaterEvents_ItemCommand">
                               <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("EventName") %></td>

                                        <td>
                                            <%# Convert.ToDateTime(Eval("EventDate")).ToString("d MMM yyyy").ToUpper() %>
                                        </td>
                                        <td><%# Eval("Location") %></td>
                                        <td>
                                            <%# Eval("MaxVendors") %>
                                        </td>
                                        <td>
                                                <span class="badge 
                                                <%# 
                                                    Eval("Status").ToString() == "Completed" ? "bg-warning" :
                                                    Eval("Status").ToString() == "Active" ? "bg-success" :
                                                    "bg-secondary"
                                                %>">
                                                <%# Eval("Status")%>
                                            </span>

                                        </td>

                                        <td>
                                            <asp:Button ID="btnManage"
                                                runat="server"
                                                CssClass="btn btn-outline-primary btn-sm ms-1"
                                                Text="Manage"
                                                CommandName="Manage"
                                                CommandArgument='<%# Eval("EventId") %>'
                                                />
                                        </td>
                                    </tr>
                               </ItemTemplate>
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
