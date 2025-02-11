using UnityEngine;

[CreateAssetMenu(fileName = "SO_Layer", menuName = "One Instance Only/SO_Layer")]
public class SO_Layer : ScriptableObject
{
    /*
     *  This Scriptable Object should only has 1 instance
     */

    public LayerMask groundLayer;
    public LayerMask playerLayer;
}
