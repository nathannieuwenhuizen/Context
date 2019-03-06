using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAtScreen : MonoBehaviour
{

    private Rigidbody2D rigidbody2;
    private Vector3 randomVector = new Vector3(0, 0, 0);
    private float tileWidth;
    private float tileHeight;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        tileWidth = (float)GetComponent<SpriteRenderer>().size.x * transform.localScale.x / 2;
        tileHeight = (float)GetComponent<SpriteRenderer>().size.y * transform.localScale.y / 2;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0.0)
        {
            //Debug.Log("I am left of the camera's view.");
            if (rigidbody2.velocity.x > 0)
            {
                return;
            }
            randomVector = Vector3.Reflect(randomVector, Vector3.right);
            rigidbody2.velocity = Vector3.Reflect(rigidbody2.velocity, Vector3.right);
        }

        if (1.0 < pos.x)
        {

            //Debug.Log("I am right of the camera's view.");
            randomVector = Vector3.Reflect(randomVector, Vector3.left);
            rigidbody2.velocity = Vector3.Reflect(rigidbody2.velocity, Vector3.left);
        }

        if (pos.y < 0.0)
        {
            //Debug.Log("I am below the camera's view.");
            randomVector = Vector3.Reflect(randomVector, Vector3.down);
            rigidbody2.velocity = Vector3.Reflect(rigidbody2.velocity, Vector3.down);
        }

        if (1.0 < pos.y)
        {
            //Debug.Log("I am above the camera's view.");
            randomVector = Vector3.Reflect(randomVector, Vector3.up);
            rigidbody2.velocity = Vector3.Reflect(rigidbody2.velocity, Vector3.up);
        }
    }
}
