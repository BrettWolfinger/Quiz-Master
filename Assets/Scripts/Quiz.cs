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

        if(index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else{
            questionText.text = "Sorry, the correct answer was;\n" + question.GetAnswer(correctAnswerIndex);
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }

        SetButtonState(false);
        timer.CancelTimer();
        isComplete = true;
    }

    void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprites();
        DisplayQuestion();
    }

    void SetDefaultButtonSprites()
    {
        Image buttonImage;
        for(int i = 0; i < answerButtons.Length;i++)
        {
            buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    void DisplayQuestion()
    {
       questionText.text = question.GetQuestion();

        for(int i =0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButtons.Length;i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
}

