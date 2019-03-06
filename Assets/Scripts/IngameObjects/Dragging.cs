using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Dragging : MonoBehaviour
{
    private bool beingDragged = false;

    private Vector3 oldPos;
    private Vector3 dif;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private IEnumerator getVelocity()
    {
        yield return new WaitForSeconds(0.01f);

        dif = transform.position - oldPos;
        oldPos = transform.position;
        if (beingDragged)
        {
            StartCoroutine(getVelocity());
        }
        
    }
    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool overSprite = GetComponent<SpriteRenderer>().bounds.Contains(mousePosition);


        //beingDragged = beingDragged && Input.GetButton("Fire1");

        if (overSprite)
        {
            //If we've pressed down on the mouse (or touched on the iphone)
            if (Input.GetButton("Fire1") && !beingDragged)
            {
                beingDragged = true;
                StartCoroutine(getVelocity());
            }
        }

        if (beingDragged)
        {
            //Set the position to the mouse position
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                             Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                                             0.0f);
            

            rb.velocity = Vector3.zero;
            if (!Input.GetButton("Fire1"))
            {
                beingDragged = false;
                rb.AddForce(dif * 2000);
            }
        }
    }
}
