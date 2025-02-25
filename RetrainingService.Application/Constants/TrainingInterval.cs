using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetrainingScheduler.RetrainingService.Application.Constants
{
    public struct TrainingInterval
    {
        public int Time;

        public TrainingInterval(int time)
        {
            Time = time;
        }
    }
}