using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject
{
    public class StrikeChecker : MonoBehaviour
    {
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus) {
            _signalBus = signalBus;
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.collider.tag == "HeroFist") {
                _signalBus.Fire(new EnemyDamagedSignal(10));
            }
            else if (collision.collider.tag == "HeroHeel") {
                _signalBus.Fire(new EnemyDamagedSignal(30));
            }
            // else { skip }            
        }
    }
}
