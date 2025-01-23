using UnityEngine;

[CreateAssetMenu(fileName = "SO_Layer", menuName = "Scriptable Objects/SO_Layer")]
public class SO_Layer : ScriptableObject
{
    [SerializeField] private string _groundLayer;
    public int GroundLayer
    {get { return LayerMask.NameToLayer(_groundLayer); }}
}
