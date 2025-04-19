using UnityEngine;

public class Skill_Set_Up : MonoBehaviour
{
    private void Start()
    {
        transform.SetParent(transform.parent.parent);
        transform.localScale = Vector3.one;
    }
}
