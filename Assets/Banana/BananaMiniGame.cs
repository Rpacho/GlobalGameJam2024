using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BananaMiniGame : MonoBehaviour
{
    private enum BananaState
    {
        None,
        Initialize,
        Start,
        End
    }

    private BananaState mState = BananaState.None;

    private float _BeginWait = 3.0f;
    private float _Wait = 0.0f;

    [SerializeField]
    private float _MaximumTime = 5.0f;
    private float _Time = 0.0f;

    [SerializeField]
    private float _TargetSpeed = 3.0f;

    [SerializeField]
    private float _BananaSpeed = 4.0f;

    public GameObject _Target;
    public GameObject _BananaObject;

    private BananaTarget _BananaTarget;

    [SerializeField]
    private Vector2 _TargetLeftOriginalPosition;
    [SerializeField]
    private Vector2 _TargetRightOriginalPosition;
    [SerializeField]
    private Vector2 _TargetLeftDestination;
    [SerializeField]
    private Vector2 _TargetRightDestination;

    [SerializeField]
    private Vector2 _BananaOriginalPosition;

    [SerializeField]
    private Vector2 _BananaDestination;

    [SerializeField]
    private AudioClip mSuccess;    
    [SerializeField]
    private AudioClip mThrow;
    private bool isShooted = false;
    private bool isWin = false;
    private bool isLeft = false;
    private Animator mMonkeyAnimator;

    public Canvas mCanvas;
    private Text mText;

    public event Action mGameStartAction;
    
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        mState = BananaState.None;
        isShooted = isWin = false;
        mGameStartAction = null;

        _Wait = _BeginWait;

        mGameStartAction += GameStart;
        _BananaObject.transform.position = _BananaOriginalPosition;
        _BananaTarget = _Target.GetComponent<BananaTarget>();
        mMonkeyAnimator = transform.Find("Monkey").GetComponent<Animator>();
        mText = mCanvas.transform.Find("Text").GetComponent<Text>();
        int rand = Random.Range(0, 100);
        isLeft = rand > 50;
        _BananaTarget.Initialize(this, _TargetSpeed, rand > 50 ? _TargetLeftOriginalPosition :
            _TargetRightOriginalPosition, rand > 50 ? _TargetLeftDestination : _TargetRightDestination);
        mState = BananaState.Initialize;
    }

    void Update()
    {
        switch (mState)
        {
            case BananaState.None:
                break;
            case BananaState.Initialize:
                {
                    if (_Wait > 0.0f)
                    {
                        _Wait -= Time.deltaTime;
                        mText.text = Mathf.RoundToInt(_Wait).ToString();
                        if (_Wait <= 0.0f)
                            mGameStartAction?.Invoke();
                        return;
                    }
                }
                break;
            case BananaState.Start:
                {
                    if (!_BananaTarget.IsRunning())
                    {
                        GameEnd();
                        return;
                    }
                    if (Input.GetKeyDown(KeyCode.Space) && !isShooted)
                    {
                        //Shooted
                        isShooted = true;
                        StartCoroutine(ShootBanana());
                    }
                }
                break;
            case BananaState.End:
                break;
            default:
                break;
        }
    }

    public void GameStart()
    {
        mText.gameObject.SetActive(false);
        _Time = _MaximumTime;
        mState = BananaState.Start;
        Debug.Log("Start!");
    }

    public void GameEnd()
    {
        mState = BananaState.End;
        mText.gameObject.SetActive(true);
        if (isWin)
        {
            mText.text = "Win!";
            Debug.Log("Win");
            // Win
        }
        else
        {
            mText.text = "Lose!";
            Debug.Log("Lose");
            // Lose Life
        }
    }

    private IEnumerator ShootBanana()
    {
        PlayAudioThrowBanana();
        while (true)
        {
            _BananaObject.transform.position = Vector2.MoveTowards(_BananaObject.transform.position, _BananaDestination, Time.deltaTime * _BananaSpeed);
            if(Vector2.Distance(_BananaObject.transform.position, _BananaDestination) < 0.5f)
            {
                _BananaTarget.Call();
                break;
            }
            else
                yield return null;
        }
        yield return null;
    }

    public void PlayAudioThrowBanana()
    {
        //Throw
    }

    public void Touch(bool touch)
    {
        isWin = touch;
        if(isWin)
        {
            // Play SFX
        }
        GameEnd();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(_BananaOriginalPosition, new Vector3(1,1,1));
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(_BananaDestination, new Vector3(1,1,1));

        Gizmos.color = Color.red;
        Gizmos.DrawCube(_TargetLeftDestination, new Vector3(1, 1, 1));
        Gizmos.color = Color.red;
        Gizmos.DrawCube(_TargetRightDestination, new Vector3(1, 1, 1));

        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(_TargetLeftOriginalPosition, new Vector3(1, 1, 1));
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(_TargetRightOriginalPosition, new Vector3(1, 1, 1));

    }
}
