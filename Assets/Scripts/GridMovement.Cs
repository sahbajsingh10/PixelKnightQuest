using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class GridMovement : MonoBehaviour
{
    public float tileSize = 1f;                // world units per tile
    [Tooltip("Units per second. For 1u tiles, 10 => 0.1s per move")]
    public float moveSpeed = 10f;              
    public LayerMask obstacleLayer;

    private bool isMoving = false;
    private Vector3 targetPos;
    private Rigidbody2D rb;
    private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        targetPos = transform.position;
    }

    void Update()
    {
        if (isMoving) return;

        Vector2 dir = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))    dir = Vector2.up;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))  dir = Vector2.down;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))  dir = Vector2.left;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) dir = Vector2.right;

        if (dir != Vector2.zero)
        {
            Vector3 newPos = transform.position + (Vector3)dir * tileSize;
            // Check for obstacles at newPos
            if (!Physics2D.OverlapCircle(newPos, 0.1f, obstacleLayer))
            {
                StartCoroutine(Move(newPos));
                // Trigger animation (you’ll need to set up parameters on your Animator)
                animator.SetFloat("MoveX", dir.x);
                animator.SetFloat("MoveY", dir.y);
                animator.SetTrigger("StartMoving");
            }
        }
    }

    private IEnumerator Move(Vector3 dest)
    {
        isMoving = true;
        float elapsed = 0f;
        float duration = tileSize / moveSpeed;  // e.g. 1/10 = 0.1 s

        Vector3 start = transform.position;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, dest, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = dest;
        isMoving = false;
        animator.SetTrigger("StopMoving");
    }
}
