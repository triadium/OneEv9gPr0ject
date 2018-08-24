using UnityEngine;

namespace OneEv9gPr0ject
{
    /// <summary>
    /// Battle field representation model
    /// Splitting into functions and MonoBehaviour
    /// </summary>
    public class BattleFieldModel
    {
        private readonly Collider2D _collider;
        private readonly SpriteRenderer _renderer;

        public BattleFieldModel(Collider2D collider, SpriteRenderer renderer) {
            _collider = collider;
            _renderer = renderer;
        }

        public Bounds Bounds {
            get { return _collider.bounds; }
        }       
    }
}
