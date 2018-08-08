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
        private readonly HeroModel _model;

        public HeroController(HeroControllerState state, HeroModel model) {
            _state = state;
            _model = model;
        }

        public void Tick() {
            if (_model.IsReady) {
                // FIXME: should use virtual buttons
                // for fast reassign
                _state.IsMovingLeft = Input.GetKey(KeyCode.A);
                _state.IsMovingRight = Input.GetKey(KeyCode.D);
                _state.IsFiring = Input.GetKey(KeyCode.Space);
                _state.IsStriking = Input.GetKey(KeyCode.J);
            }
            // else { noop }
        }
    }
}
