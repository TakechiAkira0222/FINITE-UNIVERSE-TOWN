using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Takechi.ScriptReference.DamagesThePlayerObject
{
    public class ReferencingObjectWithContactDetectionThePlayer : MonoBehaviour
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
            /// <summary>
            /// Player lazer TagName
            /// </summary>
            public const string lazerEffectTagName = "PlayerCharacterlazer";
            /// <summary>
            /// DamageFromPlayerToPlayer all tag
            /// </summary>
            public static string[] allTag = new string[3]
            {
                weaponTagName,
                bulletsTagName,
                lazerEffectTagName,
            };
        }

        public static class DeathToPlayer
        {
            /// <summary>
            /// AntiField Object Tag
            /// </summary>
            public const string antiFieldTagName = "AntiField";

            /// <summary>
            /// 
            /// </summary>
            public const string MechanicalWarriorDeathblowBulletsTagName = "MechanicalWarriorDeathblowBullets";

            /// <summary>
            /// deathToPlayer all tag
            /// </summary>
            public static string[] allTag = new string[2]
            {
                antiFieldTagName,
                MechanicalWarriorDeathblowBulletsTagName,
            };
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
