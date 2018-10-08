using System;
using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject
{
    /// <summary>
    /// Controlling  
    /// representation model values
    /// </summary>
    public class HeroActions : ITickable
    {
        private readonly HeroControllerState _controllerState;
        private readonly HeroModel _model;
        
        public HeroActions(
            HeroControllerState controllerState, 
            HeroModel model) {
            _controllerState = controllerState;
            _model = model;
        }

        public void Tick() {
            if(_model.IsActionPossible) {
                _model.IsPunching = _controllerState.IsPunching;
                _model.IsKicking = _controllerState.IsKicking;
            }
            // else { skip }
        }
    }
}
