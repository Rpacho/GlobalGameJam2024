using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WigCharacter : MonoBehaviour
{
    public enum WigState
    {
        idle,
        blowing,
        blown
    }
    [SerializeField]
    private Sprite currentSprite;
    [SerializeField]
    private List<Sprite> spritesList;
    private SpriteRenderer sr;
    public WigState state;
    public Vector3 blowingPosAdjustment;
    public Vector3 blowingScaleAdjustment;
    public Vector3 blownScaleAdjustment;


    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        switch (state)
        {
            case WigState.idle:
                currentSprite = spritesList[(int)WigState.idle];
                transform.position = Vector3.zero;
                transform.localScale = Vector3.one;
                break;
            case WigState.blowing:
                currentSprite = spritesList[(int)WigState.blowing];
                transform.position = blowingPosAdjustment;
                transform.localScale = blowingScaleAdjustment;
                break;
            case WigState.blown:
                currentSprite = spritesList[(int)WigState.blown];
                transform.position = Vector3.zero;
                transform.localScale = blownScaleAdjustment;
                break;
            default:
                break;
        }
        sr.sprite = currentSprite;
    }
}
