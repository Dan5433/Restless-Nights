using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool rawInput = true;
    [SerializeField] float speed = 1f;
    [SerializeField] bool locked = false;
    [SerializeField] Rigidbody2D rigidBody;

    const string HORIZONTAL_AXIS = "Horizontal";
    const string VERTICAL_AXIS = "Vertical";

    public bool Locked { get { return locked; } set { locked = value; } }

    void FixedUpdate()
    {
        if (locked)
        {
            rigidBody.velocity = Vector3.zero;
            return;
        }

        float horizontalInput = rawInput
            ? Input.GetAxisRaw(HORIZONTAL_AXIS)
            : Input.GetAxis(HORIZONTAL_AXIS);
        float verticalInput = rawInput
            ? Input.GetAxisRaw(VERTICAL_AXIS)
            : Input.GetAxis(VERTICAL_AXIS);

        rigidBody.velocity = new Vector2(horizontalInput, verticalInput).normalized * speed;
    }
}
