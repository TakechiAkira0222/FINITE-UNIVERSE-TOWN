using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Takechi.UI.Ms
{
    public class MsOnDisplay : MonoBehaviour
    {
        [SerializeField] private Text m_msText;

        private Text msText => m_msText;

        private void FixedUpdate()
        {
            if(!PhotonNetwork.IsConnected) return;

            msText.text = PhotonNetwork.GetPing().ToString() + " ms";

            if(PhotonNetwork.GetPing() >= 60) msText.color = Color.red;
            else if(PhotonNetwork.GetPing() < 60 && PhotonNetwork.GetPing() >= 30) msText.color = Color.yellow;
            else if(PhotonNetwork.GetPing() < 30) msText.color = Color.green;
        }
    }
}
