using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Animator fadeInTransition;

    public GameObject instructions;
    public GameObject playerControls;
    public GameObject enemyBehavior;

    private void Start()
    {
        fadeInTransition.SetTrigger("fade out");
    }

    public void StartButton()
    {
        SceneManager.LoadScene("bird game");
    }

    public void BackToMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void FadeIn()
    {
        FindObjectOfType<AudioManager>().Play("button");
        fadeInTransition.SetTrigger("fade in");
        Invoke("StartButton", 1f);
    }

    public void FadeOut()
    {
        FindObjectOfType<AudioManager>().Play("button");
        fadeInTransition.SetTrigger("fade in");
        Invoke("BackToMenuButton", 1f);
    }

    public void OpenInstructions()
    {
        instructions.SetActive(true);
        FindObjectOfType<AudioManager>().Play("button");
    }

    public void CloseInstructions()
    {
        instructions.SetActive(false);
        FindObjectOfType<AudioManager>().Play("button");
    }

    public void PlayerControls()
    {
        playerControls.SetActive(true);
        enemyBehavior.SetActive(false);
        FindObjectOfType<AudioManager>().Play("button");
    }

    public void EnemyBehaviors()
    {
        playerControls.SetActive(false);
        enemyBehavior.SetActive(true);
        FindObjectOfType<AudioManager>().Play("button");
    }
}
