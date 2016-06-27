using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

// the idea was to get the calendar to update from my CalendarEvents database,
// based on the user's selection. you'll find my attempt to do this using
// Entity Framework in the Form1.cs file in AllEvents(). 
//i haven't gotten the calendar quite working yet- it will only bold the initial dates set.
// BUT, the program does retrieve the CalendarEvents db data!

namespace EmilyK_DDfinalproject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


 
    }// end program
}// end namespace
