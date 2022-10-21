using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float angle;
    public float speed;
    private bool _isTouchingLeftCollider = false;
    private bool _isTouchingRightCollider = false;

    public bool IsTouchingLeftCollider
    {
        get => _isTouchingLeftCollider;
    }
    public bool IsTouchingRightCollider
    {
        get => _isTouchingRightCollider;
    }

    private Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Reset();
    }

    void Update()
    {
        Vector3 direction = (Vector3)(Quaternion.Euler(0, 0, angle) * Vector3.right);
        Vector2 newPosition = transform.position + speed * Time.fixedDeltaTime * direction;
        rigidbody2d.MovePosition(newPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string colliderName = collision.gameObject.name;
        if (colliderName.Equals("ColliderUp") || colliderName.Equals("ColliderDown"))
        {
            angle = -angle;
            SoundManager.Instance.PlayBoing();
        }
        else if (colliderName.Equals("LeftRacket"))
        {
            Racket racket = collision.gameObject.GetComponent<Racket>();
            if (racket.MovingDirection == -1)
                angle = 315 + Random.Range(10f, 30f);
            else if (racket.MovingDirection == 1)
                angle = 45 - Random.Range(10f, 30f);
            else
                angle = 180 - angle + Random.Range(-5f, 5f);

            SoundManager.Instance.PlayBoing();
        }
        else if (colliderName.Equals("RightRacket"))
        {
            Racket racket = collision.gameObject.GetComponent<Racket>();
            if (racket.MovingDirection == -1)
                angle = 225 - Random.Range(10f, 30f);
            else if (racket.MovingDirection == 1)
                angle = 135 + Random.Range(10f, 30f); 
            else
                angle = 180 - angle + Random.Range(-5f, 5f);

            SoundManager.Instance.PlayBoing();
        }
        else if (colliderName.Equals("ColliderLeft"))
        {
            _isTouchingLeftCollider = true;
            enabled = false;
        }
        else if (colliderName.Equals("ColliderRight"))
        {
            _isTouchingRightCollider = true;
            enabled = false;
        }
    }

    public void Reset()
    {
        rigidbody2d.MovePosition(Vector2.zero);
        ResetTouchingColliders();
        float sideChoise = Random.Range(0f, 1f);
        if(sideChoise < 0.5f)
            angle = Random.Range(-30f, 30f);
        else
            angle = Random.Range(150f, 210f);
        enabled = true;
    }

    public void ResetTouchingColliders()
    {
        _isTouchingLeftCollider = false;
        _isTouchingRightCollider = false;
    }

}
