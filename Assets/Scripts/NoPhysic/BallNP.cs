using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNP : MonoBehaviour
{
    public float angle;
    public float speed;

    void Start()
    {
        Reset();
    }

    void Update()
    {
        Vector3 direction = (Vector3)(Quaternion.Euler(0, 0, angle) * Vector3.right);
        Vector2 newPosition = transform.position + speed * Time.deltaTime * direction;
        transform.position = newPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string colliderName = collision.gameObject.name;
        if (colliderName.Equals("LeftRacket"))
        {
            RacketNP racket = collision.gameObject.GetComponent<RacketNP>();
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
            RacketNP racket = collision.gameObject.GetComponent<RacketNP>();
            if (racket.MovingDirection == -1)
                angle = 225 - Random.Range(10f, 30f);
            else if (racket.MovingDirection == 1)
                angle = 135 + Random.Range(10f, 30f); 
            else
                angle = 180 - angle + Random.Range(-5f, 5f);

            SoundManager.Instance.PlayBoing();
        }
    }

    public void Reset()
    {
        transform.position = Vector2.zero;
        float sideChoise = Random.Range(0f, 1f);
        if(sideChoise < 0.5f)
            angle = Random.Range(-30f, 30f);
        else
            angle = Random.Range(150f, 210f);
        enabled = true;
    }

}
