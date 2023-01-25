using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Takechi.ScriptReference.CustomPropertyKey
{
    public class CustomPropertyKeyReference
    {
        public static class CharacterTeamStatusName
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
        /// room custom property status key class
        /// </summary>
        public static class RoomStatusKey
        {
            /// <summary>
            /// room custom propety gameStateKey
            /// </summary>
            public const string gameStateKey = "gameState";
            /// <summary>
            /// room custom propety victoryPointKey
            /// </summary>
            public const string victoryPointKey = "victoryPoint";
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
                    gameStateKey,
                    gameTypeKey,
                    mapKey,
                    victoryPointKey,
            };
        }

        /// <summary>
        /// room custom property team status key class
        /// </summary>
        public static class RoomStatusName
        {
            public class GameState
            {
                /// <summary>
                /// room custom propety gameState Name
                /// </summary>
                public const string running = "running";
                /// <summary>
                /// room custom propety gameState Name
                /// </summary> 
                public const string stopped = "stopped";
                /// <summary>
                /// room custom property gameState array
                /// </summary>
                public static string[] gameStateNameArray =
                    new string[]
                    {
                    running,
                    stopped,
                    };
            }

            public static class Map
            {
                /// <summary>
                /// room custom property map Name
                /// </summary>
                public const string futureCity = "FutureCity";
                /// <summary>
                /// room custom property map array
                /// </summary>
                public static string[] mapSelectionNameArray =
                    new string[]
                    {
                    futureCity,
                    };
            }

            public static class GameType
            {
                /// <summary>
                /// room custom property gameType Name
                /// </summary>
                public const string domination = "Domination";
                /// <summary>
                /// room custom property gameType Name
                /// </summary>
                public const string hardpoint = "Hardpoint";
                /// <summary>
                /// room custom property gameType array
                /// </summary>
                public static string[] gameTypeSelectionNameArray =
                   new string[]
                   {
                    domination,
                    hardpoint,
                   };
            }
        }

        /// <summary>
        /// room custom property team status key class
        /// </summary>
        public static class RoomTeamStatusKey
        {
            public class DominationStatusKey
            {
                /// <summary>
                /// room custom property teamAPointKey
                /// </summary>
                public const string teamAPoint = "teamAPoint";
                /// <summary>
                /// room custom property teamBPointKey
                /// </summary>
                public const string teamBPoint = "teamBPoint";
                /// <summary>
                /// room custom property AreaLocationPoint
                /// </summary>
                public const string AreaLocationAPoint = "AreaLocationAPoint";
                /// <summary>
                /// room custom property AreaLocationPoint
                /// </summary>
                public const string AreaLocationBPoint = "AreaLocationBPoint";
                /// <summary>
                /// room custom property AreaLocationPoint
                /// </summary>
                public const string AreaLocationCPoint = "AreaLocationCPoint";
                /// <summary>
                /// room custom property AreaLocationMaxPoint
                /// </summary>
                public const string AreaLocationMaxPoint = "MaxPoint";
                /// <summary>
                /// room custom property allKeys
                /// </summary>
                public static string[] allKeys = new string[]
                {
                   teamAPoint,
                   teamBPoint,
                   AreaLocationAPoint,
                   AreaLocationBPoint,
                   AreaLocationCPoint,
                };
            }

            public class HardPointStatusKey
            {
                /// <summary>
                /// room custom property teamAPointKey
                /// </summary>
                public const string teamAPoint = "PointA";
                /// <summary>
                /// room custom property teamBPointKey
                /// </summary>
                public const string teamBPoint = "PointB";
                /// <summary>
                /// room custom property allKeys
                /// </summary>
                public static string[] allKeys = new string[]
                {
                   teamAPoint,
                   teamBPoint,
                };
            }
        }
    }
}
