using System;

public class Event<T> where T : Event<T>
{
    public static event Action mOnEventTriger;

    public static void Register(Action onEvent)
    {
        mOnEventTriger += onEvent;
    }

    public static void Unregister(Action onEvent)
    {
        mOnEventTriger -= onEvent;
    }

    public static void Trigger()
    {
        mOnEventTriger?.Invoke();
    }
}