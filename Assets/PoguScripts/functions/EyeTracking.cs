using System;
using PoguScripts.Scriptable;
using UnityEngine;

namespace PoguScripts.functions
{
    public class EyeTracking : MonoBehaviour
    {
        public Transform leftPos;
        public Transform rightPos;

        public Transform HandPos;
        public bool enable = true;
        public GameData gameData;
        private float speed = 1f;
        private void Update()
        {
            if (HandPos != null)
            {
                
                Vector3 targetPosition = new Vector3(
                    Mathf.Clamp(HandPos.position.x, leftPos.position.x, rightPos.position.x),
                    Mathf.Clamp(HandPos.position.y, leftPos.position.y, rightPos.position.y),
                    transform.position.z
                );

                // Smoothly move the eye towards the target position
                transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime * gameData.GameSpeed);
            }
            else
            {
                Debug.LogWarning("Hand transform reference not set for eye tracking!");
            }
        }
    }
}