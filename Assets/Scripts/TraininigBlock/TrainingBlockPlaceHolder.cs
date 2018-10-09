using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject
{
    public class TrainingBlockPlaceHolder : MonoBehaviour
    {
        private TrainingBlockFacade.Factory _factory;
#if UNITY_EDITOR
        public Vector2 gismoSize;
#endif

        [Inject]
        private void Construct(TrainingBlockFacade.Factory factory) {
            _factory = factory;
        }

        private void Awake() {
            TrainingBlockFacade trainingBlock = _factory.Create();
            trainingBlock.transform.SetParent(transform, false);
        }

#if UNITY_EDITOR
        internal void OnDrawGizmos() {
            Gizmos.color = new Color(0.4f, 0.9f, 0.9f, 0.3f);
            Gizmos.DrawCube(transform.position, gismoSize);
        }
#endif
    }

}
