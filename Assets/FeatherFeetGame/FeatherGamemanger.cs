using PoguScripts.GlobalEvents;
using PoguScripts.Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherGamemanger : MonoBehaviour
{
    public GameData gameData;
    public Feather feather;
    public GameObject shadow;
    public CooldownTimer tickleCooldown;
    public Feet feet;
    public FeatherGameoverCollider gameoverCollider;
    public FeatherWinCollider winCollider;
    public GameObject lose;
    public Vector3 knockbackForce;
    private bool failed;
    private bool success;

    public AudioClip featherClip;

    public AudioClip squishClip;

    public List<AudioClip> stomp;
    // Start is called before the first frame update
    void Start()
    {
        failed = false;
        success = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!failed || !success)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GlobalEvent.OnClickedSpace.Invoke();
                PlayerManager.Instance?.PlaySFX(featherClip);
                if (tickleCooldown.cooldownReady)
                {
                    if (feather.correctTiming && !gameoverCollider.gameOver && !success && !failed) //Hit!
                    {
                        feet.state = Feet.FeetState.tickle;
                        feet.feetTimer.ResetTimer();
                        feet.AddKnockback(knockbackForce * gameData.GameSpeed);
                    }
                    if ((gameoverCollider.gameOver || !feather.correctTiming) && (!success && !failed)) //Tickle missed, apply cooldown
                    {
                        tickleCooldown.ResetTimer();
                    }
                    if (Input.GetKey(KeyCode.Space))
                    {
                        feather.state = Feather.featherEnum.tickle;
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                feather.state = Feather.featherEnum.idle;
            }
            if(winCollider.win && !success && !failed)
            {
                GlobalEvent.OnHit.Invoke();
                success = true;
                PlayVictoryScene();
            }
            if (gameoverCollider.gameOver && !success && !failed)
            {
                GlobalEvent.OnMiss.Invoke();
                failed = true;
                PlayDeathScene();
            }
        }
    }

    public void PlayDeathScene()
    {
        PlayerManager.Instance?.PlaySFX(stomp[Random.Range(0,stomp.Count)]);
        PlayerManager.Instance?.PlaySFX(squishClip);
        feather.gameObject.SetActive(false);
        feet.gameObject.SetActive(false);
        shadow.SetActive(false);
        lose.SetActive(true);
    }

    public void PlayVictoryScene()
    {
        feet.gameObject.SetActive(false);
        feather.state = Feather.featherEnum.victory;
    }

    
}
