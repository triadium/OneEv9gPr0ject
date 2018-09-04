using UnityEngine;

namespace OneEv9gPr0ject
{
    /// <summary>
    /// Switching of the animator parameters
    /// based on hero model data
    /// </summary>
    public class HeroAnimationState
    {
        private readonly Animator _animator;

        public enum State
        {
            Spawn,
            Ready,
            Idle,
            Stunned,
            Move,
            Die
        }

        public HeroAnimationState(Animator animator) {
            _animator = animator;
        }

        public void SetState(State state) {
            
            //Reset();

            //switch (state) {
            //    case State.Spawn:
            //        _animator.SetBool("IsSpawning", true);
            //        break;
            //    case State.Ready:
            //        _animator.SetBool("IsReady", true);
            //        break;
            //    case State.Idle:
            //        _animator.SetBool("IsIdle", true);
            //        break;
            //    case State.Stunned:
            //        _animator.SetBool("IsStunned", true);
            //        break;
            //    case State.Move:
            //        _animator.SetBool("IsMoving", true);
            //        break;
            //    case State.Die:
            //        _animator.SetBool("IsDead", true);
            //        break;
            //        // default: noop break;
            //}
        }

        private void Reset() {
            _animator.SetBool("IsSpawning", false);
            _animator.SetBool("IsIdle", false);
            _animator.SetBool("IsStunned", false);
            _animator.SetBool("IsMoving", false);
            _animator.SetBool("IsDead", false);
        }
    }
}
