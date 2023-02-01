using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.CharacterController.Information
{
    [CreateAssetMenu(fileName = "PlayableCharacterInformationDataBase", menuName = "PlayableCharacterInformationDataBase")]
    public class PlayableCharacterInformationDataBase : ScriptableObject
    {
        [SerializeField]
        private List<PlayableCharacterInformation> m_parametersLists = new List<PlayableCharacterInformation>();

        private List<PlayableCharacterInformation> parametersLists => m_parametersLists;

        /// <summary>
        /// ParametersList‚ð•Ô‚·(get only)
        /// </summary>
        /// <returns></returns>
        public List<PlayableCharacterInformation> GetParametersLists() { return parametersLists; }
    }
}
