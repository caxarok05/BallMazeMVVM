using MVVM;
using Zenject;

namespace Client.Scripts.UI
{
    public class BindersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BinderFactory.RegisterBinder<TextBinder>();
            BinderFactory.RegisterBinder<ButtonBinder>();
            BinderFactory.RegisterBinder<ViewSetterBinder<bool>>();
        }
    }    
}
