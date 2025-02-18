using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    /*public Rigidbody2D rb;

    public float moveSpeed;

    private Vector2 _moveDirection;

    public InputActionReference move;*/
    public InputActionReference fire;




    // Update is called once per frame
    /*void Update()
    {
       _moveDirection = move.action.ReadValue<Vector2>(); 
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);
    }*/

    //private void OnEnable()
    //{
    //    fire.action.started += Fire;
    //}

    /*private void OnDisable()
    {
        fire.action.started -= Fire;
    }*/

    //private void Fire(InputAction.CallbackContext obj)
    //{
    //    Debug.Log("Fired");
    //}


    private void OnChangeSelection(InputValue value)
    {
        print(value.Get<float>());
    }
}
