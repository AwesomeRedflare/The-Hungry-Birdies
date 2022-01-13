using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float projectileCoolDown = 2f;
    public float projectileCoolDownTimer;
    public float jumpVelocity = 5f;

    public int health;

    private bool facingRight = true;

    public Rigidbody2D rb;

    public GameObject birdProjectile;
    public GameObject birdStartPos;
    public GameObject particle;
    public GameObject evilParticle;

    public Transform firePoint;

    public Animator anim;

    private void Update()
    {
        Vector3 characterScale = transform.localScale;

        if(projectileCoolDown > 0)
        {
            projectileCoolDownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && projectileCoolDownTimer < 0)
        {
            anim.SetBool("Caw", true);
            anim.SetBool("idle", false);
            FindObjectOfType<AudioManager>().Play("bird chirp");
            Instantiate(birdProjectile, firePoint.transform.position, transform.rotation);
            projectileCoolDownTimer = projectileCoolDown;
        }
        else
        {
            anim.SetBool("Caw", false);
            anim.SetBool("idle", true);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (!facingRight)
            {
                Flip();
            }
            facingRight = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (facingRight)
            {
                Flip();
            }
            facingRight = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            FindObjectOfType<AudioManager>().Play("flap");
            rb.velocity = new Vector2(0, jumpVelocity);
            anim.SetBool("hasFlapped", true);
            anim.SetBool("idle", false);
        }
        else
        {
            anim.SetBool("hasFlapped", false);
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("bee"))
        {
            FindObjectOfType<AudioManager>().Play("enemy hit");
            Instantiate(evilParticle, transform.position, transform.rotation);
            col.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            col.gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
            col.gameObject.GetComponent<BeeScript>().health = 0;
            health--;
        }

        if(col.gameObject.CompareTag("evil projectile"))
        {
            Instantiate(evilParticle, transform.position, transform.rotation);
            FindObjectOfType<AudioManager>().Play("enemy hit");
            Destroy(col.gameObject);
            health--;
        }
    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0);
    }

    public void ResetDay()
    {
        health = 4;
        transform.position = birdStartPos.transform.position;
    }
}
