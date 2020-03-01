using UnityEngine;

public class ResetOnLoad : MonoBehaviour
{
    [SerializeField]
    private bool showInEditor = true;

    [SerializeField]
    private GameObject[] objectsToHide;

    void Awake()
    {
        #if UNITY_EDITOR
            if(showInEditor) return;
        #endif

        foreach(GameObject go in objectsToHide)
        {
            go.SetActive(false);
        }
    }
}