// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using RetrainingScheduler.RetrainingService.Application;
using RetrainingScheduler.RetrainingService.Domain.Dto;
using RetrainingScheduler.RetrainingService.Presentation;

//Collect input data
List<SessionDto> sessions = DataCollector.CollectSessionsFromUser();

if (sessions == default)
{
    Console.WriteLine("No sessions to schedule");
    Environment.Exit(0);
}

// Scheduled or sessions
Scheduler scheduler = new Scheduler();
List<SessionDto> scheduledSessions = scheduler.ScheduleSessions(sessions);

// Display All scheduled sessions
Dashboard.DisplayScheduledSessions(scheduledSessions);

//Move the schedules to a background task to notify user 10 minutes before the session starts
//_ = Task.Run(() => scheduler.NotifyUser(scheduledSessions));

Environment.Exit(0);



