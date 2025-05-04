using Client.Scripts.UI;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(MonoViewBinder))]
public class MonoViewBinderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MonoViewBinder binder = (MonoViewBinder)target;

        binder.viewBinding = (MonoViewBinder.BindingMode)EditorGUILayout.EnumPopup("View Binding", binder.viewBinding);

        if (binder.viewBinding == MonoViewBinder.BindingMode.FromInstance)
        {
            binder.view = EditorGUILayout.ObjectField("View", binder.view, typeof(Object), true);
        }
        if (binder.viewBinding == MonoViewBinder.BindingMode.FromResolve)
        {
            binder.viewType = (MonoScript)EditorGUILayout.ObjectField("View Type", binder.viewType, typeof(MonoScript), false);
        }
        if (binder.viewBinding == MonoViewBinder.BindingMode.FromResolveId)
        {
            binder.viewId = EditorGUILayout.TextField("View ID", binder.viewId);
        }


        binder.viewModelBinding = (MonoViewBinder.BindingMode)EditorGUILayout.EnumPopup("ViewModel Binding", binder.viewModelBinding);


        if (binder.viewModelBinding == MonoViewBinder.BindingMode.FromInstance)
        {
            binder.viewModel = EditorGUILayout.ObjectField("ViewModel", binder.viewModel, typeof(Object), true);
        }
        if (binder.viewModelBinding == MonoViewBinder.BindingMode.FromResolve)
        {
            binder.viewModelType = (MonoScript)EditorGUILayout.ObjectField("ViewModel Type", binder.viewModelType, typeof(MonoScript), false);
        }
        if (binder.viewModelBinding == MonoViewBinder.BindingMode.FromResolveId)
        {
            binder.viewModelId = EditorGUILayout.TextField("ViewModel ID", binder.viewModelId);
        }


        if (GUI.changed)
        {
            EditorUtility.SetDirty(binder);
        }
    }
}


