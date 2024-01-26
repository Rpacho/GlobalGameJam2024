using PoguScripts.GlobalEvents;
using PoguScripts.UI.TimingBarUI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BananaTarget : MonoBehaviour
{
    public AudioClip _RunningClip;
    public AudioClip _Slip1;
    public AudioClip _Slip2;

    public AudioClip _HitClip;

    private Image mLeftRenderer;
    private Image mRightRenderer;
    private BananaMiniGame mGame;
    private bool isLeft = false;
    private bool _Running = false;
    private Vector2 _Dest = Vector2.zero;
    private Vector2 _Start = Vector2.zero;

    private Animator mAnimator;

    TimingBarView mTimingBarView;

    public void Initialize(TimingBarView view, BananaMiniGame game, Vector2 startPos, Vector2 destPos)
    {
        mTimingBarView = view;
        mGame = game;
        _Start = transform.localPosition = startPos;
        _Dest = destPos;
        _Running = false;
        isLeft = startPos.x > destPos.x;
        mLeftRenderer = transform.Find("Left").GetComponent<Image>();
        mRightRenderer = transform.Find("Right").GetComponent<Image>();
        mAnimator = GetComponent<Animator>();
        ChangeDirection();

        game.mGameStartAction += StartMovement;
    }

    public void StartMovement()
    {
        //_RunningClip
        _Running = true;
        mAnimator.SetBool("isWalk", _Running);
    }
    
    void FixedUpdate()
    {
        if(_Running)
        {
            transform.localPosition = new Vector3(Mathf.Lerp(_Start.x, _Dest.x, Mathf.PingPong(mTimingBarView.newArrowPosY * Time.deltaTime / 5.0f, 1800.0f)), 272, 0);

            if (transform.localPosition.x >= 700)
            {
                Debug.Log("Right");
                isLeft = false;
                ChangeDirection();
            }

            if (transform.localPosition.x <= -700)
            {
                Debug.Log("Left");
                isLeft = true;
                ChangeDirection();
            }
        }
    }

    private void ChangeDirection()
    {
        mLeftRenderer.transform.rotation = isLeft ? new Quaternion(0, 180, 0, 1.0f) : new Quaternion(0, 0, 0, 1.0f);
        mRightRenderer.transform.rotation = isLeft ? new Quaternion(0, 180, 0, 1.0f) : new Quaternion(0, 0, 0, 1.0f);
        gameObject.transform.Find("Left").localPosition = isLeft ? new Vector3(150, -150, 0) : new Vector3(-100, -150, 0);
        gameObject.transform.Find("Right").localPosition = isLeft ? new Vector3(200, -150, 0) : new Vector3(-150, -150, 0);
    }

    private void OnEnable()
    {
        GlobalEvent.OnHit.AddListener(Touched);
        GlobalEvent.OnMiss.AddListener(NotTouched);
    }

    private void Touched()
    {
        mGame.Touch(true);
        gameObject.transform.Find("Left").localPosition = new Vector3(gameObject.transform.Find("Left").localPosition.x, gameObject.transform.Find("Left").localPosition.y, 0);
        gameObject.transform.Find("Right").localPosition = new Vector3(gameObject.transform.Find("Right").localPosition.x, gameObject.transform.Find("Right").localPosition.y, 0);

        mAnimator.SetBool("isWalk", false);
        mAnimator.SetTrigger("Fall");
        _Running = false;
        // _HitClip
        // Stop Running Clip
    }


    private void NotTouched()
    {
        mGame.Touch(false);
    }

    private void OnDestroy()
    {
        GlobalEvent.OnHit.RemoveListener(Touched);
        GlobalEvent.OnMiss.RemoveListener(NotTouched);
    }
    private void OnDisable()
    {
        GlobalEvent.OnHit.RemoveListener(Touched);
        GlobalEvent.OnMiss.RemoveListener(NotTouched);
    }
}
