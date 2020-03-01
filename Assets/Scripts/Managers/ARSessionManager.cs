using DilmerGames.Core.Singletons;
using DilmerGames.Managers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARSessionManager : Singleton<ARSessionManager>
{
    [SerializeField]
    private Button closePlacementButton;

    [SerializeField]
    private Button exitSessionButton;

    [SerializeField]
    private ARPlacementInteractableSingle arPlacement;

    [SerializeField]
    private ARPlaneManager arPlaneManager;

    [SerializeField]
    private ARTrackedImageManager arTrackedImageManager;

    private void Awake() 
    {
        arTrackedImageManager.enabled = false;
        arPlaneManager.enabled = false;
    }

    private void OnEnable() 
    {
        arTrackedImageManager.trackedImagesChanged += TrackedImagesChanged;
    }
    
    private void OnDisable() 
    {
        arTrackedImageManager.trackedImagesChanged -= TrackedImagesChanged;
    }

    private void TrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        UIPaneManager.Hide("ExitPlacementButton");
        UIPaneManager.Show("ClosePlacementButton");
            
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            // app state -> image tracking state set
            ApplicationState.Instance.CurrentState = State.ImageTracking;

            // deactivates planes but doesn't destroy existing placement
            DeactivatePlaneDetection(destroyPlacement: true);

            Logger.Instance.LogInfo("TrackedImagee Added");

            UpdateObjectTracker(trackedImage);
        }
       
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            // app state -> image tracking state set
            ApplicationState.Instance.CurrentState = State.ImageTracking;

            Logger.Instance.LogInfo("TrackedImagee Updated");
            UpdateObjectTracker(trackedImage);
        }
    }

    private void UpdateObjectTracker(ARTrackedImage trackedImage)
    {
        arPlacement.PlacementObject.transform.position = trackedImage.transform.position;
        arPlacement.PlacementObject.transform.rotation = trackedImage.transform.rotation;
    }

    public void ActivatePlaneDetection()
    {
        // app state -> plane detection state set
        ApplicationState.Instance.CurrentState = State.PlaneDetection;

        arTrackedImageManager.enabled = true;

        ChangePlaneState(true);

        arPlaneManager.detectionMode = PlaneDetectionMode.Horizontal;
        arPlaneManager.enabled = true;
        
        UIPaneManager.Hide("ClosePlacementButton");
    }

    public void ClosePlacement()
    {
        UIPaneManager.Hide("ClosePlacementButton");
        UIPaneManager.Show("ExitPlacementButton");

        arPlacement.DestroyPlacement();

        ActivatePlaneDetection();
    }

    public void DeactivatePlaneDetection(bool destroyPlacement = true)
    {
        // app state -> idle state set
        ApplicationState.Instance.CurrentState = State.Idle;

        ChangePlaneState(false);

        if(destroyPlacement)
        {   
            Logger.Instance.LogInfo("DeactivatePlaneDetection Destroying placement");
            arPlacement.DestroyPlacement();
        }

        arPlaneManager.enabled = false;
    }

    public void DeactivateImageTracking(bool state)
    {
        arTrackedImageManager.enabled = !state;
    }

    public void ChangePlaneState(bool state)
    {
        foreach(ARPlane plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(state);
        }
    }
}
