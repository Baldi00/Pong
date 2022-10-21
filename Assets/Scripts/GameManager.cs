using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public const float RACKET_UPPER_BOUND = 5.4f;
    public const float RACKET_LOWER_BOUND = -5.4f;
    public const float RACKET_LEFT_BOUND = -9.6f;
    public const float RACKET_RIGHT_BOUND = 9.6f;
    public const float RACKET_HALF_WIDTH = 0.075f;
    public const float RACKET_HALF_HEIGHT = 0.375f;
    public const float LEFT_RACKET_EDGE = -0.425f;
    public const float RIGHT_RACKET_EDGE = 0.425f;
    public const float BALL_HALF_WIDTH = 0.06f;
    public const float BALL_HALF_HEIGHT = 0.06f;

    public Ball ball;
    public Text leftPoints;
    public Text rightPoints;
    public GameObject info;

    private int leftPointsVal = 0;
    private int rightPointsVal = 0;

    private SpriteRenderer ballSpriteRenderer;

    void Awake()
    {
        ballSpriteRenderer = ball.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        StartCoroutine(WaitThenStartGame());
    }

    void Update()
    {
        if (ball.IsTouchingLeftCollider)
        {
            SoundManager.Instance.PlayPoint();
            rightPointsVal++;
            rightPoints.text = "" + rightPointsVal;
            ballSpriteRenderer.enabled = false;
            ball.ResetTouchingColliders();
            StartCoroutine(WaitThenResetBall());
        }
        if (ball.IsTouchingRightCollider)
        {
            SoundManager.Instance.PlayPoint();
            leftPointsVal++;
            leftPoints.text = "" + leftPointsVal;
            ballSpriteRenderer.enabled = false;
            ball.ResetTouchingColliders();
            StartCoroutine(WaitThenResetBall());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("KinematicPong");
        }
    }

    IEnumerator WaitThenResetBall()
    {
        yield return new WaitForSeconds(1);
        ballSpriteRenderer.enabled = true;
        ball.Reset();
    }

    IEnumerator WaitThenStartGame()
    {
        yield return new WaitForSeconds(2);
        info.SetActive(false);
        StartCoroutine(WaitThenResetBall());
    }
}
