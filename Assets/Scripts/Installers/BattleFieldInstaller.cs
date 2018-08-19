using System;
using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject
{
    public class BattleFieldInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings() {
            Container.Bind<BattleFieldModel>().AsSingle()
                .WithArguments(_settings.Collider,
                               _settings.Renderer);
        }

        [Serializable]
        public class Settings
        {
            public Collider2D Collider;
            public SpriteRenderer Renderer;
        }
    }
}