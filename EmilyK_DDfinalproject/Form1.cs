using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// the idea was to get the calendar to update from my CalendarEvents database,
// based on the user's selection. you'll find my attempt to do this using
// Entity Framework below, in the AllEvents() method. 
// i haven't gotten the calendar quite working yet- it will only bold
// the initial dates that are set when the form loads.
// BUT the program does retrieve the CalendarEvents db data!

namespace EmilyK_DDfinalproject
{
    public partial class Form1 : Form
    {
        public List<SimpleEvent> EventList;
        public List<SimpleEvent> DisplayEvents;
        public int Month;
        public int Day;
        public int Year;
        public string Mode;

        public Form1()
        {
            InitializeComponent();
            Month = DateTime.Today.Month;
            Mode = "Month";
            Day = DateTime.Today.Day;
            Year = DateTime.Today.Year;
            EventList = AllEvents();
            DisplayEvents = GetDisplayEvents(Mode);
            SetBoldDates();
        }


        public List<SimpleEvent> AllEvents()
        {
            List<SimpleEvent>ses = new List<SimpleEvent>();

            using (var cxt = new CalendarEventsEntities2())
            {
                var events = from ce in cxt.CalendarEvents
                             join i in cxt.Importances on ce.Importance equals i.ImportanceId
                             join et in cxt.EventTypes on ce.EventType equals et.TypeId
                             join p in cxt.Persons on ce.PrimaryPerson equals p.PersonId
                             join p2 in cxt.Persons on ce.SecondaryPerson equals p2.PersonId
                             join r in cxt.Relations on p.Relation equals r.RelationId
                             join r2 in cxt.Relations on p2.Relation equals r2.RelationId
                             join pmt in cxt.Payments on ce.EventId equals pmt.EventId
                             join pt in cxt.PaymentTypes on pmt.PaymentType equals pt.TypeId
                             join re in cxt.Recurrings on ce.RecurringType equals re.RecurringId
                            
                            select new
                            {
                                id = ce.EventId,
                                name = ce.EventName,
                                date = ce.EventDate.ToString(),
                                typeName = et.TypeName,
                                imp = i.ImportanceLevel,
                                primPerson = p.FirstName + " " + p.LastName,
                                primRel = r.Relation1,
                                secPerson = p2.FirstName + " " + p2.LastName,
                                secRel = r2.Relation1,
                                rec = re.RecurringType,
                                recInt = re.RecurringId,
                                amt = pmt.PaymentAmount.ToString(),
                                payType = pt.TypeName
                            };

                foreach (var e in events)
                {
                    SimpleEvent se;
                    if (e.typeName == "Payments")
                    {
                        se = new SimplePayEvent(e.name, e.id, e.date, e.typeName, e.imp, e.payType, e.amt);
                    }
                    else
                    {
                        se = new SimpleEvent(e.name, e.id, e.date, e.typeName, e.imp);
                    }
                    if (e.primPerson != null)
                    {
                        se.Person1 = e.primPerson;
                        se.Relation1 = e.primRel;
                    }
                    if (e.secPerson != null)
                    {
                        se.Person2 = e.secPerson;
                        se.Relation2 = e.secRel;
                    }
                    if (e.recInt > 0)
                    {
                        se.RecurringType = e.rec;
                    }
                    ses.Add(se);
                }
            }
            return ses;
        }

        public void SetModeUpdateEvents(string m)
        {
            Mode = m;
            DisplayEvents = GetDisplayEvents(m);
        }

        public void SetDate(DateTime date)
        {
            Month = date.Month;
            Day = date.Day;
            Year = date.Year;
        }

        public void SetDate(int y)
        {
            Year = y;
        }

        public void SetDate(int y, int m)
        {
            Month = m;
            Year = m;
        }

        public void SetDate(int y, int m, int d)
        {
            Month = m;
            Day = d;
            Year = y;
        }

        public List<SimpleEvent> GetDisplayEvents(string m)
        {
            if (m == "Month")
            {
                return GetMonthEvents();
            }
            else if (m == "Day")
            {
                return GetDayEvents();
            }
            else if (m == "Year")
            {
                return GetYearEvents();
            }
            else
            {
                return EventList;
            }
        }


        public List<SimpleEvent> GetYearEvents()
        {
            List<SimpleEvent> ses = new List<SimpleEvent>();

            foreach (var s in EventList)
            {
                if (s.RecurringType == "Yearly")
                {
                    ses.Add(s);
                }
            }

            return ses;
        }

        public List<SimpleEvent> GetMonthEvents()
        {
            List<SimpleEvent> ses = new List<SimpleEvent>();

            foreach (var s in EventList)
            {
                if (s.RecurringType == "Monthly")
                {
                    ses.Add(s);
                }
            }

            return ses;
        }

        public List<SimpleEvent> GetDayEvents()
        {
            List<SimpleEvent> ses = new List<SimpleEvent>();

            foreach (var s in EventList)
            {
                if (s.RecurringType == null)
                {
                    ses.Add(s);
                }
            }

            return ses;
        }

        public DateTime[] BoldDates()
        {
            DateTime[] dates = new DateTime[DisplayEvents.Count() - 1];
            for (int i = 0; i < dates.Count(); i++)
            {
                dates[i] = DisplayEvents[i].Date;

            }
            return dates;
        }

        public void SetBoldDates()
        {
            calendar.RemoveAllBoldedDates();
            calendar.RemoveAllMonthlyBoldedDates();
            calendar.RemoveAllAnnuallyBoldedDates();
            DateTime[] addD = BoldDates();
            if (Mode == "Day")
            {
                foreach (DateTime d in addD)
                {
                    calendar.AddBoldedDate(d);

                }
            }
            if (Mode == "Month")
            {
                foreach (DateTime d in addD)
                {
                    calendar.AddMonthlyBoldedDate(d);
                }
            }
            if (Mode == "Year")
            {
                foreach (DateTime d in addD)
                {
                    calendar.AddAnnuallyBoldedDate(d);
                }
            }
            if (Mode == "All")
            {
                foreach (var e in EventList)
                {
                    switch (e.RecurringType)
                    {
                        case "Yearly":
                            calendar.AddAnnuallyBoldedDate(e.Date);
                            break;
                        case "Monthly":
                            calendar.AddMonthlyBoldedDate(e.Date);
                            break;
                        default:
                            calendar.AddBoldedDate(e.Date);
                            break;
                    }
                }
            }
        }

        private void modeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modeBox.SelectedText == "Monthly")
            {
                if (Mode != "Month")
                {
                    SetModeUpdateEvents("Month");
                    calendar.MonthlyBoldedDates = BoldDates();
                }
            }
            else if (modeBox.SelectedText == "Once")
            {
                if (Mode != "Day")
                {
                    SetModeUpdateEvents("Day");
                    calendar.BoldedDates = BoldDates();
                }
            }
            else if (modeBox.SelectedText == "Yearly")
            {
                if (Mode != "Year")
                {
                    SetModeUpdateEvents("Year");
                    calendar.AnnuallyBoldedDates = BoldDates();
                }
            }
            else
            {
                SetModeUpdateEvents("All");
            }
            calendar.BoldedDates = BoldDates();
        }


    }
}
