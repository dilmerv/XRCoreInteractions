using DilmerGames.Core.Singletons;
// using VoxelBusters.ReplayKit;

// To use this manager you must purchase ReplayKit which is available in the asset store
// and it provides functionality for recording native videos for iOS and Android.
// Uncomment the body of each method once you get the asset.
// Asset Available At:
// https://assetstore.unity.com/packages/tools/integration/cross-platform-replay-kit-record-every-play-133662?aid=1101l7LXo
public class ReplayKitExtensionManager : Singleton<ReplayKitExtensionManager>
{
    void Start()
    {
        /* ReplayKitManager.Initialise();

        IsAvailable();
        */
    }

    public bool IsAvailable()
    {
        /*
        bool isRecordingAPIAvailable = ReplayKitManager.IsRecordingAPIAvailable();

        string message = isRecordingAPIAvailable ? "Replay Kit recording API is available!" : "Replay Kit recording API is not available.";

        Logger.Instance.LogInfo(message);

        return isRecordingAPIAvailable;
        */
        return false;
    }

    void OnEnable()  
    { 
        /*
        ReplayKitManager.DidInitialise += DidInitialise;
        ReplayKitManager.DidRecordingStateChange += DidRecordingStateChange;
        */
    }

    void OnDisable() 
    { 
        /*
        ReplayKitManager.DidInitialise -= DidInitialise;
        ReplayKitManager.DidRecordingStateChange -= DidRecordingStateChange;
        */
    }

    /*
    private void DidInitialise(ReplayKitInitialisationState state, string message)  
    {  
        Logger.Instance.LogInfo("Received Event Callback : DidInitialise [State:"  +  state.ToString()  +  " "  +  "Message:"  +  message);

        switch (state)
        {  
            case ReplayKitInitialisationState.Success:
                Logger.Instance.LogInfo("ReplayKitManager.DidInitialise : Initialisation Success");  
                break;
            case ReplayKitInitialisationState.Failed:
                Logger.Instance.LogInfo("ReplayKitManager.DidInitialise : Initialisation Failed with message["+message+"]");  
                break;
            default:  
                Logger.Instance.LogInfo("Unknown State");
                break;
        }
    }

    private void DidRecordingStateChange(ReplayKitRecordingState state, string message)  
    {  
        Logger.Instance.LogInfo("Received Event Callback : DidInitialise [State:"  +  state.ToString()  +  " "  +  "Message:"  +  message);

        switch (state)
        {  
            case ReplayKitRecordingState.Started:
                Logger.Instance.LogInfo("ReplayKitManager.DidInitiDidRecordingStateChangealise : Started");  
                break;
            case ReplayKitRecordingState.Stopped:
                Logger.Instance.LogInfo("ReplayKitManager.DidInitiDidRecordingStateChangealise : Stopped");  
                break;
            case ReplayKitRecordingState.Available:
                
                Logger.Instance.LogInfo("ReplayKitManager.DidInitiDidRecordingStateChangealise : Avaiable");  
                
                SavePreview();

                StartCoroutine(ImageUtils.Instance
                    .CaptureScreenForVideo(UIManager.Instance.Canvas, UIManager.Instance.UpdateImagePreview));

                break;
            case ReplayKitRecordingState.Failed:  
                Logger.Instance.LogInfo("ReplayKitManager.DidInitiDidRecordingStateChangealise : Failed");  
                break;
            default:
                Logger.Instance.LogInfo("Unknown State");
                break;
        }
    }

    */
    public void StartRecording()
    {
        /*
        if(!ReplayKitManager.IsRecording())
        {
            ReplayKitManager.StartRecording(false);
        }
        else 
        {
            Logger.Instance.LogInfo("Video is already recording");  
        }
        */
    }

    public void StopRecording()
    {
        //ReplayKitManager.StopRecording();
    }

    public bool PreviewVideo()
    {
        /*
        if(ReplayKitManager.IsPreviewAvailable())
        {
            Logger.Instance.LogInfo("Vide Preview available");
            return ReplayKitManager.Preview();    
        }
        
        Logger.Instance.LogInfo("Vide Preview not available");
        // Still preview is not available. Make sure you call preview after you receive ReplayKitRecordingState.Available status from DidRecordingStateChange
        */
        return false;
    }

    public void SharePreview() 
    {
        /*
        ReplayKitManager.SharePreview();
        */
    }

    public void SavePreview()
    {
        /*
        if(ReplayKitManager.IsPreviewAvailable())
        {
            ReplayKitManager.SavePreview((error) =>
            {
                Logger.Instance.LogInfo("Saved preview to gallery with error : " + ((error == null) ? "null" : error));
            });
        }
        else
        {
            Logger.Instance.LogInfo("Recorded file not yet available. Please wait for ReplayKitRecordingState.Available status");
        }
        */
    }
}
