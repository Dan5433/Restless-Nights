using EditorAttributes;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool rawInput = true;
    [SerializeField] float speed = 1f;
    [SerializeField] bool locked = false;
    [SerializeField] Rigidbody2D rigidBody;

    [SerializeField][DisableInEditMode, DisableInPlayMode] Direction facingDirection;

    const string HORIZONTAL_AXIS = "Horizontal";
    const string VERTICAL_AXIS = "Vertical";

    public bool Locked { get { return locked; } set { locked = value; } }
    public Direction FacingDirection => facingDirection;

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

        UpdateFacingDirection(verticalInput, horizontalInput);
    }

    void UpdateFacingDirection(float verticalInput, float horizontalInput)
    {
        if (horizontalInput > 0)
            facingDirection = Direction.Right;
        if (horizontalInput < 0)
            facingDirection = Direction.Left;

        //check vertical last to prioritize vertical direction
        if (verticalInput > 0)
            facingDirection = Direction.Up;
        if (verticalInput < 0)
            facingDirection = Direction.Down;
    }

    [Serializable]
    public enum Direction
    {
        Up = 0,
        Left = 1,
        Down = 2,
        Right = 3,
    }
}
