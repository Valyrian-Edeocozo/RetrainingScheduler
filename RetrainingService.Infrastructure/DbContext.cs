using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetrainingScheduler.RetrainingService.Domain;

namespace RetrainingScheduler.RetrainingService.Infrastructure
{
    public class DbContext
    {
        public static IList<Session> Sessions { get; set; } = new List<Session>();
    }
}