using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public int startingSeconds;
    public int secondsleft = 300;
    public bool takingAway = false;
    public bool setTimerActive = true;

    private void Start()
    {
        secondsleft = startingSeconds;
    }

    private void Update()
    {
        if(takingAway == false && secondsleft > 0 && setTimerActive == true)
        {
            StartCoroutine(TimerTake());
        }
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsleft -= 1;

        int seconds = (int)(secondsleft % 60);
        int minutes = (int)(secondsleft / 60) % 60;

        if (seconds < 10)
        {
            timerText.text = minutes + ":0" + seconds;
        }
        else
        {
            timerText.text = minutes + ":" + seconds;
        }
        takingAway = false;
    }

    public void ResetDay()
    {
        secondsleft = startingSeconds + 3;
        setTimerActive = true;
    }
}
