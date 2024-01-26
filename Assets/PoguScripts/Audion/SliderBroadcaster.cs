using PoguScripts.Scriptable;
using UnityEngine;

namespace PoguScripts.Audion
{
    public class SliderBroadcaster : MonoBehaviour
    {
        public GameData gameData;

        public void SetVolume(float volume)
        {
            gameData.Volume = volume;
        }
    }
}