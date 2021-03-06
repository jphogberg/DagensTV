﻿using DagensTV.Models;
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

        #region User

        public IEnumerable<Person> GetAllPersons()
        {
            var allUsers = db.Person.ToList();
            return allUsers;
        }

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

        #endregion

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

        #region MyChannels

        public bool CheckUserHasMyChannels()
        {
            bool exists = false;
            MyChannels uc = new MyChannels();

            var userHas = db.MyChannels.Where(x => x.PersonId == Person.activeUser.Id);
            foreach(var i in userHas)
            {
                uc = i;
                break;
            }
            
            if(uc.PersonId != 0)
            {
                exists = true;
            }
            return exists;
        }

        public void AddNewUserSettings(List<MyChannels> list)
        {
            foreach(var trueCh in list)
            {
                db.MyChannels.Add(trueCh);
                db.SaveChanges();
            }
        }

        public void UpdateTrueChannels(List<MyChannels> list)
        {
            var oldList = db.MyChannels.ToList();

            foreach (var trueCh in list)
            {
                foreach (var item in oldList)
                {
                    if (!db.MyChannels.Any(c => c.ChannelId == trueCh.ChannelId && c.PersonId == Person.activeUser.Id))
                    {
                        db.MyChannels.Add(trueCh);
                        db.SaveChanges();
                    }
                    break;
                }
            }
        }
       
        public void UpdateFalseChannels(List<MyChannels> list)
        {
            var oldList = db.MyChannels.Where(x => x.PersonId == Person.activeUser.Id).ToList();

            foreach (var falseCh in list)
            {
                foreach (var item in oldList)
                {
                    if (item.PersonId == Person.activeUser.Id && item.ChannelId == falseCh.ChannelId)
                    {
                        var myOld = db.MyChannels.Where(x => x.PersonId == Person.activeUser.Id && x.ChannelId == item.ChannelId);
                        foreach (var oldKey in myOld)
                        {
                            db.MyChannels.Remove(oldKey);
                            break;
                        }
                        db.SaveChanges();
                    }
                }
            }
        }


        #endregion

        #region MyFavorites

        public bool CheckUserHasMyFavorites()
        {
            bool exists = false;
            MyFavorites uf = new MyFavorites();

            var userHas = db.MyFavorites.Where(x => x.PersonId == Person.activeUser.Id);
            foreach (var i in userHas)
            {
                uf = i;
                break;
            }

            if (uf.PersonId != 0)
            {
                exists = true;
            }
            return exists;
        }

        public void AddNewUserFavoritesSettings(List<MyFavorites> list)
        {
            foreach (var trueSh in list)
            {
                db.MyFavorites.Add(trueSh);
                db.SaveChanges();
            }
        }

        public void UpdateTrueShows(List<MyFavorites> list)
        {
            var oldList = db.MyFavorites.ToList();

            foreach (var trueSh in list)
            {
                foreach (var item in oldList)
                {
                    if (!db.MyFavorites.Any(s => s.ShowId == trueSh.ShowId && s.PersonId == Person.activeUser.Id))
                    {
                        db.MyFavorites.Add(trueSh);
                        db.SaveChanges();
                    }
                    break;
                }
            }
        }

        public void UpdateFalseShows(List<MyFavorites> list)
        {
            var oldList = db.MyFavorites.Where(x => x.PersonId == Person.activeUser.Id).ToList();

            foreach (var falseCh in list)
            {
                foreach (var item in oldList)
                {
                    if (item.PersonId == Person.activeUser.Id && item.ShowId == falseCh.ShowId)
                    {
                        var myOld = db.MyFavorites.Where(x => x.PersonId == Person.activeUser.Id && x.ShowId == item.ShowId);
                        foreach (var oldKey in myOld)
                        {
                            db.MyFavorites.Remove(oldKey);
                            break;
                        }
                        db.SaveChanges();
                    }
                }
            }
        }

        #endregion

        #region Admin

        public List<Channel> GetAllChannels()
        {
            return db.Channel.ToList();            
        }

        public void UpdatePopularContent(PopVM model)
        {
            int popular = model.Id;
            PopularContent pc = new PopularContent();

            pc = db.PopularContent.Find(popular);
            pc.ScheduleId = model.ScheduleId;
            pc.ImgUrl = "img/" + model.ImgUrl;
            pc.Icon = "mdi mdi-television";

            db.Entry(pc).State = EntityState.Modified;
            db.SaveChanges();
        }

        #endregion

        #region Schedule

        public IQueryable<ChannelVM> GetSchedule(string date)
        {
            var dt = DateTime.Parse(date);
            //var ts = new TimeSpan(05, 30, 00); // Används inte än, skulle vilja få till att tablåerna går från 05:30:00 en dag till 05:30:00 nmästa men...

            if(!db.Schedule.Any(s => s.StartTime.Day == dt.Day))
            {
                GetShowsFromJson(date);
                GetScheduleFromJson(date);
            }

            var schedule = db.Channel.Select(x => new ChannelVM
            {
                Id = x.Id,
                Name = x.Name,
                ImgUrl = x.LogoFilePath,
                Schedules = db.Schedule.Where(s => s.ChannelId == x.Id && s.StartTime.Day == dt.Day).Select(sc => new ScheduleVM
                {
                    Id = sc.Id,
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

        public IQueryable<ChannelVM> GetSchedule(string date, int id)
        {
            var dt = DateTime.Parse(date);

            if (!db.Schedule.Any(s => s.StartTime.Day == dt.Day))
            {
                GetShowsFromJson(date);
                GetScheduleFromJson(date);
            }

            var myChannels = db.MyChannels.Where(x => x.PersonId == id).Select(c => c.ChannelId).ToList();
            var myFavorites = db.MyFavorites.Where(x => x.PersonId == id).Select(s => s.ShowId).ToList();
                       
            var schedule = db.Channel.Where(c => myChannels.Contains(c.Id)).Select(x => new ChannelVM
            {
                Id = x.Id,
                Name = x.Name,
                ImgUrl = x.LogoFilePath,
                Schedules = db.Schedule.Where(s => s.ChannelId == x.Id && s.StartTime.Day == dt.Day).Select(sc => new ScheduleVM
                {
                    Id = sc.Id,
                    StartTime = sc.StartTime,
                    Duration = sc.Duration,
                    EndTime = sc.EndTime,
                    ShowId = sc.ShowId,
                    ShowName = sc.Show.Name,
                    CategoryTag = sc.Show.Category.Tag,
                    MovieGenre = sc.Show.MovieGenre,
                    ImdbRating = sc.Show.ImdbRating,
                    StarImage = sc.Show.RatingIcon,
                    HasPassed = sc.EndTime < DateTime.Now,
                    IsActive = sc.StartTime < DateTime.Now && sc.EndTime > DateTime.Now,
                    FavShow = myFavorites.Contains((int)sc.ShowId)
                }).ToList()
            });        

            return schedule;            
        }

        public IQueryable<ChannelVM> FilterScheduleByCategory(string category, string date)
        {            
            var dt = DateTime.Parse(date);
            var myFavorites = db.MyFavorites.Where(x => x.PersonId == Person.activeUser.Id).Select(s => s.ShowId).ToList();

            var filter = db.Channel.Select(x => new ChannelVM
            {
                Id = x.Id,
                Name = x.Name,
                ImgUrl = x.LogoFilePath,
                Schedules = db.Schedule.Where(s => s.ChannelId == x.Id && s.StartTime.Day == dt.Day && s.Show.Category.Name.Contains(category)).Select(sc => new ScheduleVM
                {
                    Id = sc.Id,
                    StartTime = sc.StartTime,
                    ChannelId = sc.ChannelId,
                    ShowName = sc.Show.Name,
                    CategoryTag = sc.Show.Category.Tag,
                    MovieGenre = sc.Show.MovieGenre,
                    ImdbRating = sc.Show.ImdbRating,
                    StarImage = sc.Show.RatingIcon,
                    FavShow = myFavorites.Contains((int)sc.ShowId)
                }).ToList()
            });

            return filter;
        }      

        public List<ScheduleVM> ShowInfo(int id)
        {
            var myFavorites = db.MyFavorites.Where(x => x.PersonId == Person.activeUser.Id).Select(s => s.ShowId).ToList();

            var scheduleList = db.Schedule.Where(x => x.Id == id).Select(x => new ScheduleVM
            {
                StartTime = x.StartTime,
                EndTime = (DateTime)DbFunctions.AddMinutes(x.StartTime, x.Duration),
                ChannelName = x.Channel.Name,
                ShowName = x.Show.Name,
                Resume = x.Resume,
                CategoryName = x.Show.Category.Name,
                CategoryTag = x.Show.Category.Tag,
                MovieGenre = x.Show.MovieGenre,
                ImdbRating = x.Show.ImdbRating,
                StarImage = x.Show.RatingIcon,
                FavShow = myFavorites.Contains((int)x.ShowId)
            }).ToList();

            return scheduleList;
        }
        #endregion
    }
}