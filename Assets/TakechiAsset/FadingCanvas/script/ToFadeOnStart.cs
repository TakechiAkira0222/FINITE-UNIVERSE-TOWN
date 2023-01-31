using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.PlayableCharacter.FadingCanvas
{
    public class ToFadeOnStart : MonoBehaviour
    {
        [SerializeField] private ToFade m_toFade;

        private ToFade toFade => m_toFade;

        void Start()
        {
            m_toFade.OnFadeIn("");
        }
    }
}
