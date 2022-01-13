using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyScript : MonoBehaviour
{
    public float speed;

    public int health;

    private float waitTime;
    public float startWaitTime;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private bool isRight = false;

    Vector2 randomPos;

    public Animator anim;

    public GameObject projectileParticle;

    private void Start()
    {
        waitTime = startWaitTime;
        randomPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    private void Update()
    {
        if(health <= 0)
        {
            anim.SetBool("isDead", true);
            speed = 0;
            GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<CircleCollider2D>().isTrigger = false;
        }


        transform.position = Vector2.MoveTowards(transform.position, randomPos, speed * Time.deltaTime);

        if(randomPos.x > transform.position.x && isRight == false)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            isRight = true;
        }
        else if(randomPos.x < transform.position.x && isRight == true)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            isRight = false;
        }

        if (Vector2.Distance(transform.position, randomPos) < 2) 
        {
            if (waitTime <= 0)
            {
                randomPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                startWaitTime = Random.Range(0f, .1f);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if(col.gameObject.CompareTag("bird projectile"))
        {
            FindObjectOfType<AudioManager>().Play("enemy hit");
            Instantiate(projectileParticle, transform.position, Quaternion.identity);
            health--;
            Destroy(col.gameObject);
        }

    }
}
