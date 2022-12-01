using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;

namespace Takechi.WindowsMenu.SceneChange
{
    public class WindowsMenuSceneChange : MonoBehaviour
    {
        private const string ScenesFolderPas = "Assets/Scenes/";

        //switchscene
        [MenuItem("SceneSwitching / TitleScnene _#F1")]

        private static void SceneSwitchingScene00()
        {
            EditorSceneManager.OpenScene(ScenesFolderPas + "TitleScnene.unity", OpenSceneMode.Single);
        }

        [MenuItem("SceneSwitching / LobbyScene _#F2")]
        private static void SceneSwitchingScene01()
        {
            EditorSceneManager.OpenScene(ScenesFolderPas + "LobbyScene.unity", OpenSceneMode.Single);
        }

        [MenuItem("SceneSwitching / CharacterSelectionScene _#F3")]
        private static void SceneSwitchingScene02()
        {
            EditorSceneManager.OpenScene(ScenesFolderPas + "CharacterSelectionScene.unity", OpenSceneMode.Single);
        }

        [MenuItem("SceneSwitching / FutureCityScene _#F4")]
        private static void SceneSwitchingScene03()
        {
            EditorSceneManager.OpenScene(ScenesFolderPas + "FutureCityScene.unity", OpenSceneMode.Single);
        }
    }
}
#endif
