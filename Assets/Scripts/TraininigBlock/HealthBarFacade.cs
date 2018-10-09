using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace OneEv9gPr0ject
{
    public class HealthBarFacade : MonoBehaviour
    {
        public Transform holder;

        private RectTransform _canvasRectTransform;
        private RectTransform _rectTransform;
        private Image _image;

        public void Heal(float value) {
            _image.fillAmount = Mathf.Clamp(_image.fillAmount + value, 0.1f, 1.0f);
        }

        public void TakeDamage(float value) {
            _image.fillAmount = Mathf.Clamp(_image.fillAmount - value, 0.1f, 1.0f);
        }

        private void Awake() {
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
        }

        private void Start() {
            GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
            _canvasRectTransform = canvas.GetComponent<RectTransform>();
           transform.SetParent(canvas.transform, false);
        }

        private void LateUpdate() {
            if (holder != null) {               
                Vector2 worldTargetPosition = holder.position;
                Vector2 targetPosition = Camera.main.WorldToViewportPoint(worldTargetPosition);
                Vector2 screenPosition = new Vector2(((targetPosition.x * _canvasRectTransform.sizeDelta.x) - (_canvasRectTransform.sizeDelta.x * 0.5f)),
                    ((targetPosition.y * _canvasRectTransform.sizeDelta.y) - (_canvasRectTransform.sizeDelta.y * 0.5f)));
                _rectTransform.anchoredPosition = screenPosition;
            }
            // else { skip }
        }
    }
}
