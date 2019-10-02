using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextComponent : MonoBehaviour
{
    Text text;
    int leftScore = 0;
    int rightScore = 0;
    private void Awake()
    {
        text = Helpers.GetComponentMandatory<Text>(gameObject);
        text.text = GetScoreString();
    }

    string GetScoreString()
    {
        return leftScore + " : " + rightScore;
    }
    public void AddScore(bool left)
    {
        if (left)
        {
            leftScore++;
        }
        else
        {
            rightScore++;
        }
        text.text = GetScoreString();
    }
}
