using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkControl.ModelMethods
{
    public class UserMethods
    {       

        public static Users getUserFromLoginData(string loginText, string passwordText, WorkControlEntities db)
        {            
            var user = db.Users.Where(u => u.Login == loginText && u.Password == passwordText).FirstOrDefault();
            return user;
        }

        public static bool registerUser(string secondName, string name, string lastName,
            string workPost, string login, string password, int roleId, int workWeekId, WorkControlEntities db)
        {
            try
            {
                Users user = new Users();                                
                user.SecondName = secondName;
                user.Name = name;
                user.LastName = lastName;
                user.Login = login;
                user.Password = password;
                user.WorkPost = workPost;                
                user.RoleId = roleId;
                user.WorkWeekId = workWeekId;

                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public static bool changeUser(int userId, string secondName, string name, string lastName,
            string workPost, string login, string password, int roleId, int workWeekId, WorkControlEntities db)
        {
            try
            {
                Users user = db.Users.Where(s => s.UserId == userId).FirstOrDefault();
                user.SecondName = secondName;
                user.Name = name;
                user.LastName = lastName;
                user.Login = login;
                user.Password = password;
                user.WorkPost = workPost;
                user.RoleId = roleId;
                user.WorkWeekId = workWeekId;
                                
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool removeUser(int userId, WorkControlEntities db)
        {
            try
            {
                Users user = db.Users.Where(u => u.UserId == userId).First();
                var stats = db.Stats.Where(u => u.UserId == userId).ToArray();
                foreach(var stat in stats)
                {
                    var progTimes = db.ProgramTime.Where(p => p.StatsId == stat.StatcsId).ToArray();
                    foreach(var progTime in progTimes)
                    {
                        db.ProgramTime.Remove(progTime);
                    }
                    db.Stats.Remove(stat);
                }
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
