using System;
using System.Collections.Generic;
using System.Linq;
using RetrainingScheduler.RetrainingService.Domain.Dto;

namespace RetrainingScheduler.RetrainingService.Presentation
{
    public class Dashboard
    {
        public static void DisplayScheduledSessions(List<SessionDto> scheduledSessions)
        {
            Console.WriteLine("Do you want to see the scheduled sessions? (yes/no): ");
            string response = Console.ReadLine().Trim().ToLower();

            if (response == "yes")
            {
                // Add lunch and sharing session as unique entries
                var allSessions = new List<SessionDto>(scheduledSessions);

                // Add Lunch/Break Time
                allSessions.Add(new SessionDto
                {
                    Id = -1, // Special ID for lunch
                    SessionName = "Lunch Break",
                    FacilitatorName = "N/A",
                    Duration = 60, // 1 hour
                    Interval = "12:00 PM - 1:00 PM"
                });

                // Add Sharing Session Time
                allSessions.Add(new SessionDto
                {
                    Id = -2,
                    SessionName = "Sharing Session",
                    FacilitatorName = "All Participants",
                    Duration = 90, // 1.5 hours
                    Interval = "4:00 PM - 5:30 PM"
                });

                // Sort sessions by start time using the interval
                allSessions = allSessions
                    .OrderBy(s => DateTime.Parse(s.Interval.Split(" - ")[0]))
                    .ToList();

                // Move sharing session to the last row
                var sharingSession = allSessions.FirstOrDefault(s => s.Id == -2);
                if (sharingSession != null)
                {
                    allSessions.Remove(sharingSession);
                    allSessions.Add(sharingSession);
                }

                // Calculate column widths
                int idWidth = allSessions.Max(s => s.Id.ToString().Length) + 2;
                int nameWidth = allSessions.Max(s => s.SessionName.Length) + 2;
                int facilitatorWidth = allSessions.Max(s => s.FacilitatorName.Length) + 2;
                int durationWidth = allSessions.Max(s => s.Duration.ToString().Length) + 2;
                int intervalWidth = allSessions.Max(s => s.Interval.Length) + 2;

                string separator = new string('-', idWidth + nameWidth + facilitatorWidth + durationWidth + intervalWidth + 17);

                Console.WriteLine("\nScheduled Sessions:");
                Console.WriteLine(separator);
                Console.WriteLine($"| {"ID".PadRight(idWidth)} | {"Session Name".PadRight(nameWidth)} | {"Facilitator".PadRight(facilitatorWidth)} | {"Duration".PadRight(durationWidth)} | {"Interval".PadRight(intervalWidth)} |");
                Console.WriteLine(separator);

                foreach (var session in allSessions)
                {
                    Console.WriteLine($"| {session.Id.ToString().PadRight(idWidth)} | {session.SessionName.PadRight(nameWidth)} | {session.FacilitatorName.PadRight(facilitatorWidth)} | {session.Duration.ToString().PadRight(durationWidth)} | {session.Interval.PadRight(intervalWidth)} |");
                }

                Console.WriteLine(separator);
            }
            else
            {
                Console.WriteLine("You chose not to display the scheduled sessions.");
            }
        }
    }
}