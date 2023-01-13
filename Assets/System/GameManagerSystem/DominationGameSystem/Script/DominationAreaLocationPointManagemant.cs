using UnityEngine;
using Photon.Pun;

using System;
using System.Collections;
using System.Collections.Generic;

using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.GameManagerSystem.Domination
{
    public class DominationAreaLocationPointManagemant : AreaLocationPointManagemant
    {
        [SerializeField] private DominationGameManagement m_gameManagement;
        [SerializeField] private GameObject m_navigationText;
        [SerializeField] private AreaLocationPointType m_pointType = AreaLocationPointType.AreaLocationA;

        private enum AreaLocationPointType
        {
            AreaLocationA = 0,
            AreaLocationB,
            AreaLocationC,
        }

        public class StoreAction
        {
            public Action<int> AreaLocationAAction = delegate { };
            public Action<int> AreaLocationBAction = delegate { };
            public Action<int> AreaLocationCAction = delegate { };
            public StoreAction(DominationGameManagement gameManagement , TextMesh text)
            {
                AreaLocationAAction += (point) => 
                { 
                    gameManagement.SetAreaLocationAPoint(point);
                    JudgmentToAcquirePoints(gameManagement.GetAreaLocationAPoint(), gameManagement, text);
                };
                AreaLocationBAction += (point) => 
                {
                    gameManagement.SetAreaLocationBPoint(point);
                    JudgmentToAcquirePoints(gameManagement.GetAreaLocationBPoint(), gameManagement, text);
                };
                AreaLocationCAction += (point) => 
                { 
                    gameManagement.SetAreaLocationCPoint(point);
                    JudgmentToAcquirePoints(gameManagement.GetAreaLocationCPoint(), gameManagement, text);
                };
            }

            public void JudgmentToAcquirePoints( int areaLocationPoint, DominationGameManagement gameManagement, TextMesh text)
            {
                if (areaLocationPoint > gameManagement.GetAreaLocationMaxPoint() / 2) { text.color = Color.red; }
                else if (areaLocationPoint < gameManagement.GetAreaLocationMaxPoint() / 2) { text.color = Color.blue; }
            }
        }

        private StoreAction m_storeAction;
        private Dictionary< int, Action<int>> m_storeActionDictionary = new Dictionary< int, Action<int>>();

        #region Unity Event
        private void Reset()
        {
            m_gameManagement = this.transform.root.GetComponent<DominationGameManagement>();
            m_navigationText = this.transform.GetChild(0).gameObject;
        }

        private void OnEnable()
        {
            StartCoroutine( uiRotation( m_navigationText));
        }

        private void Start()
        {
            m_storeAction = 
                new StoreAction( m_gameManagement, m_navigationText.GetComponent<TextMesh>());

            m_storeActionDictionary.Add((int)AreaLocationPointType.AreaLocationA, m_storeAction.AreaLocationAAction); 
            Debug.Log("actions[(int)AreaLocationPointType.AreaLocationA] : gameManagement.SetAreaLocationAPoint(point) to add.");

            m_storeActionDictionary.Add((int)AreaLocationPointType.AreaLocationB, m_storeAction.AreaLocationBAction);
            Debug.Log("actions[(int)AreaLocationPointType.AreaLocationB] : gameManagement.SetAreaLocationBPoint(point) to add.");

            m_storeActionDictionary.Add((int)AreaLocationPointType.AreaLocationC, m_storeAction.AreaLocationCAction);
            Debug.Log("actions[(int)AreaLocationPointType.AreaLocationC] : gameManagement.SetAreaLocationCPoint(point) to add.");
        }

        private void OnDisable()
        {
          
        }

        protected override void OnTriggerStay( Collider other)
        {
            base.OnTriggerStay(other);

            if ( other.gameObject.tag == m_gameManagement.GetJudgmentTagName())
            {
                int num = other.gameObject.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if ((string)PhotonNetwork.LocalPlayer.Get(num).CustomProperties[ CharacterStatusKey.teamKey] == CharacterTeamStatusName.teamAName)
                {
                    if ( hitTeamBMemberList.Count != 0) return;
                    m_storeActionDictionary[(int)m_pointType](1);
                }
                else
                {
                    if ( hitTeamAMemberList.Count != 0) return;
                    m_storeActionDictionary[(int)m_pointType](-1);
                }
            }
        }

        #endregion
    }
}