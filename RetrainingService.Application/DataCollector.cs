using System;
using System.Collections.Generic;
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

                DateTime morningStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 0, 0); // Morning session starts at 9 AM
                DateTime lunchStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0); // Lunch starts at 12 PM
                DateTime afternoonStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 0, 0); // Afternoon session starts at 1 PM
                DateTime sharingSessionStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0, 0); // Sharing session starts at 4 PM

                DateTime currentStartTime = morningStartTime;
                int sessionId = 1;

                while (true)
                {
                    Console.WriteLine("Enter the session name or 'done' to finish: ");
                    string sessionName = Console.ReadLine();

                    if (sessionName.ToLower() == "done")
                    {
                        break;
                    }

                    Console.WriteLine("Enter the facilitator name: ");
                    string facilitatorName = Console.ReadLine();

                    Console.WriteLine("Session Duration (in Minutes): ");
                    int sessionDuration = int.Parse(Console.ReadLine());

                    // Checking if the session fits in the morning or afternoon
                    if (currentStartTime < lunchStartTime)
                    {
                        // Morning session
                        TimeSpan remainingMorningTime = lunchStartTime - currentStartTime;
                        if (sessionDuration > remainingMorningTime.TotalMinutes)
                        {
                            Console.WriteLine($"Session duration exceeds the available morning time. Only {remainingMorningTime.TotalMinutes} minutes are left before lunch.");
                            Console.WriteLine("Please enter a shorter duration or type '0' to skip this session.\n" +
                                              "If you specify a duration greater than the remaining morning time, we will automatically move the session to the afternoon.");
                            sessionDuration = int.Parse(Console.ReadLine());

                            if (sessionDuration == 0)
                            {
                                continue;
                            }

                            if (sessionDuration > remainingMorningTime.TotalMinutes)
                            {
                                Console.WriteLine("Session still doesn't fit. Moving it to the afternoon...");
                                currentStartTime = afternoonStartTime;
                            }
                        }
                    }

                    if (currentStartTime >= afternoonStartTime && currentStartTime < sharingSessionStartTime)
                    {
                        // Afternoon session
                        TimeSpan remainingAfternoonTime = sharingSessionStartTime - currentStartTime;
                        if (sessionDuration > remainingAfternoonTime.TotalMinutes)
                        {
                            Console.WriteLine($"Session duration exceeds the available afternoon time. Only {remainingAfternoonTime.TotalMinutes} minutes are left before the sharing session.");
                            Console.WriteLine("Please enter a shorter duration or type '0' to skip this session.");
                            sessionDuration = int.Parse(Console.ReadLine());

                            if (sessionDuration == 0)
                            {
                                continue;
                            }

                            if (sessionDuration > remainingAfternoonTime.TotalMinutes)
                            {
                                Console.WriteLine("Session still doesn't fit. No more slots available.");
                                break;
                            }
                        }
                    }
                    else if (currentStartTime >= sharingSessionStartTime)
                    {
                        Console.WriteLine("No more available slots for sessions. The day is fully booked.");
                        break;
                    }

                    DateTime sessionEndTime = currentStartTime.AddMinutes(sessionDuration);
                    string interval = $"{currentStartTime.ToString("h:mm tt")} - {sessionEndTime.ToString("h:mm tt")}";

                    SessionDto session = new SessionDto
                    {
                        Id = sessionId++,
                        SessionName = sessionName,
                        Duration = sessionDuration,
                        FacilitatorName = facilitatorName,
                        Interval = interval
                    };

                    sessions.Enqueue(session);
                    currentStartTime = sessionEndTime;

                    // If the session ends at or after 12 PM, skip to the afternoon
                    if (currentStartTime >= lunchStartTime && currentStartTime < afternoonStartTime)
                    {
                        currentStartTime = afternoonStartTime;
                    }
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