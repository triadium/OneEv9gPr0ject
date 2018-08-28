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

        public BattleFieldModel(Collider2D collider) {
            _collider = collider;
        }

        public Bounds Bounds {
            get { return _collider.bounds; }
        }       
    }
}
