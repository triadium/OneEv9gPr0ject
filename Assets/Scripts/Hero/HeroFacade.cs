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

        void Update() {
            //if (_model.IsMoving) {            
            //}
            //else if (_model.IsStunned) { }
        }

        public void Spawn() {

        }

        public void Die() {

        }
    }
}
