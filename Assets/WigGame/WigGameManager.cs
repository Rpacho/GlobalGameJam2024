using System.Collections;
using System.Collections.Generic;
using PoguScripts.GlobalEvents;
using PoguScripts.Scriptable;
using Unity.VisualScripting;
using UnityEngine;

public class WigGameManager : MonoBehaviour
{
    public GameData gameData;
    public WigFan fan;
    public WigCharacter character;
    public Wig wig;
    public int clickCount;
    public int maxClickCount;
    public float elapsedTime = 0.0f;
    public float timeLimit = 5;
    public float difficultyModifier = 1.0f;
    public int difficultyScoreBonusModifier = 1;
    [SerializeField]
    private float characterStateTimer;
    [SerializeField]
    private float characterStateResetTime;
    public bool failed = false;
    public bool success = false;

    public List<AudioClip> wigMoveClipList;
    public List<AudioClip> fanClipList;
    public AudioClip wigFallClip;
    public AudioClip cryingClip;
    public AudioClip panicClip;
    private void Start()
    {
        clickCount = 0;
        characterStateTimer = 0;
        elapsedTime = 0.0f;
        failed = false;
        success = false;
    }

    private void Update()
    {
        if (!success && !failed)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerManager.Instance.PlaySFX(fanClipList[Random.Range(0,fanClipList.Count)]);
                PlayerManager.Instance.PlaySFX(wigMoveClipList[Random.Range(0,wigMoveClipList.Count)]);
                clickCount++;
                if (clickCount >= maxClickCount && elapsedTime <= timeLimit)
                {
                    character.state = WigCharacter.WigState.blown;
                    wig.state = WigCharacter.WigState.blown;
                    GlobalEvent.OnHit.Invoke();
                    // AddScore(1 * difficultyScoreBonusModifier);
                    success = true;
                    Debug.Log("Success!");
                    PlayerManager.Instance.PlaySFX(cryingClip);
                    PlayerManager.Instance.PlaySFX(wigFallClip);
                }
                else
                {
                    character.state = WigCharacter.WigState.blowing;
                }
            }
            if (Input.GetKey(KeyCode.Space))
            {
                fan.state = WigFan.FanState.blowing;
                if (clickCount < maxClickCount)
                {
                    wig.state = WigCharacter.WigState.blowing;
                    
                }

                if (clickCount == 2)
                {
                    PlayerManager.Instance.PlaySFX(panicClip);
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                fan.state = WigFan.FanState.idle;
                if (character.state != WigCharacter.WigState.blown)
                {
                    character.state = WigCharacter.WigState.idle;
                }
            }
            ResetCharacterBlowningState();
            CalcualteTime();
        }
    }

    private void ResetCharacterBlowningState()
    {
        if(character.state == WigCharacter.WigState.blowing)
        {
            characterStateTimer += Time.deltaTime;
            if(characterStateTimer > characterStateResetTime)
            {
                character.state = WigCharacter.WigState.idle;
                wig.state = WigCharacter.WigState.idle;
            }
        }
        else
        {
            characterStateTimer = 0.0f;
        }
    }

    private void CalcualteTime()
    {
        if(success) { return; }
        if (!failed)
        {
            elapsedTime += gameData.GameSpeed * Time.deltaTime;
        }
        if(elapsedTime > timeLimit && !failed && !success)
        {
            GlobalEvent.OnMiss.Invoke();
            // LoseLife();
            failed = true;
            Debug.Log("Failed!");
        }
    }

    // private void AddScore(int score)
    // {
    //     gameData.Score += score;
    // }

    // private void LoseLife()
    // {
    //     gameData.Life -= 1;
    // }

    public float GetTimeRemaining()
    {
        return (timeLimit-elapsedTime)/gameData.GameSpeed;
    }
}
