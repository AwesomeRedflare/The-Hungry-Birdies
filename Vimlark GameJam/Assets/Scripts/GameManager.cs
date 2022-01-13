using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Timer timer;
    public PlayerController playerController;
    public BabyBirds babyBirds;
    public BugSpawner bugSpawner;

    public GameObject playerHearts;
    public CanvasGroup gameOverCanvas;
    public CanvasGroup gameWinCanvas;

    public Text dayText;
    public Image hungerMeter;

    public Animator dayCompleteAnim;
    public Animator gameOverAnim;
    public Animator gameWinAnim;

    public int dayNumber = 1;

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (timer.secondsleft <= 0 || playerController.health <= 0)
        {
            gameOverAnim.SetTrigger("fade in");
            gameOverCanvas.blocksRaycasts = true;
            playerHearts.SetActive(false);
            timer.setTimerActive = false;
            timer.timerText.enabled = false;
            playerController.speed = 0;
        }

        if(hungerMeter.fillAmount >= 1)
        {
            if(dayNumber == 7)
            {
                playerHearts.SetActive(false);
                timer.setTimerActive = false;
                timer.timerText.enabled = false;
                playerController.speed = 0;
                gameWinAnim.SetTrigger("fade in");
                gameWinCanvas.blocksRaycasts = true;
                return;
            }

            dayText.text = "Day " + dayNumber + " Complete";
            playerHearts.SetActive(false);
            timer.setTimerActive = false;
            timer.timerText.enabled = false;
            dayCompleteAnim.SetTrigger("fade in");
            babyBirds.ResetDay();
            timer.ResetDay();
            Invoke("NextDay", 3);
        }
    }

    void NextDay()
    {
        dayNumber++;
        playerHearts.SetActive(true);
        bugSpawner.ResetDay();
        playerController.ResetDay();
        timer.timerText.enabled = true;
        dayCompleteAnim.SetTrigger("fade out");
    }
}
