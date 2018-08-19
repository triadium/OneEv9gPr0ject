using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

namespace OneEv9gPr0ject.Tests
{
    public class FocusAreaTest
    {
        public class InitializationExceptionsTest
        {
            Bounds maxBounds;
            Bounds targetBounds;
            Vector2 size;

            [SetUp]
            public void SetupEveryTest() {
                size = new Vector2(30, 30);
                maxBounds.center = Vector2.zero;
                maxBounds.size = new Vector2(100, 100);
                targetBounds.center = Vector2.zero;
                targetBounds.size = new Vector2(10, 10);
            }

            [Test]
            public void Throws_Exception_For_Every_LessOrEqual_Zero_Value_Dimension() {
                Assert.Throws<ArgumentException>(() => new CameraFollow.FocusArea(Vector2.zero, maxBounds, targetBounds));
                Assert.Throws<ArgumentException>(() => new CameraFollow.FocusArea(Vector2.left, maxBounds, targetBounds));
                Assert.Throws<ArgumentException>(() => new CameraFollow.FocusArea(Vector2.right, maxBounds, targetBounds));
                Assert.Throws<ArgumentException>(() => new CameraFollow.FocusArea(Vector2.up, maxBounds, targetBounds));
                Assert.Throws<ArgumentException>(() => new CameraFollow.FocusArea(Vector2.down, maxBounds, targetBounds));
            }

            [Test]
            public void Throws_Exception_For_MaxBounds_LessOrEqual_Size() {
                size = new Vector2(100, 100);
                maxBounds.size = new Vector2(10, 10);
                Assert.Throws<ArgumentException>(() => new CameraFollow.FocusArea(size, maxBounds, targetBounds));
            }

            [Test]
            public void Throws_Exception_For_Zero_MaxBounds() {
                maxBounds.size = Vector2.zero;
                Assert.Throws<ArgumentException>(() => new CameraFollow.FocusArea(size, maxBounds, targetBounds));
            }

        }

        public class InitializationTest
        {
            Bounds maxBounds;
            Bounds targetBounds;
            Vector2 size;

            [SetUp]
            public void SetupEveryTest() {
                size = new Vector2(30, 30);
                maxBounds.center = Vector2.zero;
                maxBounds.size = new Vector2(300, 300);
                targetBounds.center = Vector2.zero;
                targetBounds.size = new Vector2(10, 10);
            }

            [Test]
            public void Fully_Within_MaxBounds_Corner_LeftTop() {
                maxBounds.center = new Vector2(-maxBounds.size.x / 2, -maxBounds.size.y / 2);
                var focusArea = new CameraFollow.FocusArea(size, maxBounds, targetBounds);
                Bounds bounds = focusArea.bounds;
                Assert.IsTrue(maxBounds.ContainBounds(bounds));
            }

            [Test]
            public void Fully_Within_MaxBounds_Corner_LeftBottom() {
                maxBounds.center = new Vector2(-maxBounds.size.x / 2, maxBounds.size.y / 2);
                var focusArea = new CameraFollow.FocusArea(size, maxBounds, targetBounds);
                Bounds bounds = focusArea.bounds;
                Assert.IsTrue(maxBounds.ContainBounds(bounds));
            }

            [Test]
            public void Fully_Within_MaxBounds_Corner_RightBottom() {
                maxBounds.center = new Vector2(maxBounds.size.x / 2, maxBounds.size.y / 2);
                var focusArea = new CameraFollow.FocusArea(size, maxBounds, targetBounds);
                Bounds bounds = focusArea.bounds;
                Assert.IsTrue(maxBounds.ContainBounds(bounds));
            }

            [Test]
            public void Fully_Within_MaxBounds_Corner_RightTop() {
                maxBounds.center = new Vector2(maxBounds.size.x / 2, -maxBounds.size.y / 2);
                var focusArea = new CameraFollow.FocusArea(size, maxBounds, targetBounds);
                Bounds bounds = focusArea.bounds;
                Assert.IsTrue(maxBounds.ContainBounds(bounds));
            }

            [Test]
            public void Velocity_Equals_Zero_For_TargetBounds_Within() {
                targetBounds.center = new Vector2(50, 50);
                var focusArea = new CameraFollow.FocusArea(size, maxBounds, targetBounds);
                // FIXME: check components with delta?
                Assert.AreEqual(focusArea.velocity, Vector2.zero);
            }

            [Test]
            public void Velocity_Not_Zero_For_TargetBounds_Outside_Or_At_Intersection() {
                targetBounds.center = new Vector2(290, 290);
                var focusArea = new CameraFollow.FocusArea(size, maxBounds, targetBounds);
                // FIXME: check components with delta?
                Assert.AreNotEqual(focusArea.velocity, Vector2.zero);
            }
        }


        //[UnityTest]
        //public IEnumerator NewTestScriptWithEnumeratorPasses() {
        //    yield return null;
        //}
        
        public class CameraFollow
        {
            public struct FocusArea
            {
                private readonly Bounds _maxBounds;

                public Vector2 size;
                public Vector2 center;
                public Bounds bounds;
                public Vector2 velocity;
                

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

                public FocusArea(Vector2 iniSize, Bounds maxBounds, Bounds targetBounds) {
                    size = iniSize;

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

                    center = targetBounds.center;
                    velocity = Vector2.zero;
                    bounds = new Bounds(center, size);

                    _maxBounds = maxBounds;

                    float boundsShiftX = CalculateShift(maxBounds.min.x, maxBounds.max.x, bounds.min.x, bounds.max.x);
                    float boundsShiftY = CalculateShift(maxBounds.min.y, maxBounds.max.y, bounds.min.y, bounds.max.y);                    

                    // FIXME: checking a float value without epsilon?
                    if (boundsShiftX != 0 && boundsShiftY != 0) {
                        center.x += boundsShiftX;
                        center.y += boundsShiftY;
                        bounds = new Bounds(center, size);
                    }
                    // else { ok }

                    float shiftX = CalculateShift(bounds.min.x, bounds.max.x, targetBounds.min.x, targetBounds.max.x);
                    float shiftY = CalculateShift(bounds.min.y, bounds.max.y, targetBounds.min.y, targetBounds.max.y);
                    velocity.x = shiftX;
                    velocity.y = shiftY;
                }

            }
        }
    }

    public static class BoundsExtension
    {
        public static bool ContainBounds(this Bounds bounds, Bounds target) {
            return bounds.Contains(target.min) && bounds.Contains(target.max);
        }
    }
}
