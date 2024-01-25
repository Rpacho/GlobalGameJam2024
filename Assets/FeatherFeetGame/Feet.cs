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
    public Vector2 feetMaxSpeed;
    public Vector2 feetAccel;
    public Vector2 currentSpeed;
    public Rigidbody2D rb;
    public FeetState state;
    public CooldownTimer feetTimer;
    public SpriteRenderer sr;
    public List<Sprite> sprites;
    private int successCount = 1;

    private void Start()
    {
        feetTimer.OnReady.AddListener(OnFeetReady);
        rb.velocity = feetMaxSpeed * gameData.GameSpeed;
        feetAccel = feetAccel * gameData.GameSpeed;
    }
    private void FixedUpdate()
    {
        if (rb.velocity.x < feetMaxSpeed.x)
        {
            rb.velocity += feetAccel * Time.deltaTime;
        }
        if (rb.velocity.x > feetMaxSpeed.x)
        {
            rb.velocity = feetMaxSpeed;
        }
    }
    void Update()
    {
        sr.sprite = sprites[(int)state];
    }

    public void AddKnockback(Vector3 force)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(force * successCount);
        successCount++;
    }

    public void OnFeetReady()
    {
        state = FeetState.idle;
    }
}
