using System;
using PoguScripts.GlobalEvents;
using PoguScripts.Scriptable;
using UnityEngine;

namespace PoguScripts.UI.TimingBarUI
{
    public class TimingBarView : MonoBehaviour
    {
        public float arrowSpeed = 2f;
        public float rangeOfArrow = 0.01f;
        public bool spacePressed = false;
        [SerializeField]
        private GameData gameData;

        public GameObject arrow;
        public GameObject hitZone;
        
        public float rangeOfHitZone = 20f;
        public float positionYOfHitZone;
        private float maxValueBar;
        private float minValueBar = 0;
        public float newArrowPosY = 0;

        public bool startOnAwake = true;
        
        private void OnEnable()
        {
            GlobalEvent.OnClickedSpace.AddListener(OnClickedSpace);
            maxValueBar = GetComponent<RectTransform>().rect.height;
            
            SetHitZone(rangeOfHitZone, positionYOfHitZone);
            if (startOnAwake == false) spacePressed = true;
        }

        public void Initialize()
        {
            SetHitZone(rangeOfHitZone, positionYOfHitZone);
            
        }
        /// <summary>
        /// this include initialization. Will start the timer UI
        /// </summary>
        /// <param name="range"></param>
        /// <param name="posY"></param>
        public void SetHitZone(float range, float posY)
        {
            positionYOfHitZone = posY;
            CheckBoundaryOfHitZone();
            spacePressed = false;
            RectTransform rectTransformHitZone = hitZone.GetComponent<RectTransform>();
            rectTransformHitZone.sizeDelta = new Vector2(rectTransformHitZone.sizeDelta.x, range);
            rectTransformHitZone.transform.localPosition = new Vector3(rectTransformHitZone.transform.localPosition.x,
                -positionYOfHitZone, rectTransformHitZone.transform.localPosition.z);
        }

        private void CheckBoundaryOfHitZone()
        {
            if (positionYOfHitZone > maxValueBar)
            {
                positionYOfHitZone = maxValueBar;
            }
            if (positionYOfHitZone < 0)
            {
                positionYOfHitZone = minValueBar;
            }
        }

        private void OnDisable()
        {
            GlobalEvent.OnClickedSpace.RemoveListener(OnClickedSpace);
        }

        private void OnClickedSpace()
        {
            spacePressed = true;
            BroadcastHitZone();
        }

        private void BroadcastHitZone()
        {
            if (IsArrowWithinHitZone())
            {
                GlobalEvent.OnHit.Invoke();
                Debug.Log("On Hit");
            }
            else
            {
                GlobalEvent.OnMiss.Invoke();
                Debug.Log("On Miss");
            }
        }

        private bool IsArrowWithinHitZone()
        {
            return arrow.transform.localPosition.y <= -positionYOfHitZone &&
                   arrow.transform.localPosition.y >= -positionYOfHitZone + -rangeOfHitZone;
        }
        [Tooltip("Enable space input for testing the UI")]
        public bool IsDev = true;
        private void Update()
        {
            if (IsDev)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    OnClickedSpace();
                }
            }
            if (spacePressed == false)
            {
                MoveArrow();
            }
        }

        private void MoveArrow()
        {
            newArrowPosY = Mathf.PingPong(Time.time * arrowSpeed * gameData.GameSpeed, maxValueBar);
            arrow.transform.localPosition =
                new Vector3(arrow.transform.localPosition.x, -newArrowPosY, arrow.transform.localPosition.z);
        }
    }
}