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
        /// PlayerStatus team Key Name
        /// </summary>
        public static readonly string s_CharacterStatusTeam = "team";

        /// <summary>
        /// PlayerStatus Keys
        /// </summary>
        public static readonly string[] s_CharacterStatusKeys =
            new string[] { s_CharacterStatusAttackPower, s_CharacterStatusMass , s_CharacterStatusTeam};

        /// <summary>
        /// RoomStatus GameType Key Name
        /// </summary>
        public static readonly string s_RoomStatusGameType = "gametype";
        
        /// <summary>
        /// RoomStatus Map Key Nmae
        /// </summary>
        public static readonly string s_RoomSatusMap = "Map";

        /// <summary>
        /// RoomStatus Keys
        /// </summary>
        public static readonly string[] s_RoomStatusKeys =
            new string[] { s_RoomStatusGameType, s_RoomSatusMap };
    }
}
