using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject
{
    /// <summary>
    /// Capturing input and switching controller state accordingly
    /// </summary>
    public class HeroController : ITickable
    {
        private readonly HeroControllerState _state;
        private readonly HeroFacade _hero;

        public HeroController(HeroControllerState state, HeroFacade hero) {
            _state = state;
            _hero = hero;
        }

        public void Tick() {
            if (_hero.IsReady) {
                // FIXME: should use virtual buttons
                // for fast reassign
                _state.IsMovingLeft = Input.GetKey(KeyCode.A);
                _state.IsMovingRight = Input.GetKey(KeyCode.D);
                _state.IsFiring = Input.GetKey(KeyCode.Space);
                _state.IsStriking = Input.GetKey(KeyCode.J);

                _state.DeviationX = (_state.IsMovingLeft ? -1 : (_state.IsMovingRight ? 1 : 0));
            }
            // else { noop }
        }
    }
}
