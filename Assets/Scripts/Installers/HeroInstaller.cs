using System;
using UnityEngine;
using Zenject;

namespace OneEv9gPr0ject
{
    public class HeroInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings() {
            Container.Bind<HeroModel>().AsSingle()
                .WithArguments(_settings.Collider,
                               _settings.Rigidbody,
                               _settings.Renderer);

            // Container.Bind<HeroControllerState>().AsSingle();            
            // Container.BindInterfacesTo<HeroController>().AsSingle();
            Container.Bind<HeroAnimationState>().AsSingle().WithArguments(_settings.Animator);

            Container.BindInterfacesTo<HeroAnimatorProcessor>().AsSingle();
            Container.BindInterfacesTo<HeroMovement>().AsSingle();

            InstallSignals();
        }

        private void InstallSignals() {
            Container.DeclareSignal<HeroReadySignal>();
            Container.DeclareSignal<HeroDiedSignal>();

            Container.BindSignal<HeroReadySignal>()
                .ToMethod<HeroModel>(x => x.Ready)
                .FromResolve();
            Container.BindSignal<HeroDiedSignal>()
                .ToMethod<HeroModel>(x => x.Dead)
                .FromResolve();
        }

        [Serializable]
        public class Settings
        {
            public Animator Animator;
            public Collider2D Collider;
            public Rigidbody2D Rigidbody;
            public SpriteRenderer Renderer;
        }
    }
}