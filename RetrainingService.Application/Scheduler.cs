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
        public List<SessionDto> ScheduleSessions(List<SessionDto> sessions)
        {

            sessions.Sort((a, b) => a.Duration.CompareTo(b.Duration));

            Queue<SessionDto> sessionQueue = new Queue<SessionDto>(sessions);

            while (sessionQueue.Count > 0)
            {
                SessionDto currentSession = sessionQueue.Peek();

                var session = new Session
                {
                    Id = currentSession.Id,
                    SessionName = currentSession.SessionName,
                    FacilitatorName = currentSession.FacilitatorName,
                    Duration = currentSession.Duration,
                    IsScheduled = true
                };
                DbContext.Sessions.Add(session);
                sessionQueue.Dequeue();
            }

            return DbContext.Sessions.Select(s => new SessionDto
            {
                Id = s.Id,
                SessionName = s.SessionName,
                FacilitatorName = s.FacilitatorName,
                Duration = s.Duration,
                IsScheduled = s.IsScheduled
            }).ToList();
        }
    }
}
