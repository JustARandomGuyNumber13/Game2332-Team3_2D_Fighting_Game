using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using UnityEngine.InputSystem.Interactions;

public class OriginalSlot : MonoBehaviour
{

    [SerializeField]
    private Transform originalParent;

    public Transform getOriginalParent() {  return originalParent; }

    Vector3 originalPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void resetPosition()
    {
        transform.localPosition = originalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
