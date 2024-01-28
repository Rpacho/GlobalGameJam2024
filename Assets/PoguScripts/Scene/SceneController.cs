using System;
using System.Collections;
using System.Collections.Generic;
using PoguScripts.Enums;
using PoguScripts.Scriptable;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace PoguScripts.Scene
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField]
        private List<SceneData> sceneNames;

        [SerializeField] private GameData _gameData;
        public bool loadSceneOnStart = false;
        private int nextSceneIndex = 0;
        public TextMeshProUGUI textSceneName;
        public List<string> gameName;
        public SpriteRenderer renderer;
        private IEnumerator Start()
        {
            GameStage prevSceneIndex = _gameData.CurrentGameStage;
            nextSceneIndex = GetRandomSceneIndex();
            do
            {
                nextSceneIndex = GetRandomSceneIndex();
                Debug.Log(nextSceneIndex);
                Debug.Log(prevSceneIndex != sceneNames[nextSceneIndex].gameStage);
            } while (prevSceneIndex == sceneNames[nextSceneIndex].gameStage);

            renderer.sprite = sceneNames[nextSceneIndex].sprite;
            textSceneName.text = sceneNames[nextSceneIndex].gameName;
            yield return new WaitForSeconds(2f);
            if(loadSceneOnStart)
                LoadRandomScene();
        }

        public void LoadRandomScene()
        {
            // make sure you don't load scene twice in a row
            _gameData.CurrentGameStage = sceneNames[nextSceneIndex].gameStage;
            SceneManager.LoadScene(sceneNames[nextSceneIndex].sceneName);
        }

        private int GetRandomSceneIndex()
        {
            return Random.Range(0, sceneNames.Count);
        }
    }
    [Serializable]
    public class SceneData
    {
        public GameStage gameStage;
        public string sceneName;
        public string gameName;
        public Sprite sprite;
    }
}