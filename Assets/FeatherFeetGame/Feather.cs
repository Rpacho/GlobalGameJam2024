using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour
{
    public enum featherEnum
    {
        idle,
        tickle,
        victory
    }
    public featherEnum state = featherEnum.idle;
    public bool correctTiming;
    public List<Sprite> sprites;
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        correctTiming = false;
        state = featherEnum.idle;
    }

    // Update is called once per frame
    void Update()
    {
        sr.sprite = sprites[(int)state];
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        correctTiming = true;
        Debug.Log("Entered correct timing frame");
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        correctTiming = false;
        Debug.Log("Exited correct timing frame");
    }
}
