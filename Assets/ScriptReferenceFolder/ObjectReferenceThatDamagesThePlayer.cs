using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.ScriptReference.DamagesThePlayerObject
{
    public class ObjectReferenceThatDamagesThePlayer : MonoBehaviour
    {
        /// <summary>
        /// Player���APlayer�Ƀ_���[�W��^����object��Tag���X�g
        /// </summary>
        public static readonly string s_PlayerCharacterWeaponTagName = "PlayerCharacterWeapon";

        /// <summary>
        /// Player�Ƀ_���[�W��^����object�̖��O���X�g
        /// </summary>
        public static readonly List<string> s_DamagesThePlayerObjectNameList =
            new List<string>()
            {
                "Cube",
                "Barrel",
            };

        /// <summary>
        /// Player�Ƀ_���[�W��^����object�̈З͂̎���
        /// </summary>
        public static readonly Dictionary<string, int> s_DamageObjectPowerDictionary =
            new Dictionary<string, int>()
            {
                { s_DamagesThePlayerObjectNameList[0], 10},
                { s_DamagesThePlayerObjectNameList[1], 10},
            };
    }
}
