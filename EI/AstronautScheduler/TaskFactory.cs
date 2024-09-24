public static class TaskFactory
{
    public static Task CreateTask(string description, DateTime startTime, DateTime endTime, string priority)
    {
        return new Task(description, startTime, endTime, priority);
    }
}
