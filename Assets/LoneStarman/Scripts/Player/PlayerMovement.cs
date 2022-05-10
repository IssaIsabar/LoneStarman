using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    public SpriteRenderer rend;

    Vector2 playerPos;
    Vector2 mousePos;


    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        playerPos = transform.position;

        playerPos.x += h * MovementSpeed * Time.deltaTime;
        playerPos.y += v * MovementSpeed * Time.deltaTime;

        transform.position = playerPos;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if(mousePos.x < playerPos.x)
            rend.flipY = true;
        else
            rend.flipY = false;
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rb.rotation = angle;
    }


}
