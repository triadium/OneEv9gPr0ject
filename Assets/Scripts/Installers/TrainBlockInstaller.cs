using System;
using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject
{
    public class TrainBlockInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings() {
            //Container.Bind<TrainBlockModel>().AsSingle()
            //    .WithArguments(_settings.Collider);

            InstallSignals();
        }

        private void InstallSignals() {
            Container.DeclareSignal<TrainBlockDamagedSignal>();

            //Container.BindSignal<HeroReadySignal>()
            //    .ToMethod<HeroModel>(x => x.Ready)
            //    .FromResolve();
        }

        [Serializable]
        public class Settings
        {
            public Collider2D Collider;
        }
    }
}