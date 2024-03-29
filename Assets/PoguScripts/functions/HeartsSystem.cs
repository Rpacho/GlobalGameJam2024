﻿using System;
using System.Collections.Generic;
using PoguScripts.Scriptable;
using UnityEngine;

namespace PoguScripts.functions
{
    public class HeartsSystem : MonoBehaviour
    {
        public GameData gameData;
        private bool hasLostLife = false;
        public List<Animator> HeartList; 
        private void Awake()
        {
            if (gameData.prevLifePoints > gameData.Life)
            {
                hasLostLife = true;
            }

            for (int i = 0; i < gameData.prevLifePoints; i++)
            {
                HeartList[i].gameObject.SetActive(true);
            }

            if (hasLostLife)
            {
                HeartList[gameData.prevLifePoints - 1].SetTrigger("Hide");
                gameData.prevLifePoints = gameData.Life;
            }
        }
        
        
    }
}