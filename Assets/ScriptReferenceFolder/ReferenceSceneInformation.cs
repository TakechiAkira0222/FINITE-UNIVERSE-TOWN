using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.ScriptReference.SceneInformation
{
    public class ReferenceSceneInformation 
    {
        public static class SceneName 
        {
            /// <summary>
            /// scene name titleScnene
            /// </summary>
            public const string titleScnene = "TitleScnene";
            /// <summary>
            /// scene name lobbyScene
            /// </summary>
            public const string lobbyScene = "LobbyScene";
            /// <summary>
            /// scene name characterSelectionScene
            /// </summary>
            public const string characterSelectionScene = "CharacterSelectionScene";
            /// <summary>
            /// scene name futureCityScene
            /// </summary>
            public const string futureCityScene = "FutureCityScene";
            /// <summary>
            /// scene name trainingGroundScene
            /// </summary>
            public const string trainingGroundScene = "TrainingGroundScene";
            /// <summary>
            /// scene name resultScene
            /// </summary>
            public const string resultScene = "ResultScene";
            /// <summary>
            /// scene name allKeys
            /// </summary>
            public static string[] allKeys = new string[]
            {
                  titleScnene,
                  lobbyScene,
                  characterSelectionScene,
                  futureCityScene,
                  trainingGroundScene,
                  resultScene,
            };
        }

        public static class SceneIndex 
        {
            /// <summary>
            /// scene index titleScnene
            /// </summary>
            public const int titleScnene = 0;
            /// <summary>
            /// scene index lobbyScene
            /// </summary>
            public const int lobbyScene = 1;
            /// <summary>
            /// scene index characterSelectionScene
            /// </summary>
            public const int characterSelectionScene = 2;
            /// <summary>
            /// scene index futureCityScene
            /// </summary>
            public const int futureCityScene = 3;
            /// <summary>
            /// scene index trainingGroundScene
            /// </summary>
            public const int trainingGroundScene = 4;
            /// <summary>
            /// scene index resultScene
            /// </summary>
            public const int resultScene = 5;
            /// <summary>
            /// scene index allKeys
            /// </summary>
            public static int[] allKeys = new int[]
            {
                  titleScnene,
                  lobbyScene,
                  characterSelectionScene,
                  futureCityScene,
                  trainingGroundScene,
                  resultScene,
            };
        }
    }
}
