using System;
using UnityEngine;

namespace Assets.FantasyHeroes.Scripts
{
    /// <summary>
    /// Take a screnshoot in play mode [S]
    /// </summary>
    public class ScreenshotTransparent : MonoBehaviour
    {
        #if UNITY_EDITOR

        public int Width = 1920;
        public int Height = 1280;
        public string Directory = "Screenshots";

        public string GetName()
        {
            return string.Format("{0}/ScreenshotTransparent_{1}.png", Directory, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                var renderTexture = new RenderTexture(Width, Height, 24);
                var texture2D = new Texture2D(Width, Height, TextureFormat.ARGB32, false);

                GetComponent<Camera>().targetTexture = renderTexture;
                GetComponent<Camera>().Render();
                RenderTexture.active = renderTexture;
                texture2D.ReadPixels(new Rect(0, 0, Width, Height), 0, 0);
                GetComponent<Camera>().targetTexture = null;
                RenderTexture.active = null;
                Destroy(renderTexture);
                
                var bytes = texture2D.EncodeToPNG();
                var filename = GetName();

                System.IO.Directory.CreateDirectory(Directory);
                System.IO.File.WriteAllBytes(filename, bytes);
                Debug.Log(string.Format("Screenshot saved: {0}", filename));
            }
        }

        #endif
    }
}