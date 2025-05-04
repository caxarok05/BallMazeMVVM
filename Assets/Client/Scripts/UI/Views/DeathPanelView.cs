using MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Scripts.UI.Views
{
    public class DeathPanelView : MonoBehaviour
    {

        [Data("RestartButton")]
        public Button restartButton;

        [Data("MenuButton")]
        public Button menuButton;

        [Setter("GoToMenu")]
        public bool Active
        {
            set { gameObject.SetActive(value); }
        }

        public void ShowHide(bool param)
        {
            gameObject.SetActive(param);
        }
    }
}
