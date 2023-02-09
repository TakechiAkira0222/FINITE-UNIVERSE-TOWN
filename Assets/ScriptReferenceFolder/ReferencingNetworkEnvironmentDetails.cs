using Photon.Pun;
using POpusCodec.Enums;
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
            /// synchro time before game start second
            /// </summary>
            public const int synchroTimeBeforeGameStart_Seconds = 10;

            /// <summary>
            /// Client latency countermeasure time
            /// </summary>
            public static readonly float clientLatencyCountermeasureTime =  PhotonNetwork.GetPing() / 10;
#if UNITY_EDITOR
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
#else
            public const int connectionSynchronizationTime = 1000;
            public const int instantiationSettingSynchronizationTime = 10000;
#endif




        }
    }
}