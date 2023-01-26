using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.ScriptReference.NetworkEnvironment
{
    public class ReferencingNetworkEnvironmentDetails : MonoBehaviour
    {
        public static class NetworkSyncSettings
        {
            /// <summary>
            /// fade Production Time_second
            /// </summary>
            public const int fadeProductionTime_Seconds = 5;

            /// <summary>
            /// Synchro time before game start second
            /// </summary>
            public const int synchroTimeBeforeGameStart_Seconds = 3;
            // public const int synchroTimeBeforeGameStart_Seconds = 10;

            /// <summary>
            /// connection Synchronization Time Frame
            /// </summary>
            //public const int connectionSynchronizationTime = 1;
            public const int connectionSynchronizationTime = 1000;

            /// <summary>
            /// instantiationSetting Synchronization Time Frame
            /// </summary>
            //public const int instantiationSettingSynchronizationTime = 1;
            public const int instantiationSettingSynchronizationTime = 10000;
        }
    }
}