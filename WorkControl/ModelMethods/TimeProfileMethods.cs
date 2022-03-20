using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkControl.ModelMethods
{
    public static class TimeProfileMethods
    {
        public static bool createAndSaveTimeProfile(int tp_id, string nameOfProfile, TimeSpan lunchTime,
            TimeSpan startWorkTime, TimeSpan endWorkTime, WorkControlEntities db)
        {
            try
            {
                TimeProfiles tp = db.TimeProfiles.Where(t => t.TimeProfileId == tp_id).FirstOrDefault();
                if (tp == null)
                {
                    tp = new TimeProfiles()
                    {
                        NameOfProfile = nameOfProfile,
                        StartWorkTime = startWorkTime,
                        EndWorkTime = endWorkTime,
                        LanchTime = lunchTime
                    };
                    db.TimeProfiles.Add(tp);
                }
                else
                {
                    tp.NameOfProfile = nameOfProfile;
                    tp.StartWorkTime = startWorkTime;
                    tp.EndWorkTime = endWorkTime;
                    tp.LanchTime = lunchTime;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool isHoliday(TimeProfiles tp)
        {
            TimeSpan datetimeDiff = tp.EndWorkTime.Value - tp.StartWorkTime.Value;
            return datetimeDiff == TimeSpan.Zero;
        }

        public static TimeProfiles getTimeProfileFromWorkWeek(WorkWeeks workWeek)
        {
            TimeProfiles tp;
            DateTime nowtime = DateTime.Today;
            string weekDay = nowtime.DayOfWeek.ToString();

            if (weekDay == "Monday")
            {
                tp = workWeek.TimeProfiles;
            }
            else if (weekDay == "Tuesday")
            {
                tp = workWeek.TimeProfiles1;
            }
            else if (weekDay == "Wednesday")
            {
                tp = workWeek.TimeProfiles2;
            }
            else if (weekDay == "Thursday")
            {
                tp = workWeek.TimeProfiles3;
            }
            else if (weekDay == "Friday")
            {
                tp = workWeek.TimeProfiles4;
            }
            else if (weekDay == "Saturday")
            {
                tp = workWeek.TimeProfiles5;
            }
            else
            {
                tp = workWeek.TimeProfiles6;
            }

            return tp;
        }

        public static List<string> getTimeProfileRelations(int tp_id, WorkControlEntities db)
        {
            TimeProfiles tp = db.TimeProfiles.Where(t => t.TimeProfileId == tp_id).FirstOrDefault();
            if (tp != null)
            {
                var workWeeks = db.WorkWeeks.Where(week =>
                    week.Monday == tp.TimeProfileId
                    || week.Tuesday == tp.TimeProfileId
                    || week.Wednesday == tp.TimeProfileId
                    || week.Thursday == tp.TimeProfileId
                    || week.Friday == tp.TimeProfileId
                    || week.Saturday == tp.TimeProfileId
                    || week.Sunday == tp.TimeProfileId
                ).ToList();
                if (workWeeks.Count > 0)
                {
                    return workWeeks.Select(w => w.NameOfWeek).ToList();
                }
            }
            return new List<string>();
        }

        public static Exception removeTimeProfile(int tp_id, WorkControlEntities db)
        {
            try
            {
                //throw new Exception();
                TimeProfiles tp = db.TimeProfiles.Where(t => t.TimeProfileId == 666).First();

                var workWeeks = db.WorkWeeks.Where(week =>
                    week.Monday == tp.TimeProfileId
                    || week.Tuesday == tp.TimeProfileId
                    || week.Wednesday == tp.TimeProfileId
                    || week.Thursday == tp.TimeProfileId
                    || week.Friday == tp.TimeProfileId
                    || week.Saturday == tp.TimeProfileId
                    || week.Sunday == tp.TimeProfileId
                ).ToList();
                foreach (var week in workWeeks)
                {
                    if (week.Monday == tp.TimeProfileId)
                        week.Monday = null;
                    if (week.Tuesday == tp.TimeProfileId)
                        week.Tuesday = null;
                    if (week.Wednesday == tp.TimeProfileId)
                        week.Wednesday = null;
                    if (week.Thursday == tp.TimeProfileId)
                        week.Thursday = null;
                    if (week.Friday == tp.TimeProfileId)
                        week.Friday = null;
                    if (week.Saturday == tp.TimeProfileId)
                        week.Saturday = null;
                    if (week.Sunday == tp.TimeProfileId)
                        week.Sunday = null;
                }
                db.TimeProfiles.Remove(tp);
                db.SaveChanges();                
                return null;

            }
            catch (Exception e)
            {
                return e;
            }
        }
    }
}
