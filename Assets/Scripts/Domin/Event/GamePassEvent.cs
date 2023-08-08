using System;

public class GamePassEvent
{
    private static Action mOnEventTrigger;


    public static void Register(Action action)
    {
        mOnEventTrigger += action;
    }


    public static void Unregister(Action action)
    {
        mOnEventTrigger -= action;
    }

    public static void Trigger()
    {
        mOnEventTrigger?.Invoke();
    }
}