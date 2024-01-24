using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WigCharacter;

public class Wig : MonoBehaviour
{
    [SerializeField]
    private Sprite currentSprite;
    [SerializeField]
    private List<Sprite> spritesList;
    private SpriteRenderer sr;
    public WigState state;
    public Vector3 originalPos;
    public Vector3 blownOffPos;
    public Quaternion blownOffRotation;

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
                transform.position = originalPos;
                transform.rotation = Quaternion.identity;
                break;
            case WigState.blowing:
                //currentSprite = spritesList[(int)WigState.blowing];
                transform.position = originalPos;
                break;
            case WigState.blown:
                //currentSprite = spritesList[(int)WigState.blown];
                transform.position = blownOffPos;
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 45.089f);
                break;
            default:
                break;
        }
        sr.sprite = currentSprite;
    }
}
