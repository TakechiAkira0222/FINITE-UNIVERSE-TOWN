using System;
using UnityEngine;
using Photon.Pun;

using TakechiEngine.PUN.CustomProperties;
using Takechi.CharacterController.Address;

using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using Photon.Realtime;
using Takechi.CharacterController.Information;

namespace Takechi.CharacterController.Parameters
{
    /// <summary>
    /// パラメータの中間管理を行うクラス
    /// </summary>
    [RequireComponent(typeof(CharacterAddressManagement))]
    public class CharacterStatusManagement : TakechiPunCustomProperties
    {
        #region SerializeField

        [Header("=== addressManagement === ")]
        /// <summary>
        /// 住所
        /// </summary>
        [SerializeField] private CharacterAddressManagement m_addressManagement;
       
        #endregion

        #region private variable
        /// <summary>
        /// character parameters
        /// </summary>
        private PlayableCharacterParameters characterParameters => addressManagement.GetCharacterParameters();
       
        #endregion

        #region protected member variable
        /// <summary>
        /// character addressManagement
        /// </summary>
        protected CharacterAddressManagement addressManagement => m_addressManagement;
        /// <summary>
        /// this photonViwe
        /// </summary>
        protected PhotonView thisPhotonView => addressManagement.GetMyPhotonView();
        /// <summary>
        /// main rb
        /// </summary>
        protected Rigidbody rb => addressManagement.GetMyRigidbody();
        /// <summary>
        /// model Outline
        /// </summary>
        protected Outline modelOutline => addressManagement.GetMyOuline();
        /// <summary>
        /// local player custom properties
        /// </summary>
        protected ExitGames.Client.Photon.Hashtable m_localPlayerCustomProperties =
              new ExitGames.Client.Photon.Hashtable();
        /// <summary>
        /// 移動スピード
        /// </summary>
        protected float movingSpeed;
        /// <summary>
        /// 移動量
        /// </summary>
        protected float movingScalar;
        /// <summary>
        /// 移動ベクトル
        /// </summary>
        protected Vector3 movingVector;
        /// <summary>
        /// 横移動の移動量 割合
        /// </summary>
        protected float lateralMovementRatio;
        /// <summary>
        /// 攻撃力
        /// </summary>
        protected float attackPower;
        /// <summary>
        /// ジャンプ力
        /// </summary>
        protected float jumpPower;
        /// <summary>
        /// リスポーン時間
        /// </summary>
        protected int   respawnTime_Seconds;
        /// <summary>
        /// 必殺技 使用可能
        /// </summary>
        protected bool  canUseDeathblow = false;
        /// <summary>
        /// アビリティ1 使用可能
        /// </summary>
        protected bool  canUseAbility1 = false;
        /// <summary>
        /// アビリティ2 使用可能
        /// </summary>
        protected bool  canUseAbility2 = false;
        /// <summary>
        /// アビリティ3 使用可能
        /// </summary>
        protected bool  canUseAbility3 = false;
        /// <summary>
        /// 必殺技 使用可能 TimeCount
        /// </summary>
        protected float canUseDeathblow_TimeCount_Seconds;
        /// <summary>
        /// アビリティ1 使用可能 TimeCount
        /// </summary>
        protected float canUseAbility1_TimeCount_Seconds;
        /// <summary>
        /// アビリティ2 使用可能 TimeCount
        /// </summary>
        protected float canUseAbility2_TimeCount_Seconds;
        /// <summary>
        /// アビリティ3 使用可能 TimeCount
        /// </summary>
        protected float canUseAbility3_TimeCount_Seconds;

        #endregion

        #region this script status flag
        /// <summary>
        /// status setup complete : true
        /// </summary>
        private bool characterStatusSetUpComplete = false;
        /// <summary>
        /// status setup complete : true
        /// </summary>
        private bool localPlayerCustomPropertiesStatusSetUpComplete = false;

        #endregion

