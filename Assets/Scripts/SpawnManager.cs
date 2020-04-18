using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;

    private float startDelay = 5f;
    private float repeatRate = 2.5f;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private int randomObstacle;
    private PlayerController playerControllerScript;
    // private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        // playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false && !playerControllerScript.isEntering) // !playerAnim.GetBool("isEntering"))
        {
            randomObstacle = Random.Range(0, obstaclePrefab.Length);
            Instantiate(obstaclePrefab[randomObstacle], spawnPos, obstaclePrefab[randomObstacle].transform.rotation);
        }
    }

}
