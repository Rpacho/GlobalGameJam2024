using System;
using PoguScripts.GlobalEvents;
using PoguScripts.Scriptable;
using UnityEngine;

namespace PoguScripts.functions
{
    public class HandController : MonoBehaviour
    {
        private HandMover _handMover;
        private Animator animator;
        public GameData gameData;
        public Timer timer;
        private bool isHit;

        public GameObject angryFace;
        public GameObject flincFace;
        private void Start()
        {
            animator = GetComponent<Animator>();
            _handMover = GetComponent<HandMover>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Space");
                StartNosePicking();
            }
        }

        private void StartNosePicking()
        {
            _handMover.enable = false;
            PlayAnimation();
            timer.Pause();
        }

        private void PlayAnimation()
        {
            animator?.SetTrigger("Pick");
        }

        private void CheckResult()
        {
            if (isHit)
            {
                GlobalEvent.OnHit.Invoke();
                flincFace?.SetActive(false);
                angryFace?.SetActive(true);
            }
            else
            {
                GlobalEvent.OnMiss.Invoke();
            }
        }

        public void SetHit()
        {
            isHit = true;
        }

        public void SetFlichFace()
        {
            flincFace?.SetActive(true);
        }
    }
}