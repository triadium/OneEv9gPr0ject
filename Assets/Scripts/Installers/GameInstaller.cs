using System;
using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject
{
    /// <summary>
    /// Bootstrap installer for the game
    /// </summary>
    public class GameInstaller : MonoInstaller
    {
        [Inject] private readonly Settings _settings = null;

        public override void InstallBindings() {
            SignalBusInstaller.Install(Container);
            InstallHero();
        }

        private void InstallHero() {
            Container.Bind<HeroFacade>()
                .FromComponentInNewPrefab(_settings.HeroPrefab)
                //.UnderTransformGroup("Hero")
                .AsSingle();

            //Container.Bind<BattleFieldFacade>()
            //    .FromComponentInHierarchy()

            Container.Bind<HeroControllerState>().AsSingle();            
            Container.BindInterfacesTo<HeroController>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public GameObject HeroPrefab;
            public GameObject PowerOrbPrefab;
            public GameObject MonsterPrefab;
        }
    }
}

