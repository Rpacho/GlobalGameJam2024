using System;
using UnityEngine;
using UnityEngine.Events;

namespace PoguScripts.functions
{
    public class HandCollusion : MonoBehaviour
    {
        public UnityEvent OnTrigger = new UnityEvent();
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Collide");
            OnTrigger.Invoke();
        }
    }
}