        #region event action
        /// <summary>
        /// status setup complete action
        /// </summary>
        public event Action characterStatusSetUpCompleteAction = delegate{};
        /// <summary>
        /// status setup complete action
        /// </summary>
        public event Action localPlayerCustomPropertiesStatusSetUpCompleteAction = delegate{};
        /// <summary>
        /// initialize camera settings
        /// </summary>
        public event Action InitializeCameraSettings = delegate { };
        /// <summary>
        /// Initialize effect setting
        /// </summary>
        public event Action InitializeEffectSettings = delegate { };

        #endregion

        #region unity event
        protected virtual void Awake()
        {
            setupCharacterParameters();

            if (!thisPhotonView.IsMine) return;

            setupLocalPlayerCustomProperties();
        }

        #endregion

        #region set up function

        /// <summary>
        /// CharacterStatus を、データベースの変数で設定します。
        /// </summary>
        private void setupCharacterParameters()
        {
            SetMovingSpeed(characterParameters.GetSpeed());
            SetMovingScalar(0);
            SetMovingVector(Vector3.zero);
            SetLateralMovementRatio(characterParameters.GetLateralMovementRatio());
            SetAttackPower(characterParameters.GetAttackPower());
            SetJumpPower(characterParameters.GetJumpPower());
            SetMass(characterParameters.GetCleanMass());
            SetRespawnTime_Seconds( characterParameters.GetRespawnTime_Seconds());
            SetCanUseDeathblow(false);
            SetCanUseAbility1(false); 
            SetCanUseAbility2(false);
            SetCanUseAbility3(false);

            Debug.Log($"<color=green> settingCharacterParameters </color>\n" +
                      $"<color=blue> info</color>\n" +
                      $" NickName : {PhotonNetwork.LocalPlayer.NickName} \n" +
                      $" movingSpeed = {movingSpeed}\n" +
                      $" movingScalar = {movingScalar}\n" +
                      $" movingVector = {movingVector}\n" +
                      $" attackPower = {attackPower}\n" +
                      $" jumpPower = {jumpPower}\n" +
                      $" mass = {rb.mass}\n"+
                      $" respawnTime_Seconds = {respawnTime_Seconds}\n"+
                      $" canUseDeathblow = {canUseDeathblow}\n"+
                      $" canUseAbility1  = {canUseAbility1}\n"+
                      $" canUseAbility2  = {canUseAbility2}\n"+
                      $" canUseAbility3  = {canUseAbility3}\n"
                      );

            characterStatusSetUpComplete = true;
            Debug.Log(" characterStatusSetUpComplete = <color=blue>true</color>");
            characterStatusSetUpCompleteAction();
            Debug.Log("<color=yellow> characterStatusSetUpCompleteAction</color>()");
        }
        /// <summary>
        /// LocalPlayerCustomPropertiesを、データベースの変数で設定します。
        /// </summary>
        private void setupLocalPlayerCustomProperties()
        {
            SetCustomPropertiesAttackPower(characterParameters.GetAttackPower());
            SetCustomPropertiesMass(characterParameters.GetCleanMass());

            localPlayerCustomPropertiesStatusSetUpComplete = true;
            Debug.Log(" localPlayerCustomPropertiesStatusSetUpComplete = <color=blue>true</color>");
            localPlayerCustomPropertiesStatusSetUpCompleteAction();
            Debug.Log("<color=yellow> localPlayerCustomPropertiesStatusSetUpCompleteAction</color>() ");
        }

        #endregion

