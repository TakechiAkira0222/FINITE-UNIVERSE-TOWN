using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using UnityEngine;
using TakechiEngine.PUN;
using UnityEngine.UI;
using System.Reflection;

namespace Takechi.UI.GameLogTextScrollView
{
    public class GameLogTextScrollViewController : TakechiPunCallbacks
    {
        #region serializeField
        [SerializeField] private GameObject m_logTextPrefab;
        [SerializeField] private Transform  m_contentTransform;
        [SerializeField] private PhotonView m_thisPhotonView;
        [SerializeField] private int textContentLimitExponent = 10;

        #endregion

        #region private variable
        private PhotonView thisPhotonView   => m_thisPhotonView;
        private Transform  contentTransform => m_contentTransform;
        private GameObject logTextPrefab    => m_logTextPrefab;
        private GameLogTextInterface logTextInterface => GameObject.Find("GameLogTextInterface").GetComponent<GameLogTextInterface>();
        private bool isMine => thisPhotonView.IsMine;

        #endregion

        #region unity event
        private void Reset()
        {
            m_thisPhotonView = this.GetComponent<PhotonView>();
        }
        private void FixedUpdate()
        {
            if (isMine) DisplayTextUpdate();
        }

        #endregion


        #region public funciotn
        public List<string> GetTextContentList() => logTextInterface.GetTextContentList();
        public void AddTextContent(string s)
        {
            thisPhotonView.RPC(nameof(RPC_AddList), RpcTarget.AllBufferedViaServer, s);
            StartCoroutine(DelayMethod( 1, () => { DisplayTextUpdate(); }));
        }
        public void RemoveTextContent(string s)
        {
            thisPhotonView.RPC(nameof(RPC_RemoveList), RpcTarget.AllBufferedViaServer, s);
            StartCoroutine(DelayMethod( 1, () => { DisplayTextUpdate(); }));
        }
        public void ClearTextContent(string s)
        {
            thisPhotonView.RPC(nameof(RPC_ClearList),  RpcTarget.AllBufferedViaServer);
            StartCoroutine(DelayMethod( 1, () => { DisplayTextUpdate(); }));
        }
        public void DisplayTextUpdate()
        {
            foreach (Transform c in contentTransform.transform)
            {
                GameObject.Destroy(c.gameObject);
            }

            foreach (string s in logTextInterface.GetTextContentList())
            {
                Text text = Instantiate(logTextPrefab, contentTransform).GetComponent<Text>();
                text.text = s;
            }
        }

        #endregion

        #region RPC function
        [PunRPC]
        private void RPC_AddList(string s)
        {
            logTextInterface.OnAddList(s);

            if( logTextInterface.GetTextContentList().Count >= textContentLimitExponent)
            {
                logTextInterface.OnRemoveAt();
            }
        }
        [PunRPC]
        private void RPC_RemoveList(string s) => logTextInterface.OnRemoveList(s);
        [PunRPC]
        private void RPC_ClearList() => logTextInterface.OnClearList();

        #endregion
    }
}
