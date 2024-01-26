using System;
using PoguScripts.Scene;
using PoguScripts.Scriptable;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PoguScripts.functions
{
    public class TimerLoadNextScene : MonoBehaviour
    {
        private float timeElapse = 0;
        public float duration = 3f;
        public GameData gameData;
        private SceneController _sceneController;
        public bool isEnable = true;
        private void Awake()
        {
            _sceneController = GetComponent<SceneController>();
        }

        private void Update()
        {
            if (isEnable == false) return;
            timeElapse += Time.deltaTime * gameData.GameSpeed;
            if (timeElapse < duration) return;
            LoadNextScene();
            timeElapse = 0f;
        }

        public void LoadNextScene()
        {
            if (gameData.Life <= 0)
            {
                SceneManager.LoadScene("GameOverScene");
                return;
            }
            _sceneController?.LoadRandomScene();
        }
    }
}