        #region set function
        // Character Status
        public void SetRespawnTime_Seconds(int time) { respawnTime_Seconds = time; }
        public void SetCanUseDeathblow(bool flag) { canUseDeathblow = flag; }
        public void SetCanUseAbility1(bool flag) { canUseAbility1 = flag; }
        public void SetCanUseAbility2(bool flag) { canUseAbility2 = flag; }
        public void SetCanUseAbility3(bool flag) { canUseAbility3 = flag; }
        public void SetCanUseDeathblow_TimeCount_Seconds(float changeValue) { canUseDeathblow_TimeCount_Seconds = changeValue; }
        public void SetCanUsecanUseAbility1_TimeCount_Seconds(float changeValue) { canUseAbility1_TimeCount_Seconds = changeValue; }
        public void SetCanUsecanUseAbility2_TimeCount_Seconds(float changeValue) { canUseAbility2_TimeCount_Seconds = changeValue; }
        public void SetCanUsecanUseAbility3_TimeCount_Seconds(float changeValue) { canUseAbility3_TimeCount_Seconds = changeValue; }
        public void SetMovingSpeed(float changeValue) { movingSpeed = changeValue; }
        public void SetMovingScalar(float changeValue) { movingScalar = changeValue; }
        public void SetMovingVector(Vector3 vec) { movingVector = vec; }
        public void SetLateralMovementRatio(float changeValue){ lateralMovementRatio = changeValue;}
        public void SetAttackPower(float changeValue) { attackPower = changeValue; }
        public void SetJumpPower(float changeValue) { jumpPower = changeValue; }
        public void SetMass(float changeValue) { rb.mass = changeValue; }
        public void SetVelocity(Vector3 velocityValue) { rb.velocity = velocityValue; }
        public void SetIsKinematic(bool flag) { rb.isKinematic = flag; }
        public void SetModelOulineColor(Color color) { modelOutline.OutlineColor = color; }
        public void SetModelOulineMode(Outline.Mode mode) { modelOutline.OutlineMode = mode; }

        // CustomProperties
        public void SetThisPhotonViewRequestOwnership() { thisPhotonView.RequestOwnership(); }
        public void SetCustomPropertiesTeamName(string teamName) { setLocalPlayerCustomProperties(CharacterStatusKey.teamNameKey, teamName); }
        public void SetCustomPropertiesMass(float changeValue) { setLocalPlayerCustomProperties(CharacterStatusKey.massKey, changeValue); }
        public void SetCustomPropertiesAttackPower(float changeValue) { setLocalPlayerCustomProperties(CharacterStatusKey.attackPowerKey, changeValue); }
        public void SetCustomPropertiesSelectedCharacterNumber(int characterNumber) { setLocalPlayerCustomProperties(CharacterStatusKey.selectedCharacterNumberKey, characterNumber); }

        #endregion

