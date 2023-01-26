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
        private static void SceneSwitchingSceneTitleScnene()
        {
            EditorSceneManager.OpenScene(ScenesFolderPas + "/TitleScneneFolder/" + "TitleScnene.unity", OpenSceneMode.Single);
        }
        [MenuItem("SceneSwitching / LobbyScene _#F2")]
        private static void SceneSwitchingSceneLobbyScene()
        {
            EditorSceneManager.OpenScene(ScenesFolderPas + "/LobbySceneFolder/" + "LobbyScene.unity", OpenSceneMode.Single);
        }
        [MenuItem("SceneSwitching / CharacterSelectionScene _#F3")]
        private static void SceneSwitchingSceneCharacterSelectionScene()
        {
            EditorSceneManager.OpenScene(ScenesFolderPas + "/CharacterSelectionSceneFolder/" + "CharacterSelectionScene.unity", OpenSceneMode.Single);
        }
        [MenuItem("SceneSwitching / FutureCityScene _#F4")]
        private static void SceneSwitchingSceneFutureCityScene()
        {
            EditorSceneManager.OpenScene(ScenesFolderPas + "/FutureCitySceneFolder/" + "FutureCityScene.unity", OpenSceneMode.Single);
        }
        [MenuItem("SceneSwitching / ResultScene _#F5")]
        private static void SceneSwitchingSceneResultScene()
        {
            EditorSceneManager.OpenScene(ScenesFolderPas + "/ResultSceneFolder/" + "ResultScene.unity", OpenSceneMode.Single);
        }
        [MenuItem("SceneSwitching / TrainingGroundScene _#F6")]
        private static void SceneSwitchingSceneTrainingGroundScene()
        {
            EditorSceneManager.OpenScene(ScenesFolderPas + "/TrainingGroundSceneFolder/" + "TrainingGroundScene.unity", OpenSceneMode.Single);
        }
    }
}
#endif
