using MVVM;
using TMPro;
using UnityEngine;

namespace Client.Scripts.UI.Views
{
    public class MoneyView : MonoBehaviour
    {
        [Data("MoneyAmount")]
        public TMP_Text Money;
    }
}