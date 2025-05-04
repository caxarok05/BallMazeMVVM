using UnityEngine;
using MVVM;
using TMPro;

namespace Client.Scripts.UI.Views
{
    public class BallStatView : MonoBehaviour
    {
        [Data("Speed")]
        public TMP_Text speedText;

        [Data("Rotation")]
        public TMP_Text rotationText;
    }
}
