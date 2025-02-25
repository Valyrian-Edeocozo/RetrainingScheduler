using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                int idWidth = scheduledSessions.Max(s => s.Id.ToString().Length) + 2;
                int nameWidth = scheduledSessions.Max(s => s.SessionName.Length) + 2;
                int facilitatorWidth = scheduledSessions.Max(s => s.FacilitatorName.Length) + 2;
                int durationWidth = scheduledSessions.Max(s => s.Duration.ToString().Length) + 2;
                int intervalWidth = scheduledSessions.Max(s => s.Interval.Length) + 2;

                string separator = new string('-', idWidth + nameWidth + facilitatorWidth + durationWidth + intervalWidth + 17);

                Console.WriteLine("\nScheduled Sessions:");
                Console.WriteLine(separator);
                Console.WriteLine($"| {"ID".PadRight(idWidth)} | {"Session Name".PadRight(nameWidth)} | {"Facilitator".PadRight(facilitatorWidth)} | {"Duration".PadRight(durationWidth)} | {"Interval".PadRight(intervalWidth)} |");
                Console.WriteLine(separator);

                foreach (var session in scheduledSessions)
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