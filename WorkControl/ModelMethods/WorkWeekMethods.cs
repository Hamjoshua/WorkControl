using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkControl.ModelMethods
{
    public static class WorkWeekMethods
    {
        public static bool saveAndCreateWorkWeek(int workWeekId, List<int> mondayDays, string nameOfWeek, WorkControlEntities db)
        {
            try
            {
                WorkWeeks workWeek = db.WorkWeeks.Where(w => w.WorkWeekId == workWeekId).FirstOrDefault();
                if (workWeek == null)
                {
                    workWeek = new WorkWeeks()
                    {
                        NameOfWeek = nameOfWeek,
                        Monday = mondayDays[0],
                        Tuesday = mondayDays[1],
                        Wednesday = mondayDays[2],
                        Thursday = mondayDays[3],
                        Friday = mondayDays[4],
                        Saturday = mondayDays[5],
                        Sunday = mondayDays[6],
                    };
                    db.WorkWeeks.Add(workWeek);
                }
                else
                {
                    workWeek.NameOfWeek = nameOfWeek;
                    workWeek.Monday = mondayDays[0];
                    workWeek.Tuesday = mondayDays[1];
                    workWeek.Wednesday = mondayDays[2];
                    workWeek.Thursday = mondayDays[3];
                    workWeek.Friday = mondayDays[4];
                    workWeek.Saturday = mondayDays[5];
                    workWeek.Sunday = mondayDays[6];
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static List<string> getWorkWeekRelations(int workWeekId, WorkControlEntities db)
        {
            WorkWeeks workWeek = db.WorkWeeks.Where(w => w.WorkWeekId == workWeekId).FirstOrDefault();
            if (workWeek != null)
            {
                var users = db.Users.Where(u => u.WorkWeekId == workWeekId).ToList();
                if (users.Count > 0)
                {
                    return users.Select(d => d.Login).ToList();
                }
            }
            return new List<string>();
        }

        public static bool removeWorkWeek(int workWeekId, WorkControlEntities db)
        {
            try
            {
                WorkWeeks workWeek = db.WorkWeeks.Where(w => w.WorkWeekId == workWeekId).First();

                var users = db.Users.Where(u => u.WorkWeekId == workWeekId).ToList();
                foreach (var user in users)
                {
                    user.WorkWeekId = null;
                }
                db.WorkWeeks.Remove(workWeek);
                db.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
