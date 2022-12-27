using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Takechi.PhotonVoiceController
{
    /// <summary>
    /// Photon Voice Controller instance ê∂ê¨
    /// </summary>
    public class GeneratePhotonVoiceControllerInstance : MonoBehaviour
    {
        [SerializeField] private GameObject m_instanceObject;

        void Awake()
        {
            SceneManager.sceneLoaded += SceneLoaded;
            setInstantiate();
        }

        void SceneLoaded(Scene nextScene, LoadSceneMode mode)
        {
            Debug.Log(nextScene.name);
            Debug.Log(mode);
        }

        void setInstantiate()
        {
            if (GameObject.Find( m_instanceObject.name + "(Clone)") != null)
                return;

            Instantiate(m_instanceObject, this.transform.position, Quaternion.identity);
        }
    }
}
