using PoguScripts.GlobalEvents;
using PoguScripts.Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    public GameData gameData;
    public List<Sprite> sprites;
    public SpriteRenderer sr;
    public bool won = false;

    public void Start()
    {
        GlobalEvent.OnHit.AddListener(OnTimeHit);
        GlobalEvent.OnMiss.AddListener(OnTimeMiss);
        sr.sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GlobalEvent.OnClickedSpace.Invoke();
        }
    }

    public void OnTimeHit()
    {
        sr.sprite = sprites[1];
        if(won) { return; }
        gameData.Score += 1;
        won = true;
    }

    public void OnTimeMiss()
    {
        sr.sprite = sprites[2];
        gameData.Life -= 1;
    }
}
