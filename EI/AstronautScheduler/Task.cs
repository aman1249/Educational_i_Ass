using System;

public class Task
{
    public string Description { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public string Priority { get; }

    public Task(string description, DateTime startTime, DateTime endTime, string priority)
    {
        Description = description;
        StartTime = startTime;
        EndTime = endTime;
        Priority = priority;
    }
}
