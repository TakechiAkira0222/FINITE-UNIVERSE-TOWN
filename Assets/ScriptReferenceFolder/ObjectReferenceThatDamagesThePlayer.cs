using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.ScriptReference.DamagesThePlayerObject
{
    public class ObjectReferenceThatDamagesThePlayer : MonoBehaviour
    {
        public static class DamageFromPlayerToPlayer
        {
            /// <summary>
            /// Player Weapon TagName
            /// </summary>
            public const string weaponTagName = "PlayerCharacterWeapon";

            /// <summary>
            /// Player Bullets TagName
            /// </summary>
            public const string bulletsTagName = "PlayerCharacterBullets";
        }

        public static class DamageFromObjectToPlayer
        {
            /// <summary>
            /// Playerにダメージを与えるobjectの名前リスト
            /// </summary>
            public static readonly List<string> objectNameList =
                new List<string>()
                {

                };

            /// <summary>
            /// Playerにダメージを与えるobjectの威力の辞書
            /// </summary>
            public static readonly Dictionary<string, int> objectDamagesDictionary =
                new Dictionary<string, int>()
                {

                };
        }
    }
}
