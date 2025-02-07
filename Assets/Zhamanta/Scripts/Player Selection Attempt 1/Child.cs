using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using UnityEngine.InputSystem.Interactions;

public class Child : MonoBehaviour
{

    [SerializeField]
    private Transform originalParent;

    public Transform getOriginalParent() { return originalParent; }

}
