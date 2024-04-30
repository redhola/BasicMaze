using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public LayerMask obstacleLayer;
    private Rigidbody rb;
    private bool isMoving = false;
    private Vector3 movementDirection;
    public AudioSource audioSource;
    public AudioClip collisionSound; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        TrailRenderer trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer != null)
        {
            trailRenderer.time = Mathf.Infinity;
        }
    }

    void Update()
    {
        if (!isMoving)
        {
            CheckForMovementInput();
        }
    }

    void CheckForMovementInput()
    {
        movementDirection = Vector3.zero; 

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movementDirection = Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movementDirection = Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movementDirection = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movementDirection = Vector3.right;
        }

        if (movementDirection != Vector3.zero && !ObstacleInDirection(movementDirection))
        {
            isMoving = true;
        }
    }

    bool ObstacleInDirection(Vector3 direction)
    {
        RaycastHit hit;
        float maxDistance = 0.2f; 

        if (Physics.Raycast(transform.position, direction, out hit, maxDistance, obstacleLayer))
        {
            return hit.collider.gameObject.CompareTag("Obstacle");
        }
        return false;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.MovePosition(rb.position + movementDirection * speed * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isMoving = false;            
            rb.position -= movementDirection * 0.1f; 

            if (audioSource && collisionSound) 
        {
            audioSource.PlayOneShot(collisionSound);
        }
        }
    }
}
