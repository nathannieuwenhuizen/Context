using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAtScreen : MonoBehaviour
{

    private Rigidbody2D rigidbody2;
    private Vector3 randomVector = new Vector3(0, 0, 0);
    private Vector2 size;
    private Vector2 cameraSize;
    private Vector2 precentageSize;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        size = new Vector2(
            2.5f / 2 * transform.localScale.x,
            2.5f / 2 * transform.localScale.y 
            );
        cameraSize = new Vector2(
            Camera.main.orthographicSize * 2f / Screen.height * Screen.width,
            Camera.main.orthographicSize * 2f
        );
        precentageSize = new Vector2(
            size.x / cameraSize.x,
            size.y / cameraSize.y
        );

        Debug.Log(cameraSize);
        Debug.Log((Screen.height * Screen.width));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < precentageSize.x)
        {
            //Debug.Log("I am left of the camera's view.");
            if (rigidbody2.velocity.x > 0)
            {
                return;
            }
            randomVector = Vector3.Reflect(randomVector, Vector3.right);
            rigidbody2.velocity = Vector3.Reflect(rigidbody2.velocity, Vector3.right);
        }

        if (1.0 - precentageSize.x < pos.x)
        {

            //Debug.Log("I am right of the camera's view.");
            randomVector = Vector3.Reflect(randomVector, Vector3.left);
            rigidbody2.velocity = Vector3.Reflect(rigidbody2.velocity, Vector3.left);
        }

        if (pos.y < precentageSize.y)
        {
            //Debug.Log("I am below the camera's view.");
            randomVector = Vector3.Reflect(randomVector, Vector3.down);
            rigidbody2.velocity = Vector3.Reflect(rigidbody2.velocity, Vector3.down);
        }

        if (1.0 - precentageSize.y < pos.y)
        {
            //Debug.Log("I am above the camera's view.");
            randomVector = Vector3.Reflect(randomVector, Vector3.up);
            rigidbody2.velocity = Vector3.Reflect(rigidbody2.velocity, Vector3.up);
        }
    }
}
