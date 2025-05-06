using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Client.Scripts.Presenters
{
    public class CurtainPresenter : MonoBehaviour
    {
        public CanvasGroup Curtain;

        private const int DelationTime = 30;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Curtain.alpha = 1;
        }

        public void Hide() => DoFadeIn().Forget();

        private async UniTaskVoid DoFadeIn()
        {
            await UniTask.WaitForSeconds(2);
            while (Curtain.alpha > 0)
            {
                Curtain.alpha -= 0.03f;
                await UniTask.Delay(DelationTime);
            }

            gameObject.SetActive(false);
        }
    }
}
