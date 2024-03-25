using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    public bool isComplete = false;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        DisplayQuestion();

    }

    void Update() {
        timerImage.fillAmount = timer.fillFraction;
    }


    public void OnAnswerSelected(int index)
    {
        correctAnswerIndex = question.GetCorrectIndex();
        Image buttonImage;

        //Change the sprite for the button with when correct answer is selected
        if(index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        //Color the correct answer on incorrect selection
        else{
            questionText.text = "Sorry, the correct answer was;\n" + question.GetAnswer(correctAnswerIndex);
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }

        SetButtonState(false);
        timer.CancelTimer();
        isComplete = true;
    }

    //Resets state of game for the next question
    void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprites();
        DisplayQuestion();
    }

    //Reload all buttons to default to reset interface
    void SetDefaultButtonSprites()
    {
        Image buttonImage;
        for(int i = 0; i < answerButtons.Length;i++)
        {
            buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    //Display the question text and answer choices
    void DisplayQuestion()
    {
       questionText.text = question.GetQuestion();

        for(int i =0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    //Toggle between button interactbility if there has been an answer
    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButtons.Length;i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
}

