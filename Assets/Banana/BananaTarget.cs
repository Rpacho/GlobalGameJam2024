using PoguScripts.GlobalEvents;
using PoguScripts.UI.TimingBarUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BananaTarget : MonoBehaviour
{
    [SerializeField]
    private float mDistToSlide = 0.5f;

    private Image mLeftRenderer;
    private Image mRightRenderer;
    private BananaMiniGame mGame;
    private GameObject mBanana;
    private float _Speed = 0.0f;
    private bool _Running = false;
    private bool _isThrow = false;
    private bool _isFailed = false;
    private Vector2 _Dest = Vector2.zero;
    private Vector2 _Start = Vector2.zero;

    private Animator mAnimator;

    TimingBarView mTimingBarView;

    public void Initialize(TimingBarView view, BananaMiniGame game, float speed, Vector2 startPos, Vector2 destPos)
    {
        mTimingBarView = view;
        mGame = game;
        mBanana = game._BananaObject;
        _Speed = startPos.x > destPos.x ? -speed : speed;
        _Start = startPos;
        transform.position = startPos;
        _Dest = destPos;
        _isThrow = false;
        _Running = false;
        _isFailed = false;
        gameObject.transform.Find("Left").localPosition = startPos.x > destPos.x ? new Vector3(-250, -150, 0) : new Vector3(150, -150, 0);
        gameObject.transform.Find("Right").localPosition = startPos.x > destPos.x ? new Vector3(-150, -150, 0) : new Vector3(250, -150, 0);
        mLeftRenderer = transform.Find("Left").GetComponent<Image>();
        mRightRenderer = transform.Find("Right").GetComponent<Image>();
        mAnimator = GetComponent<Animator>();

        mLeftRenderer.transform.rotation = startPos.x > destPos.x ? new Quaternion(0,0,0,1.0f) : new Quaternion(0, 180, 0, 1.0f);
        mRightRenderer.transform.rotation = startPos.x > destPos.x ? new Quaternion(0,0,0,1.0f) : new Quaternion(0, 180, 0, 1.0f);
        Debug.Log(startPos.x > destPos.x ? "Left" : "Right");
        game.mGameStartAction += StartMovement;
    }

    public void StartMovement()
    {
        _Running = true;
        mAnimator.SetBool("isWalk", _Running);
    }

    public void Call()
    {
        Debug.Log("Call");
        _isThrow = true;
    }

    void FixedUpdate()
    {
        if(_Running)
        {
            transform.localPosition = new Vector3(Mathf.Lerp(_Start.x, _Dest.x, Time.deltaTime * mTimingBarView.newArrowPosY),0,0);
        }
    }

    public bool IsRunning()
    {
        return _Running;
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
    }


    private void NotTouched()
    {
        _isFailed = true;
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
