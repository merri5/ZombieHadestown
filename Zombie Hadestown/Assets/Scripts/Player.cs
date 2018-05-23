using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{

    [SerializeField] private float jumpForce = 100;
    [SerializeField] private AudioClip sfxJump;
    [SerializeField] private AudioClip sfxDeath;

    [SerializeField] private float maxPosition = 18f;

    Animator anim;
    private Rigidbody rigidBody;
    private bool jump = false;
    private AudioSource audioSource;
    private Vector3 setPosition;
    private Quaternion setRotation;
    private Vector3 startingPosition;


    private void Awake()
    {
        Assert.IsNotNull(sfxJump);
        Assert.IsNotNull(sfxDeath);
    }


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        setPosition.x = transform.position.x;
        setPosition.z = transform.position.z;
        startingPosition = setPosition;
        startingPosition.y = transform.position.y;
        setRotation = transform.rotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.Replayed)
        {


            transform.SetPositionAndRotation(startingPosition, setRotation);
            rigidBody.detectCollisions = true;
            GameManager.instance.Replayed = false;
        }
        if (!GameManager.instance.GameOver && GameManager.instance.GameStarted)
        {
            rigidBody.isKinematic = false;
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.instance.PlayerStartedGame();
                anim.Play("jump");
                audioSource.PlayOneShot(sfxJump);
                jump = true;
            }

            setPosition.y = transform.position.y;
            transform.position = setPosition;
            transform.rotation = setRotation;
        }
        if (!GameManager.instance.GameStarted)
        {

            rigidBody.isKinematic = true;
            rigidBody.velocity = Vector3.zero;
        }

    }
    void FixedUpdate()
    {
        if (jump == true)
        {
            jump = false;
            rigidBody.velocity = new Vector2(0, 0);
            
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
            
            if (rigidBody.transform.position.y > maxPosition)
            {
                print(rigidBody.transform.position.y);
                rigidBody.velocity = new Vector2(0, -5);
                rigidBody.MovePosition(new Vector3(rigidBody.position.x, maxPosition, rigidBody.position.z));
            }
            rigidBody.velocity = new Vector2(0, 0);
            
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            audioSource.PlayOneShot(sfxDeath);
            rigidBody.AddForce(new Vector2(-50, 20), ForceMode.Impulse);
            rigidBody.detectCollisions = false;
            GameManager.instance.PlayerCollided();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            GameManager.instance.AddPoint();
            collision.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }




    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5.0f);

    }
}
