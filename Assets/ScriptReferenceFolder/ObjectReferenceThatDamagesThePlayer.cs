using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.ScriptReference.DamagesThePlayerObject
{
    public class ObjectReferenceThatDamagesThePlayer : MonoBehaviour
    {
        /// <summary>
        /// Player�Ƀ_���[�W��^����object�̖��O���X�g
        /// </summary>
        public static readonly List<string> s_DamagesThePlayerObjectNameList =
            new List<string>()
            {
                "Cube",
                "Barrel"
            };

        /// <summary>
        /// Player�Ƀ_���[�W��^����object��Tag���X�g
        /// </summary>
        public static readonly List<string> s_DamagesThePlayerObjectTagList =
            new List<string>()
            {
                ""
            };
    }
}
