using System;
using Domin.Enitiy;

namespace Domin.Event
{
    public static class GopherRecycleEvent
    {
        private static Action<GopherController> _mOnEventTrigger;


        public static void Register(Action<GopherController> action)
        {
            _mOnEventTrigger += action;
        }


        public static void Unregister(Action<GopherController> action)
        {
            _mOnEventTrigger -= action;
        }

        public static void Trigger(GopherController gopherController)
        {
            _mOnEventTrigger?.Invoke(gopherController);
        }
    }
}