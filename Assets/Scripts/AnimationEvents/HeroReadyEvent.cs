using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject.AnimationEvents
{
    public class HeroReadyEvent : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;

        // FIXME: It is better to use the unique function name 
        // for clarity in the animation editor 
        public void HeroReady() {
            _signalBus.Fire(new HeroReadySignal());
        }
    }
}
