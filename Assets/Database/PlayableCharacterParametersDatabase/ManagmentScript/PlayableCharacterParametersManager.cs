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
        private Dictionary<PlayableCharacterParameters, int> m_numOfParameters =
            new Dictionary<PlayableCharacterParameters, int>();

        /// <summary>
        /// キャラクターパラメーターを番号取得
        /// </summary>
        private Dictionary<PlayableCharacterParameters, string> m_nameOfParameters =
            new Dictionary<PlayableCharacterParameters, string>();

        [SerializeField] private string m_characterName = "";
        [NonSerialized]  public PlayableCharacterParameters CharacterNameOfParameters;

        void Awake()
        {
            SetParameters( m_characterName);
        }

        void Start()
        {
            for (int i = 0; i < m_parametersDataBase.GetParametersLists().Count; i++)
            {
                m_numOfParameters.Add(m_parametersDataBase.GetParametersLists()[i], i);
                m_nameOfParameters.Add(m_parametersDataBase.GetParametersLists()[i], m_parametersDataBase.GetParametersLists()[i].GetCharacterName());

                Debug.Log(m_parametersDataBase.GetParametersLists()[i].GetCharacterName() + ": " + m_parametersDataBase.GetParametersLists()[i].GetInformation());
            }

            Debug.Log(GetParameters(this.gameObject.name).GetAttackPower());
        }

        private void SetParameters(string searchName)
        {
            CharacterNameOfParameters = m_parametersDataBase.GetParametersLists().Find(parameters => parameters.GetCharacterName() == searchName);
            Debug.Log($"<color=green> set CharacterParameters : {searchName}");
        }

        private PlayableCharacterParameters GetParameters(string searchName)
        {
            return m_parametersDataBase.GetParametersLists().Find(parameters => parameters.GetCharacterName() == searchName);
        }
    }
}