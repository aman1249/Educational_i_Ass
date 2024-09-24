using System;

public class TaskConflictException : Exception
{
    public TaskConflictException(string message) : base(message)
    {
    }
}
