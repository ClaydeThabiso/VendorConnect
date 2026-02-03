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
        public int RegisterUser(string name, string lastName, string username, string password, char role)
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
         u.Password.Equals(HashedPassword) && u.IsActive.Equals(true)
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
        public int registerOrganizer(string name, string email, string phone, int UserID)
        {
            var organizer = (from o in db.Organizers where o.UserId.Equals(UserID) select o).FirstOrDefault();

            if (organizer == null)
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
                } catch (Exception e)
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

        public int registerVendor(string name, string email, string category, string phone, int UserID)
        {
            var vendor = (from v in db.Vendors
                          where v.UserId.Equals(UserID)
                          select v).FirstOrDefault();

            if (vendor == null)
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
                } catch (Exception e)
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

            if (user != null)
            {
                User objUser = new User();

                objUser.FirstName = user.FirstName;
                objUser.LastName = user.LastName;
                objUser.Password = user.Password;
                objUser.Role = user.Role;
                objUser.Username = user.Username;
                return objUser;
            }
            else
            {
                return null;
            }
        }
        public VendorDTO GetVendor(int id)
        {
            return db.Vendors
               .Where(v => v.VendorId == id)
               .Select(v => new VendorDTO
               {
                   BusinessName = v.BusinesName,
                   ContactEmail = v.ContactEmail,
                   Category = v.Category,
                   phone = v.Phone
               })
               .FirstOrDefault();
        }
        public OrganizerDTO GetOrganizer(int id)
        {
            return db.Organizers
                 .Where(o => o.OrganizerId == id)
                 .Select(o => new OrganizerDTO
                 {
                     OrganizationName = o.OrganizationName,
                     ContactEmail = o.ContactEmail,
                     Phone = o.Phone
                 })
            .FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            dynamic user = (from u in db.Users select u);

            if (user != null)
            {
                foreach (User us in user)
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
        public int getUpcomingEvents()
        {
            var totUpcoming = (from e in db.Events where e.status.Equals("Upcoming") select e).Count();
            return totUpcoming;
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

            if (Event == null)
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
                catch (Exception ex)
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
            dynamic tempEvent = (from e in db.Events where e.status.Equals("Upcoming") select e);

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
                objEvent.Description = tempEvent.Description;
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
                    UpdateEventStatus(eve);
                    Event objEvent = new Event();
                    objEvent.EventId = eve.EventId;
                    objEvent.EventName = eve.EventName;
                    objEvent.EventDate = eve.EventDate;
                    objEvent.Location = eve.Location;
                    objEvent.MaxVendors = eve.MaxVendors;
                    objEvent.status = eve.status;

                    events.Add(objEvent);
                }
                db.SubmitChanges();
                return events;
            }
            else
            {
                return null;
            }
        }
        public int CancelEvent(int eventId)
        {
            var ev = db.Events.FirstOrDefault(e => e.EventId == eventId);
            if (ev != null)
            {
                ev.status = "Cancelled";
                db.SubmitChanges();
                return 1;
            }
            return 0;
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
        public int getTotalUpcomingEvents(int id)
        {
            var tot = (from e in db.Events where e.OrganizerId.Equals(id) && e.status.Equals("Upcoming") select e).Count();
            return tot;
        }
        public int getTotalVendorApplicationPerVendo(int id)
        {
            var tot = (from va in db.VendorApplications where va.VendorId.Equals(id) select va).Count();
            return tot;
        }
        public int getTotAcceptVendorApplication(int id)
        {
            var tot = (from va in db.VendorApplications where va.VendorId.Equals(id) && va.Status.Equals("Approved") select va).Count();
            return tot;
        }
        public VendorDTO GetVendorByUserId(int userID)
        {
            var vendor = (from v in db.Vendors where v.UserId.Equals(userID) select v).FirstOrDefault();

            if (vendor == null)
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

            if (apply == null)
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
                } catch (Exception ex)
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
            var applications = (from va in db.VendorApplications
                                join ev in db.Events on va.EventId equals ev.EventId
                                join v in db.Vendors on va.VendorId equals v.VendorId
                                where va.VendorId == vendorID
                                select new VendorApplicationDTO
                                {
                                    ApplicationId = va.ApplicationId,
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
        public List<AdminVendorReportDTO> VendorReport()
        {
            var ven = (from v in db.Vendors
                       select new AdminVendorReportDTO
                       {
                           UserID=v.UserId,
                           VendorID = v.VendorId,
                           Email=v.ContactEmail,
                           BusinessName = v.BusinesName,
                           Category = v.Category,
                           CreatedAt = (DateTime)v.CreatedAt,
                           IsActive=v.IsActive,
                           TotalApproved= (from va in db.VendorApplications where va.VendorId==v.VendorId && va.Status.Equals("Approved") select va).Count(),
                           TotalDecline = (from va in db.VendorApplications where va.VendorId==v.VendorId && va.Status.Equals("Declined") select va).Count()

                       }).ToList();
            return ven; 
        }
        public List<AdminOragnizerReportDTO> OragnizerReport()
        {
            var orga = (from o in db.Organizers
                        select new AdminOragnizerReportDTO
                        {
                            UserID = o.UserId,
                            OrganizerID = o.OrganizerId,
                            Email=o.ContactEmail,
                            CreatedAt=o.CreatedAt,
                            IsActive=o.IsActive,
                            OrganizationName=o.OrganizationName,

                            TotalEvents=(from e in db.Events where e.OrganizerId==o.OrganizerId select e).Count(),
                            CompletedEvents=(from e in db.Events where e.OrganizerId==o.OrganizerId && e.status.Equals("Completed") select e).Count(),
                            UpcomingEvents=(from e in db.Events where e.OrganizerId==o.OrganizerId && e.status.Equals("Upcoming") select e).Count(),
                            ApprovedVendors=(from va in db.VendorApplications join e in db.Events on va.EventId equals e.EventId
                                             where va.Status.Equals("Approved") && e.OrganizerId==o.OrganizerId select va).Count()

                        }).ToList();
            return orga;
        }
        public List<AdminEventReportDTO> EventReport()
        {
            var ev = (from e in db.Events
                      select new AdminEventReportDTO
                      {
                          EventId = e.EventId,
                          EventName = e.EventName,
                          EventDate = e.EventDate,
                          EventLocation = e.Location,
                          EventStatus = e.status,
                          OrganizationName = (from o in db.Organizers where o.OrganizerId == e.OrganizerId select o.OrganizationName).FirstOrDefault(),
                          TotalApplied=(from va in db.VendorApplications where va.EventId==e.EventId select va).Count(),
                          TotalApproved=(from va in db.VendorApplications where va.EventId==e.EventId && va.Status=="Approved" select va).Count(),
                          TotalDeclined=(from va in db.VendorApplications where va.EventId==e.EventId && va.Status=="Declined" select va).Count(),
                         
                      }).ToList();
            return ev;
        }
        public List<OrganizerEventReportDTO> GetEventReportperOrganizer(int id)
        {
            var eve = (from e in db.Events
                      where e.OrganizerId.Equals(id)
                      select new OrganizerEventReportDTO
                      {
                          EventId = e.EventId,
                          EventName = e.EventName,
                          EventDate = e.EventDate,
                          EventLocation = e.Location,
                          EventStatus = e.status,
                          TotalApplied = (from va in db.VendorApplications where va.EventId == e.EventId select va).Count(),
                          TotalApproved = (from va in db.VendorApplications where va.EventId == e.EventId && va.Status == "Approved" select va).Count(),
                          TotalDeclined = (from va in db.VendorApplications where va.EventId == e.EventId && va.Status == "Declined" select va).Count(),

                      }).ToList();
            return eve;
        }
        public List<VendorApplicationDTO> GetApplicationsPerOrganizer(int OrgaID)
        {
            var application = (from o in db.Organizers
                               join e in db.Events on o.OrganizerId equals e.OrganizerId
                               join va in db.VendorApplications on e.EventId equals va.EventId
                               join v in db.Vendors on va.VendorId equals v.VendorId
                               where o.OrganizerId == OrgaID && e.status!="Completed" 
                               select new VendorApplicationDTO
                               {
                                   ApplicationId = va.ApplicationId,
                                   EventId = e.EventId,
                                   EventName = e.EventName,
                                   EventDate = e.EventDate,
                                   Location = e.Location,
                                   VendorId = v.VendorId,
                                   BusinessName = v.BusinesName,
                                   Category = v.Category,
                                   MaxVendors = e.MaxVendors,
                                   Status = va.Status,
                                   Eventstatus = e.status,
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
        public VendorApplicationDTO DeclineApplication(int ApplicationId)
        {
            var application = db.VendorApplications
                                .FirstOrDefault(va => va.ApplicationId == ApplicationId);

            if (application != null)
            {
                try
                {
                    application.Status = "Declined";

                    db.SubmitChanges();

                    VendorApplicationDTO objApplication = new VendorApplicationDTO
                    {
                        ApplicationId = application.ApplicationId,
                        VendorId = application.VendorId,
                        EventId = application.EventId,
                        Status = application.Status
                    };

                    return objApplication;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("DECLINE ERROR: " + ex.GetBaseException().Message);
                    throw;
                }
            }

            return null;
        }

        public VendorApplicationDTO AccepptApplication(int ApplicationId)
        {
            var application = db.VendorApplications
                                .FirstOrDefault(va => va.ApplicationId == ApplicationId);

            if (application != null)
            {
                try
                {
                    application.Status = "Approved";

                    db.SubmitChanges();

                    VendorApplicationDTO objApplication = new VendorApplicationDTO
                    {
                        ApplicationId = application.ApplicationId,
                        VendorId = application.VendorId,
                        EventId = application.EventId,
                        Status = application.Status
                    };

                    return objApplication;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("DECLINE ERROR: " + ex.GetBaseException().Message);
                    throw;
                }
            }

            return null;
        }
        public int DeactivateUser(int id)
        {
            var user = (from u in db.Users where u.UserId == id select u).FirstOrDefault();
            if(user!=null)
            {
                user.IsActive = false;
            }
            else
            {
                return 0;
            }

            var vendor = (from v in db.Vendors where v.UserId == id select v).FirstOrDefault();
            if(vendor!=null)
            {
                vendor.IsActive = false;
            }
           

            var orga = (from o in db.Organizers where o.UserId == id select o).FirstOrDefault();
            if(orga!=null)
            {
                orga.IsActive = false;
            }
         
                db.SubmitChanges();
                return 1;
           
        }
        public int ActivateUser(int id)
        {
            var user = (from u in db.Users where u.UserId == id select u).FirstOrDefault();
            if (user != null)
            {
                user.IsActive = true;
            }
            else
            {
                return 0;
            }

            var vendor = (from v in db.Vendors where v.UserId == id select v).FirstOrDefault();
            if (vendor != null)
            {
                vendor.IsActive = true;
            }


            var orga = (from o in db.Organizers where o.UserId == id select o).FirstOrDefault();
            if (orga != null)
            {
                orga.IsActive = true;
            }

            db.SubmitChanges();
            return 1;
        }
        public int getApprovedApplication(int eventId)
        {
            var application = (from va in db.VendorApplications where va.EventId==eventId && va.Status == "Approved" select va).Count();
            return application;
        }
        private void UpdateEventStatus(Event eve)
        {
            DateTime today = DateTime.Now.Date;

            if (eve.status != "Cancelled") 
            {
                if (eve.EventDate.Date < today)
                {
                    eve.status = "Completed";
                }
                else if (eve.EventDate.Date == today)
                {
                    eve.status = "Active";
                }
                else
                {
                    eve.status = "Upcoming";
                }
            }
        }
        public int UpdateEvent(int eventId, string name, DateTime date, string location, string description, int maxVendors)
        {
            var ev = db.Events.FirstOrDefault(e => e.EventId == eventId);

            if (ev != null)
            {
                ev.EventName = name;
                ev.EventDate = date;
                ev.Location = location;
                ev.Description = description;
                ev.MaxVendors = maxVendors;

                db.SubmitChanges();
                return 1;
            }

            return 0;
        }

        public bool updateVendorProfile(int id, string FirstName, string LastName, string password, string email, string BusinessName, string category, string ContactEmail, string phone)
        {
            var user = (from u in db.Users where u.UserId.Equals(id) select u).FirstOrDefault();
            var vendor = (from v in db.Vendors where v.UserId.Equals(id) select v).FirstOrDefault();

            if(user !=null && vendor!=null )
            {
                user.FirstName = FirstName;
                user.LastName = LastName;
                if (!string.IsNullOrWhiteSpace(password))
                {
                    user.Password = Secrecy.HashPassword(password);
                }
                user.Username = email;

                vendor.BusinesName = BusinessName;
                vendor.Category = category;
                vendor.ContactEmail = ContactEmail;
                vendor.Phone = phone;

                db.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool updateOrganizerProfile(int id, string FirstName, string LastName, string password,string email, string OrganizationName, string ContactEmail, string phone)
        {
            var user = (from u in db.Users where u.UserId.Equals(id) select u).FirstOrDefault();
            var organizer = (from o in db.Organizers where o.UserId.Equals(id) select o).FirstOrDefault();

            if(user!= null && organizer!=null)
            {
                user.FirstName = FirstName;
                user.LastName = LastName;
                if (!string.IsNullOrWhiteSpace(password))
                {
                    user.Password = Secrecy.HashPassword(password);
                }
                user.Username = email;

                organizer.OrganizationName = OrganizationName;
                organizer.ContactEmail = ContactEmail;
                organizer.Phone = phone;

                db.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public EventReportsDashboardDTO GetEventReportsDashboard()
        {
            var events = db.Events.ToList();

            var dto = new EventReportsDashboardDTO
            {
                TotalEvents = events.Count,
                ActiveEvents = events.Count(e => e.status == "Active"),
                CompletedEvents = events.Count(e => e.status == "Completed"),
                CancelledEvents = events.Count(e => e.status == "Cancelled"),

                EventNames = new List<string>(),
                ApplicationsPerEvent = new List<int>(),
                ApprovalRates = new List<int>()
            };

            foreach (var ev in events)
            {
                int totalApplied = db.VendorApplications
                    .Count(v => v.EventId == ev.EventId);

                int approved = db.VendorApplications
                    .Count(v => v.EventId == ev.EventId && v.Status == "Approved");

                int declined = db.VendorApplications
                    .Count(v => v.EventId == ev.EventId && v.Status == "Declined");

                dto.TotalApplications += totalApplied;
                dto.TotalApproved += approved;
                dto.TotalDeclined += declined;

                dto.EventNames.Add(ev.EventName);
                dto.ApplicationsPerEvent.Add(totalApplied);

                // SAFE approval rate
                int rate = totalApplied == 0 ? 0 : (approved * 100 / totalApplied);
                dto.ApprovalRates.Add(rate);
            }

            return dto;
        }
        public EventStatusChartDTO GetEventStatusChart()
        {
            return new EventStatusChartDTO
            {
                Upcoming = db.Events.Count(e => e.status == "Upcoming"),
                Active = db.Events.Count(e => e.status == "Active"),
                Completed = db.Events.Count(e => e.status == "Completed"),
                Cancelled = db.Events.Count(e => e.status == "Cancelled")
            };
        }
        public List<EventApplicationsChartDTO> GetEventApplicationsChart()
        {
            return (from e in db.Events
                    select new EventApplicationsChartDTO
                    {
                        EventName = e.EventName,
                        TotalApplications = db.VendorApplications
                            .Count(v => v.EventId == e.EventId)
                    }).ToList();
        }
        public List<TopEventApplicationsDTO> GetTopAppliedEvents()
        {
            var data = (from e in db.Events
                        select new TopEventApplicationsDTO
                        {
                            EventName = e.EventName,
                            TotalApplications = db.VendorApplications
                                .Count(v => v.EventId == e.EventId)
                        })
                        .OrderByDescending(x => x.TotalApplications)
                        .Take(5)
                        .ToList();

            return data;
        }
        public List<MonthlyEventsDTO> GetMonthlyEventsTrend()
        {
           
            var groupedData = db.Events
                .GroupBy(e => new { e.EventDate.Year, e.EventDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalEvents = g.Count()
                })
                .ToList(); 

            
            var result = groupedData
                .Select(x => new MonthlyEventsDTO
                {
                    Month = new DateTime(x.Year, x.Month, 1).ToString("MMM yyyy"), 
            TotalEvents = x.TotalEvents
                })
                .OrderBy(x => DateTime.ParseExact(x.Month, "MMM yyyy", null)) 
                .ToList();

            return result;
        }
        public List<OrganizerEventsDTO> GetTopOrganizersByEvents()
        {
            var data = (from o in db.Organizers
                        select new OrganizerEventsDTO
                        {
                            OrganizationName = o.OrganizationName,
                            TotalEvents = db.Events.Count(e => e.OrganizerId == o.OrganizerId)
                        })
                        .OrderByDescending(x => x.TotalEvents)
                        .Take(5)
                        .ToList();

            return data;
        }
        public void SendNotification(
    int userId,
    char role,
    string title,
    string message,
    string redirectUrl)
        {
            try
            {
                Notification n = new Notification
                {
                    UserId = userId,
                    Role = role,
                    Title = title,
                    Message = message,
                    RedirectURL = redirectUrl,
                    IsRead = false,
                    CreatedAt = DateTime.Now
                };

                db.Notification.InsertOnSubmit(n);
                db.SubmitChanges();
            }
            catch (Exception)
            {
                throw new FaultException("Unable to send notification.");
            }
        }
        public List<NotificationDTO> GetUserNotifications(int userId)
        {
            var notifications =
                (from n in db.Notification
                 where n.UserId == userId
                 orderby n.CreatedAt descending
                 select new NotificationDTO
                 {
                     NotificationId = n.NotificationId,
                     Title = n.Title,
                     Message = n.Message,
                     RedirectUrl = n.RedirectURL,
                     IsRead = n.IsRead,
                     CreatedAt = n.CreatedAt
                 }).ToList();

            return notifications;
        }



    }
}
