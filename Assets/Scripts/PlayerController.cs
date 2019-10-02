using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private KeyCode upKey = KeyCode.A;
    [SerializeField]
    private KeyCode downKey = KeyCode.A;
    [SerializeField]
    private KeyCode leftKey = KeyCode.A;
    [SerializeField]
    private KeyCode rightKey = KeyCode.A;
    [SerializeField]
    private float speed = 0;

    private Rigidbody2D myRigidbody;
    private Vector3 startPos;
    private Vector2 plannedMovement = Vector2.zero;
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

    private void Update()
    {
        Vector2 plannedDirection = Vector2.zero;
        if (Input.GetKey(upKey)) plannedDirection += Vector2.up;
        if (Input.GetKey(downKey)) plannedDirection += Vector2.down;
        plannedMovement = plannedDirection * speed;
    }

    private void FixedUpdate()
    {
        if(plannedMovement != Vector2.zero)
        {
            myRigidbody.AddForce(plannedMovement, ForceMode2D.Impulse);
            plannedMovement = Vector2.zero;
        }
    }

    public void Reset()
    {
        transform.position = startPos;
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.angularVelocity = 0;
    }
}
