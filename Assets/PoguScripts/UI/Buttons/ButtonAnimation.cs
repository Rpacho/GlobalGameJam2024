using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace PoguScripts.UI.Buttons
{
    public class ButtonAnimation : MonoBehaviour
    {
        public Animator animatorButton;
        public List<AudioClip> clips;
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