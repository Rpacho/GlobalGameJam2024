using PoguScripts.GlobalEvents;
using PoguScripts.UI.TimingBarUI;
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
    private float _BananaSpeed = 10.0f;

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
    private int _Score = 1000;

    [SerializeField]
    private AudioClip mSuccess;    
    [SerializeField]
    private AudioClip mThrow;
    private bool isShooted = false;
    private bool isLeft = false;
    private Animator mMonkeyAnimator;
    private SpriteRenderer mMonkeySprite;
    private WaitForSeconds _WaitSec = new WaitForSeconds(1);

    public TimingBarView mView;

    public Canvas mCanvas;
    private Text mText;

    public event Action mGameStartAction;
    public static int mLevel = 0;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        StopCoroutine(ShootBanana());
        mState = BananaState.None;
        isShooted = false;
        mGameStartAction = null;   

        _Wait = _BeginWait;

        mView.gameObject.SetActive(false);
        mGameStartAction += GameStart;
        _BananaObject.transform.position = _BananaOriginalPosition;
        _BananaTarget = _Target.GetComponent<BananaTarget>();
        mMonkeyAnimator = transform.Find("Monkey").GetComponent<Animator>();
        mMonkeySprite = transform.Find("Monkey").GetComponent<SpriteRenderer>();
        mText = mCanvas.transform.Find("Text").GetComponent<Text>();
        int rand = Random.Range(0, 100);
        isLeft = rand > 50;
        _BananaTarget.Initialize(this, _TargetSpeed, isLeft ? _TargetLeftOriginalPosition :
            _TargetRightOriginalPosition, isLeft ? _TargetLeftDestination : _TargetRightDestination);
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
                    if(Input.GetKeyDown(KeyCode.Space) && !isShooted)
                    {
                        GlobalEvent.OnClickedSpace?.Invoke();
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
        mView.gameObject.SetActive(true);
        GlobalEvent.OnChangedGameSpeed?.Invoke(mLevel++);
        mText.gameObject.SetActive(false);
        _Time = _MaximumTime;
        mState = BananaState.Start;
        Debug.Log("Start!");
    }

    private void Shoot()
    {
        isShooted = true;
        StartCoroutine(ShootBanana());
    }

    private IEnumerator ShootBanana()
    {
        PlayAudioThrowBanana();
        while (true)
        {
            _BananaObject.transform.position = Vector2.MoveTowards(_BananaObject.transform.position, _BananaDestination, Time.deltaTime * _BananaSpeed);
            if (Vector2.Distance(_BananaObject.transform.position, _BananaDestination) <= 0.5f)
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
        if(touch)
        {
            // Play SFX
        }
    }

    public void Condition()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return _WaitSec;
        mState = BananaState.End;
        mText.gameObject.SetActive(true);
        float dist = Vector2.Distance(_BananaObject.transform.position, _Target.transform.position);
        Debug.Log(dist);
        if (dist < 4.0f)
        {
            mText.text = "Win!";
            GlobalEvent.OnChangedScore?.Invoke(_Score);
        }
        else
        {
            mText.text = "Lose!";
            GlobalEvent.OnChangedLife?.Invoke(-1);
        }
        yield return _WaitSec;
        Initialize();
    }

    private void OnEnable()
    {
        GlobalEvent.OnClickedSpace.AddListener(Shoot);
        GlobalEvent.OnHit.AddListener(Condition);
        GlobalEvent.OnMiss.AddListener(Condition);
    }

    private void OnDestroy()
    {
        GlobalEvent.OnClickedSpace.RemoveListener(Shoot);
        GlobalEvent.OnHit.RemoveListener(Condition);
        GlobalEvent.OnMiss.RemoveListener(Condition);
    }
    private void OnDisable()
    {
        GlobalEvent.OnClickedSpace.RemoveListener(Shoot);
        GlobalEvent.OnHit.RemoveListener(Condition);
        GlobalEvent.OnMiss.RemoveListener(Condition);
    }
    private void OnApplicationQuit()
    {
        GlobalEvent.OnClickedSpace.RemoveListener(Shoot);
        GlobalEvent.OnHit.RemoveListener(Condition);
        GlobalEvent.OnMiss.RemoveListener(Condition);
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
