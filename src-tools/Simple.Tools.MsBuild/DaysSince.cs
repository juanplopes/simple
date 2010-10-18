using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;

namespace Simple.Tools.MsBuild
{
    public class DaysSince : Task
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Output]
        public ITaskItem Output { get; set; }

        public override bool Execute()
        {
            Output = new TaskItem(((int)DateTime.Now.Subtract(StartDate).TotalDays).ToString());

            return true;
        }
    }
}
