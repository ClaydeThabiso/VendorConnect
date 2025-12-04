using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace VnedorConnect_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public int RegisterUser(string name, string lastName ,string username, string password, char role)
        {
            var HashedPassword = Secrecy.HashPassword(password);
            var user = (from u in db.Users
                        where u.Username.Equals(username)
                        select u).FirstOrDefault();

            if (user == null)
            {
                User newUser = new User();
                newUser.FirstName = name;
                newUser.LastName = lastName;
                newUser.Username = username;
                newUser.Password = HashedPassword;
                newUser.Role = role;

                db.Users.InsertOnSubmit(newUser);
                try
                {
                    db.SubmitChanges();
                    return newUser.UserId;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("REGISTER ERROR: " + e.GetBaseException().Message);
                    throw;
                }
            }
            else
            {
                return 0;
            }
        }
        public UserDTO login(string username, string Password)
        {
            var HashedPassword = Secrecy.HashPassword(Password);
            var tempUser = (from u in db.Users
                            where
         u.Username.Equals(username) &&
         u.Password.Equals(HashedPassword)
                            select u).FirstOrDefault();

            // If no user found, return null
            if (tempUser == null)
                return null;

            // Map EF user to DTO
            return new UserDTO
            {
                Id = tempUser.UserId,
                UserType = tempUser.Role
            };
        }
        public int registerOrganizer(string name, string email, string phone,int UserID)
        {
            var organizer = (from o in db.Organizers where  o.UserId.Equals(UserID) select o).FirstOrDefault();

            if(organizer==null)
            {
                Organizer newOrganizer = new Organizer();
                newOrganizer.OrganizationName = name;
                newOrganizer.ContactEmail = email;
                newOrganizer.Phone = phone;
                newOrganizer.UserId = UserID;

                db.Organizers.InsertOnSubmit(newOrganizer);
                try
                {
                    db.SubmitChanges();
                    return 1;
                }catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("REGISTER ERROR: " + e.GetBaseException().Message);
                    throw;
                }
            }
            else
            {
                return 0;
            }
        }

        public int registerVendor(string name, string email, string category, string phone,int UserID)
        {
            var vendor = (from v in db.Vendors
                          where  v.UserId.Equals(UserID)
                          select v).FirstOrDefault();

            if(vendor==null)
            {
                Vendor newVendor = new Vendor();
                newVendor.BusinesName = name;
                newVendor.ContactEmail = email;
                newVendor.Phone = phone;
                newVendor.Category = category;
                newVendor.UserId = UserID;

                db.Vendors.InsertOnSubmit(newVendor);
                try
                {
                    db.SubmitChanges();
                    return 1;
                }catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("REGISTER ERROR: " + e.GetBaseException().Message);
                    throw;
                }
            }
            else
            {
                return 0;
            }
        }
        public User GetUser(int id)
        {
            var user = (from u in db.Users where u.UserId.Equals(id) select u).FirstOrDefault();

            if(user != null)
            {
                User objUser = new User();

                objUser.FirstName = user.FirstName;
                objUser.LastName = user.LastName;
                return objUser;
            }
            else
            {
                return null;
            }
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            dynamic user = (from u in db.Users select u);

            if(user!=null)
            {
                foreach( User us in user)
                {
                    User objUser = new User();
                    objUser.FirstName = us.FirstName;
                    objUser.LastName = us.LastName;
                    objUser.Username = us.Username;
                    objUser.Role = us.Role;

                    users.Add(objUser);
                }
                return users;
            }
            else
            {
                return null;
            }
        }

        public int totalVendors()
        {
            var totVendors = (from u in db.Users where u.Role.Equals('V') select u).Count();
            return totVendors;
        }
        public int totalOrganizers()
        {
            var totaOrg = (from u in db.Users where u.Role.Equals('O') select u).Count();
            return totaOrg;
        }

        public int CreateEvent(string name, DateTime eventDate, string location, int maxVendors, string description, int OrganizerID)
        {
            var Event = (from e in db.Events where e.EventName.Equals(name) && e.EventDate.Equals(eventDate)
                         && e.Location.Equals(location) select e).FirstOrDefault();
            
            if(Event==null)
            {
                Event newEvent = new Event();
                newEvent.EventName = name;
                newEvent.EventDate = eventDate;
                newEvent.Location = location;
                newEvent.MaxVendors = maxVendors;
                newEvent.Description = description;
                newEvent.OrganizerId = OrganizerID;
                newEvent.status = "Active";

                db.Events.InsertOnSubmit(newEvent);
                try
                {
                    db.SubmitChanges();
                    return 1;
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("REGISTER ERROR: " + ex.GetBaseException().Message);
                    throw;
                }
            }
            else
            {
                return 0;
            }

        }

        public List<Event> GetEvents()
        {
            List<Event> events = new List<Event>();
            dynamic tempEvent = (from e in db.Events where e.status.Equals("Active") select e);

            if(tempEvent!=null)
            {
                foreach(Event eve in tempEvent)
                {
                    Event objEvent = new Event();
                    objEvent.EventId = eve.EventId;
                    objEvent.EventName = eve.EventName;
                    objEvent.EventDate = eve.EventDate;
                    objEvent.Location = eve.Location;
                    objEvent.MaxVendors = eve.MaxVendors;

                    events.Add(objEvent);
                }
                return events;
            }
            else
            {
                return null;
            }
        }
        public Event GetEvent(int id)
        {
            var tempEvent = (from e in db.Events where e.EventId.Equals(id) select e).FirstOrDefault();
            if (tempEvent != null)
            {
                    Event objEvent = new Event();
                    objEvent.EventName = tempEvent.EventName;
                    objEvent.EventDate = tempEvent.EventDate;
                    objEvent.Location = tempEvent.Location;
                    objEvent.MaxVendors = tempEvent.MaxVendors;
                    objEvent.status = tempEvent.status;        
                return objEvent;
            }
            else
            {
                return null;
            }
        }
        public List<Event> GetEventPerOrganizer(int id)
        {
            List<Event> events = new List<Event>();
            dynamic tempEvent = (from e in db.Events where e.OrganizerId.Equals(id) select e);

            if (tempEvent != null)
            {
                foreach (Event eve in tempEvent)
                {
                    Event objEvent = new Event();
                    objEvent.EventId = eve.EventId;
                    objEvent.EventName = eve.EventName;
                    objEvent.EventDate = eve.EventDate;
                    objEvent.Location = eve.Location;
                    objEvent.MaxVendors = eve.MaxVendors;
                    objEvent.status = eve.status;

                    events.Add(objEvent);
                }
                return events;
            }
            else
            {
                return null;
            }
        }
        public OrganizerDTO GetOrganizerByUserId(int userId)
        {
            var organizer = db.Organizers.FirstOrDefault(o => o.UserId == userId);

            if (organizer == null)
                return null;

            return new OrganizerDTO
            {
                OrganizerId = organizer.OrganizerId,
                UserId = organizer.UserId,
                OrganizationName = organizer.OrganizationName,
                ContactEmail = organizer.ContactEmail,
                Phone = organizer.Phone
            };
        }
        public int getTotalEventPerOrganizer(int id)
        {
            var totEvent = (from e in db.Events where e.OrganizerId.Equals(id) select e).Count();
            return totEvent;
        }

        public VendorDTO GetVendorByUserId(int userID)
        {
            var vendor = (from v in db.Vendors where v.UserId.Equals(userID) select v).FirstOrDefault();

            if(vendor==null)
            {
                return null;
            }
            else
            {
                return new VendorDTO
                {
                    UserID = vendor.UserId,
                    VendorID = vendor.VendorId
                };
            }
        }
        public int EventApplication(int vendorID, int eventID)
        {
            var apply = (from v in db.VendorApplications
                         where v.VendorId.Equals(vendorID) && v.EventId.Equals(eventID)
                         select v).FirstOrDefault();

            if(apply==null)
            {
                VendorApplication newApplication = new VendorApplication();
                newApplication.VendorId = vendorID;
                newApplication.EventId = eventID;
                newApplication.Status = "Pending";

                db.VendorApplications.InsertOnSubmit(newApplication);
                try
                {
                    db.SubmitChanges();
                    return 1;
                }catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("REGISTER ERROR: " + ex.GetBaseException().Message);
                    throw;
                }
            }
            else
            {
                return 0;
            }
        }
        public List<VendorApplicationDTO> GetApplicationPerVendor(int vendorID)
        {
            var applications =(from va in db.VendorApplications
                join ev in db.Events on va.EventId equals ev.EventId
                join v in db.Vendors on va.VendorId equals v.VendorId
                where va.VendorId == vendorID
                select new VendorApplicationDTO
                {
                    ApplicationId=va.ApplicationId,
                    EventId = ev.EventId,
                    EventName = ev.EventName,
                    EventDate = ev.EventDate,
                    Location = ev.Location,
                    VendorId = v.VendorId,
                    Status = va.Status,
                    AppliedAt = (DateTime)va.AppliedAt
                }).ToList();

            return applications;
        }
        public List<VendorApplicationDTO> GetApplicationsPerOrganizer(int OrgaID)
        {
            var application = (from o in db.Organizers
                               join e in db.Events on o.OrganizerId equals e.OrganizerId
                               join va in db.VendorApplications on e.EventId equals va.EventId
                               join v in db.Vendors on va.VendorId equals v.VendorId
                               where o.OrganizerId == OrgaID
                               select new VendorApplicationDTO
                               {
                                   ApplicationId = va.ApplicationId,
                                   EventId = e.EventId,
                                   EventName = e.EventName,
                                   EventDate = e.EventDate,
                                   Location = e.Location,
                                   VendorId = v.VendorId,
                                   BusinessName = v.BusinesName,
                                   Category=v.Category,
                                   Status = va.Status,
                                   AppliedAt = (DateTime)va.AppliedAt
                               }).ToList();
            return application;
        }
        public int deleteApplication(int EventId)
        {
            var application = (from va in db.VendorApplications where va.ApplicationId.Equals(EventId) select va).FirstOrDefault();

            if(application!=null)
            {
                db.VendorApplications.DeleteOnSubmit(application);
                try
                {
                    db.SubmitChanges();
                    return 1;
                }catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("REGISTER ERROR: " + ex.GetBaseException().Message);
                    throw;
                }
            }
            else
            {
                return 0;
            }
        }

    }
}
