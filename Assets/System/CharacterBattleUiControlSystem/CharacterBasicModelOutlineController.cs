using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;

namespace Takechi.NetworkInstantiation.OutlineController
{
    [RequireComponent(typeof(CharacterAddressManagement))]
    [RequireComponent(typeof(CharacterStatusManagement))]
    public class CharacterBasicModelOutlineController : MonoBehaviour
    {
        #region SerializeField 
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        #endregion

        #region private variable
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        #endregion

        public struct MyColor
        {
            public Color ATeamColor => new Color( 1, 0, 0, 0.1f);
            public Color BTeamColor => new Color( 0, 0, 1, 0.1f);
        }

        private MyColor m_myColor;

        private void Reset()
        {
            m_characterAddressManagement = this.transform.GetComponent<CharacterAddressManagement>();
            m_characterStatusManagement = this.transform.GetComponent<CharacterStatusManagement>();
        }

        private void Start()
        {
            int number = myPhotonView.ControllerActorNr;

            string team =
                    (string)PhotonNetwork.LocalPlayer.Get(number).CustomProperties[CharacterStatusKey.teamKey];

            if (team == CharacterTeamStatusName.teamAName) { m_characterStatusManagement.SetModelOulineColor(m_myColor.ATeamColor); }
            else { m_characterStatusManagement.SetModelOulineColor(m_myColor.BTeamColor); }
        }
    }
}
