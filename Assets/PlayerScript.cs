using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            velocity += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity += Vector2.right;
        }
        if (velocity.magnitude > 0.1) velocity = velocity.normalized;
        MapGenerator.map.offset -= speed * velocity;
    }
}
