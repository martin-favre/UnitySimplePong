using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField]
    private float speed = 0;
    public float Speed { get => speed; set => speed = value; }
    private Vector2 plannedMovement = Vector2.zero;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = Helpers.GetComponentMandatory<Rigidbody2D>(gameObject);
    }

    public void Move(Vector2 movement)
    {
        plannedMovement = movement;
    }
    private void FixedUpdate()
    {
        if(plannedMovement != Vector2.zero)
        {
            //transform.position += (Vector3)plannedMovement * Speed * Time.deltaTime;
            rigidBody.AddForce(plannedMovement, ForceMode2D.Impulse);
            plannedMovement = Vector2.zero;
        }
    }
}
