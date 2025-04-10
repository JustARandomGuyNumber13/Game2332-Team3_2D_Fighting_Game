using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] protected Vector2 spawnZoneMin;
    [SerializeField] protected Vector2 spawnZoneMax;
    public bool IsAvailable { get; private set; }

    public virtual void Activate()
    {
        IsAvailable = false;
        gameObject.SetActive(true);
        transform.position = GetRandomPos();
        TrapBehavior();
    }
    protected Vector2 GetRandomPos()
    { 
        return Vector2.right * Random.Range(spawnZoneMin.x, spawnZoneMax.x) + Vector2.up * Random.Range(spawnZoneMin.y, spawnZoneMax.y);
    }

    protected virtual void Deactivate() 
    {
        gameObject.SetActive(false);
        IsAvailable = true;
    }
    protected virtual void TrapBehavior()
    { 
    
    }
}
