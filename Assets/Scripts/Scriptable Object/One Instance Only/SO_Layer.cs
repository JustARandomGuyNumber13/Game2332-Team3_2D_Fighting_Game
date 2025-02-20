using UnityEngine;

[CreateAssetMenu(fileName = "SO_Layer", menuName = "One Instance Only/SO_Layer")]
public class SO_Layer : ScriptableObject
{
    /*
     *  This Scriptable Object should only has 1 instance
     */

    [SerializeField] private string groundLayerName;
    public LayerMask groundLayer;
    public int groundLayerIndex
    {
        get { return LayerMask.NameToLayer(groundLayerName); }
        private set { }
    }

    [SerializeField] private string playerLayerName;
    public LayerMask playerLayer;
    public int playerLayerIndex
    { 
        get { return LayerMask.NameToLayer(playerLayerName); }
        private set { }
    }

    [SerializeField] private string ghostLayerName;
    public LayerMask ghostLayer;
    public int ghostLayerIndex
    {
        get { return LayerMask.NameToLayer(ghostLayerName); }
        private set { }
    }
}
