using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject
{
    public class HeroFacade : MonoBehaviour
    {
        private HeroModel _model;

        [Inject]
        public void Construct(HeroModel model) {
            _model = model;
        }

        public Bounds Bounds
        {
            get { return _model.Bounds;  }
        }

        public void Die() {
            _model.Die();
        }
    }
}
