using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetrainingScheduler.RetrainingService.Application.Constants;
using RetrainingScheduler.RetrainingService.Domain.Dto;

namespace RetrainingScheduler.RetrainingService.Application
{
    public class DataCollector
    {
        public static Queue<SessionDto> CollectSessionsFromUser()
        {
            try
            {
                Queue<SessionDto> sessions = new Queue<SessionDto>();

                Console.WriteLine("Enter session details (type 'done' to finish):");

                Console.WriteLine("Enter start time (e.g., 10:00 AM): ");
                DateTime startTime = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter end time (e.g., 5:00 PM): ");
                DateTime endTime = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter break duration (in minutes): ");
                int breakDuration = int.Parse(Console.ReadLine());

                int totalAvailableMinutes = (int)(endTime - startTime).TotalMinutes - breakDuration;
                int remainingProgramInterval = totalAvailableMinutes;
                int i = 0;

                DateTime currentStartTime = startTime;

                while (true)
                {
                    Console.WriteLine("Enter the session name or 'done' to discontinue: ");
                    string sessionName = Console.ReadLine();

                    if (sessionName == "done")
                    {
                        break;
                    }

                    Console.WriteLine("Enter the facilitator name: ");
                    string facilitatorName = Console.ReadLine();

                    Console.WriteLine("Session Duration (in Minutes): ");
                    int sessionDuration = int.Parse(Console.ReadLine());

                    while (sessionDuration <= 0 || sessionDuration > remainingProgramInterval)
                    {
                        Console.WriteLine("Session duration exceeds the available time ({0} minutes). Please enter a valid duration. Or enter (0) to quit", remainingProgramInterval);
                        sessionDuration = int.Parse(Console.ReadLine());
                        if (sessionDuration == 0)
                        {
                            break;
                        }
                    }

                    if (sessionDuration == 0)
                    {
                        break;
                    }

                    DateTime sessionEndTime = currentStartTime.AddMinutes(sessionDuration);
                    string interval = $"{currentStartTime.ToString("h:mm tt")} - {sessionEndTime.ToString("h:mm tt")}";

                    SessionDto session = new SessionDto
                    {
                        Id = i + 1,
                        SessionName = sessionName,
                        Duration = sessionDuration,
                        FacilitatorName = facilitatorName,
                        Interval = interval
                    };

                    sessions.Enqueue(session);
                    remainingProgramInterval -= sessionDuration;
                    currentStartTime = sessionEndTime;
                    i++;
                }

                return sessions;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred. Please retry again: " + ex.Message);
            }

            return default;
        }
    }
}