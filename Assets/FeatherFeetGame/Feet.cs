using System.Collections;
using System.Collections.Generic;
using PoguScripts.Scriptable;
using UnityEditorInternal;
using UnityEngine;


public class Feet : MonoBehaviour
{
    public enum FeetState
    {
        idle,
        tickle
    }
    public GameData gameData;
    public GameObject feetpos;
    public GameObject antpos;
    public float feetMaxSpeed;
    public float accelSpeed;
    private Vector2 feetAccel;
    private Vector2 maxSpeedVector;
    public Rigidbody2D rb;
    public FeetState state;
    public CooldownTimer feetTimer;
    public SpriteRenderer sr;
    public List<Sprite> sprites;
    private int successCount = 1;

    private void Start()
    {
        feetTimer.OnReady.AddListener(OnFeetReady);

        maxSpeedVector = antpos.transform.position - feetpos.transform.position;
        maxSpeedVector.Normalize();
        feetAccel = maxSpeedVector * accelSpeed * gameData.GameSpeed; //Set acceleration
        maxSpeedVector = maxSpeedVector * feetMaxSpeed * gameData.GameSpeed;
        rb.velocity = maxSpeedVector;
    }
    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < feetMaxSpeed)
        {
            rb.velocity += feetAccel * Time.deltaTime;
        }
        /*
        if (rb.velocity.magnitude > feetMaxSpeed)
        {
            rb.velocity = maxSpeedVector;
        }
        */
    }
    void Update()
    {
        sr.sprite = sprites[(int)state];
    }

    public void AddKnockback(Vector3 force)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(maxSpeedVector.normalized * force.x * successCount);
        successCount++;
    }

    public void OnFeetReady()
    {
        state = FeetState.idle;
    }
}