        #region get function
        // Character Status
        public bool  GetCharacterStatusSetUpCompleteFlag() { return characterStatusSetUpComplete; }
        public bool  GetlocalPlayerCustomPropertiesStatusSetUpCompleteFlag() { return localPlayerCustomPropertiesStatusSetUpComplete; }
        public bool  GetIsGrounded()
        {
            Ray ray =
                   new Ray( rb.gameObject.transform.position + new Vector3( 0, 0.1f),
                            Vector3.down * 0.1f);

            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit, 0.1f))
            {
                Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.green);
                return true;
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.red);
                return false;
            }
        }
        public int   GetRespawnTime_Seconds() { return respawnTime_Seconds; }
        public bool  GetCanUseDeathblow() { return canUseDeathblow; }
        public float GetCanUseDeathblow_TimeCount_Seconds() { return canUseDeathblow_TimeCount_Seconds; }
        public float GetCanUseDeathblow_RecoveryTime_Seconds() {return characterParameters.GetDeathblow_RecoveryTime_Seconds(); }
        public bool  GetCanUseAbility1() { return canUseAbility1; }
        public float GetCanUseAbility1_TimeCount_Seconds() { return canUseAbility1_TimeCount_Seconds; }
        public float GetCanUseAbility1_RecoveryTime_Seconds() {return characterParameters.GetAbility1_RecoveryTime_Seconds(); }
        public bool  GetCanUseAbility2() { return canUseAbility2; }
        public float GetCanUseAbility2_TimeCount_Seconds() { return canUseAbility2_TimeCount_Seconds; }
        public float GetCanUseAbility2_RecoveryTime_Seconds() {return characterParameters.GetAbility2_RecoveryTime_Seconds(); }
        public bool  GetCanUseAbility3() { return canUseAbility3; }
        public float GetCanUseAbility3_TimeCount_Seconds() { return canUseAbility3_TimeCount_Seconds; }
        public float GetCanUseAbility3_RecoveryTime_Seconds() {return characterParameters.GetAbility3_RecoveryTime_Seconds(); }
        public float GetMovingSpeed() { return movingSpeed; }
        public float GetMovingScalar() { return movingScalar; }
        public Vector3 GetMovingVector() { return movingVector; }
        public float GetLateralMovementRatio() { return lateralMovementRatio; }
        public float GetAttackPower() { return attackPower; }
        public float GetJumpPower() { return jumpPower; }
        public float GetMass() { return rb.mass;}
        public float GetCleanMass() { return characterParameters.GetCleanMass(); }

        // CustomProperties
        public bool   GetIsLocal() { return PhotonNetwork.LocalPlayer.IsLocal; }
        public bool   GetIsMine() { return thisPhotonView.IsMine; }
        public string GetCustomPropertiesTeamName() { return (string)PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.teamNameKey]; }
        public string GetCustomPropertiesTeamName(Player player) { return (string)player.CustomProperties[CharacterStatusKey.teamNameKey]; }
        public string GetCustomPropertiesTeamName(int controllerActorNr) { return (string)PhotonNetwork.LocalPlayer.Get(controllerActorNr).CustomProperties[CharacterStatusKey.teamNameKey]; }

        public float  GetCustomPropertiesMass() { return (float)PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.massKey]; }
        public float  GetCustomPropertiesMass(Player player) { return (float)player.CustomProperties[CharacterStatusKey.massKey]; }
        public float  GetCustomPropertiesMass(int controllerActorNr) { return (float)PhotonNetwork.LocalPlayer.Get(controllerActorNr).CustomProperties[CharacterStatusKey.massKey]; }

        public float  GetCustomPropertiesAttackPower() { return (float)PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.attackPowerKey]; }
        public float  GetCustomPropertiesAttackPower(Player player) { return (float)player.CustomProperties[CharacterStatusKey.attackPowerKey]; }
        public float  GetCustomPropertiesTeamAttackPower(int controllerActorNr) { return (float)PhotonNetwork.LocalPlayer.Get(controllerActorNr).CustomProperties[CharacterStatusKey.attackPowerKey]; }

        public int    GetCustomPropertiesSelectedCharacterNumber() { return (int)PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.selectedCharacterNumberKey]; }
        public int    GetCustomPropertiesSelectedCharacterNumber(Player player) { return (int)player.CustomProperties[CharacterStatusKey.selectedCharacterNumberKey]; }
        public int    GetCustomPropertiesSelectedCharacterNumber(int controllerActorNr) { return (int)PhotonNetwork.LocalPlayer.Get(controllerActorNr).CustomProperties[CharacterStatusKey.selectedCharacterNumberKey]; }

        #endregion

        #region update function

        /// <summary>
        /// LocalPlayerCustomPropertiesを、status の変数で更新します。
        /// </summary>
        public void UpdateLocalPlayerCustomProrerties()
        {
            if (!thisPhotonView.IsMine) return;

            SetCustomPropertiesAttackPower(attackPower);
            SetCustomPropertiesMass(rb.mass);

            Debug.Log($"<color=green> updateLocalPlayerCustomProrerties </color>\n" +
                      $"<color=blue> info</color>\n" +
                      $" NickName : {PhotonNetwork.LocalPlayer.NickName} \n" +
                      $" {CharacterStatusKey.attackPowerKey} = {GetCustomPropertiesAttackPower()}\n" +
                      $" {CharacterStatusKey.massKey} = {GetCustomPropertiesMass()}\n"
                    );
        }
        public void UpdateMovingSpeed(float changeValue)
        {
            Debug.Log($" UpdateSpeed {movingSpeed} -> { movingSpeed + changeValue}");
            movingSpeed += changeValue;
        }
        public void UpdateAttackPower(float changeValue)
        {
            Debug.Log($" UpdateAttackPower {attackPower} -> { attackPower + changeValue}");
            attackPower += changeValue;
        }
        public void UpdateJumpPower(float changeValue)
        {
            Debug.Log($" UpdateJumpPower {jumpPower} -> { jumpPower + changeValue}");
            jumpPower += changeValue;
        }
        public void UpdateMass(float changeValue)
        {
            if ( rb.mass + changeValue >= GetCleanMass())
            {
                rb.mass = GetCleanMass();
                Debug.Log($"{rb.mass} -> {GetCleanMass()} GetCleanMass to max");
            }
            else if (rb.mass + changeValue <= 1)
            {
                rb.mass = 1;
                Debug.Log($"{rb.mass} -> {1}");
            }
            else
            {
                rb.mass += changeValue;
                Debug.Log($"{ rb.mass} ->: {rb.mass + changeValue}");
            }
        }
        public void UpdateCanUseDeathblow_TimeCount_Seconds(float changeValue) { canUseDeathblow_TimeCount_Seconds += changeValue; }
        public void UpdateCanUseAbility1_TimeCount_Seconds(float changeValue)  { canUseAbility1_TimeCount_Seconds += changeValue; }
        public void UpdateCanUseAbility2_TimeCount_Seconds(float changeValue)  { canUseAbility2_TimeCount_Seconds += changeValue; }
        public void UpdateCanUseAbility3_TimeCount_Seconds(float changeValue)  { canUseAbility3_TimeCount_Seconds += changeValue; }

        #endregion

        #region reset parameters

        /// <summary>
        /// CharacterStatus を、データベースの変数で設定します。
        /// </summary>
        public void ResetCharacterParameters()
        {
            SetMovingSpeed( characterParameters.GetSpeed());
            SetLateralMovementRatio(characterParameters.GetLateralMovementRatio());
            SetAttackPower( characterParameters.GetAttackPower());
            SetJumpPower( characterParameters.GetJumpPower());
            SetMass( characterParameters.GetCleanMass());

            Debug.Log($"<color=green> settingCharacterParameters </color>\n" +
                      $"<color=blue> info</color>\n" +
                      $" NickName : {PhotonNetwork.LocalPlayer.NickName} \n" +
                      $" m_movingSpeed = {movingSpeed}\n" +
                      $" m_attackPower = {attackPower}\n" +
                      $" m_jumpPower = {jumpPower}\n" +
                      $" m_mass = {rb.mass}\n"
                      );
        }

        /// <summary>
        /// LocalPlayerCustomPropertiesを、データベースの変数で設定します。
        /// </summary>
        public void ResetLocalPlayerCustomProperties()
        {
            if (!thisPhotonView.IsMine) return;

            SetCustomPropertiesAttackPower(characterParameters.GetAttackPower());
            SetCustomPropertiesMass(characterParameters.GetCleanMass());

            Debug.Log($"<color=green> setLocalPlayerCustomProrerties </color>\n" +
                   $"<color=blue> info</color>\n" +
                   $" NickName : {PhotonNetwork.LocalPlayer.NickName} \n" +
                   $" {CharacterStatusKey.attackPowerKey} = {GetCustomPropertiesAttackPower()}\n"+
                   $" {CharacterStatusKey.massKey} = {GetCustomPropertiesMass()}\n"
                   );

            localPlayerCustomPropertiesStatusSetUpComplete = true;
            Debug.Log(" localPlayerCustomPropertiesStatusSetUpComplete = <color=blue>true</color>");
            localPlayerCustomPropertiesStatusSetUpCompleteAction();
            Debug.Log("<color=yellow> localPlayerCustomPropertiesStatusSetUpCompleteAction</color>()");
        }
        #endregion

        #region reset state
        /// <summary>
        /// Character Instans の状態を初期化します。
        /// </summary>
        public virtual void ResetCharacterInstanceState()
        {
            InitializeCameraSettings();
            InitializeEffectSettings();
        }

        /// <summary>
        /// Character Instans のカメラの方向を、初期化します。
        /// </summary>
        public void ResetCameraSettings()
        {
            InitializeCameraSettings();
        }

        /// <summary>
        /// Character Instans のエフェクトの状態を、初期化します。
        /// </summary>
        public void ResetEffectSettings()
        {
            InitializeEffectSettings();
        }

        #endregion
    }
}
