using System;
using PoguScripts.Scriptable;
using UnityEngine;

namespace PoguScripts.functions
{
    public class HandMover : MonoBehaviour
    {
        public Transform leftDestination;
        public Transform rightDestination;

        public float speed;
        public GameData gameData;
        private float speedTime = 0f;
        public float limit = 100;
        private Transform destination;
        public bool enable = true;
        private void Awake()
        {
            destination = leftDestination;
            transform.position = new Vector3(destination.position.x, destination.position.y, destination.position.z);
        }

        private void Update()
        {
            if (enable == false) return;
            if (destination == leftDestination)
            {
                speedTime += speed * Time.deltaTime * gameData.GameSpeed;
                transform.position =
                    new Vector3(
                        Mathf.Lerp(leftDestination.position.x, rightDestination.position.x,
                            speedTime), transform.position.y, transform.position.z);
                if (transform.position.x >= rightDestination.position.x)
                {
                    destination = rightDestination;
                }
                
            }
            
            if (destination == rightDestination)
            {
                speedTime -= speed * Time.deltaTime * gameData.GameSpeed;
                transform.position =
                    new Vector3(
                        Mathf.Lerp(leftDestination.position.x, rightDestination.position.x,
                            speedTime), transform.position.y, transform.position.z);
                if (transform.position.x <= leftDestination.position.x)
                {
                    destination = leftDestination;
                }
            }
            
        }
    }
}