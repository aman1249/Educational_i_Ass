using System;
using System.Collections.Generic;

public interface IObserver
{
    void Update(string message);
}

public class TaskObserver : IObserver
{
    public void Update(string message)
    {
        Console.WriteLine($"[Observer] {message}");
    }
}

public class ObserverManager
{
    private List<IObserver> observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void NotifyAll(string message)
    {
        foreach (var observer in observers)
        {
            observer.Update(message);
        }
    }
}
