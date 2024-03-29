﻿using System;
using System.Collections.Generic;
using PoguScripts.Enums;
using PoguScripts.GlobalEvents;
using UnityEngine;

namespace PoguScripts.Scriptable
{
    [CreateAssetMenu(menuName = "Game Data", fileName = "GameData")]
    public class GameData : ScriptableObject
    {
        [SerializeField]private int lifePoints;
        [SerializeField]private int scorePoints;
        [SerializeField]private float gameSpeed;
        [SerializeField]private float volumeValue;
        [SerializeField]private GameStage currentGameState = GameStage.NONE;
        public List<GameProgressionData> gameProgressions = new List<GameProgressionData>();

        public int prevLifePoints = 3;
        public int prevScore = 0;


        public int Life
        {
            get => lifePoints;
            set
            {
                prevLifePoints = lifePoints;
                lifePoints = value;
                GlobalEvent.OnChangedLife.Invoke(lifePoints);
            } 
        }
        
        public int Score
        {
            get => scorePoints;
            set
            {
                prevScore = scorePoints;
                scorePoints = value;
                GlobalEvent.OnChangedScore.Invoke(scorePoints);
            } 
        }
        
        public float GameSpeed
        {
            get => gameSpeed;
            set
            {
                gameSpeed = value;
                GlobalEvent.OnChangedGameSpeed.Invoke(gameSpeed);
            }
        }
        
        public float Volume
        {
            get => volumeValue;
            set
            {
                volumeValue = value;
                GlobalEvent.OnChangedVolumeSettings.Invoke(volumeValue);
            }
        }
        
        public GameStage CurrentGameStage
        {
            get => currentGameState;
            set
            {
                currentGameState = value;
                GlobalEvent.OnChangeGameStage.Invoke(currentGameState);
            }
        }
    }
    [Serializable]
    public class GameProgressionData
    {
        public bool isComplete = false;
        public string message;
        public int scoreLimit = 0;
        public void Reset()
        {
            isComplete = false;
        }
    }
}