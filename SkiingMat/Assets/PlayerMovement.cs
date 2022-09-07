using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float mSpeed = 5;
    Vector2 moveDir = Vector2.zero;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDir = new Vector2(moveX, moveY);
    }

    private void FixedUpdate()
    {
        rb.velocity  = new Vector2(moveDir.x * mSpeed, moveDir.y * mSpeed);
        Debug.Log(moveDir);
    }
}
