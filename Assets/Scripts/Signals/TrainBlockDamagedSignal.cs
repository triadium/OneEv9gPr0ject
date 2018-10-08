using Zenject;

namespace OneEv9gPr0ject
{
    /// <summary>
    /// A training block is damaged
    /// </summary>
    public class TrainBlockDamagedSignal
    {
        public TrainBlockDamagedSignal(TrainBlockFacade model, int damage) {
            Model = model;
            Damage = damage;
        }

        public TrainBlockFacade Model { get; private set; }
        public int Damage { get; private set; }
    }
}
