using System;
using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public GameInstaller.Settings GameInstaller;
        public HeroSettings Hero;

        public override void InstallBindings() {
            Container.BindInstance(GameInstaller).IfNotBound();
            Container.BindInstance(Hero.Movement);
        }

        [Serializable]
        public class HeroSettings
        {
            public HeroMovement.Settings Movement;
        }
    }
}