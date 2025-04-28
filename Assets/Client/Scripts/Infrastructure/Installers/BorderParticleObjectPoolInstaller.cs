using Client.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure.Installers
{
    public class BorderParticleObjectPoolInstaller : MonoInstaller
    {
        [SerializeField] private int _prewarmedObjects = 6;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private GameObject _parent;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PoolFactory<BorderParticleWrapper>>().AsSingle().WithArguments(_parent, _prefab.GetComponent<BorderParticleWrapper>());
            Container.Bind<CustomPool<BorderParticleWrapper>>().AsSingle().WithArguments(_prewarmedObjects);
        }

    }
}
