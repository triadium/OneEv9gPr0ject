using UnityEngine;

namespace OneEv9gPr0ject
{
    /// <summary>
    /// Hero representation model
    /// Splitting into functions and MonoBehaviour
    /// </summary>
    /// <remarks>
    /// Using of the physics components for hero movements
    /// </remarks>
    public class HeroModel
    {
        private readonly Collider2D _collider;
        private readonly Rigidbody2D _rigidbody;
        private readonly SpriteRenderer _renderer;

        public HeroModel(Collider2D collider, Rigidbody2D rigidbody, SpriteRenderer renderer) {
            _collider = collider;
            _rigidbody = rigidbody;
            _renderer = renderer;

            IsReady = false;
        }

        // FIXME: Move to the separate state class
        // TODO: use with calculated state machine
        public bool IsReady { get; private set; }
        public bool IsMoving { get; set; }
        public bool IsPunching { get; set; }
        public bool IsKicking { get; set; }
        public bool IsFiring { get; set; }
        public bool IsStunned { get; set; }
        public bool IsDying { get; private set; }
        public bool IsDead { get; private set; }

        public void Ready() {
            IsReady = true;
        }

        public void Die() {
            if (!IsDead) {
                IsDying = true;
            }
            // else { skip }
        }

        public void Died() {
            IsDead = true;
        }

        public bool IsActionPossible
        {
            get {
                return IsReady && !(IsStunned || IsDying || IsDead);
            }
        }

        public bool IsMovementPossible
        {
            get {
                return IsReady && !(IsStunned || IsDying || IsDead || IsPunching || IsKicking || IsFiring);
            }
        }
        
        /// <summary>
        /// Used to position child objects
        /// </summary>
        public Vector2 Position
        {
            get { return _rigidbody.position; }
            private set { _rigidbody.position = value; }
        }

        public Vector2 Velocity
        {
            get { return _rigidbody.velocity; }
            set { _rigidbody.velocity = value; }
        }

        public Bounds Bounds
        {
            get { return _collider.bounds; }
        }

        /// <summary>
        /// Used for uncontrolled movements
        /// </summary>
        /// <param name="force"></param>
        public void AddForce(Vector2 force) {
            _rigidbody.AddForce(force);
        }

        public bool IsFacingLeft { get; private set; }
        public void Turn(bool toLeft) {
            _renderer.flipX = toLeft;
            IsFacingLeft = toLeft;
        }
    }
}
