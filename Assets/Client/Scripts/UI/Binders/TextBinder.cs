using MVVM;
using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace Client.Scripts.UI
{
    public sealed class TextBinder : IBinder, IObserver<string>
    {
        private readonly TMP_Text view;
        private readonly IReadOnlyReactiveProperty<string> property;
        private IDisposable _handle;

        public TextBinder(TMP_Text view, IReadOnlyReactiveProperty<string> property)
        {
            this.view = view;
            this.property = property;
        }

        public void Bind()
        {
            OnNext(property.Value);
            _handle = property.Subscribe(this);
        }

        public void Unbind()
        {
            _handle?.Dispose();
            _handle = null;
        }

        public void OnNext(string value)
        {
            view.text = value;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error) => Debug.LogError(error);
    }
}