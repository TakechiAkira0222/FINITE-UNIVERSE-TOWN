using Photon.Voice.PUN.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.CharacterController.Parameters
{
    [CreateAssetMenu(fileName = "PlayableCharacterParametersDataBase", menuName = "PlayableCharacterData/PlayableCharacterParametersDataBase")]
    public class PlayableCharacterParametersDataBase : ScriptableObject
    {
        [SerializeField]
        private List<PlayableCharacterParameters> m_parametersLists = new List<PlayableCharacterParameters>();

        private List<PlayableCharacterParameters> parametersLists => m_parametersLists;

        /// <summary>
        /// ParametersList‚ð•Ô‚·(get only)
        /// </summary>
        /// <returns></returns>
        public List<PlayableCharacterParameters> GetParametersLists() { return parametersLists; }
    }
}
