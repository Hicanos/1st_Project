using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public int cardCount = 0;
    float time = 0.00f;
    public Text timeTxt;
    public GameObject endTxt;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (time<=30f)
        {
            time += Time.deltaTime;
            timeTxt.text = time.ToString("N2");
        }
        else
        {
            time = 30f;
            
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
            timeTxt.text = time.ToString("N2");
            
        }
    }

    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if(cardCount == 0)
            {
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard= null;
        secondCard= null;
    }
}
