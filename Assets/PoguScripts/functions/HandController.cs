using System;
using System.Collections.Generic;
using PoguScripts.GlobalEvents;
using PoguScripts.Scriptable;
using UnityEngine;
using Random = UnityEngine.Random;

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
        public AudioSource source;
        public List<AudioClip> pokeHitList = new List<AudioClip>();
        public AudioClip pokeMiss;
        private void Start()
        {
            animator = GetComponent<Animator>();
            _handMover = GetComponent<HandMover>();
            source.Play();
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
            source.Stop();
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
            PlayerManager.Instance?.PlaySFX(pokeHitList[Random.Range(0, pokeHitList.Count)]);
        }

        public void SetFlichFace()
        {
            flincFace?.SetActive(true);
            PlayerManager.Instance?.PlaySFX(pokeMiss);
        }
    }
}