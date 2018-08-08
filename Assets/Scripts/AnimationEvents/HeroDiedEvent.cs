using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject.AnimationEvents
{
    public class HeroDiedEvent : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;

        // FIXME: It is better to use the unique function name 
        // for clarity in the animation editor 
        public void HeroDied() {
            _signalBus.Fire(new HeroDiedSignal());
        }
    }
}
