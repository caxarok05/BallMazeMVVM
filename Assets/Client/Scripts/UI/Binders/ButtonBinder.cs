using MVVM;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Client.Scripts.UI
{
    public sealed class ButtonBinder : IBinder
    {
        private readonly Button view;
        private readonly UnityAction modelAction;

        public ButtonBinder(Button view, Action modelAction)
        {
            this.view = view;
            this.modelAction = new UnityAction(modelAction);
        }

        public void Bind()
        {
            view.onClick.AddListener(modelAction);
        }

        public void Unbind()
        {
            view.onClick.RemoveListener(modelAction);
        }

    }
}