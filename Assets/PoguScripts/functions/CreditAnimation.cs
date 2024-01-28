using System;
using UnityEngine;

namespace PoguScripts.functions
{
    public class CreditAnimation : MonoBehaviour
    {
        private Animator animator;
        private bool isCreditsOpen = false;
        public bool isRunOnAwake = false;
        private void Start()
        {
            animator = GetComponent<Animator>();
            if(isRunOnAwake)SetCredit();
        }

        public void SetCredit()
        {
            isCreditsOpen = !isCreditsOpen;
            animator?.SetBool("CreditsShow", isCreditsOpen);
        }
        
        
    }
}