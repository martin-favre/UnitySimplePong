using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private float startingSpeed = 0;
    private Rigidbody2D myRigidbody;
    private Vector3 startPos;
    private void Awake()
    {
        myRigidbody = Helpers.GetComponentMandatory<Rigidbody2D>(gameObject);
        GameResetController.RegisterResetFunction(Reset);
    }
    private void Start()
    {
        startPos = transform.position;
        Reset();
    }

    private void SetVelocityToRandom()
    {
        Vector2 randDir = GetRandomDirection();
        myRigidbody.velocity = randDir * startingSpeed;
    }

    private Vector2 GetRandomDirection()
    {
        float x = 0;
        float y = 0;
        while (x == 0 && y == 0)
        {
            x = UnityEngine.Random.Range(-1f, 1f);
            y = UnityEngine.Random.Range(-0.5f, 0.5f);  // not 1 to avoid cases where the ball gets x ~ 0
        }
        return new Vector2(x, y).normalized;
    }

    private void Reset()
    {
        transform.position = startPos;
        SetVelocityToRandom();

    }
}
