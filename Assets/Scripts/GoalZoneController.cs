using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZoneController : MonoBehaviour
{
    [SerializeField]
    private ScoreTextComponent scoreTextComp = null;

    [SerializeField]
    private bool leftSide = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BallController>() != null)
        {
            if (scoreTextComp != null)
            {

                scoreTextComp.AddScore(leftSide);
                GameResetController.Reset();
            }
            else
            {
                Debug.Log(this.name + " has not been populated with ScoreTextComponent");
            }
        }
    }
}
