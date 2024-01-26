using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PoguScripts.UI.Buttons
{
    public class ButtonAnimation : MonoBehaviour
    {
        public Animator animatorButton;
        public void OnHover()
        {
            animatorButton.SetBool("Hover", true);
            Debug.Log("Hover");
        }

        public void OnUnHover()
        {
            animatorButton.SetBool("Hover", false);
        }
    }
}