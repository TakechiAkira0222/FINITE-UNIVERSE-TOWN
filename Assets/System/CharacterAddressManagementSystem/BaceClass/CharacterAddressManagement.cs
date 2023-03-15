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

        [Header(" === Camera Address ===")]
        /// <summary>
        /// main camera
        /// </summary>
        [SerializeField] private Camera m_mainCamera;
        /// <summary>
        /// death Camera
        /// </summary>
        [SerializeField] private Camera m_deathCamera;

        [Header(" === Model Address ===")]
        /// <summary>
        /// hand only animetor
        /// </summary>
        [SerializeField] private Animator   m_handOnlyModelAnimator;
        /// <summary>
        /// hand only model object
        /// </summary>
        [SerializeField] private GameObject m_handOnlyModelObject;
        /// <summary>
        /// hand only model material
        /// </summary>
        [SerializeField] private Renderer   m_handOnlyModelRenderer;
        /// <summary>
        /// network model animator
        /// </summary>
        [SerializeField] private Animator   m_networkModelAnimator;
        /// <summary>
        /// network model object
        /// </summary>
        [SerializeField] private GameObject m_networkModelObject;
        /// <summary>
        /// network model material
        /// </summary>
        [SerializeField] private Renderer   m_networkModelRenderer;
        /// <summary>
        /// model Outline
        /// </summary>
        [SerializeField] private Outline m_modelOutline;

        [Header(" === Ui Address ===")]
        /// <summary>
        /// to Fade
        /// </summary>
        [SerializeField] private ToFade  m_toFade;
        /// <summary>
        /// battle ui Menu
        /// </summary>
        [SerializeField] private GameObject m_battleUiMenu;
        /// <summary>
        /// stan Menu
        /// </summary>
        [SerializeField] private GameObject m_stanMenu;
        /// <summary>
        /// user ui Menu
        /// </summary>
        [SerializeField] private GameObject m_userUiMenu;
        /// <summary>
        /// map ui Menu
        /// </summary>
        [SerializeField] private GameObject m_mapUiMenu;
        /// <summary>
        /// reticle Canvas
        /// </summary>
        [SerializeField] private Canvas m_reticleCanvas;
        /// <summary>
        /// display to thers cnavas
        /// </summary>
        [SerializeField] private Canvas m_displayToOthersCanvas;
        /// <summary>
        /// navigation Ui 
        /// </summary>
        [SerializeField] private List<GameObject> m_navigationUiList = new List<GameObject>();

        [Header(" === Effect Address ===")]
        /// <summary>
        /// attackHits effect folder name
        /// </summary>
        [SerializeField] private List<string> m_takenDamageEffectFolderName = new List<string>(4);
        /// <summary>
        /// attackHit Effct
        /// </summary>
        [SerializeField] private GameObject m_takenDamageEffct;
        /// <summary>
        /// death effect folder name
        /// </summary>
        [SerializeField] private List<string> m_deathEffectFolderName = new List<string>(4);
        /// <summary>
        /// death Effect
        /// </summary>
        [SerializeField] private GameObject m_deathEffect;
        /// <summary>
        /// stan effect
        /// </summary>
        [SerializeField] private GameObject m_stanEffect;
        /// <summary>
        /// ai slime hit effect
        /// </summary>
        [SerializeField] private GameObject m_aiSlimeHitEffect;

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

        #region Get function
        public PlayableCharacterParameters GetCharacterParameters() => characterParameters; 
        public PlayableCharacterInformation GetCharacterInformation() => characterInformation; 
        public RoomStatusManagement GetMyRoomStatusManagement() => m_thisRoomStatusManagement;
        public PhotonView  GetMyPhotonView() => m_thisPhotonView;
        public Rigidbody   GetMyRigidbody() => m_rb;
        public AudioSource GetMyMainAudioSource() => m_mainAudioSource;
        public PlayableDirector GetMyAvatarPlayableDirector() => m_myAvatarPlayableDirector;
        public Collider   GetMyCollider() => m_mainCollider;
        public GameObject GetMyAvater() => m_avater;
        public Camera     GetMyMainCamera() => m_mainCamera;
        public Camera     GetMyDeathCamera() => m_deathCamera;
        public Animator   GetHandOnlyModelAnimator() => m_handOnlyModelAnimator;
        public GameObject GetHandOnlyModelObject() => m_handOnlyModelObject;
        public Renderer   GetHandOnlyModelRenderer() => m_handOnlyModelRenderer;
        public Animator   GetNetworkModelAnimator() => m_networkModelAnimator;
        public GameObject GetNetworkModelObject () => m_networkModelObject;
        public Renderer   GetNetworkModelRenderer() => m_handOnlyModelRenderer;
        public Outline    GetMyOuline() => m_modelOutline;

        public ToFade     GetToFade() => m_toFade; 
        public GameObject GetBattleUiMenu() => m_battleUiMenu;
        public GameObject GetStanMenu() => m_stanMenu;
        public GameObject GetUserUiMenu() => m_userUiMenu;
        public GameObject GetMapUiMenu() => m_mapUiMenu;
        public Canvas     GetReticleCanvas() => m_reticleCanvas;
        public Canvas     GetDisplayToOthersCanvas() => m_displayToOthersCanvas;
        public List<GameObject> GetNavigationUiList() => m_navigationUiList;

        public GameObject GetDeathEffect() => m_deathEffect;
        public GameObject GetStanEffect() =>  m_stanEffect;
        public GameObject GetAiSlimeHitEffect() => m_aiSlimeHitEffect;
        public GameObject GetAttackHitEffct()   => m_takenDamageEffct;
        
        public string GetTakenDamageEffectFolderName()
        {
            string path = "";
            foreach (string s in m_takenDamageEffectFolderName) { path += s + "/"; }

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
