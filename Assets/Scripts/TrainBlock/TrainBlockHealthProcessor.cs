using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject
{
    public class TrainBlockHealthProcessor : MonoBehaviour
    {
        public Transform target;
        public RectTransform canvasRect;
        public RectTransform myRect;
        public Vector3 offset;

        private void Update() {
            if (target != null) {
                Vector2 worldTargetPosition = target.position + offset;
                Vector2 targetPosition = Camera.main.WorldToViewportPoint(worldTargetPosition);
                Vector2 screenPosition = new Vector2(((targetPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
                    ((targetPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));
                screenPosition.y += myRect.rect.height / 2;
                myRect.anchoredPosition = screenPosition;
            }
        }
    }
}
