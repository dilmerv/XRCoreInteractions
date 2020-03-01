using System.Collections;
using UnityEngine;
using DilmerGames.Core.Singletons;
using System;

public class ImageUtils : Singleton<ImageUtils>
{
    public IEnumerator CaptureScreenForImage(Canvas ignoredCanvas, string defaultGallery, string defaultScreenshotFileName, Action<string, Texture2D, CaptureType> callback = null)
    {
        ignoredCanvas.enabled = false;

        yield return new WaitForEndOfFrame();

        Texture2D screenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshotTexture.Apply();

        // Save the screenshot to Gallery/Photos
        Debug.Log( "Permission result: " + NativeGallery.SaveImageToGallery(screenshotTexture, defaultGallery, defaultScreenshotFileName, (error) =>
        {
            if(callback == null)
                return;

            callback.Invoke(error, screenshotTexture, CaptureType.Image);
        }));
        
        ignoredCanvas.enabled = true;
    }

    public IEnumerator CaptureScreenForVideo(Canvas ignoredCanvas, Action<string, Texture2D, CaptureType> callback = null)
    {
        ignoredCanvas.enabled = false;

        yield return new WaitForEndOfFrame();

        Texture2D screenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshotTexture.Apply();

        callback.Invoke(string.Empty, screenshotTexture, CaptureType.Video);

        ignoredCanvas.enabled = true;
    }
}