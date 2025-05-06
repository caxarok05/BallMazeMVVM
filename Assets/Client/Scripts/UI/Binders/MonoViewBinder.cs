using System;
using MVVM;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Client.Scripts.UI
{
    public sealed class MonoViewBinder : MonoBehaviour
    {
        public enum BindingMode
        {
            FromInstance = 0,
            FromResolve = 1,
            FromResolveId = 2
        }

        [SerializeField]
        public BindingMode viewBinding;

        [SerializeField]
        public Object view;

        [SerializeField]
        public string viewTypeName;

        [SerializeField]
        public string viewId;

        [Space(8)]
        [SerializeField]
        public BindingMode viewModelBinding;

        [SerializeField]
        public Object viewModel;

        [SerializeField]
        public string viewModelTypeName;

        [SerializeField]
        public string viewModelId;

        [Inject]
        public DiContainer diContainer;

        public IBinder _binder;

        private void Awake()
        {
            _binder = this.CreateBinder();
        }

        private void OnEnable()
        {
            _binder.Bind();
        }

        private void OnDisable()
        {
            _binder.Unbind();
        }

        private IBinder CreateBinder()
        {
            object view = this.viewBinding switch
            {
                BindingMode.FromInstance => this.view,
                BindingMode.FromResolve => this.diContainer.Resolve(Type.GetType(this.viewTypeName)),
                BindingMode.FromResolveId => this.diContainer.ResolveId(Type.GetType(this.viewTypeName), this.viewId),
                _ => throw new Exception($"Binding type of view {this.viewBinding} is not found!")
            };

            object model = this.viewModelBinding switch
            {
                BindingMode.FromInstance => this.viewModel,
                BindingMode.FromResolve => this.diContainer.Resolve(Type.GetType(this.viewModelTypeName)),
                BindingMode.FromResolveId => this.diContainer.ResolveId(Type.GetType(this.viewModelTypeName), this.viewModelId),
                _ => throw new Exception($"Binding type of view {this.viewBinding} is not found!")
            };

            return BinderFactory.CreateComposite(view, model);
        }
    }
}