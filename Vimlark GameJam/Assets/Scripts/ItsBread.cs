using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItsBread : MonoBehaviour
{
    public GameObject projectileParticle;

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("bird projectile"))
        {
            FindObjectOfType<AudioManager>().Play("enemy hit");
            Instantiate(projectileParticle, transform.position, Quaternion.identity);
            Destroy(col.gameObject);
        }
    }
}
