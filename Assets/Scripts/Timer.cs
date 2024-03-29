using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToAnswer = 30f;
    [SerializeField] float timeToReview = 10f;
    public bool loadNextQuestion = true;
    public bool isAnsweringQuestion = false;
    public float fillFraction;

    float timerValue = 0f;
    

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        //Change fill fraction on display depending on whether answering or reviewing
        if(timerValue > 0)
        {
            if(isAnsweringQuestion)
            {
                fillFraction = timerValue / timeToAnswer;
            }
            else
            {
                fillFraction = timerValue / timeToReview;
            }
        }
        else
        {
            //Toggle between answering and reviewing once time elapses
            if(!isAnsweringQuestion)
            {
                timerValue = timeToAnswer;
                isAnsweringQuestion = true;
            }
            else
            {
                timerValue = timeToReview;
                isAnsweringQuestion = false;
            }

        }

        Debug.Log(fillFraction);
    }

}
