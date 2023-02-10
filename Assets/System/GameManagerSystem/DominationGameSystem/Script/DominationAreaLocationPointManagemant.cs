using UnityEngine;
using Photon.Pun;

using System;
using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.RoomStatus;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.GameManagerSystem.Domination
{
    public class DominationAreaLocationPointManagemant : AreaLocationPointManagemant
    {
        private enum AreaLocationPointType
        {
            AreaLocationA = 0,
            AreaLocationB,
            AreaLocationC,
        }

        #region SerializeField
        [Header("=== DominationGameManagement ===")]
        [SerializeField] private DominationGameManagement m_dominationGameManagement;
        [Header("=== DominationSoundEffectsManagement ===")]
        [SerializeField] private DominationSoundEffectsManagement m_dominationSoundEffectsManagement;
        [Header("=== ScriptSetting ===")]
        [SerializeField] private GameObject m_navigationText;
        [SerializeField] private AreaLocationPointType m_pointType = AreaLocationPointType.AreaLocationA;

        #endregion

        #region private variable
        private DominationGameManagement gameManagement => m_dominationGameManagement;
        private DominationSoundEffectsManagement soundEffectsManagement => m_dominationSoundEffectsManagement;
        private RoomStatusManagement roomStatusManagement => gameManagement.GetMyRoomStatusManagement();
        private GameObject navigationText => m_navigationText;
        private TextMesh   navigationTextTextMesh => m_navigationText.GetComponent<TextMesh>();
        private bool localGameEnd => gameManagement.GetLocalGameEnd();

        private Dictionary< int, Action<int>> m_storeActionDictionary = new Dictionary< int, Action<int>>();

        public event Action RisingAreaPoints = delegate { };

        #endregion


        #region Unity Event
        private void Reset()
        {
            m_dominationGameManagement = this.transform.root.GetComponent<DominationGameManagement>();
            m_navigationText = this.transform.GetChild(0).gameObject;
        }

        private void OnEnable()
        {
            RisingAreaPoints += soundEffectsManagement.RisingPointsSound;
            Debug.Log("RisingAreaPoints += soundEffectsManagement.<color=yellow>RisingPointsSound</color> <color=green> to add</color>.");

            StartCoroutine( uiRotation( navigationText));
        }

        private void Start()
        {
            RisingAreaPoints += soundEffectsManagement.RisingPointsSound;
            Debug.Log("RisingAreaPoints += soundEffectsManagement.<color=yellow>RisingPointsSound</color> <color=green> to add</color>.");

            setupOfStart();
        }

        private void OnDisable()
        {
          
        }

        protected override void OnTriggerStay( Collider other)
        {
            if(roomStatusManagement.IsGameRunning() && !localGameEnd) { GameState_Running(other); }
        }

    
        #endregion

        #region set up function

        private void setupOfStart()
        {
            m_storeActionDictionary.Add((int)AreaLocationPointType.AreaLocationA, (point) =>
            {
                roomStatusManagement.UpdateAreaLocationAPoint_domination(point);
                JudgmentToAcquirePoints(roomStatusManagement.GetAreaLocationAPoint_domination(), roomStatusManagement, navigationTextTextMesh);
            });

            Debug.Log("actions[(int)AreaLocationPointType.AreaLocationA] : roomStatusManagement.UpdateAreaLocationAPoint_domination(point) to add.");

            m_storeActionDictionary.Add((int)AreaLocationPointType.AreaLocationB, (point) =>
            {
                roomStatusManagement.UpdateAreaLocationBPoint_domination(point);
                JudgmentToAcquirePoints(roomStatusManagement.GetAreaLocationBPoint_domination(), roomStatusManagement, navigationTextTextMesh);
            });

            Debug.Log("actions[(int)AreaLocationPointType.AreaLocationB] : roomStatusManagement.UpdateAreaLocationBPoint_domination(point) to add.");

            m_storeActionDictionary.Add((int)AreaLocationPointType.AreaLocationC, (point) =>
            {
                roomStatusManagement.UpdateAreaLocationCPoint_domination(point);
                JudgmentToAcquirePoints(roomStatusManagement.GetAreaLocationCPoint_domination(), roomStatusManagement, navigationTextTextMesh);
            });

            Debug.Log("actions[(int)AreaLocationPointType.AreaLocationC] : roomStatusManagement.UpdateAreaLocationCPoint_domination(point) to add.");
        }

        #endregion

        #region mian game function
        private void GameState_Running(Collider other)
        {
            if (other.gameObject.tag == gameManagement.GetJudgmentTagName())
            {
                int num = other.gameObject.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if ((string)PhotonNetwork.LocalPlayer.Get(num).CustomProperties[CharacterStatusKey.teamNameKey] == CharacterTeamStatusName.teamAName)
                {
                    RisingAreaPoints();
                    m_storeActionDictionary[(int)m_pointType](1);
                }
                else
                {
                    RisingAreaPoints();
                    m_storeActionDictionary[(int)m_pointType](-1);
                }
            }
        }

        #endregion


        public void JudgmentToAcquirePoints(int areaLocationPoint, RoomStatusManagement roomStatusManagement, TextMesh text)
        {
            if (areaLocationPoint > roomStatusManagement.GetAreaLocationMaxPoint_domination() / 2) { text.color = Color.red; }
            else if (areaLocationPoint < roomStatusManagement.GetAreaLocationMaxPoint_domination() / 2) { text.color = Color.blue; }
        }
    }
}