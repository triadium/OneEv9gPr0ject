using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject
{
    /// <summary>
    /// Controlling of the animator states
    /// </summary>
    public class HeroAnimatorProcessor : ITickable
    {
        private readonly Animator _animator;
        private readonly HeroModel _model;

        public HeroAnimatorProcessor(Animator animator, HeroModel model) {
            _animator = animator;
            _model = model;
        }

        public void Tick() {
            Move();
            Actions();
            Dead();
        }

        private void Move() {
            _animator.SetBool("IsMoving", _model.IsMoving);
        }
    
        private void Actions() {
            _animator.SetBool("IsPunching", _model.IsPunching);
            _animator.SetBool("IsKicking", _model.IsKicking);
        }

        private void Dead() {
            if (_model.IsDying && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Hero Dying Animation")) {
                _animator.SetTrigger("Died");
            }
            // else { skip }
        }
    }
}

