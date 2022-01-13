using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdProjectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    public GameObject projectileParticle;

    private void Start()
    {
        Invoke("DestroyObject", lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void DestroyObject()
    {
        Instantiate(projectileParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
