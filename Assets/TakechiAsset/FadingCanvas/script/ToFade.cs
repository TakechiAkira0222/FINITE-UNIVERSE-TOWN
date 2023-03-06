
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

using System;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;
using UnityEngine.Rendering;

namespace Takechi.PlayableCharacter.FadingCanvas
{
    public class ToFade : MonoBehaviour
    {
        /// <summary>
        ///　キャンバス内のFadeIn時に状態が変化するtext
        /// </summary>
        [SerializeField] private List< UnityEngine.UI.Text>  m_changeOnFadeInCanvasTextList;
        /// <summary>
        ///　キャンバス内のFadeOut時に状態が変化するtext
        /// </summary>
        [SerializeField] private List< UnityEngine.UI.Text>  m_changeOnFadeOutCanvasTextList;
        /// <summary>
        ///　キャンバス内のFadeIn時に状態が変化するImage
        /// </summary>
        [SerializeField] private List<UnityEngine.UI.Image> m_changeOnFadeInCanvasImageList;
        /// <summary>
        ///　キャンバス内のFadeOut時に状態が変化するImage
        /// </summary>
        [SerializeField] private List<UnityEngine.UI.Image> m_changeOnFadeOutCanvasImageList;
        /// <summary>
        /// 変更内容を反映するText
        /// </summary>
        [SerializeField] private List<UnityEngine.UI.Text> m_outputTextList;
        /// <summary>
        /// フェイドスピード
        /// </summary>
        private float m_fadeSpeed = 0.01f;
        /// <summary>
        /// 表示継続時間
        /// </summary>
        private float m_fadeDisplayTime => NetworkSyncSettings.fadeProductionTime_Seconds;
        /// <summary>
        /// フェイドイン
        /// </summary>
        /// <param name="outputText">　変更内容のテキスト </param>
        public void OnFadeIn( string outputText)
        {
            StartCoroutine( nameof( StartFadeIn));

            ChangeDisplayTextContent( m_outputTextList, outputText);

            Debug.Log(" OnFadeIn : <color=green>start.</color>");
        }

        /// <summary>
        /// フェイドアウト
        /// </summary>
        /// <param name="outputText">　変更内容のテキスト </param>
        public void OnFadeOut( string outputText)
        {
            StartCoroutine( nameof( StartFadeOut));

            ChangeDisplayTextContent( m_outputTextList, outputText);

            Debug.Log(" OnFadeOut : <color=green>start.</color>");
        }

        /// <summary>
        /// アウト と フェイドイン
        /// </summary>
        /// <param name="outputText">　変更内容のテキスト </param>
        public void OnFadeInAndOut( string outputText)
        {
            StartCoroutine( nameof( StartFadeOutAndIn));

            ChangeDisplayTextContent( m_outputTextList, outputText);

            Debug.Log(" OnFadeInAndOut : <color=green>start.</color>");
        }

        //////////////////////////////////////
        /// IEnumerator //////////////////////
        //////////////////////////////////////
        private IEnumerator StartFadeOutAndIn()
        {
            StartCoroutine( nameof( StartFadeOut));

            yield return new WaitForSeconds( m_fadeDisplayTime);

            StartCoroutine( nameof( StartFadeIn));
        }

        private IEnumerator StartFadeOut()
        {
            ChangeDawingState( m_outputTextList, true);
            ChangeDawingState( m_changeOnFadeOutCanvasTextList, true);
            ChangeDawingState( m_changeOnFadeOutCanvasImageList, true);

            for (float alpha = 0; alpha <= 1; alpha += m_fadeSpeed)
            {
                foreach ( UnityEngine.UI.Text text in m_outputTextList)    { text.color = new Color( 1, 1, 1, alpha);}
                foreach ( UnityEngine.UI.Text text in m_changeOnFadeOutCanvasTextList)    { text.color = new Color( 1, 1, 1, alpha);}
                foreach ( UnityEngine.UI.Image image in m_changeOnFadeOutCanvasImageList) { image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);}

                yield return new WaitForSeconds( m_fadeDisplayTime / 2 * m_fadeSpeed);
            }
           
            Debug.Log(" OnFadeOut : <color=green>End.</color>");
        }

        private IEnumerator StartFadeIn()
        {
            ChangeDawingState(m_outputTextList, true);
            ChangeDawingState(m_changeOnFadeInCanvasTextList, true);
            ChangeDawingState(m_changeOnFadeInCanvasImageList, true);

            for ( float alpha = 1; alpha >= 0; alpha -= m_fadeSpeed)
            {
                foreach (UnityEngine.UI.Text  text in m_outputTextList)   { text.color = new Color(1, 1, 1, alpha); }
                foreach (UnityEngine.UI.Text  text in m_changeOnFadeInCanvasTextList)   { text.color = new Color(1, 1, 1, alpha); }
                foreach (UnityEngine.UI.Image image in m_changeOnFadeInCanvasImageList) { image.color = new Color(image.color.r, image.color.g, image.color.b, alpha); }

                yield return new WaitForSeconds( m_fadeDisplayTime / 2 * m_fadeSpeed);
            }

            ChangeDawingState(m_outputTextList, false);
            ChangeDawingState(m_changeOnFadeInCanvasTextList, false);
            ChangeDawingState(m_changeOnFadeInCanvasImageList, false);

            Debug.Log(" OnFadeIn : <color=green>End.</color>");
        }

        /// <summary>
        /// 表示テキスト内容の変更
        /// </summary>
        /// <param name="canvasTextList"> 変更したいテキスト </param>
        /// <param name="outputText">　変更内容のテキスト </param>
        private void ChangeDisplayTextContent(List<Text> canvasTextList, string outputText)
        {
            foreach (UnityEngine.UI.Text text in canvasTextList)
            {
                text.text = outputText;
                Debug.Log($"{text.text} => {outputText} <color=green>text change.</color>");
            }
        }

        /// <summary>
        ///　リスト内オブジェクトの描画状態の変更
        /// </summary>
        /// <typeparam name="T">　MonoBehaviour　</typeparam>
        /// <param name="lists"> object List</param>
        /// <param name="stateflag"> state flag </param>
        private void ChangeDawingState<T>(List<T> lists, bool stateflag) where T : MonoBehaviour
        {
            foreach (T t in lists) { t.gameObject.SetActive(stateflag); };
        }
    }
}