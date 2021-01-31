using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamCat
{
    /// <summary>
    /// Gets a camera set up for generating render textures
    /// </summary>
    public class RTCameraManager : MonoBehaviour
    {
        const int CameraWidth = 256;
        const int CameraHeight = 256;
        const int CameraDepth = 8;
        private Camera cam = null;

        public event Action<Texture2D> OnTextureGenerated;

        // I know this isn't great for singletons lol, but should be okay for this purpose
        public static RTCameraManager Instance { get; private set; }
        private bool isCapturing = false;


        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;
            cam = GetComponent<Camera>();
        }


        public void SetupCameraLocation(Transform t)
        {
            cam.transform.parent = t;
            cam.transform.localPosition = Vector3.zero;
            cam.transform.localRotation = Quaternion.identity;
        }

        private void OnPostRender()
        {
            if (isCapturing)
            {
                isCapturing = false;

                var tex = cam.targetTexture;
                var output = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
                var rect = new Rect(0, 0, tex.width, tex.height);
                output.ReadPixels(rect, 0, 0);

                if(OnTextureGenerated != null)
                {
                    OnTextureGenerated(output);
                }
            }
        }

        public void TakeCapture()
        {
            cam.targetTexture = RenderTexture.GetTemporary(CameraWidth, CameraHeight, CameraDepth);
            isCapturing = true;
        }
    }
}
