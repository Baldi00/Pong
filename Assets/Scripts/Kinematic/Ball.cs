using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float angle;
    public float speed;
    private bool _isTouchingLeftCollider = false;
    private bool _isTouchingRightCollider = false;
    private Vector3 direction;

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
        direction = (Vector3)(Quaternion.Euler(0, 0, angle) * Vector3.right);
        Vector2 newPosition = transform.position + speed * Time.deltaTime * direction;
        rigidbody2d.MovePosition(newPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 outDirection = Vector3.Reflect(direction, collision.contacts[0].normal);
        float outAngle = Vector3.SignedAngle(Vector3.right, outDirection, Vector3.forward);

        string colliderName = collision.gameObject.name;
        switch (colliderName)
        {
            case "ColliderUp":
            case "ColliderDown":
            case "LeftRacket":
            case "RightRacket":
                angle = outAngle;
                SoundManager.Instance.PlayBoing();
                break;
            case "ColliderLeft":
                _isTouchingLeftCollider = true;
                enabled = false;
                break;
            case "ColliderRight":
                _isTouchingRightCollider = true;
                enabled = false;
                break;
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
