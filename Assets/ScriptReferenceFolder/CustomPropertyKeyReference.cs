using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.ScriptReference.CustomPropertyKey
{
    public class CustomPropertyKeyReference
    {
        public static class CharacterStatusTeamName
        {
            /// <summary>
            /// player custom property teamA name
            /// </summary>
            public const string teamAName = "TeamA";
            /// <summary>
            /// player custom property teamB name
            /// </summary>
            public const string teamBName = "TeamB";
        }

        /// <summary>
        /// player custom property key class
        /// </summary>
        public static class CharacterStatusKey
        {
            /// <summary>
            /// player custom property attackPowerKey
            /// </summary>
            public const string attackPowerKey = "attackPower";
            /// <summary>
            /// player custom property massKey
            /// </summary>
            public const string massKey = "mass";
            /// <summary>
            /// player custom property teamKey
            /// </summary>
            public const string teamKey = "team";
            /// <summary>
            /// player custom property selectedCharacterKey
            /// </summary>
            public const string selectedCharacterKey = "selectedCharacter";
            /// <summary>
            /// player custom property allKeys
            /// </summary>
            public static string[] allKeys = new string[]
            {
                  attackPowerKey,
                  massKey,
                  teamKey,
                  selectedCharacterKey
            };
        }

        /// <summary>
        /// room custom property key class
        /// </summary>
        public static class RoomStatusKey
        {
            /// <summary>
            /// room custom property gameTypeKey
            /// </summary>
            public const string gameTypeKey = "gametype";
            /// <summary>
            /// room custom property mapKey 
            /// </summary>
            public const string mapKey = "Map";
            /// <summary>
            /// room custom property allKeys
            /// </summary>
            public static string[] allKeys = new string[]
            {
                    gameTypeKey,
                    mapKey,
            };
        }
    }
}
