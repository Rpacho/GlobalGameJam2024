using System;
using System.Collections.Generic;
using PoguScripts.GlobalEvents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace PoguScripts.UI.Buttons
{
    public class ButtonAnimation : MonoBehaviour
    {
        public Animator animatorButton;
        public List<AudioClip> clips;
        public AudioClip clickedClips;
        private Button button;
        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(PlayClickSFX);
        }

        private void PlayClickSFX()
        {
            PlayerManager.Instance.PlaySFX(clickedClips);
        }
        private void OnDestroy()
        {
            button.onClick.RemoveListener(PlayClickSFX);
        }

        public void OnHover()
        {
            animatorButton.SetBool("Hover", true);
            PlaySFX();
        }

        public void OnUnHover()
        {
            animatorButton.SetBool("Hover", false);
        }

        private void PlaySFX()
        {
            PlayerManager.Instance.PlaySFX(clips[Random.Range(0,clips.Count)]);
        }
    }
}