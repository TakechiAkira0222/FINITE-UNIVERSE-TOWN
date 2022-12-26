using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.ScriptReference.CustomPropertyKey
{
    public class CustomPropertyKeyReference : MonoBehaviour
    {
        /// <summary>
        /// PlayerStatus AttackPower Key Name
        /// </summary>
        public static readonly string s_CharacterStatusAttackPower = "attackPower";

        /// <summary>
        /// PlayerStatus Mass Key Name
        /// </summary>
        public static readonly string s_CharacterStatusMass = "mass";

        /// <summary>
        /// RoomStatus GameType Key Name
        /// </summary>
        public static readonly string s_RoomStatusGameType = "gametype";
        
        /// <summary>
        /// RoomStatus Map Key Nmae
        /// </summary>
        public static readonly string s_RoomSatusMap = "Map";
    }
}
