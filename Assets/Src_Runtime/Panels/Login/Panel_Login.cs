using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GJ {

    public class Panel_Login : MonoBehaviour, IPanelAsset {

        PanelType IPanelAsset.Type => PanelType.Login;

        [SerializeField] Button btn_start;
        [SerializeField] Button btn_exitGame;

        [SerializeField] CanvasGroup canvasGroup;
        float fadeOutDuration = 0.1f;

        public Action OnStartHandle;
        public Action OnExitGameHandle;

        public void Ctor() {
            btn_start.onClick.AddListener(() => {
                OnStartHandle?.Invoke();
            });

            btn_exitGame.onClick.AddListener(() => {
                OnExitGameHandle?.Invoke();
            }); 
        }

        public void Init() {
            if (canvasGroup != null) {
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
        }

        public void Close() {
            StopAllCoroutines();
            btn_start.onClick.RemoveAllListeners();
            // 使用协程进行淡出
            StartCoroutine(FadeOutAndDestroy());
        }

        IEnumerator FadeOutAndDestroy() {
            // 禁用交互
            if (canvasGroup != null) {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }

            // 淡出效果
            if (canvasGroup != null && fadeOutDuration > 0) {
                float startAlpha = canvasGroup.alpha;
                float timer = 0f;

                while (timer < fadeOutDuration) {
                    timer += Time.deltaTime;
                    float progress = timer / fadeOutDuration;
                    canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, progress);
                    yield return null;
                }
            }

            // 销毁对象
            GameObject.Destroy(gameObject);
        }

    }

}