using System.Collections;
using System.Collections.Generic;
using System.Data;
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
            /// player custom property attackPowerKey (float)
            /// </summary>
            public const string attackPowerKey = "attackPower";
            /// <summary>
            /// player custom property massKey (float)
            /// </summary>
            public const string massKey = "mass";
            /// <summary>
            /// player custom property teamNameKey (string)
            /// </summary>
            public const string teamNameKey = "team";
            /// <summary>
            /// player custom property selectedCharacterKey(int)
            /// </summary>
            public const string selectedCharacterNumberKey = "selectedCharacterNumber";
            /// <summary>
            /// player custom property allKeys
            /// </summary>
            public static string[] allKeys = new string[]
            {
                  attackPowerKey,
                  massKey,
                  teamNameKey,
                  selectedCharacterNumberKey
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
                /// room custom propety gameState beforeStartName
                /// </summary>
                public const string beforeStart = "beforeStart";
                /// <summary>
                /// room custom propety gameState runningName
                /// </summary>
                public const string running = "running";
                /// <summary>
                /// room custom propety gameState stoppedName
                /// </summary> 
                public const string stopped = "stopped";
                /// <summary>
                /// room custom propety gameState endName
                /// </summary>
                public const string end = "end";
                /// <summary>
                /// room custom propety gameState nullName
                /// </summary>
                public const string  NULL = "null";
                /// <summary>
                /// room custom property gameState array
                /// </summary>
                public static string[] gameStateNameArray =
                    new string[]
                    {
                        beforeStart,
                        running,
                        stopped,
                        end,
                        NULL,
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
