using UnityEngine;

[CreateAssetMenu(fileName = "SO_Layer", menuName = "Scriptable Objects/SO_Layer")]
public class SO_Layer : ScriptableObject
{
    /*
     *  This Scriptable Object should only has 1 instance
     */

    // Add layers as template:
    [SerializeField] private string _groundLayer;   // private, adjust by scriptable object only
    public int GroundLayer  // public, getter
    { get { return LayerMask.NameToLayer(_groundLayer); } }

    // Add more layers here:
}
