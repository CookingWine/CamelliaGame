using UnityEngine;
using UnityEngine.UIElements;
namespace CamelliaUnityEditor.Generate
{
    public class ProtocBuff:ICamellieEditorModule
    {
        public string ModuleName => "ProtocBuff";

        private VisualElement _RootVisualElement;

        public VisualElement CreateContent( )
        {
            return _RootVisualElement;
        }

        public void OnInitialize( )
        {
            if(_RootVisualElement == null)
            {
                var visualTree = UnityEditor.AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/CamelliaUnityEditor/Generate/ProtocBuff/ProtocBuff.uxml");
                _RootVisualElement = visualTree.CloneTree( );
                _RootVisualElement.styleSheets.Add(UnityEditor.AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/CamelliaUnityEditor/Generate/ProtocBuff/ProtocBuff.uss"));
            }
        }
        public void OnRefresh( )
        {

        }
        public void OnHide( )
        {

        }
        public void OnSave( )
        {

        }
        public void OnDestroy( )
        {

        }
    }
}
