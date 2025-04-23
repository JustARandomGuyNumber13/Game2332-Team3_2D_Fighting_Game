using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private float spinSpeed;

    private void Update()
    {
        transform.eulerAngles += Vector3.forward * spinSpeed * Time.deltaTime;
    }
}
