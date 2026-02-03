using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace VnedorConnect_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        int RegisterUser(string name,string lastName ,string username, string password, char role);

        [OperationContract]
        UserDTO login(string username, string password);

        [OperationContract]
        int registerOrganizer(string name, string email, string phone,int UserID);
        [OperationContract]
        int registerVendor(string name, string email, string category, string phone,int UserID);

        [OperationContract]
        User GetUser(int id);

        [OperationContract]
        VendorDTO GetVendor(int id);

        [OperationContract]
        OrganizerDTO GetOrganizer(int id);

        [OperationContract]
        List<User> GetUsers();

        [OperationContract]
        int totalVendors();

        [OperationContract]
        int totalOrganizers();

        [OperationContract]
        int CreateEvent(string name, DateTime eventDate, string location, int maxVendors, string description, int OrganizerID);

        [OperationContract]
        List<Event> GetEvents();

        [OperationContract]
        Event GetEvent(int id);

        [OperationContract]
        List<Event> GetEventPerOrganizer(int id);

        [OperationContract]
        OrganizerDTO GetOrganizerByUserId(int userId);

        [OperationContract]
        int getTotalEventPerOrganizer(int id);

        [OperationContract]
        VendorDTO GetVendorByUserId(int userID);

        [OperationContract]
        int EventApplication(int vendorID, int eventID);

        [OperationContract]
        List<VendorApplicationDTO> GetApplicationPerVendor(int vendorID);

        [OperationContract]
        int deleteApplication(int AppId);

        [OperationContract]
        List<VendorApplicationDTO> GetApplicationsPerOrganizer(int OrgaID);

        [OperationContract]
        VendorApplicationDTO DeclineApplication(int ApplicationId);

        [OperationContract]
        VendorApplicationDTO AccepptApplication(int ApplicationId);

        [OperationContract]
        int getApprovedApplication(int eventId);

        [OperationContract]
        int getTotalVendorApplicationPerVendo(int id);

        [OperationContract]
        int getTotAcceptVendorApplication(int id);

        [OperationContract]
        int CancelEvent(int eventId);

        [OperationContract]
        int UpdateEvent(int eventId, string name, DateTime date, string location, string description, int maxVendors);

        [OperationContract]
        int getTotalUpcomingEvents(int id);

        [OperationContract]
        bool updateVendorProfile(int id, string FirstName, string LastName, string password,string email, string BusinessName, string category, string ContactEmail, string phone);

        [OperationContract]
        bool updateOrganizerProfile(int id, string FirstName, string LastName, string password,string email, string OrganizationName, string ContactEmail, string phone);

        [OperationContract]
        int getUpcomingEvents();

        [OperationContract]
        List<AdminVendorReportDTO> VendorReport();

        [OperationContract]
        List<AdminOragnizerReportDTO> OragnizerReport();

        [OperationContract]
        List<AdminEventReportDTO> EventReport();

        [OperationContract]
        int DeactivateUser(int id);

        [OperationContract]
        int ActivateUser(int id);

        [OperationContract]
        EventReportsDashboardDTO GetEventReportsDashboard();

        [OperationContract]
        EventStatusChartDTO GetEventStatusChart();

        [OperationContract]
        List<EventApplicationsChartDTO> GetEventApplicationsChart();

        [OperationContract]
        List<TopEventApplicationsDTO> GetTopAppliedEvents();

        [OperationContract]
        List<MonthlyEventsDTO> GetMonthlyEventsTrend();

        [OperationContract]
        List<OrganizerEventsDTO> GetTopOrganizersByEvents();

        [OperationContract]
        List<OrganizerEventReportDTO> GetEventReportperOrganizer(int id);

        [OperationContract]
        void SendNotification(int userId,char role,string title,string message,string link = null);


    }
    [DataContract]
    public class UserDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public char UserType { get; set; }
    }

    [DataContract]
    public class OrganizerDTO
    {
        [DataMember]
        public int OrganizerId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string OrganizationName { get; set; }

        [DataMember]
        public string ContactEmail { get; set; }

        [DataMember]
        public string Phone { get; set; }
    }

    [DataContract]
    public class VendorDTO
    {
        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public int VendorID { get; set; }

        [DataMember]
        public string BusinessName { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string ContactEmail { get; set; }

        [DataMember]
        public string phone { get; set; }

    }

    [DataContract]
    public class VendorApplicationDTO
    {
        [DataMember]
        public int OrganizerID { get; set; }
        [DataMember]
        public int ApplicationId { get; set; }
        [DataMember]
        public int EventId { get; set; }
        [DataMember]
        public string EventName { get; set; }
        [DataMember]
        public DateTime EventDate { get; set; }
       
        
        [DataMember]
        public string Eventstatus { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public int VendorId { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public DateTime AppliedAt { get; set; }
        
        [DataMember]
        public  string BusinessName { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public int MaxVendors { get; set; }
    }
    [DataContract]
    public class VendorProfileDTO
    {
        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public int VendorID { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string BusinessName { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string password { get; set; }

        [DataMember]
        public string ContactEmail { get; set; }

        [DataMember]
        public string phone { get; set; }
    }

    [DataContract]
    public class OrganizerProfileDTO
    {
        [DataMember]
        public int OrganizerID { get; set; }

        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string OrganizationName { get; set; }

        [DataMember]
        public string ContactEmail { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string password { get; set; }

        [DataMember]
        public string phone { get; set; }
    }

    [DataContract]
    public class AdminVendorReportDTO
    {
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public int VendorID { get; set; }

        [DataMember]
        public string BusinessName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public int TotalApproved { get; set; }

        [DataMember]
        public int TotalDecline { get; set; }
    }
    public class AdminOragnizerReportDTO
    {
        [DataMember]
        public int OrganizerID { get; set; }

        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public string OrganizationName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public int TotalEvents { get; set; }

        [DataMember]
        public int CompletedEvents { get; set; }

        [DataMember]
        public int UpcomingEvents { get; set; }

        [DataMember]
        public int ApprovedVendors { get; set; }
    }

    [DataContract]
    public class AdminEventReportDTO
    {
        [DataMember]
        public int EventId { get; set; }
        [DataMember]
        public string EventName { get; set; }

        [DataMember]
        public DateTime EventDate { get; set; }

        [DataMember]
        public string OrganizationName { get; set; }

        [DataMember]
        public string EventStatus { get; set; }

        [DataMember]
        public string EventLocation { get; set; }

        [DataMember]
        public int TotalApplied { get; set; }

        [DataMember]
        public int TotalApproved { get; set; }

        [DataMember]
        public int TotalDeclined { get; set; }

        [DataMember]
        public string ApprovalRate { get; set; } 
        
    }
    [DataContract]
    public class EventReportsDashboardDTO
    {
        /* ===== KPI COUNTS ===== */

        [DataMember]
        public int TotalEvents { get; set; }

        [DataMember]
        public int ActiveEvents { get; set; }

        [DataMember]
        public int CompletedEvents { get; set; }

        [DataMember]
        public int CancelledEvents { get; set; }


        /* ===== APPLICATION TOTALS ===== */

        [DataMember]
        public int TotalApplications { get; set; }

        [DataMember]
        public int TotalApproved { get; set; }

        [DataMember]
        public int TotalDeclined { get; set; }


        /* ===== CHART DATA ===== */

        [DataMember]
        public List<string> EventNames { get; set; }

        [DataMember]
        public List<int> ApplicationsPerEvent { get; set; }

        [DataMember]
        public List<int> ApprovalRates { get; set; }
    }

    [DataContract]
    public class EventStatusChartDTO
    {
        [DataMember]
        public int Upcoming { get; set; }

        [DataMember]
        public int Active { get; set; }

        [DataMember]
        public int Completed { get; set; }

        [DataMember]
        public int Cancelled { get; set; }
    }
    [DataContract]
    public class EventApplicationsChartDTO
    {
        [DataMember]
        public string EventName { get; set; }

        [DataMember]
        public int TotalApplications { get; set; }
    }
    [DataContract]
    public class TopEventApplicationsDTO
    {
        [DataMember]
        public string EventName { get; set; }

        [DataMember]
        public int TotalApplications { get; set; }
    }
    [DataContract]
    public class MonthlyEventsDTO
    {
        [DataMember]
        public string Month { get; set; }

        [DataMember]
        public int TotalEvents { get; set; }
    }
    [DataContract]
    public class OrganizerEventsDTO
    {
        [DataMember]
        public string OrganizationName { get; set; }

        [DataMember]
        public int TotalEvents { get; set; }
    }
    [DataContract]
    public class OrganizerEventReportDTO
    {
        [DataMember]
        public int EventId { get; set; }
        [DataMember]
        public string EventName { get; set; }

        [DataMember]
        public DateTime EventDate { get; set; }

        [DataMember]
        public string EventStatus { get; set; }

        [DataMember]
        public string EventLocation { get; set; }

        [DataMember]
        public int TotalApplied { get; set; }

        [DataMember]
        public int TotalApproved { get; set; }

        [DataMember]
        public int TotalDeclined { get; set; }

        
    }











}



