using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.CharacterController.Information
{
    public class PlayableCharacterInformationManager : MonoBehaviour
    {
        /// <summary>
        /// キャラクターのデータベース
        /// </summary>
        [SerializeField]
        private PlayableCharacterInformationDataBase m_informationDataBase;
        /// <summary>
        /// キャラクターパラメーターを番号取得
        /// </summary>
        private Dictionary< int, PlayableCharacterInformation> m_numOfInformation =
            new Dictionary< int, PlayableCharacterInformation>();
        /// <summary>
        /// キャラクターパラメーターを番号取得
        /// </summary>
        private Dictionary< string , PlayableCharacterInformation> m_nameOfInformation =
            new Dictionary< string,  PlayableCharacterInformation>();

        void Awake()
        {
            setParameters();
        }

        private void setParameters()
        {
            for (int i = 0; i < m_informationDataBase.GetParametersLists().Count; i++)
            {
                m_numOfInformation.Add( i, m_informationDataBase.GetParametersLists()[i]);
                m_nameOfInformation.Add(m_informationDataBase.GetParametersLists()[i].GetCharacterName(), m_informationDataBase.GetParametersLists()[i]);

                Debug.Log($"{m_numOfInformation[i].GetCharacterName()} : <color=blue>List to set.</color> ");
                Debug.Log($"{m_nameOfInformation[m_informationDataBase.GetParametersLists()[i].GetCharacterName()].GetCharacterName()} : <color=blue>List to set.</color> ");
            }
        }

        public PlayableCharacterInformation GetInformation(string searchName) { return m_nameOfInformation[searchName]; }
        public PlayableCharacterInformation GetInformation(int searchNum) { return m_numOfInformation[searchNum]; }
    }
}