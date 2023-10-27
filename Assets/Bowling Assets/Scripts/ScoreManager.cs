using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private int totalScore;
    public int currentThrow { get; private set; }
    public int currentFrame { get; private set; }

    private int[] frames = new int[10];

    private bool isSpare = false;
    private bool isStrike = false;

    private void Start()
    {
        ResetScore();   
    }

    //Set value for our framescore each time we throw the ball
    public void SetFrameScore(int score)
    {
        //BALL ONE
        if (currentThrow == 1)
        {
            frames[currentFrame - 1] += score; //Setting the right frame index and adding score value from the parameter passed

            //Parallel process to check spare
            if (isSpare)
            {
                frames[currentFrame - 2] += score;
                isSpare = false;
            }
            //----------------------------------------

            if (score == 10)
            {
                if (currentFrame == 10)
                {
                    currentThrow++; //Wait for BALL TWO
                }
                else
                {
                    isStrike = true;
                    currentFrame++; //Move to next frame since all pins down
                }
                //Reset ALL pins VIA GameManager
                gameManager.ResetAllPins();

            }
            else
            {
                currentThrow++; //Wait for BALL TWO
            }
            return;
        }

        // BALL TWO
        if (currentThrow == 2)
        {
            frames[currentFrame] += score;

            //Parallel process to check strike
            if (isStrike)
            {
                frames[currentFrame - 2] += frames[currentFrame - 1];
                isStrike = false;
            }
            //----------------------------

            if (frames[currentFrame - 1] == 10) //Is total frame score 10?
            {
                if (currentFrame == 10)
                {
                    currentThrow++; //Wait for BALL THREE
                }
                else
                {
                    isSpare = true;
                    currentFrame++;
                    currentThrow = 1;
                }
            }
            else
            {
                if (currentFrame == 10)
                {
                    //End of all throws/game
                    currentThrow = 0;
                    currentFrame = 0;
                }
                else
                {
                    currentFrame++;
                    currentThrow = 1;
                }
            }

            //Reset ALL pins VIA GameManager
            gameManager.ResetAllPins();

            return;
        }

        //BALL THREE ONLY FRAME TEN
        if (currentThrow == 3 && currentFrame == 10)
        {
            frames[currentFrame - 1] += score;
            //End of all throws
            currentThrow = 0;
            currentFrame = 0;
        }
    }

    public int CalculateTotalScore()
    {
        totalScore = 0;
        foreach (var frame in frames)
        {
            totalScore += frame;
        }

        return totalScore;
    }

    private void ResetScore()
    {
        totalScore = 0;
        currentFrame = 1;
        currentThrow = 1;
        frames = new int[10];
    }

}
