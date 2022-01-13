using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBirdScript : MonoBehaviour
{
    public float speed;
    public float deathSpeed;

    public int health;

    private float waitTime;
    public float startWaitTime;

    public float stoppingDistance;
    public float retreatDistance;
    public float dangerZone;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private bool isRight = true;
    private bool inRange = false;
    private bool isFlyingAway = false;

    public GameObject projectile;
    public GameObject projectileParticle;
    public GameObject bread;

    Vector2 randomPos;
    Vector2 deathPos;

    private Transform player;

    public Animator anim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        waitTime = startWaitTime;
        randomPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        timeBtwShots = startTimeBtwShots;
    }

    private void Update()
    {
        

        if(Vector2.Distance(transform.position, player.position) < dangerZone)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        if (health <= 0)
        { 
            if(isFlyingAway == false)
            {
            deathPos = new Vector2(Random.Range(-140, -20), 65);
            Instantiate(bread, transform.position, Quaternion.identity);
            GetComponent<CapsuleCollider2D>().enabled = false;
            isFlyingAway = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, deathPos, deathSpeed * Time.deltaTime);
            if (transform.position.x == deathPos.x && transform.position.y == deathPos.y)
            {
                Destroy(gameObject);
            }
        }

        if (inRange == true && health > 0)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }

            if (player.position.x > transform.position.x && isRight == false)
            {
                Flip();
                isRight = true;
            }
            else if (player.position.x < transform.position.x && isRight == true)
            {
                Flip();
                isRight = false;
            }

            if (timeBtwShots <= 0)
            {
                anim.SetBool("Caw", true);
                FindObjectOfType<AudioManager>().Play("deep caw");
                Instantiate(projectile, transform.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                anim.SetBool("Caw", false);
                timeBtwShots -= Time.deltaTime;
            }
        }
        else if (health > 0) 
        {
            transform.position = Vector2.MoveTowards(transform.position, randomPos, speed * Time.deltaTime);

            if (randomPos.x > transform.position.x && isRight == false)
            {
                Flip();
                isRight = true;
            }
            else if (randomPos.x < transform.position.x && isRight == true)
            {
                Flip();
                isRight = false;
            }

            if (Vector2.Distance(transform.position, randomPos) < 2)
            {
                if (waitTime <= 0)
                {
                    randomPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                    startWaitTime = Random.Range(0f, .2f);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("bird projectile"))
        {
            FindObjectOfType<AudioManager>().Play("enemy hit");
            Instantiate(projectileParticle, transform.position, Quaternion.identity);
            health--;
            Destroy(col.gameObject);
        }
    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0);
    }
}
