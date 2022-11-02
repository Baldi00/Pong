using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketD : MonoBehaviour
{
    public KeyCode upButton;
    public KeyCode downButton;
    public float speed;

    public int movingFramesTreshold = 5;
    private int remainingMovingFrames = 0;
    private int _movingDirection;
    public int MovingDirection
    {
        get => _movingDirection;
    }

    private Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(remainingMovingFrames > 0)
        {
            remainingMovingFrames--;
            _movingDirection = 0;
        }

        if (Input.GetKey(upButton))
        {
            Vector3 currentPosition = transform.position;
            if (currentPosition.y < GameManager.RACKET_UPPER_BOUND)
            {
                Vector2 newPosition = new Vector2(
                    currentPosition.x,
                    currentPosition.y + speed * Time.deltaTime);
                rigidbody2d.MovePosition(newPosition);
                remainingMovingFrames = movingFramesTreshold;
                _movingDirection = 1;
            }
        }
        if (Input.GetKey(downButton))
        {
            Vector3 currentPosition = transform.position;
            if (currentPosition.y > GameManager.RACKET_LOWER_BOUND)
            {
                Vector2 newPosition = new Vector2(
                    currentPosition.x,
                    currentPosition.y - speed * Time.deltaTime);
                rigidbody2d.MovePosition(newPosition);
                remainingMovingFrames = movingFramesTreshold;
                _movingDirection = -1;
            }
        }
    }
    public bool IsMoving()
    {
        if(remainingMovingFrames <= 0)
            return false;
        else
            return true;
    }
}
