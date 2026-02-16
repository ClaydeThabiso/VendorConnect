<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VendorEventReport.aspx.cs" Inherits="VendorConnect_Frontend.VendorEventReport" %>

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
                    <a href="VendorEventReport.aspx" class="nav-link">
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
                    <div class="user-avatar" style="position: relative;">
                        <a class="btn position-relative"
                            href="VendorNotifications.aspx"
                            role="button"
                            id="notificationDropdown"
                            data-bs-toggle="dropdown"
                            aria-expanded="false">
                            <i class="fas fa-bell fa-lg"></i>
                            <span id="notifCount"
                                runat="server"
                                class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger"
                                visible="false"></span>
                        </a>
                    </div>
                </div>
            </div>

            <!-- Content Area -->
            <div class="content-area">
                <!-- Dashboard Cards -->
                <div class="content-area">

                    <!-- Dashboard Cards -->
                    <div class="dashboard-cards">
                        <div class="card">
                            <div class="card-body">
                                <div class="card-icon events">
                                    <i class="fas fa-paper-plane"></i>
                                </div>
                                <div class="card-title">Total Applications</div>
                                <div class="card-value" runat="server" id="displayTotAppli"></div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-body">
                                <div class="card-icon organizers">
                                    <i class="fas fa-check-circle"></i>
                                </div>
                                <div class="card-title">Approved</div>
                                <div class="card-value" runat="server" id="displayApproved"></div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-body">
                                <div class="card-icon vendors">
                                    <i class="fas fa-hourglass-half"></i>
                                </div>
                                <div class="card-title">Pending</div>
                                <div class="card-value" runat="server" id="displayPending"></div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-body">
                                <div class="card-icon payments">
                                    <i class="fas fa-times-circle"></i>
                                </div>
                                <div class="card-title">Declined</div>
                                <div class="card-value" runat="server" id="displayDeclined"></div>
                            </div>
                        </div>
                    </div>
                    <h3 class="section-title mt-5">Application Analytics</h3>

                    <div class="row">
                        <div class="col-lg-6 mb-4">
                            <div class="card">
                                <div class="card-body">
                                    <h5 class="card-title">Application Status</h5>
                                    <canvas id="statusChart" height="220"></canvas>
                                </div>
                            </div>
                        </div>

                        <!-- Applications Over Time -->
                        <div class="col-lg-6 mb-4">
                            <div class="card">
                                <div class="card-body">
                                    <h5 class="card-title">Applications Over Time</h5>
                                    <canvas id="monthlyChart" height="220"></canvas>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-6 mb-4">
                            <div class="card">
                                <div class="card-body">
                                    <h5 class="card-title">Monthly outcome</h5>
                                    <canvas id="outcomeTrendChart" height="220"></canvas>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        // Toggle sidebar on mobile
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

            const statusChart = new Chart(document.getElementById('statusChart'), {
                type: 'doughnut',
                data: {
                    labels: ['Approved', 'Pending', 'Declined'],
                    datasets: [{
                        data: [
                    <%= ViewState["Approved"] %>,
                    <%= ViewState["Pending"] %>,
                    <%= ViewState["Declined"] %>
                        ]
                    }]
                }
            });

            const monthlyData = <%= 
        new System.Web.Script.Serialization.JavaScriptSerializer()
        .Serialize(ViewState["MonthlyStats"]) %>;

            const labels = monthlyData.map(m => m.Month);
            const values = monthlyData.map(m => m.Count);

            new Chart(document.getElementById('monthlyChart'), {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Applications per Month',
                        data: values
                    }]
                }
            });

            const outcomes = <%= 
        new System.Web.Script.Serialization.JavaScriptSerializer()
        .Serialize(ViewState["MonthlyOutcomes"]) %>;

            const months = outcomes.map(o => o.Month);
            const approved = outcomes.map(o => o.Approved);
            const declined = outcomes.map(o => o.Declined);

            new Chart(document.getElementById('outcomeTrendChart'), {
                type: 'line',
                data: {
                    labels: months,
                    datasets: [
                        {
                            label: 'Approved',
                            data: approved,
                            tension: 0.3
                        },
                        {
                            label: 'Declined',
                            data: declined,
                            tension: 0.3
                        }
                    ]
                }
            });
        });
    </script>
</asp:Content>
