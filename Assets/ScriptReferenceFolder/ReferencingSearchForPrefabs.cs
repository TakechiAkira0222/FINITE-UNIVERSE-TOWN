using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.ScriptReference.SearchForPrefabs
{
    public class ReferencingSearchForPrefabs : MonoBehaviour
    {
        public static class SearchForPrefabName
        {
            public const string handCanvasPrefabName = "HandCanvas";
            public const string characterModelPrefabName = "CharacterModel";
            public const string mainCameraPrefabName = "MainCamera";
            public const string battleUiMenuScreenCanvasPrefabName = "BattleUiMenuScreenCanvas";
        }

        public static class SearchForPrefabTag
        {
            public const string playerCharacterPrefabTag = "PlayerCharacter";
            public const string mainCameraPrefabTag = "MainCamera";
        }
    }
}
