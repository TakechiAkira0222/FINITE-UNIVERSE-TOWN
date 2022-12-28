using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Takechi.CharacterController.Parameters
{
    public class PlayableCharacterParametersManager : MonoBehaviour
    {
        /// <summary>
        /// キャラクターのデータベース
        /// </summary>
        [SerializeField]
        private PlayableCharacterParametersDataBase m_parametersDataBase;

        /// <summary>
        /// キャラクターパラメーターを番号取得
        /// </summary>
        private Dictionary< int, PlayableCharacterParameters> m_numOfParameters =
            new Dictionary< int, PlayableCharacterParameters>();

        /// <summary>
        /// キャラクターパラメーターを番号取得
        /// </summary>
        private Dictionary< string ,PlayableCharacterParameters> m_nameOfParameters =
            new Dictionary< string, PlayableCharacterParameters>();

        void Awake()
        {
            setParameters();
        }

        private void setParameters()
        {
            for (int i = 0; i < m_parametersDataBase.GetParametersLists().Count; i++)
            {
                m_numOfParameters.Add( i, m_parametersDataBase.GetParametersLists()[i]);
                m_nameOfParameters.Add( m_parametersDataBase.GetParametersLists()[i].GetCharacterName(), m_parametersDataBase.GetParametersLists()[i]);

                Debug.Log($"{ m_numOfParameters[i].GetCharacterName()} : <color=blue>List to set.</color> ");
                Debug.Log($"{ m_nameOfParameters[ m_parametersDataBase.GetParametersLists()[i].GetCharacterName()].GetCharacterName()} : <color=blue>List to set.</color> ");
            }
        }

        public PlayableCharacterParameters GetParameters(string searchName) { return m_nameOfParameters[searchName]; }

        public PlayableCharacterParameters GetParameters(int searchNum) { return m_numOfParameters[searchNum]; }
    }
}