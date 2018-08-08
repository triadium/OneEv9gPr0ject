﻿using System;
using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject
{
    /// <summary>
    /// Controlling a movement velocity and 
    /// representation model values
    /// </summary>
    public class HeroMovement : IFixedTickable
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

        public void FixedTick() {
            if(!_model.IsSpawning && !_model.IsStunned && !_model.IsDying && !_model.IsDead) {

                if (_controllerState.IsMovingLeft) {
                    // TODO: make the parameter an enumeration
                    _model.Turn(true);
                    _model.Velocity = Vector2.left * _settings.MoveSpeed;
                }
                else if (_controllerState.IsMovingRight) {
                    _model.Turn(false);
                    _model.Velocity = Vector2.right * _settings.MoveSpeed;
                }
                // else { noop }

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
