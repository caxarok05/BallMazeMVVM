using MVVM;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Client.Scripts.UI.Views
{
    public class HealthView : MonoBehaviour
    {
        [Data("Health")]
        public TMP_Text scoreText;
    }
}