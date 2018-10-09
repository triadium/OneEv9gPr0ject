using Zenject;

namespace OneEv9gPr0ject
{
    /// <summary>
    /// An enemy is damaged
    /// </summary>
    public class EnemyDamagedSignal
    {
        public EnemyDamagedSignal(int damage) {
            Damage = damage;
        }

        public int Damage { get; private set; }
    }
}
