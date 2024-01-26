using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetShadow : MonoBehaviour
{
    public GameObject feet;
    public GameObject antPos;
    public Vector2 shadowPositionOffset;
    public float shadowScaleOffset;
    private Vector2 xVector;
    private float scaleShadow;
    private float distToAnt;

    private void Start()
    {
        distToAnt = Vector2.Distance(antPos.transform.position, feet.transform.position);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        xVector.x = feet.transform.position.x + shadowPositionOffset.x;
        xVector.y = transform.position.y;
        transform.position = xVector;

        scaleShadow = (distToAnt - Vector2.Distance(antPos.transform.position, feet.transform.position)) / distToAnt;
        scaleShadow *= shadowScaleOffset;
        if (scaleShadow < 0.0f)
        {
            scaleShadow = 0.0f;
        }
        else if(scaleShadow > 1.0f * shadowScaleOffset)
        {
            scaleShadow = 1.0f * shadowScaleOffset;
        }
        transform.localScale = Vector2.one * scaleShadow;
    }
}
