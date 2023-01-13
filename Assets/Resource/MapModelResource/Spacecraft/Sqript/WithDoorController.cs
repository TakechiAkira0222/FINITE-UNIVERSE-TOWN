using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace Takechi.MapObject.AnimatorController
{
    public class WithDoorController : MonoBehaviour
    {
        #region SerialzeField
        [SerializeField] private Animator m_animator;
        [SerializeField] private bool     m_isOperation = true; 
        [SerializeField] private float    m_openingTime = 3;
        #endregion

        private Action OnOpenAction = delegate { };
        private Action OnCloseAction = delegate { };

        #region UnityEvent
        void Reset()
        {
            m_isOperation = true;
            m_openingTime = 3;
            m_animator = this.transform.parent.gameObject.GetComponent<Animator>();
        }

        void Awake()
        {
            OnOpenAction += OnOpen;
            OnOpenAction += setClose;

            OnCloseAction+= OnClose;
        }

        void Update()
        {
            if (!m_isOperation) return;

            OnJudgmentByDistance();
        }

        void OnCollisionEnter(Collision collision)
        {
            if (!m_isOperation) return;

            OnCloseAction();
        }

        void OnCollisionExit(Collision collision)
        {
           
        }

        #endregion

        void OnClose()
        {
            m_animator.SetBool("character_nearby", false);
        }

        void OnOpen()
        {
            m_animator.SetBool("character_nearby", true);
        }

        void setClose()
        {
            Invoke(nameof(OnClose), m_openingTime);
        }

        void OnJudgmentByDistance()
        {
            if (Vector3.Distance(this.gameObject.transform.position, Camera.main.transform.position) <= 2) { OnOpenAction(); }
        }
    }
}
