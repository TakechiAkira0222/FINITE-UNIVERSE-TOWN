using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Takechi.ScriptReference;
using TakechiEngine.PUN.Information;
using Takechi.ScriptReference.CustomPropertyKey;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.EditorDebug.Windows.NetworkDebug
{
    public class WindowsNetworkDebugController : TakechiPunInformationDisclosure
    {
        [SerializeField] private KeyCode m_photonNetworkDrawingButton = KeyCode.F3;
        [SerializeField] private KeyCode m_informationButton = KeyCode.I;

        private void Update()
        {
            if (PhotonNetwork.IsMasterClient && PhotonNetwork.InRoom)
            {
                if (Input.GetKeyDown(m_informationButton))
                {
                    RoomInfoAndJoinedPlayerInfoDisplay
                        (RoomStatusKey.allKeys, CharacterStatusKey.allKeys);
                }
            }
            else
            {
                if (Input.GetKeyDown(m_informationButton))
                {
                    Debug.Log("<color=yellow> Not in the room </color>");
                }
            }
        }

        private void OnGUI()
        {
            if (Input.GetKey(m_photonNetworkDrawingButton))
            {
                GUILayout.Box(PhotonNetwork.GetPing().ToString());
                GUILayout.Box(PhotonNetwork.NetworkClientState.ToString());
                GUILayout.Box(PhotonNetwork.Server.ToString());
            }
        }
    }
}