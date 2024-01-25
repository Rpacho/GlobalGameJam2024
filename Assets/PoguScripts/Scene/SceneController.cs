using System;
using System.Collections;
using System.Collections.Generic;
using PoguScripts.Enums;
using PoguScripts.Scriptable;
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

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2f);
            LoadRandomScene();
        }

        public void LoadRandomScene()
        {
            // make sure you don't load scene twice in a row
            GameStage prevSceneIndex = _gameData.CurrentGameStage;
            int nextSceneIndex = GetRandomSceneIndex();
            do
            {
                nextSceneIndex = GetRandomSceneIndex();
                Debug.Log(nextSceneIndex);
                Debug.Log(prevSceneIndex != sceneNames[nextSceneIndex].gameStage);
            } while (prevSceneIndex == sceneNames[nextSceneIndex].gameStage);

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
    }
}