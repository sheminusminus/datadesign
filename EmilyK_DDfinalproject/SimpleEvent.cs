using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmilyK_DDfinalproject
{
    public class SimpleEvent
    {
        public string Name;
        public int? ID;
        public DateTime Date;
        public string Type;
        public string Importance;
        public int Month;
        public string Person1;
        public string Relation1;
        public string Person2;
        public string Relation2;
        public string RecurringType;

        public SimpleEvent()
        {
            this.Name = "Default";
            this.ID = 0;
            this.Date = DateTime.Today;
            this.Type = "Default";
            this.Importance = "Medium";
            this.Month = Date.Month;
            this.Person1 = "";
            this.Relation1 = "";
            this.Person2 = "";
            this.Relation2 = "";
            this.RecurringType = "";

        }

        public SimpleEvent(string name, int? id, string date, string type, string importance)
        {
            this.Name = name;
            this.ID = id;
            this.Date = DateTime.Parse(date);
            this.Month = this.Date.Month;
            this.Type = type;
            this.Importance = importance;
            this.Person1 = "";
            this.Relation1 = "";
            this.Person2 = "";
            this.Relation2 = "";
            this.RecurringType = "";
        }

    }
}
