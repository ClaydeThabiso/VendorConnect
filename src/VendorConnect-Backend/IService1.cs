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

}



