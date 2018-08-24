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

        void Update() {
            //if (_model.IsMoving) {
            //    Debug.Log("Moving");
            //}
            //else if (_model.IsStunned) {
            //    Debug.Log("Moving");
            //}
        }

        public void Spawn() {

        }

        public void Die() {

        }

        public bool IsReady { get { return _model.IsReady;  } }

    }
}
