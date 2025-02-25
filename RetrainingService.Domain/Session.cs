using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetrainingScheduler.RetrainingService.Domain
{
    public class Session
    {
        public int Id { get; set; }
        public string SessionName { get; set; }
        public string FacilitatorName { get; set; }
        public int Duration { get; set; }
        public bool IsScheduled { get; set; }
        public string Interval { get; set; }
    }
}