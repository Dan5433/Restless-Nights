using EditorAttributes;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool rawInput = true;
    [SerializeField] float speed = 1f;
    [SerializeField] bool locked = false;
    [SerializeField] Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;

    [SerializeField][DisableInEditMode, DisableInPlayMode] Direction facingDirection;

    const string HORIZONTAL_AXIS = "Horizontal";
    const string VERTICAL_AXIS = "Vertical";

    public bool Locked { get { return locked; } set { locked = value; } }
    public Direction FacingDirection => facingDirection;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
        UpdateSprite();
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

    void UpdateSprite()
    {
        switch (facingDirection)
        {
            case Direction.Up:
                spriteRenderer.flipY = false;
                transform.rotation = Quaternion.identity;
                break;
            case Direction.Left:
                spriteRenderer.flipY = false;
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case Direction.Down:
                spriteRenderer.flipY = true;
                transform.rotation = Quaternion.identity;
                break;
            case Direction.Right:
                spriteRenderer.flipY = true;
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }
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
