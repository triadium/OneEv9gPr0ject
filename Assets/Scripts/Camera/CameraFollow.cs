using System;
using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject
{
    public class CameraFollow : MonoBehaviour
    {
        public float verticalOffset;
        public float lookAheadDistance;
        public float lookAheadSmoothTime;
        public float verticalSmoothTime;
        public Vector2 focusAreaSize;
        public float stopDistance;
        public float cameraMargin;
        public float cameraDistance;

        private HeroControllerState _controllerState;
        private HeroFacade _hero;
        private BattleFieldModel _battleFieldModel;

        private Camera _camera;
        private FocusArea _focusArea;

        private float _currentLookAhead;
        private float _targetLookAhead;
        private float _lookAheadDirection;
        private float _smoothLookVelocity;
        private float _smoothVerticalVelocity;
        private bool _lookAheadStopped;

        [Inject]
        void Construct(HeroControllerState controllerState, HeroFacade hero, BattleFieldModel battleFieldModel) {
            _controllerState = controllerState;
            _hero = hero;
            _battleFieldModel = battleFieldModel;
        }

        void Start() {
            float focusAreaHalfWidth = focusAreaSize.x * 0.5f;

            _camera = GetComponent<Camera>();
            _camera.orthographicSize = (stopDistance + focusAreaHalfWidth) * _camera.scaledPixelHeight / _camera.scaledPixelWidth;


            float cameraHalfWidth = _camera.orthographicSize * _camera.aspect;
            Bounds battleFieldBounds = _battleFieldModel.Bounds;
            float maxWidth = battleFieldBounds.size.x - 2 * (cameraHalfWidth + lookAheadDistance - focusAreaHalfWidth - cameraMargin);
            float maxBoundsCenterY = battleFieldBounds.max.y + maxWidth * 0.5f;
            Vector2 maxBoundsCenter = new Vector2(battleFieldBounds.center.x, maxBoundsCenterY);
            Vector2 maxBoundsSize = new Vector2(maxWidth, maxWidth);

            _focusArea = new FocusArea(
                focusAreaSize,
                new Bounds(maxBoundsCenter, maxBoundsSize),
                _hero.Bounds);

            _lookAheadStopped = true;
        }

        void LateUpdate() {

            _focusArea.Update(_hero.Bounds);

            Vector2 focusAreaCenter = _focusArea.bounds.center;
            Vector2 focusPosition = focusAreaCenter + Vector2.up * verticalOffset * _camera.orthographicSize;

            if (_focusArea.velocity.x != 0) {

                _lookAheadDirection = Mathf.Sign(_focusArea.velocity.x);

                if (_controllerState.DeviationX != 0 && Mathf.Sign(_controllerState.DeviationX) == _lookAheadDirection) {
                    _lookAheadStopped = false;
                    _targetLookAhead = _lookAheadDirection * lookAheadDistance;
                }
                else if (!_lookAheadStopped) {
                    _lookAheadStopped = true;
                    _targetLookAhead = _currentLookAhead + (_lookAheadDirection * lookAheadDistance - _currentLookAhead);

                }
                //else { noop }
            }
            //else { noop }

            _currentLookAhead = Mathf.SmoothDamp(_currentLookAhead, _targetLookAhead, ref _smoothLookVelocity, lookAheadSmoothTime);
            focusPosition += (Vector2.right * _currentLookAhead);
            focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref _smoothVerticalVelocity, verticalSmoothTime);
            transform.position = (Vector3)focusPosition + Vector3.forward * -cameraDistance;
        }

#if UNITY_EDITOR
        void OnDrawGizmos() {
            _focusArea.DrawGizmos();
        }
#endif

        public struct FocusArea
        {
            private readonly Bounds _maxBounds;

            public Bounds bounds;
            public Vector2 velocity;

#if UNITY_EDITOR
            internal void DrawGizmos() {
                Gizmos.color = new Color(1, 0, 0, 0.3f);
                Gizmos.DrawCube(bounds.center, bounds.size);

                Gizmos.color = new Color(0, 1, 0, 0.3f);
                Gizmos.DrawCube(_maxBounds.center, _maxBounds.size);
            }
#endif

            private float CalculateShift(float outerMin, float outerMax, float insideMin, float insideMax) {
                float diffMin = outerMin - insideMin;
                float diffMax = outerMax - insideMax;

                float shift = 0;

                if (diffMin > 0) {
                    shift = diffMin;
                }
                else if (diffMax < 0) {
                    shift = diffMax;
                }
                // else { ok }

                return shift;
            }

            public FocusArea(Vector2 size, Bounds maxBounds, Bounds targetBounds) {

                if (size.x <= 0 || size.y <= 0) {
                    throw new ArgumentException("Must be greater than zero", "size");
                }
                else if (maxBounds.size.x <= size.x || maxBounds.size.y <= size.y) {
                    throw new ArgumentException("Must be greater than size", "maxBounds");
                }
                else if (targetBounds.size.x > size.x || targetBounds.size.y > size.y) {
                    throw new ArgumentException("Must be less or equal to size", "targetBounds");
                }
                // else { ok }

                Vector2 center = targetBounds.center;
                velocity = Vector2.zero;
                bounds = new Bounds(center, size);

                _maxBounds = maxBounds;

                float boundsShiftX = CalculateShift(maxBounds.min.x, maxBounds.max.x, bounds.min.x, bounds.max.x);
                float boundsShiftY = CalculateShift(maxBounds.min.y, maxBounds.max.y, bounds.min.y, bounds.max.y);

                // FIXME: checking a float value without epsilon?
                if (boundsShiftX != 0 || boundsShiftY != 0) {
                    center.x += boundsShiftX;
                    center.y += boundsShiftY;
                    bounds.center = center;
                }
                // else { ok }

                float shiftX = CalculateShift(bounds.min.x, bounds.max.x, targetBounds.min.x, targetBounds.max.x);
                float shiftY = CalculateShift(bounds.min.y, bounds.max.y, targetBounds.min.y, targetBounds.max.y);

                // reverse direction
                velocity.x = -shiftX;
                velocity.y = -shiftY;
            }

            public void Update(Bounds targetBounds) {
                // reverse direction
                float shiftX = -CalculateShift(bounds.min.x, bounds.max.x, targetBounds.min.x, targetBounds.max.x);
                float shiftY = -CalculateShift(bounds.min.y, bounds.max.y, targetBounds.min.y, targetBounds.max.y);

                if (shiftX != 0 || shiftY != 0) {
                    Vector2 center = bounds.center;
                    center.x += shiftX;
                    center.y += shiftY;
                    bounds.center = center;
                }
                // else { skip }

                velocity.x = shiftX;
                velocity.y = shiftY;

                float boundsShiftX = CalculateShift(_maxBounds.min.x, _maxBounds.max.x, bounds.min.x, bounds.max.x);
                float boundsShiftY = CalculateShift(_maxBounds.min.y, _maxBounds.max.y, bounds.min.y, bounds.max.y);

                // FIXME: checking a float value without epsilon?
                if (boundsShiftX != 0 || boundsShiftY != 0) {
                    Vector2 center = bounds.center;
                    center.x += boundsShiftX;
                    center.y += boundsShiftY;
                    bounds.center = center;
                }
                // else { ok }
            }
        }
    }
}
