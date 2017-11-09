using DagensTV.Models;
using DagensTV.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;

namespace DagensTV.Data
{
    public class DbOperations
    {
        private DagensTVEntities db = new DagensTVEntities();

        public bool CheckUser(string username, string password)
        {
            var user = db.Person.Where(
                x => x.Username.Equals(username) && x.Password.Equals(password));
            if (user.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsInRole(string userName, string roleName)
        {
            var role = db.Person
                .Where(x => x.Username.Equals(userName))
                .Include(x => x.Role).Where(x => x.Role.Name.Equals(roleName));
            return role.Any();
        }

        #region JSON
        /// <summary>
        /// "dynamic" response på samtliga så att vi kan iterera över innehållet som är i objekt-format
        /// TODO: Läsa in kategorier automatiskt då dessa nu måste justeras manuellt för alla shower som läggs in, detta då namnet på dem är så långt ifrån
        /// det vi använt oss av för att styla kategorier i tablå-vyn
        /// </summary>
        /// <param name="date"></param>
        public void GetShowsFromJson(string date)
        {
            List<Channel> c = db.Channel.ToList();
            Show sh;
            using (var wc = new WebClient())
            {
                foreach (var cha in c)
                {
                    string showList = wc.DownloadString("http://json.xmltv.se/" + cha.DownloadPath + "_" + date + ".js.gz");
                    dynamic response = JsonConvert.DeserializeObject(showList);                    
                    
                    foreach (var item in response.jsontv.programme)
                    {
                        sh = new Show()
                        {
                            Name = item.title.sv
                        };

                        if (!db.Show.Any(s => s.Name == sh.Name))
                        {                            
                            try
                            {
                                db.Show.Add(sh);
                                db.SaveChanges();
                            }
                            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                            {
                                Exception raise = dbEx;
                                foreach (var validationErrors in dbEx.EntityValidationErrors)
                                {
                                    foreach (var validationError in validationErrors.ValidationErrors)
                                    {
                                        string message = string.Format("{0}:{1}",
                                            validationErrors.Entry.Entity.ToString(),
                                            validationError.ErrorMessage);
                                        // raise a new exception nesting  
                                        // the current instance as InnerException
                                        raise = new InvalidOperationException(message, raise);
                                    }
                                }
                                throw raise;
                            }
                        }
                    }
                }
            }
        }

        public void GetScheduleFromJson(string date)
        {
            List<Channel> c = db.Channel.ToList();
            Schedule sc;
            using (var wc = new WebClient())
            {
                foreach (var cha in c)
                {
                    string json = wc.DownloadString("http://json.xmltv.se/" + cha.DownloadPath + "_" + date + ".js.gz");
                    dynamic response = JsonConvert.DeserializeObject(json);
                    foreach (var item in response.jsontv.programme)
                    {
                        string showName = item.title.sv;
                        var startUTC = DateTimeOffset.FromUnixTimeSeconds(long.Parse(item.start.Value)).UtcDateTime;
                        var startLocal = startUTC.ToLocalTime();
                        var endUTC = DateTimeOffset.FromUnixTimeSeconds(long.Parse(item.stop.Value)).UtcDateTime;
                        var endLocal = endUTC.ToLocalTime();
                        var timeSpan = endLocal - startLocal;

                        sc = new Schedule()
                        {
                            ChannelId = cha.Id,
                            ShowId = db.Show.FirstOrDefault(s => s.Name == showName).Id,
                            StartTime = startLocal,
                            EndTime = endLocal,
                            Duration = (int)timeSpan.TotalMinutes
                        };

                        // Alla program innehåller inte någon description i JSON-filerna..
                        try
                        {
                            sc.Resume = item.desc.sv;
                        }
                        catch
                        {
                            sc.Resume = "Ingen beskrivning finns tillgänglig.";
                        }

                        if (!db.Schedule.Any(s => s.ChannelId == cha.Id && s.StartTime == sc.StartTime))
                        {
                            try
                            {
                                db.Schedule.Add(sc);
                                db.SaveChanges();
                            }
                            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                            {
                                Exception raise = dbEx;
                                foreach (var validationErrors in dbEx.EntityValidationErrors)
                                {
                                    foreach (var validationError in validationErrors.ValidationErrors)
                                    {
                                        string message = string.Format("{0}:{1}",
                                            validationErrors.Entry.Entity.ToString(),
                                            validationError.ErrorMessage);
                                        // raise a new exception nesting  
                                        // the current instance as InnerException
                                        raise = new InvalidOperationException(message, raise);
                                    }
                                }
                                throw raise;
                            }
                        }
                    }

                }
            }
        }
        #endregion

        public IQueryable<ChannelVM> GetSchedule(string date)
        {
            var dt = DateTime.Parse(date);

            var schedule = db.Channel.Select(x => new ChannelVM
            {
                Id = x.Id,
                Name = x.Name,
                ImgUrl = x.LogoFilePath,
                Schedules = db.Schedule.Where(s => s.ChannelId == x.Id && s.StartTime.Day == dt.Day).Select(sc => new ScheduleVM
                {                    
                    StartTime = sc.StartTime,
                    Duration = sc.Duration,
                    EndTime = sc.EndTime,
                    ShowName = sc.Show.Name,
                    CategoryTag = sc.Show.Category.Tag,
                    MovieGenre = sc.Show.MovieGenre,
                    ImdbRating = sc.Show.ImdbRating,
                    StarImage = sc.Show.RatingIcon,
                    HasPassed = sc.EndTime < DateTime.Now,
                    IsActive = sc.StartTime < DateTime.Now && sc.EndTime > DateTime.Now
                }).ToList()
            });

            return schedule;
        }

        public IQueryable<ChannelVM> FilterScheduleByCategory(string category)
        {
            var dt = DateTime.Now;
            //var dt = DateTime.Parse(date);

            var filter = db.Channel.Select(x => new ChannelVM
            {
                Id = x.Id,
                Name = x.Name,
                ImgUrl = x.LogoFilePath,
                Schedules = db.Schedule.Where(s => s.ChannelId == x.Id && s.StartTime.Day == dt.Day && s.Show.Category.Name.Contains(category)).Select(sc => new ScheduleVM
                {                    
                    StartTime = sc.StartTime,
                    ChannelId = sc.ChannelId,
                    ShowName = sc.Show.Name,
                    CategoryTag = sc.Show.Category.Tag,
                    MovieGenre = sc.Show.MovieGenre,
                    ImdbRating = sc.Show.ImdbRating,
                    StarImage = sc.Show.RatingIcon
                }).ToList()
            });

            return filter;
        }

        public List<Category> GetCategories()
        {
            return db.Category.ToList();
        }
    }
}