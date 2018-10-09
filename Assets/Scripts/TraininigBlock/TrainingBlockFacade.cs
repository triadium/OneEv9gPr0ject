using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject
{
    public class TrainingBlockFacade : MonoBehaviour
    {
        [Range(10, 1000)]
        public float maxHealth;
        [Range(1, 1000)]
        public float currentHealth;
        public Transform healthBarHolder;
        private HealthBarFacade _healthBar;

        [Inject]
        private void Construct(HealthBarFacade healthBar) {
            _healthBar = healthBar;
            _healthBar.holder = healthBarHolder;
        }
        
        public void TakeDamage(float damage) {
            currentHealth -= damage;
            // TODO: just set exact value
            _healthBar.TakeDamage(damage / maxHealth);

            if(currentHealth < 1f) {
                Destroy(_healthBar.gameObject, 0.01f);
                Destroy(gameObject, 0.01f);
            }
            // else { alive }
        }

        public class Factory : PlaceholderFactory<TrainingBlockFacade> {}
    }

}
