using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 20f;
    private float lowerBound = -25f;
    private PlayerController playerControllerScript;
    // private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        // playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.isWalking)
        {
            transform.Translate(Vector3.left * Time.deltaTime * (speed/3));
        }

        if (!playerControllerScript.isEntering) // !playerAnim.GetBool("isEntering"))
        {
            if (playerControllerScript.gameOver == false)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }

            if (transform.position.y < lowerBound)
            {
                Destroy(gameObject);
            }
        }
    }
}
