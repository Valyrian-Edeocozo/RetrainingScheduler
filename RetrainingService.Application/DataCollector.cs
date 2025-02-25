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
        public static List<SessionDto> CollectSessionsFromUser()
        {
            try
            {

                List<SessionDto> sessions = new List<SessionDto>();

                Console.WriteLine("Enter session details (type 'done' to finish):");

                Console.WriteLine("Enter start time (e.g., 10:00 AM): ");
                DateTime startTime = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter end time (e.g., 5:00 PM): ");
                DateTime endTime = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter break duration (in minutes): ");
                int breakDuration = int.Parse(Console.ReadLine());

                TrainingInterval trainingInterval = new TrainingInterval((int)(endTime - startTime).TotalMinutes);
                int totalAvailableMinutes = trainingInterval.Time - breakDuration;
                int remainingProgramInterval = totalAvailableMinutes;
                int i = 0;

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

                    SessionDto session = new SessionDto
                    {
                        Id = i + 1,
                        SessionName = sessionName,
                        Duration = sessionDuration,
                        FacilitatorName = facilitatorName
                    };

                    sessions.Add(session);
                    remainingProgramInterval -= sessionDuration;
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