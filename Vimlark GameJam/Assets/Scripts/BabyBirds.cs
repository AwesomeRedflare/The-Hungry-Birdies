using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabyBirds : MonoBehaviour
{
    public Image hungerMeter;
    public Text hungerAmountText;

    public GameObject loveParticles;
    Vector3 particleLocation;

    public float hunger = 0;
    public float maxHunger = 10;

    private void Start()
    {
        particleLocation = new Vector3(transform.position.x, transform.position.y, -9);
    }

    private void Update()
    {
        hungerMeter.fillAmount = hunger / maxHunger;

        hungerAmountText.text = hunger + "/" + maxHunger;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("bug"))
        {
            FindObjectOfType<AudioManager>().Play("baby chirps");
            Instantiate(loveParticles, particleLocation, Quaternion.identity);
            Destroy(col.gameObject);
            hunger += 1;
        }

        if (col.gameObject.CompareTag("bee") && col.gameObject.GetComponent<BeeScript>().health <= 0)
        {
            FindObjectOfType<AudioManager>().Play("baby chirps");
            Instantiate(loveParticles, particleLocation, Quaternion.identity);
            Destroy(col.gameObject);
            hunger += 3;
        }

        if (col.gameObject.CompareTag("bread"))
        {
            FindObjectOfType<AudioManager>().Play("baby chirps");
            Instantiate(loveParticles, particleLocation, Quaternion.identity);
            Destroy(col.gameObject);
            hunger += 5;
        }
    }

    public void ResetDay()
    {
        hunger = 0;
        maxHunger += 5;
    }
}
