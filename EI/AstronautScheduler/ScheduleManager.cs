using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

public class ScheduleManager
{
private static ScheduleManager? _instance;  // Mark as nullable
    private List<Task> tasks;
    private readonly ObserverManager _observerManager;

    private ScheduleManager(ObserverManager observerManager)
    {
        tasks = new List<Task>();
        _observerManager = observerManager;
    }

    public static ScheduleManager GetInstance(ObserverManager observerManager)
    {
        if (_instance == null)
            _instance = new ScheduleManager(observerManager);
        return _instance;
    }

    public void AddTask(Task task)
    {
        try
        {
            if (!CheckOverlap(task))
            {
                tasks.Add(task);
                LogManager.GetCurrentClassLogger().Info($"Task added: {task.Description}");
                NotifyObservers();
            }
            else
            {
                throw new TaskConflictException("Task conflicts with an existing task.");
            }
        }
        catch (TaskConflictException ex)
        {
            LogManager.GetCurrentClassLogger().Error(ex, ex.Message);
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            LogManager.GetCurrentClassLogger().Error(ex, "Unexpected error during task addition.");
            Console.WriteLine("An unexpected error occurred.");
        }
    }

    public void RemoveTask(string description)
    {
        var task = tasks.Find(t => t.Description == description);
        if (task != null)
        {
            tasks.Remove(task);
            Console.WriteLine("Task removed successfully.");
        }
        else
        {
            Console.WriteLine("Error: Task not found.");
        }
    }

    public void ViewTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks scheduled for the day.");
            return;
        }

        foreach (var task in tasks.OrderBy(t => t.StartTime))
        {
            Console.WriteLine($"{task.StartTime} - {task.EndTime}: {task.Description} [{task.Priority}]");
        }
    }

    private bool CheckOverlap(Task newTask)
    {
        foreach (var task in tasks)
        {
            if (!(newTask.EndTime <= task.StartTime || newTask.StartTime >= task.EndTime))
            {
                return true;
            }
        }
        return false;
    }

    private void NotifyObservers()
    {
        _observerManager.NotifyAll("A task was added or removed.");
    }
}
