using System;
using UnityEngine;
using UnityEngine.UI;

namespace PoguScripts.functions
{
    public class StartButton : MonoBehaviour
    {
        private Button button;

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(StartGame);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(StartGame);
        }

        private void StartGame()
        {
            PlayerManager.Instance?.LoadResultScene();
        }
    }
}