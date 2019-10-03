using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private float startingSpeed = 0;
    [SerializeField]
    private float rotationalSpeedVariation = 0;
    private Rigidbody2D myRigidbody;
    private Vector3 startPos;
    private bool started = false;
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

    private void SetRotationToRandom()
    {
        myRigidbody.angularVelocity = UnityEngine.Random.Range(-rotationalSpeedVariation, rotationalSpeedVariation);
    }

    private void Update()
    {
        if(!started)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                SetVelocityToRandom();
                SetRotationToRandom();
                started = true;
            }
        }
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
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.angularVelocity = 0;
        started = false;

    }
}
