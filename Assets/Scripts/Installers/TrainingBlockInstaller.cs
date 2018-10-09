using System;
using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject
{
    public class TrainingBlockInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings() {
            Container.Bind<TrainingBlockFacade>().FromComponentOnRoot();
            Container
                .Bind<HealthBarFacade>()
                .FromComponentInNewPrefab(_settings.HealthBarPrefab)
                .AsSingle();                
            InstallSignals();
        }

        private void InstallSignals() {
            Container.DeclareSignal<EnemyDamagedSignal>();
            Container.BindSignal<EnemyDamagedSignal>()
                .ToMethod<TrainingBlockFacade>((x, s) => x.TakeDamage(s.Damage)).FromResolve();
        }

        [Serializable]
        public class Settings
        {
            public GameObject HealthBarPrefab;
        }
    }
}