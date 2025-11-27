using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GJ {

    public class Panel_Curtain : MonoBehaviour, IPanelAsset {

        PanelType IPanelAsset.Type => PanelType.Curtain;

        [SerializeField] Button btn_reStart;
        [SerializeField] Button btn_exitGame;

        [SerializeField] CanvasGroup canvasGroup;
        public Action OnStartHandle;
        public Action OnExitGameHandle;

        public void Ctor() {

            btn_reStart.gameObject.SetActive(false);
            btn_reStart.onClick.AddListener(() => {
                OnStartHandle?.Invoke();
            });

            btn_exitGame.gameObject.SetActive(false);
            btn_exitGame.onClick.AddListener(() => {
                OnExitGameHandle?.Invoke();
            });
        }

        public void Init() {

        }

        public void Close() {
            StopAllCoroutines();
            btn_reStart.onClick.RemoveAllListeners();
            GameObject.Destroy(gameObject);
        }

    }

}