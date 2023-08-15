using System;

namespace Domin.Event
{
    public static class GameEndEvent
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
}