using UnityEngine;
using UnityEngine.Events;

public class ARInteraction : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnARInteractionSelected;

    [SerializeField]
    private UnityEvent OnARInteractionEnded;

    [SerializeField]
    private Material ActiveMaterial;

    [SerializeField]
    private Material InactiveMaterial;

    public bool IsSelected { get; private set; }

    void Awake() 
    {
        OnARInteractionSelected.AddListener(() =>
        {
            Logger.Instance.LogInfo("AR Interaction Started");
        });

        OnARInteractionEnded.AddListener(() =>
        {
            Logger.Instance.LogInfo("AR Interaction Ended");
        });
    }

    void Update() 
    {
        if (Input.touchCount > 0)
        {            
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;

            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.collider.CompareTag("ARInteraction"))
                    {
                        OnARInteractionSelected.Invoke();
                        GetComponent<MeshRenderer>().material = ActiveMaterial;
                        IsSelected = true;
                    }
                }
            }
            if(Input.GetTouch(0).phase == TouchPhase.Ended && IsSelected)
            {
                OnARInteractionEnded.Invoke();
                GetComponent<MeshRenderer>().material = InactiveMaterial;
                IsSelected = false;
            }
        }
    }
}
