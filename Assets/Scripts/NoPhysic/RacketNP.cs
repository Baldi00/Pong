using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketNP : MonoBehaviour
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
            if (currentPosition.y < GameManagerNP.UPPER_BOUND)
            {
                Vector2 newPosition = new Vector2(
                    currentPosition.x,
                    currentPosition.y + speed * Time.deltaTime);
                transform.position = newPosition;
                remainingMovingFrames = movingFramesTreshold;
                _movingDirection = 1;
            }
        }
        if (Input.GetKey(downButton))
        {
            Vector3 currentPosition = transform.position;
            if (currentPosition.y > GameManagerNP.LOWER_BOUND)
            {
                Vector2 newPosition = new Vector2(
                    currentPosition.x,
                    currentPosition.y - speed * Time.deltaTime);
                transform.position = newPosition;
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
