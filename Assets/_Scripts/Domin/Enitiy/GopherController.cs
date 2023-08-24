using _Scripts.Domin.Event;
using Domin.Event;
using UnityEngine;

namespace Domin.Enitiy
{
    public class GopherController : MonoBehaviour
    {
        private readonly float _existTime = 0.8f;
        private float _timer;
        private Animator _animator;
        private static readonly int HasHurt = Animator.StringToHash("hasHurt");
        private static readonly int HasTimeOut = Animator.StringToHash("hasTimeOut");
        public Hole Hole { get; set; }

        private void OnMouseDown()
        {
            var animator = GetAnimator();
            animator.SetBool(HasHurt, true);
        }

        private void Update()
        {
            if (GameSystem.Instance.GameState == GameState.GamePlaying)
            {
                _timer += Time.deltaTime;

                if (_timer >= _existTime)
                {
                    _timer = 0;

                    var animator = GetAnimator();
                    animator.SetBool(HasTimeOut, true);
                }
            }
        }

        private Animator GetAnimator() => _animator ? _animator : (_animator = GetComponent<Animator>());
    }
}