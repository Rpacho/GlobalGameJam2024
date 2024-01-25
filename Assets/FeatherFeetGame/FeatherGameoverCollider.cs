using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherGameoverCollider : MonoBehaviour
{
    public bool gameOver;
    private void Start()
    {
        gameOver = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameOver = true;
        //collision.GetComponent<Feet>().rb.velocity = Vector3.zero;
        Debug.Log("Gameover!");
    }
}
