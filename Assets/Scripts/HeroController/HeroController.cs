using UnityEngine;
using Zenject;
using UnityStandardAssets.CrossPlatformInput;

namespace OneEv9gPr0ject
{
    /// <summary>
    /// Capturing input and switching controller state accordingly
    /// </summary>
    public class HeroController : ITickable
    {
        private readonly HeroControllerState _state;

        public HeroController(HeroControllerState state) {
            _state = state;
        }

        public void Tick() {
            //// FIXME: should use virtual buttons
            //// for fast reassign
            //_state.IsMovingLeft = Input.GetKey(KeyCode.A);
            //_state.IsMovingRight = Input.GetKey(KeyCode.D);
            //_state.IsFiring = Input.GetKey(KeyCode.Space);
            //_state.IsPunching = Input.GetKey(KeyCode.J);
            //_state.IsKicking = Input.GetKey(KeyCode.K);
            //_state.DeviationX = (_state.IsMovingLeft ? -1 : (_state.IsMovingRight ? 1 : 0));

            _state.DeviationX = CrossPlatformInputManager.GetAxis("Horizontal");
            _state.DeviationX = (_state.DeviationX < -0.1f ? -1 : _state.DeviationX > 0.1f ? 1 : 0);
            _state.IsMovingLeft = (_state.DeviationX < -0.1f);
            _state.IsMovingRight = (_state.DeviationX > 0.1f);
            _state.IsFiring = false;
            _state.IsPunching = CrossPlatformInputManager.GetButton("Punch");
            _state.IsKicking = CrossPlatformInputManager.GetButton("Kick");
           
        }
    }
}
