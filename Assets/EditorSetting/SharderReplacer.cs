using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Takechi.SharderReplacer
{
#if UNITY_EDITOR
    public class SharderReplacer : EditorWindow
    {
        #region private
        private int selectBeforeShaderIndex;
        private int selectAfterShaderIndex;
        #endregion

        [MenuItem("MyEditorSetting/SharderReplace", false, 2000)]
        private static void Open()
        {
            GetWindow<SharderReplacer>();
        }

        #region　UnityEvent

        private void OnGUI()
        {
            var sharders = ShaderUtil.GetAllShaderInfo();
            var sharderNames = sharders.Select( x => x.name).ToArray();

            selectBeforeShaderIndex = 
                EditorGUILayout.Popup("Before", selectBeforeShaderIndex, sharderNames);

            selectAfterShaderIndex =  
                EditorGUILayout.Popup("After", selectAfterShaderIndex, sharderNames);

            if (GUILayout.Button("Replace"))
            {
                ReplaceAll(sharderNames[selectBeforeShaderIndex], sharderNames[selectAfterShaderIndex]);
            }
        }
        #endregion

        #region Function
        /// <summary>
        /// Shader を　変更する。
        /// </summary>
        /// <param name="beforeShaderName"> 変更前　</param>
        /// <param name="afterShaderName"> 変更後　</param>
        private void ReplaceAll(string beforeShaderName, string afterShaderName)
        {
            var beforeShader = 
                Shader.Find(beforeShaderName);

            var afterShader = 
                Shader.Find(afterShaderName);

            var guids = AssetDatabase.FindAssets("t: Material", null);
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var material = AssetDatabase.LoadAssetAtPath<Material>(path);

                if (material != null && material.shader == beforeShader)
                {
                    material.shader = afterShader;
                }

            }

            AssetDatabase.SaveAssets();
        }
        #endregion
    }
#endif
}