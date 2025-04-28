using Client.Scripts.Services;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UniRx.Toolkit;
using UnityEngine;
using Zenject;

namespace Assets.Client.Scripts.LogicViews
{
    public class MoneyView : MonoBehaviour
    {
        private CustomPool<MoneyPickedParticleWrapper> _pool;
        private const int PlayerLayer = 7;

        [Inject]
        private void Construct(CustomPool<MoneyPickedParticleWrapper> objectPool)
        {
            _pool = objectPool;
        }

        private async void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.layer == PlayerLayer)
            {
                gameObject.SetActive(false);
                MoneyPickedParticleWrapper particle = _pool.Get();
                particle.gameObject.transform.position = gameObject.transform.position;
                particle.GetComponent<ParticleSystem>().Play();
                await ReleaseParticle(particle);
            }
        }

        public async UniTask ReleaseParticle(MoneyPickedParticleWrapper particle)
        {
            await UniTask.WaitForSeconds(particle.GetComponent<ParticleSystem>().main.duration);
            _pool.Release(particle);
        }
    }
}
