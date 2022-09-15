using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rgdb2d;
    Vector3 movementVector;

    [SerializeField] float speed = 3f;

    Animate animate;

    private void Awake()
    {
        rgdb2d = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        animate = GetComponent<Animate>();
    }


    // Update is called once per frame
    void Update()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        animate.horizontal = movementVector.x;

        movementVector *= speed;

        rgdb2d.velocity = movementVector;
    }
}
