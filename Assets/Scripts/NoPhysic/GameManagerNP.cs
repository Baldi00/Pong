using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerNP : MonoBehaviour
{
    public const float UPPER_BOUND = 5.4f;
    public const float LOWER_BOUND = -5.4f;
    public const float LEFT_BOUND = -9.6f;
    public const float RIGHT_BOUND = 9.6f;
    public const float RACKET_HALF_WIDTH = 0.075f;
    public const float RACKET_HALF_HEIGHT = 0.375f;
    public const float LEFT_RACKET_EDGE = -0.425f;
    public const float RIGHT_RACKET_EDGE = 0.425f;
    public const float BALL_HALF_WIDTH = 0.06f;
    public const float BALL_HALF_HEIGHT = 0.06f;

    public BallNP ball;
    public RacketNP leftRacket;
    public RacketNP rightRacket;
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
        Vector2 ballPosition = ball.transform.position;

        if (ballPosition.y >= UPPER_BOUND || ballPosition.y <= LOWER_BOUND)
        {
            ball.angle = -ball.angle;
            SoundManager.Instance.PlayBoing();
        }

        if (IsBallCollidingRacket(leftRacket))
        {
            if (leftRacket.MovingDirection == -1)
                ball.angle = 315 + Random.Range(10f, 30f);
            else if (leftRacket.MovingDirection == 1)
                ball.angle = 45 - Random.Range(10f, 30f);
            else
                ball.angle = 180 - ball.angle + Random.Range(-5f, 5f);

            SoundManager.Instance.PlayBoing();
        }

        if (IsBallCollidingRacket(rightRacket))
        {
            if (rightRacket.MovingDirection == -1)
                ball.angle = 225 - Random.Range(10f, 30f);
            else if (rightRacket.MovingDirection == 1)
                ball.angle = 135 + Random.Range(10f, 30f);
            else
                ball.angle = 180 - ball.angle + Random.Range(-5f, 5f);

            SoundManager.Instance.PlayBoing();
        }

        if (ballPosition.x <= LEFT_BOUND)
        {
            ball.enabled = false;
            ball.transform.position = Vector2.zero;
            SoundManager.Instance.PlayPoint();
            rightPointsVal++;
            rightPoints.text = "" + rightPointsVal;
            ballSpriteRenderer.enabled = false;
            StartCoroutine(WaitThenResetBall());
        }

        if (ballPosition.x >= RIGHT_BOUND)
        {
            ball.enabled = false;
            ball.transform.position = Vector2.zero;
            SoundManager.Instance.PlayPoint();
            leftPointsVal++;
            leftPoints.text = "" + leftPointsVal;
            ballSpriteRenderer.enabled = false;
            StartCoroutine(WaitThenResetBall());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("NoPhysicPong");
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

    public bool IsBallCollidingRacket(RacketNP racket)
    {
        Rect racketRect = new Rect(
            racket.transform.position.x - RACKET_HALF_WIDTH,
            racket.transform.position.y - RACKET_HALF_HEIGHT,
            RACKET_HALF_WIDTH * 2 * racket.transform.localScale.x,
            RACKET_HALF_HEIGHT * 2 * racket.transform.localScale.y);

        float ballX = ball.transform.position.x;
        float ballY = ball.transform.position.y;

        return
            racketRect.Contains(new Vector2(ballX - BALL_HALF_WIDTH, ballY - BALL_HALF_HEIGHT)) || 
            racketRect.Contains(new Vector2(ballX - BALL_HALF_WIDTH, ballY + BALL_HALF_HEIGHT)) || 
            racketRect.Contains(new Vector2(ballX + BALL_HALF_WIDTH, ballY - BALL_HALF_HEIGHT)) || 
            racketRect.Contains(new Vector2(ballX + BALL_HALF_WIDTH, ballY + BALL_HALF_HEIGHT));

    }
}
