using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Information;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.RoomStatus;
using Takechi.PlayableCharacter.FadingCanvas;
using UnityEngine;
using UnityEngine.Playables;

namespace Takechi.CharacterController.Address
{
    public class CharacterAddressManagement : MonoBehaviour
    {
        #region serializeField
        [Header("=== CharacterAddressSetting ===")]
        /// <summary>
        /// character parameters
        /// </summary>
        [SerializeField] private PlayableCharacterParameters  m_playableCharacterParameters;
        /// <summary>
        /// character Information
        /// </summary>
        [SerializeField] private PlayableCharacterInformation m_playableCharacterInformation;
        /// <summary>
        /// this roomStatus management
        /// </summary>
        [SerializeField] private RoomStatusManagement m_thisRoomStatusManagement;
        /// <summary>
        /// this photonViwe
        /// </summary>
        [SerializeField] private PhotonView m_thisPhotonView;
        /// <summary>
        /// main avater
        /// </summary>
        [SerializeField] private GameObject m_avater;
        /// <summary>
        /// main colloder
        /// </summary>
        [SerializeField] private Collider m_mainCollider;
        /// <summary>
        /// main rb
        /// </summary>
        [SerializeField] private Rigidbody m_rb;
        /// <summary>
        /// main audioSource
        /// </summary>
        [SerializeField] private AudioSource m_mainAudioSource;
        /// <summary>
        /// my avatar playable Director
        /// </summary>
        [SerializeField] private PlayableDirector m_myAvatarPlayableDirector;
        /// <summary>
        /// main camera
        /// </summary>
        [SerializeField] private Camera m_mainCamera;
        /// <summary>
        /// death Camera
        /// </summary>
        [SerializeField] private Camera m_deathCamera;
        /// <summary>
        /// hand only animetor
        /// </summary>
        [SerializeField] private Animator   m_handOnlyModelAnimator;
        /// <summary>
        /// hand only model object
        /// </summary>
        [SerializeField] private GameObject m_handOnlyModelObject;
        /// <summary>
        /// network model animator
        /// </summary>
        [SerializeField] private Animator   m_networkModelAnimator;
        /// <summary>
        /// network model object
        /// </summary>
        [SerializeField] private GameObject m_networkModelObject;
        /// <summary>
        /// model Outline
        /// </summary>
        [SerializeField] private Outline m_modelOutline;
        /// <summary>
        /// to Fade
        /// </summary>
        [SerializeField] private ToFade  m_toFade;
        /// <summary>
        /// battle ui Menu
        /// </summary>
        [SerializeField] private GameObject m_battleUiMenu;
        /// <summary>
        /// user ui Menu
        /// </summary>
        [SerializeField] private GameObject m_userUiMenu;
        /// <summary>
        /// reticle Canvas
        /// </summary>
        [SerializeField] private Canvas m_reticleCanvas;
        /// <summary>
        /// attackHits effect folder name
        /// </summary>
        [SerializeField] private List<string> m_attackHitsEffectFolderName = new List<string>(4);
        /// <summary>
        /// attackHit Effct
        /// </summary>
        [SerializeField] private GameObject m_attackHitEffct;
        /// <summary>
        /// death effect folder name
        /// </summary>
        [SerializeField] private List<string> m_deathEffectFolderName = new List<string>(4);
        /// <summary>
        /// death Effect
        /// </summary>
        [SerializeField] private GameObject m_deathEffect;

        #endregion

        #region private variable
        /// <summary>
        /// character parameters
        /// </summary>
        private PlayableCharacterParameters characterParameters => m_playableCharacterParameters;
        /// <summary>
        /// character infometion
        /// </summary>
        private PlayableCharacterInformation characterInformation => m_playableCharacterInformation;


        #endregion

        #region protected variable 
        /// <summary>
        /// this roomStatus management
        /// </summary>
        protected RoomStatusManagement thisRoomStatusManagement => m_thisRoomStatusManagement;
        /// <summary>
        /// this photonViwe
        /// </summary>
        protected PhotonView thisPhotonView => m_thisPhotonView;
        /// <summary>
        /// main avater
        /// </summary>
        protected GameObject avater => m_avater;
        /// <summary>
        /// main colloder
        /// </summary>
        protected Collider mainCollider => m_mainCollider;
        /// <summary>
        /// main rb
        /// </summary>
        protected Rigidbody rb => m_rb;
        /// <summary>
        /// main camera
        /// </summary>
        protected Camera mainCamera => m_mainCamera;
        /// <summary>
        /// death Camera
        /// </summary>
        protected Camera deathCamera => m_deathCamera;
        /// <summary>
        /// hand only animetor
        /// </summary>
        protected Animator handOnlyModelAnimator => m_handOnlyModelAnimator;
        /// <summary>
        /// hand only Model object
        /// </summary>
        protected GameObject handOnlyModelObject => m_handOnlyModelObject;
        /// <summary>
        /// network model animator
        /// </summary>
        protected Animator networkModelAnimator => m_networkModelAnimator;
        /// <summary>
        /// network model object
        /// </summary>
        protected GameObject networkModelObject => m_networkModelObject;
        /// <summary>
        /// model Outline
        /// </summary>
        protected Outline modelOutline => m_modelOutline;
        /// <summary>
        /// to Fade
        /// </summary>
        protected ToFade toFade => m_toFade;

        #endregion

        #region Get function
        public PlayableCharacterParameters GetCharacterParameters() { return characterParameters; }
        public PlayableCharacterInformation GetCharacterInformation() { return characterInformation; }
        public RoomStatusManagement GetMyRoomStatusManagement() { return thisRoomStatusManagement;}
        public PhotonView  GetMyPhotonView() { return thisPhotonView; }
        public Rigidbody   GetMyRigidbody() { return rb; }
        public AudioSource GetMyMainAudioSource() { return m_mainAudioSource; }
        public PlayableDirector GetMyAvatarPlayableDirector() { return m_myAvatarPlayableDirector; }
        public Collider   GetMyCollider() { return mainCollider; }
        public GameObject GetMyAvater() { return avater; }
        public Camera GetMyMainCamera() { return mainCamera; }
        public Camera GetMyDeathCamera() { return m_deathCamera; }
        public Animator   GetHandOnlyModelAnimator() { return handOnlyModelAnimator; }
        public GameObject GetHandOnlyModelObject() { return handOnlyModelObject; }
        public Animator   GetNetworkModelAnimator() { return networkModelAnimator; }
        public GameObject GetNetworkModelObject() { return networkModelObject; }
        public Outline GetMyOuline() { return modelOutline; }
        public ToFade  GetToFade() { return m_toFade; }
        public GameObject GetBattleUiMenu() { return m_battleUiMenu; }
        public GameObject GetUserUiMenu() { return m_userUiMenu;} 
        public Canvas     GetReticleCanvas() { return m_reticleCanvas;}
        public GameObject GetDeathEffect() { return m_deathEffect; }
        public GameObject GetAttackHitEffct() { return m_attackHitEffct; }
        public string GetAttackHitsEffectFolderName()
        {
            string path = "";
            foreach (string s in m_attackHitsEffectFolderName) { path += s + "/"; }

            return path;
        }
        public string GetDeathEffectFolderName()
        {
            string path = "";
            foreach (string s in m_deathEffectFolderName) { path += s + "/"; }

            return path;
        }

        #endregion
    }
}
