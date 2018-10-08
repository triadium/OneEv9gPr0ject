using System;
using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject
{
    /// <summary>
    /// Controlling a movement velocity and 
    /// representation model values
    /// </summary>
    public class HeroMovement : ITickable // IFixedTickable
    {
        private readonly HeroControllerState _controllerState;
        private readonly HeroModel _model;
        private readonly Settings _settings;
        
        public HeroMovement(
            HeroControllerState controllerState, 
            HeroModel model,
            Settings settings) {
            _controllerState = controllerState;
            _model = model;
            _settings = settings;
        }

        public void Tick() { // FixedTick() {
            if(_model.IsMovementPossible) {

                if (_controllerState.IsMovingLeft) {
                    // TODO: make the parameter an enumeration
                    _model.Turn(toLeft: true);
                }
                else if (_controllerState.IsMovingRight) {
                    _model.Turn(toLeft: false);
                }
                // else { noop }
                _model.Velocity = new Vector2(_settings.MoveSpeed * _controllerState.DeviationX, 0);
                _model.IsMoving = (_controllerState.IsMovingLeft || _controllerState.IsMovingRight);
            }
            else {
                _model.IsMoving = false;
            }
        }

        [Serializable]
        public class Settings
        {
            public float MoveSpeed;
        }
    }
}
