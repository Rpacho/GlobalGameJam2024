using System;
using PoguScripts.Scene;
using PoguScripts.Scriptable;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PoguScripts.functions
{
    public class PlayCameraAndNextScene : MonoBehaviour
    {
        public SceneController sceneController;
        private Animator animator;
        public GameData gameData;
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayCamera()
        {
            if (gameData.Life <= 0)
            {
                SceneManager.LoadScene("GameOverScene");
                return;
            }
            animator.SetTrigger("Zoom");
        }

        public void NextScene()
        {
            sceneController.LoadRandomScene();
        }
    }
}