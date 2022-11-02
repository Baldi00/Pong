using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallD : MonoBehaviour
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


        //Vector2 newPosition = transform.position + speed * Time.deltaTime * direction;
        //rigidbody2d.MovePosition(newPosition);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManager.Instance.PlayBoing();
        //string colliderName = collision.gameObject.name;
        //if (colliderName.Equals("ColliderUp") || colliderName.Equals("ColliderDown"))
        //{
        //    angle = -angle;
        //    SoundManager.Instance.PlayBoing();
        //}
        //else if (colliderName.Equals("LeftRacket"))
        //{
        //    RacketD racket = collision.gameObject.GetComponent<RacketD>();
        //    if (racket.MovingDirection == -1)
        //        angle = 315 + Random.Range(10f, 30f);
        //    else if (racket.MovingDirection == 1)
        //        angle = 45 - Random.Range(10f, 30f);
        //    else
        //        angle = 180 - angle + Random.Range(-5f, 5f);

        //    SoundManager.Instance.PlayBoing();
        //}
        //else if (colliderName.Equals("RightRacket"))
        //{
        //    RacketD racket = collision.gameObject.GetComponent<RacketD>();
        //    if (racket.MovingDirection == -1)
        //        angle = 225 - Random.Range(10f, 30f);
        //    else if (racket.MovingDirection == 1)
        //        angle = 135 + Random.Range(10f, 30f); 
        //    else
        //        angle = 180 - angle + Random.Range(-5f, 5f);

        //    SoundManager.Instance.PlayBoing();
        //}
        //else if (colliderName.Equals("ColliderLeft"))
        //{
        //    _isTouchingLeftCollider = true;
        //    enabled = false;
        //}
        //else if (colliderName.Equals("ColliderRight"))
        //{
        //    _isTouchingRightCollider = true;
        //    enabled = false;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("ColliderLeft"))
        {
            _isTouchingLeftCollider = true;
            enabled = false;
            rigidbody2d.simulated = false;
        }
        else if (collision.gameObject.name.Equals("ColliderRight"))
        {
            _isTouchingRightCollider = true;
            enabled = false;
            rigidbody2d.simulated = false;
        }
    }

    public void Reset()
    {
        transform.position = Vector2.zero;
        ResetTouchingColliders();
        float sideChoise = Random.Range(0f, 1f);
        if(sideChoise < 0.5f)
            angle = Random.Range(-30f, 30f);
        else
            angle = Random.Range(150f, 210f);
        enabled = true;
        rigidbody2d.simulated = true;

        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
        rigidbody2d.velocity = direction * speed;
    }

    public void ResetTouchingColliders()
    {
        _isTouchingLeftCollider = false;
        _isTouchingRightCollider = false;
    }

}
