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


        public int Life
        {
            get => lifePoints;
            set
            {
                lifePoints = value;
                GlobalEvent.OnChangedLife.Invoke(lifePoints);
            } 
        }
        
        public int Score
        {
            get => scorePoints;
            set
            {
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
}