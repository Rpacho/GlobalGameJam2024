using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WigCharacter;

public class WigFan : MonoBehaviour
{
    public enum FanState
    {
        idle,
        blowing
    }
    [SerializeField]
    private Sprite currentSprite;
    [SerializeField]
    private List<Sprite> spritesList;
    private SpriteRenderer sr;

    public FanState state;

    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        switch (state)
        {
            case FanState.idle:
                currentSprite = spritesList[(int)FanState.idle];
                break;
            case FanState.blowing:
                currentSprite = spritesList[(int)FanState.blowing];
                break;
            default:
                break;
        }
        sr.sprite = currentSprite;
    }
}
