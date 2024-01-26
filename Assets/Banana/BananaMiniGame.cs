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

    public GameObject _Target;
    public GameObject _BananaObject;

    private BananaTarget _BananaTarget;

    [SerializeField]
    private Vector3 _TargetLeftOriginalPosition;
    [SerializeField]
    private Vector3 _TargetRightOriginalPosition;
    [SerializeField]
    private Vector3 _TargetLeftDestination;
    [SerializeField]
    private Vector3 _TargetRightDestination;

    private Vector3 _BananaOriginalPosition;

    [SerializeField]
    private Vector3 _BananaDestination;

    [SerializeField]
    private int _Score = 1000;

    [SerializeField]
    private AudioClip mHit;
    [SerializeField]
    private AudioClip mSuccess;
    [SerializeField]
    private AudioClip mFail;
    [SerializeField]
    private AudioClip mThrow1;  
    [SerializeField]
    private AudioClip mThrow2;
    private bool isShooted = false;
    private bool isLeft = false;
    private Animator mMonkeyAnimator;
    private WaitForSeconds _WaitSec = new WaitForSeconds(0.5f);

    public TimingBarView mView;

    public Canvas mCanvas;

    public event Action mGameStartAction;
    public static int mLevel = 0;
    private bool isTouched = false;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        StopCoroutine(ShootBanana());

        mCanvas = transform.parent.GetComponent<Canvas>();

        _BananaOriginalPosition = _BananaObject.transform.position;

        mState = BananaState.None;
        isShooted = false;
        mGameStartAction = null;   

        mView.gameObject.SetActive(false);
        mGameStartAction += GameStart;
        _BananaTarget = _Target.GetComponent<BananaTarget>();
        mMonkeyAnimator = transform.Find("Monkey").Find("Arm").GetComponent<Animator>();
        int rand = Random.Range(0, 100);
        isLeft = rand > 50;
        _BananaTarget.Initialize(mView,this, isLeft ? _TargetLeftOriginalPosition : _TargetRightOriginalPosition, isLeft ? _TargetLeftDestination : _TargetRightDestination);
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
                    mGameStartAction?.Invoke();
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
        mState = BananaState.Start;
        mView.gameObject.SetActive(true);
        Debug.Log("Start!");
    }

    private void Shoot()
    {
        isShooted = true;
        StartCoroutine(ShootBanana());
    }

    private IEnumerator ShootBanana()
    {
        mMonkeyAnimator.SetTrigger("Throw");
        PlayAudioThrowBanana();
        float increase = 1.1f;
        while (true)
        {
            _BananaObject.transform.position = Vector2.MoveTowards(_BananaObject.transform.position, mCanvas.transform.position + _BananaDestination, Time.deltaTime * 300.0f * increase);
            if (Vector2.Distance(_BananaObject.transform.position, mCanvas.transform.position + _BananaDestination) <= 0.1f)
            {
                break;
            }
            else
            {
                increase += 0.1f;
                yield return null;
            }
        }
        yield return null;
    }

    public void PlayAudioThrowBanana()
    {
        // Play Throw
    }

    public void Touch(bool touch)
    {
        isTouched = touch;
        if (touch)
        {
            // Play Monkey Laugh SFX
        }
        else
        {
            // Play Human Laugh Sfx
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
        if (isTouched)
        {
            GlobalEvent.OnChangedScore?.Invoke(_Score);
        }
        else
        {
            GlobalEvent.OnChangedLife?.Invoke(-1);
        }
        yield return _WaitSec;
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
