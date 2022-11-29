using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

namespace Takechi.ScriptReference.DamagesThePlayerObject
{
    public class ObjectReferenceThatDamagesThePlayer : MonoBehaviour
    {
        /// <summary>
        /// Playerにダメージを与えるobjectの名前リスト
        /// </summary>
        public static readonly List<string> s_DamagesThePlayerObjectNameList =
            new List<string>()
            {
                "Cube"
            };

        /// <summary>
        /// Playerにダメージを与えるobjectのTagリスト
        /// </summary>
        public static readonly List<string> s_DamagesThePlayerObjectTagList =
            new List<string>()
            {
                ""
            };
    }
}
