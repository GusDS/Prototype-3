using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool isDoubleJump = false;
    public bool isEntering = true;
    public bool isWalking = false;
    public bool isGhosting = false;
    public bool gameOver;
    public Material ghostMat;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    private Rigidbody playerRB;
    private Animator playerAnim;
    private Renderer playerRenderer;
    private Light dirLight;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerRenderer = this.GetComponentInChildren<Renderer>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        dirLight = GameObject.Find("Directional Light").GetComponent<Light>();

        // Entry position and movement
        transform.position = (Vector3.left * 3);
        playerAnim.SetBool("isEntering", true);
        isEntering = true;
        // Invoke("ToggleDance", 5f);
        Invoke("ToggleRun", 5f);
        // ToggleEntering();

        // playerRenderer.material.SetTexture
        //playerRenderer.material.shader = Shader.Find("Standard");
        //Debug.Log(playerRenderer.material.shaderKeywords);
        //playerRenderer.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        //// playerRenderer.material.color.a = 0f; // Completely transparent
    }

    void Update()
    {
        if (!playerAnim.GetBool("isEntering"))
        {
            if (Input.GetKeyDown(KeyCode.Space) && !gameOver) // && isOnGround)
            {
                if (isOnGround || !isDoubleJump)
                {
                    dirtParticle.Stop();
                    playerAnim.SetTrigger("Jump_trig");
                    playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    if (isOnGround) isOnGround = false;
                    else if (!isDoubleJump) isDoubleJump = true;
                    playerAudio.PlayOneShot(jumpSound, 1.0f);
                }
            }
            if (isGhosting)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 2f);
                // transform.Rotate(Vector3.right * Time.deltaTime * 5f);
            }
        }
    }

    void ToggleDance()
    {
        playerAnim.SetTrigger("Dance_trig");
        Invoke("ToggleSalute", 5f);
    }

    void ToggleSalute()
    {
        playerAnim.SetTrigger("Salute_trig");
        Invoke("ToggleWalk", 3f);
    }

    void ToggleWalk()
    {
        dirtParticle.Stop();
        playerAnim.SetTrigger("Walk_trig");
        isWalking = true;
        Invoke("ToggleRun", 1f);
    }

    void ToggleRun()
    {
        playerAnim.SetBool("isEntering", false);
        playerAnim.SetTrigger("Run_trig");
        isWalking = false;
        isEntering = false;
        playerRB.constraints = RigidbodyConstraints.FreezePositionX;
        dirtParticle.Play();
    }

    void ToggleGhosting()
    {
        // Ghosting
        playerRB.useGravity = false;
        playerRB.isKinematic = true;
        isGhosting = true;
        playerRenderer.material = ghostMat;
        playerRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        //dirLight.shadowStrength = 0;
        //playerRB.constraints = RigidbodyConstraints.None;
        //playerRB.constraints = RigidbodyConstraints.FreezeRotation;
        //playerRB.constraints = RigidbodyConstraints.FreezePositionX;
        //playerRB.constraints = RigidbodyConstraints.FreezePositionZ;
        //playerAnim.SetTrigger("Ghosting_trig");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            isDoubleJump = false;
            if (!isWalking) dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerAudio.PlayOneShot(crashSound, 1.0f);
            dirtParticle.Stop();
            explosionParticle.Play();
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            gameOver = true;
            // Debug.Log("Game Over!");
            Invoke("ToggleGhosting", 3.5f);
        }
    }
}
