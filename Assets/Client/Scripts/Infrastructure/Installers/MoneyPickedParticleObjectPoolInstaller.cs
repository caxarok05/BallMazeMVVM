using Client.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure.Installers
{
    public class MoneyPickedParticleObjectPoolInstaller : MonoInstaller
    {
        [SerializeField] private int _prewarmedObjects = 6;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private GameObject _parent;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PoolFactory<MoneyPickedParticleWrapper>>().AsSingle().WithArguments(_parent, _prefab.GetComponent<MoneyPickedParticleWrapper>());
            Container.Bind<CustomPool<MoneyPickedParticleWrapper>>().AsSingle().WithArguments(_prewarmedObjects);
        }

    }
}
