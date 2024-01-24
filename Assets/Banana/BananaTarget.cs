using PoguScripts.GlobalEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaTarget : MonoBehaviour
{
    [SerializeField]
    private float mDistToSlide = 0.5f;

    private SpriteRenderer mRenderer;
    private BananaMiniGame mGame;
    private GameObject mBanana;
    private float _Speed = 0.0f;
    private bool _Running = false;
    private bool _isThrow = false;
    private Vector2 _Dest = Vector2.zero;
    private Animator mAnimator;

    public void Initialize(BananaMiniGame game, float speed, Vector2 startPos, Vector2 destPos)
    {
        mGame = game;
        mBanana = game._BananaObject;
        _Speed = speed;
        transform.position = startPos;
        _Dest = destPos;
        _isThrow = false;
        _Running = false;
        mRenderer = GetComponent<SpriteRenderer>();
        mRenderer.flipX = startPos.x > destPos.x;
        Debug.Log((mRenderer.flipX) ? "Left" : "Right");
        game.mGameStartAction += StartMovement;
    }

    public void StartMovement()
    {
        _Running = true;
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
            if (Vector2.Distance(_Dest, transform.position) < mDistToSlide && !_isThrow)
            {
                _Running = false;
                GlobalEvent.OnMiss?.Invoke();
                return;
            }
            else if(Vector2.Distance(mBanana.transform.position, transform.position) < mDistToSlide)
            {
                _Running = false;
                return;
            }
            else
                transform.position = Vector2.MoveTowards(transform.position, _Dest, Time.deltaTime * _Speed);
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
    private void OnApplicationQuit()
    {
        GlobalEvent.OnHit.RemoveListener(Touched);
        GlobalEvent.OnMiss.RemoveListener(NotTouched);
    }
}
