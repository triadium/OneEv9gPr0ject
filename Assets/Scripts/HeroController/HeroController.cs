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

        //
        HeroFacade _hero;

        public HeroController(HeroControllerState state, HeroFacade hero) {
            _state = state;
            //
            _hero = hero;
        }

        public void Tick() {
            // FIXME: should use virtual buttons
            // for fast reassign
            _state.IsMovingLeft = Input.GetKey(KeyCode.A);
            _state.IsMovingRight = Input.GetKey(KeyCode.D);
            _state.IsFiring = Input.GetKey(KeyCode.Space);
            _state.IsPunching = Input.GetKey(KeyCode.J);
            _state.IsKicking = Input.GetKey(KeyCode.K);

            _state.DeviationX = (_state.IsMovingLeft ? -1 : (_state.IsMovingRight ? 1 : 0));

            //
            if (Input.GetKey(KeyCode.O)) { _hero.Die();  }
        }
    }
}
