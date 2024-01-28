using System;
using UnityEngine;
using UnityEngine.Video;

namespace PoguScripts.functions
{
    
    public class PlaYVideo : MonoBehaviour
    {
        public string videoFileName;

        private void Awake()
        {
            PlayVideo();
        }

        public void PlayVideo()
        {
            VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
            if (videoPlayer)
            {
                string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
                Debug.Log(videoPath);
                videoPlayer.url = videoPath;
                videoPlayer.Play();
            }
        }
    }
}