using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.ScriptReference.DamagesThePlayerObject
{
    public class ObjectReferenceThatDamagesThePlayer : MonoBehaviour
    {
        /// <summary>
        /// Playerが、Playerにダメージを与えるobjectのTagリスト
        /// </summary>
        public static readonly string s_PlayerCharacterWeaponTagName = "PlayerCharacterWeapon";

        /// <summary>
        /// Playerにダメージを与えるobjectの名前リスト
        /// </summary>
        public static readonly List<string> s_DamagesThePlayerObjectNameList =
            new List<string>()
            {
                "Cube",
                "Barrel",
            };

        /// <summary>
        /// Playerにダメージを与えるobjectの威力の辞書
        /// </summary>
        public static readonly Dictionary<string, int> s_DamageObjectPowerDictionary =
            new Dictionary<string, int>()
            {
                { s_DamagesThePlayerObjectNameList[0], 10},
                { s_DamagesThePlayerObjectNameList[1], 10},
            };
    }
}
