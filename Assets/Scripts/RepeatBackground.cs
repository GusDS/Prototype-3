using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPost;
    private float repeatWidth;

    void Start()
    {
        startPost = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2; // 56.4f
    }

    void Update()
    {
        if (transform.position.x < startPost.x - repeatWidth)
        {
            transform.position = startPost;
        }
    }
}
