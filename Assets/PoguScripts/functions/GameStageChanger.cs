using System;
using PoguScripts.Enums;
using PoguScripts.Scriptable;
using UnityEngine;

namespace PoguScripts.functions
{
    public class GameStageChanger : MonoBehaviour
    {
        public GameData gameData;
        public GameStage gameStage;
        private void Awake()
        {
            gameData.CurrentGameStage = gameStage;
        }
    }
}