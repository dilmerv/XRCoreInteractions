using System.IO;
using System.Linq;
using DilmerGames.Core.Singletons;
using DilmerGames.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private GameObject imagePreviewContainer;

    [SerializeField]
    private RawImage imagePreviewTexture;

    [SerializeField]
    private TextMeshProUGUI previewLabel;

    [SerializeField]
    private RawImage largePreviewTexture;

    [SerializeField]
    private string defaultGallery = "Screenshots";

    [SerializeField]
    private string defaultScreenshotFileName = "Screenshot.png";

    private CaptureType lastCapturedType;

    private Texture2D lastCapturedImage;

    public Canvas Canvas
    {
        get { return canvas; }
    }

    public RawImage ImagePreviewTexture
    {
        get { return imagePreviewTexture; }
    }
    
    private void Awake() 
    {
        imagePreviewContainer.SetActive(false);    
    }

    public void LaunchPreview()
    {
        Logger.Instance.LogInfo("Launching Image Preview");
        previewLabel.text = lastCapturedType == CaptureType.Image ? "IMAGE PREVIEW" : "VIDEO PREVIEW";
        largePreviewTexture.texture = imagePreviewTexture.texture;
        UIPaneManager.Show("PreviewImage");

        if(lastCapturedType == CaptureType.Video)
        {
            UIPaneManager.Show("PreviewVideo");
        }
        else
        {
            UIPaneManager.Hide("PreviewVideo");
        }
    }

    public void PlayVideoPreview() => ReplayKitExtensionManager.Instance.PreviewVideo();

    public void SharePreview() 
    {
        if(lastCapturedType == CaptureType.Video)
        {
            ReplayKitExtensionManager.Instance.SharePreview();
        }
        else
        {
            string filePath = Path.Combine(Application.temporaryCachePath, defaultScreenshotFileName);
	        File.WriteAllBytes( filePath, lastCapturedImage.EncodeToPNG());
            new NativeShare().AddFile(filePath).Share();
        }
    }

    public void ShareTrigger() 
    {
        GameObject container = GameObject.Find("DetailContainer");

        Image activeTrigger = container.GetComponentInChildren<Image>();
        
        if(activeTrigger?.sprite?.texture != null)
        {
            string filePath = Path.Combine(Application.temporaryCachePath, $"{activeTrigger.gameObject.name}.png");
	        File.WriteAllBytes( filePath, activeTrigger.sprite.texture.EncodeToPNG());
            new NativeShare().AddFile(filePath).Share();
        }
    }

    public void TakeScreenshot() => StartCoroutine(ImageUtils.Instance
        .CaptureScreenForImage(canvas, defaultGallery, defaultScreenshotFileName, UpdateImagePreview));
    
    public void UpdateImagePreview(string error, Texture2D texture, CaptureType captureType)
    {
        lastCapturedImage = texture;

        if(string.IsNullOrEmpty(error))
        {
            imagePreviewContainer.SetActive(true);
            imagePreviewTexture.texture = texture;
            lastCapturedType = captureType;
        }
        else
        {
            imagePreviewContainer.SetActive(false);
            Logger.Instance.LogError(error);
        }
    }
}
