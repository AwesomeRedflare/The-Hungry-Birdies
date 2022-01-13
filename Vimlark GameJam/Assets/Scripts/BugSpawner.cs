using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public int maxFlyCount;
    public int maxBeeCount;
    public int maxBirdCount;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public GameObject[] bugs;

    public GameManager gameManager;

    public BabyBirds babyBirds;

    private void Start()
    {
        ResetDay();
    }

    void SpawnBugs()
    {
        for (int i = 0; i < maxFlyCount; i++)
        {
            Vector2 randomSpawn = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            Instantiate(bugs[0], randomSpawn, Quaternion.identity, transform);
        }

        for (int i = 0; i < maxBeeCount; i++)
        {
            Vector2 randomSpawn = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            Instantiate(bugs[1], randomSpawn, Quaternion.identity, transform);
        }

        for (int i = 0; i < maxBirdCount; i++)
        {
            Vector2 randomSpawn = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            Instantiate(bugs[2], randomSpawn, Quaternion.identity, transform);
        }
    }

    public void ResetDay()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < maxBirdCount; i++)
        {
            Destroy(GameObject.FindGameObjectWithTag("bread"));
        }

        if (gameManager.dayNumber == 1)
        {
            maxFlyCount = 30;
            maxBeeCount = 0;
            maxBirdCount = 0;
            babyBirds.maxHunger = 5;
        }

        if (gameManager.dayNumber == 2)
        {
            maxFlyCount = 15;
            maxBeeCount = 15;
            maxBirdCount = 0;
            babyBirds.maxHunger = 10;
        }

        if (gameManager.dayNumber == 3)
        {
            maxFlyCount = 0;
            maxBeeCount = 30;
            maxBirdCount = 0;
            babyBirds.maxHunger = 10;
        }

        if (gameManager.dayNumber == 4)
        {
            maxFlyCount = 20;
            maxBeeCount = 0;
            maxBirdCount = 10;
            babyBirds.maxHunger = 20;
        }

        if (gameManager.dayNumber == 5)
        {
            maxFlyCount = 10;
            maxBeeCount = 10;
            maxBirdCount = 10;
            babyBirds.maxHunger = 20;
        }

        if (gameManager.dayNumber == 6)
        {
            maxFlyCount = 0;
            maxBeeCount = 20;
            maxBirdCount = 10;
            babyBirds.maxHunger = 30;
        }

        if (gameManager.dayNumber == 7)
        {
            maxFlyCount = 0;
            maxBeeCount = 0;
            maxBirdCount = 20;
            babyBirds.maxHunger = 30;
        }

        SpawnBugs();
    }
}
