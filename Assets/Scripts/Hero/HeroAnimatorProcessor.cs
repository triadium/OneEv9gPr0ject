using System;
using Zenject;

namespace OneEv9gPr0ject
{
    /// <summary>
    /// Controlling of the animator states
    /// </summary>
    public class HeroAnimatorProcessor : ITickable
    {
        // FIXME: prefix `hero` is removed - 
        // the way to not rewrite the code 
        // when generating abstract classes or interfaces
        private readonly HeroAnimationState _animationState;
        private readonly HeroModel _model;

        public HeroAnimatorProcessor(HeroAnimationState animationState, HeroModel model) {
            _animationState = animationState;
            _model = model;
        }

        public void Tick() {
            Spawn();
            Idle();
            Move();
            Freeze();
            Die();
        }

        private void Spawn() {
            if (_model.IsReady) {
                _animationState.SetState(HeroAnimationState.State.Ready);
            }
            // else { noop }
        }

        private void Idle() {
            if (!_model.IsMoving) {
                _animationState.SetState(HeroAnimationState.State.Idle);
            }
            // else { noop }
        }

        private void Move() {
            if (_model.IsMoving) {
                _animationState.SetState(HeroAnimationState.State.Move);
            }
            // else { noop }
        }

        private void Freeze() {
            if (_model.IsStunned) {
                _animationState.SetState(HeroAnimationState.State.Stunned);
            }
            // else { noop }
        }

        private void Die() {
            if (_model.IsDead) {
                _animationState.SetState(HeroAnimationState.State.Die);
            }
            // else { noop }
        }
    }
}
