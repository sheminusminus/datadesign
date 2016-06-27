using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmilyK_DDfinalproject
{
    class SimplePayEvent : SimpleEvent
    {
        public int PaymentAmount;
        public string PaymentType;

        public SimplePayEvent()
        {
            this.PaymentAmount = 0;
            this.PaymentType = "";
        }

        public SimplePayEvent(string name, int? id, string date, string type, string importance, string payType, string payAmount)
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
            this.PaymentType = payType;
            this.PaymentAmount = Int32.Parse(payAmount);
        }

    }
}
