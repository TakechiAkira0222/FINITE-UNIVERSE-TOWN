using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
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
            /// DamageFromPlayerToPlayer all tag
            /// </summary>
            public static string[] allTag = new string[2]
            {
                weaponTagName,
                bulletsTagName,
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
            /// Player�Ƀ_���[�W��^����object�̖��O���X�g
            /// </summary>
            public static readonly List<string> objectNameList =
                new List<string>()
                {

                };

            /// <summary>
            /// Player�Ƀ_���[�W��^����object�̈З͂̎���
            /// </summary>
            public static readonly Dictionary<string, int> objectDamagesDictionary =
                new Dictionary<string, int>()
                {

                };
        }
    }
}
