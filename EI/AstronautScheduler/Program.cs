using System;

class Program
{
    static void Main(string[] args)
    {
        var observerManager = new ObserverManager();
        var scheduleManager = ScheduleManager.GetInstance(observerManager);
        var taskObserver = new TaskObserver();
        observerManager.AddObserver(taskObserver);
        
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Remove Task");
            Console.WriteLine("3. View Tasks");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter task description: ");
                    string description = Console.ReadLine() ?? string.Empty;
                    
                    Console.Write("Enter start time (hh:mm): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime startTime))
                    {
                        Console.WriteLine("Invalid start time format.");
                        continue;
                    }
                    
                    Console.Write("Enter end time (hh:mm): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime endTime))
                    {
                        Console.WriteLine("Invalid end time format.");
                        continue;
                    }

                    Console.Write("Enter priority (High/Medium/Low): ");
                    string priority = Console.ReadLine() ?? "Medium";

                    if (string.IsNullOrEmpty(description) || string.IsNullOrEmpty(priority))
                    {
                        Console.WriteLine("Description and priority cannot be empty.");
                        continue;
                    }

                    Task task = TaskFactory.CreateTask(description, startTime, endTime, priority);
                    scheduleManager.AddTask(task);
                    observerManager.NotifyAll("Task added successfully.");
                    break;

                case "2":
                    Console.Write("Enter task description to remove: ");
                    description = Console.ReadLine() ?? string.Empty;
                    
                    if (string.IsNullOrEmpty(description))
                    {
                        Console.WriteLine("Task description cannot be empty.");
                        continue;
                    }

                    scheduleManager.RemoveTask(description);
                    break;

                case "3":
                    scheduleManager.ViewTasks();
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
