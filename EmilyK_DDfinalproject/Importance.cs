//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmilyK_DDfinalproject
{
    using System;
    using System.Collections.Generic;
    
    public partial class Importance
    {
        public Importance()
        {
            this.CalendarEvents = new HashSet<CalendarEvent>();
        }
    
        public int ImportanceId { get; set; }
        public string ImportanceLevel { get; set; }
    
        public virtual ICollection<CalendarEvent> CalendarEvents { get; set; }
    }
}
