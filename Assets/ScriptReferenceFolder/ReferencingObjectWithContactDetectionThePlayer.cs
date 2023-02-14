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
            public class ColliderTag
            {
                /// <summary>
                /// Player Weapon ColliderTagName
                /// </summary>
                public const string weaponTagName = "PlayerCharacterWeapon";
                /// <summary>
                /// Player Bullets ColliderTagName
                /// </summary>
                public const string bulletsTagName = "PlayerCharacterBullets";
                /// <summary>
                /// Player lazer ColliderTagName
                /// </summary>
                public const string lazerEffectTagName = "PlayerCharacterLazer";
                /// <summary>
                /// DamageFromPlayerToPlayer all ColliderTag
                /// </summary>
                public static string[] allTag = new string[3]
                {
                   weaponTagName,
                   bulletsTagName,
                   lazerEffectTagName,
                };
            }

            public class ColliderName
            {
                /// <summary>
                /// Player Tornado ColliderName
                /// </summary>
                public const string slimeAblityTornadoColliderName = "SliemAblityTornadoEffect";
                /// <summary>
                /// Player Stan ColliderName
                /// </summary>
                public const string slimeAblityStanColliderName = "SliemAblityStan2Effect";
                /// <summary>
                /// DamageFromPlayerToPlayer all ColliderName
                /// </summary>
                public static string[] allName = new string[2]
                {
                   slimeAblityTornadoColliderName,
                   slimeAblityStanColliderName,
                };
            }
        }

        public static class DeathToPlayer
        {
            public class ColliderTag
            {
                /// <summary>
                /// AntiField Object Tag
                /// </summary>
                public const string antiFieldTagName = "AntiField";

                /// <summary>
                /// deathToPlayer all tag
                /// </summary>
                public static string[] allTag = new string[1]
                {
                antiFieldTagName,
                };
            }

            public class ColliderName
            {
                /// <summary>
                /// 
                /// </summary>
                public const string mechanicalWarriorDeathblowBulletsColliderName = "MechanicalWarriorDeathblowBullets(Clone)";

                /// <summary>
                /// deathToPlayer all naem
                /// </summary>
                public static string[] allnaem = new string[1]
                {
                mechanicalWarriorDeathblowBulletsColliderName,
                };
            }
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
