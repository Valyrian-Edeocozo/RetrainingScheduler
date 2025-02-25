using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetrainingScheduler.RetrainingService.Domain;
using RetrainingScheduler.RetrainingService.Domain.Dto;
using RetrainingScheduler.RetrainingService.Infrastructure;

namespace RetrainingScheduler.RetrainingService.Application
{
    public class Scheduler
    {
        public List<SessionDto> ScheduleSessions(Queue<SessionDto> sessions)
        {

            while (sessions.Count > 0)
            {
                SessionDto currentSession = sessions.Peek();

                var session = new Session
                {
                    Id = currentSession.Id,
                    SessionName = currentSession.SessionName,
                    FacilitatorName = currentSession.FacilitatorName,
                    Duration = currentSession.Duration,
                    IsScheduled = true,
                    Interval = currentSession.Interval
                };
                DbContext.Sessions.Enqueue(session);
                sessions.Dequeue();
            }

            return DbContext.Sessions.Select(s => new SessionDto
            {
                Id = s.Id,
                SessionName = s.SessionName,
                FacilitatorName = s.FacilitatorName,
                Duration = s.Duration,
                IsScheduled = s.IsScheduled,
                Interval = s.Interval
            }).ToList();
        }
    }
}
