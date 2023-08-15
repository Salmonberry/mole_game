using System;
using Domin.Enitiy;
using Domin.Event;
using UnityEngine;

namespace _Scripts.Domin.Event
{
    public class MoleAnimationEventHandler : MonoBehaviour
    {
        public void OnHideAnimationFinishedTrigger()
        {
            var gopherController = GetComponent<GopherController>();
            GopherRecycleEvent.Trigger(gopherController);
            
            HoleManager.Instance.RecycleHole(gopherController.Hole);
        }

        public void OnHurtAnimationFinishedTrigger()
        {
            var gopherController = GetComponent<GopherController>();
            
            // 被点击对象必须挂载一个collider，才能被检测到
            print("onMouseDown");
            GopherRecycleEvent.Trigger(gopherController);
            ScoreCalculationEvent.Trigger(1);
            
            HoleManager.Instance.RecycleHole(gopherController.Hole);
        }
    }
}