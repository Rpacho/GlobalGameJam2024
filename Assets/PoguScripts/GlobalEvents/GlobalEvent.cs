using PoguScripts.Enums;
using UnityEngine.Events;

namespace PoguScripts.GlobalEvents
{
    public class GlobalEvent
    {
        public static UnityEvent OnClickedSpace = new UnityEvent();
        public static UnityEvent OnHit = new UnityEvent();
        public static UnityEvent OnMiss = new UnityEvent();
        public static UnityEvent<int> OnChangedLife = new UnityEvent<int>();
        public static UnityEvent<int> OnChangedScore = new UnityEvent<int>();
        public static UnityEvent<float> OnChangedGameSpeed = new UnityEvent<float>();
        public static UnityEvent<float> OnChangedVolumeSettings = new UnityEvent<float>();
        public static UnityEvent<GameStage> OnChangeGameStage = new UnityEvent<GameStage>();
    }
}