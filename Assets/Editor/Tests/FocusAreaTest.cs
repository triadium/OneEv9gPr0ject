using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;
using NUnit.Framework;
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
                Assert.Throws<ArgumentException>(() => new CameraFollow.FocusArea(Vector2.zero, maxBounds, targetBounds), "Size: 0 x 0");
                Assert.Throws<ArgumentException>(() => new CameraFollow.FocusArea(Vector2.left, maxBounds, targetBounds), "Size: -1 x 0");
                Assert.Throws<ArgumentException>(() => new CameraFollow.FocusArea(Vector2.right, maxBounds, targetBounds), "Size: 0 x 1");
                Assert.Throws<ArgumentException>(() => new CameraFollow.FocusArea(Vector2.up, maxBounds, targetBounds), "Size: 0 x 1");
                Assert.Throws<ArgumentException>(() => new CameraFollow.FocusArea(Vector2.down, maxBounds, targetBounds), "Size: 0 x-1");
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
            public void Fully_Within_MaxBounds_Top() {
                maxBounds.center = new Vector2(0, -maxBounds.size.y / 2);
                var focusArea = new CameraFollow.FocusArea(size, maxBounds, targetBounds);
                Bounds bounds = focusArea.bounds;
                Assert.IsTrue(maxBounds.ContainBounds(bounds));
            }

            [Test]
            public void Fully_Within_MaxBounds_Left() {
                maxBounds.center = new Vector2(-maxBounds.size.x / 2, 0);
                var focusArea = new CameraFollow.FocusArea(size, maxBounds, targetBounds);
                Bounds bounds = focusArea.bounds;
                Assert.IsTrue(maxBounds.ContainBounds(bounds));
            }

            [Test]
            public void Fully_Within_MaxBounds_Right() {
                maxBounds.center = new Vector2(maxBounds.size.x / 2, 0);
                var focusArea = new CameraFollow.FocusArea(size, maxBounds, targetBounds);
                Bounds bounds = focusArea.bounds;
                Assert.IsTrue(maxBounds.ContainBounds(bounds));
            }

            [Test]
            public void Fully_Within_MaxBounds_Bottom() {
                maxBounds.center = new Vector2(0, maxBounds.size.y / 2);
                var focusArea = new CameraFollow.FocusArea(size, maxBounds, targetBounds);
                Bounds bounds = focusArea.bounds;
                Assert.IsTrue(maxBounds.ContainBounds(bounds));
            }

            [Test]
            public void Velocity_Equals_Zero_For_TargetBounds_Within() {
                targetBounds.center = new Vector2(50, 50);
                var focusArea = new CameraFollow.FocusArea(size, maxBounds, targetBounds);
                Assert.IsTrue(Vector2EqualityComparer.Instance.Equals(focusArea.velocity, Vector2.zero), "Velocity is zero");
            }

            [Test]
            public void Velocity_Not_Zero_For_TargetBounds_Outside_Or_At_Intersection() {
                targetBounds.center = maxBounds.size;
                var focusArea = new CameraFollow.FocusArea(size, maxBounds, targetBounds);
                Assert.IsFalse(Vector2EqualityComparer.Instance.Equals(focusArea.velocity, Vector2.zero), "Velocity is not zero");
            }

            [Test]
            public void Velocity_Is_Directed_To_Target_Corner_LeftTop() {
                targetBounds.center = new Vector2(-maxBounds.size.x, -maxBounds.size.y);
                var focusArea = new CameraFollow.FocusArea(size, maxBounds, targetBounds);
                Assert.Negative(focusArea.velocity.x, "Velocity X");
                Assert.Negative(focusArea.velocity.y, "Velocity Y");
            }

            [Test]
            public void Velocity_Is_Directed_To_Target_Corner_LeftBottom() {
                targetBounds.center = new Vector2(-maxBounds.size.x, maxBounds.size.y);
                var focusArea = new CameraFollow.FocusArea(size, maxBounds, targetBounds);
                Assert.Negative(focusArea.velocity.x, "Velocity X");
                Assert.Positive(focusArea.velocity.y, "Velocity Y");
            }

            [Test]
            public void Velocity_Is_Directed_To_Target_Corner_RightBottom() {
                targetBounds.center = new Vector2(maxBounds.size.x, maxBounds.size.y);
                var focusArea = new CameraFollow.FocusArea(size, maxBounds, targetBounds);
                Assert.Positive(focusArea.velocity.x, "Velocity X");
                Assert.Positive(focusArea.velocity.y, "Velocity Y");
            }

            [Test]
            public void Velocity_Is_Directed_To_Target_Corner_RightTop() {
                targetBounds.center = new Vector2(maxBounds.size.x, -maxBounds.size.y);
                var focusArea = new CameraFollow.FocusArea(size, maxBounds, targetBounds);
                Assert.Positive(focusArea.velocity.x, "Velocity X");
                Assert.Negative(focusArea.velocity.y, "Velocity Y");
            }
        }

        public class UpdateTest
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
        }

        //[UnityTest]
        //public IEnumerator NewTestScriptWithEnumeratorPasses() {
        //    yield return null;
        //}

    }

    public static class BoundsExtension
    {
        public static bool ContainBounds(this Bounds bounds, Bounds target) {
            return bounds.Contains(target.min) && bounds.Contains(target.max);
        }
    }
}
