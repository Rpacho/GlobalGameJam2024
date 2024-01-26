using TMPro;
using UnityEngine;

namespace PoguScripts.functions
{
    public class Loading : MonoBehaviour
    {
        private float timeElapse = 0;
        public float duration = 0.01f;
        public TextMeshProUGUI text;
        private string[] loadingStrings = new[] { "Loading", "Loading.", "Loading..", "Loading..." }; // haha too lazy
        private int currentIndex = 0;
        private void Update()
        {
            timeElapse += Time.deltaTime;
            if (timeElapse < duration) return;
            text.text = loadingStrings[currentIndex];
            currentIndex++;
            if (currentIndex >= loadingStrings.Length) currentIndex = 0;
            timeElapse = 0f;
        }


    }
}