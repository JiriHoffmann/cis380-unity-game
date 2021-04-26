using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public GameObject enemy;


    [Header("Pathfinding")]
    public Transform target1;
    public Transform target2 = null;
    public float nextWaypointDistance = 3f;
    public float pathUpdateTimeSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    Rigidbody2D enemyRigidBody;

    [Header("Custom Behavior")]
    public bool jumpEnabled = false;
    public bool jumpOnly = false;
    public float jumpTimout = 0f;
    public bool horizonalJumpMovementEnabled = false;
    public float mininalHorizontalDistanceToJump = 0.7f;
    public bool twoPaths = true;
    [SerializeField] private LayerMask platformLayerMask;

    Path path;
    Transform currentTarget;
    Seeker seeker;
    int currentWaypoint = 0;
    bool reachedEndofPath = false;
    bool isGrounded = false;
    float nextJumpTime = 0f;

    BoxCollider2D boxCollider;
    CapsuleCollider2D capsCollider;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        boxCollider = GetComponent<BoxCollider2D>();
        capsCollider = GetComponent<CapsuleCollider2D>();
        enemyRigidBody = enemy.GetComponent<Rigidbody2D>();
        currentTarget = target1;
        InvokeRepeating("UpdatePath", 0f, pathUpdateTimeSeconds);
    }

    void UpdatePath()
    {
        if (twoPaths)
        {
            if (currentTarget == target2 && (target2.position.x <= enemyRigidBody.position.x))
            {
                currentTarget = target1;
            }
            else if (currentTarget == target1 && target1.position.x >= enemyRigidBody.position.x)
            {
                currentTarget = target2;
            }
        }
        if (seeker.IsDone())
            seeker.StartPath(enemyRigidBody.position, currentTarget.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
            if (reachedEndofPath) { return; }
        }
        else
        {
            reachedEndofPath = false;
        }

        isGrounded = isGroundedCheck();

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - enemyRigidBody.position).normalized;
        Vector2 force = new Vector2(direction.x * speed * Time.fixedDeltaTime, 0f);

        // if jump only then keep jumping
        if (jumpEnabled && jumpOnly && isGrounded && nextJumpTime < Time.time && direction.y > jumpNodeHeightRequirement)
        {
            
            force = Vector2.up * speed * jumpModifier;
            enemyRigidBody.AddForce(force);
            nextJumpTime = Time.time + jumpTimout;
        }
        // if jump enabled and the target is above
        else if (jumpEnabled && isGrounded && direction.y >= mininalHorizontalDistanceToJump && nextJumpTime < Time.time)
        {
            enemyRigidBody.AddForce(Vector2.up * speed * jumpModifier);
            if (!horizonalJumpMovementEnabled)
            {
                force.x = 0;
            }
            enemyRigidBody.AddForce(force);
            nextJumpTime = Time.time + jumpTimout;
        }
        else if (direction.x > 0.6 || direction.x < -0.6)
        {
            enemyRigidBody.AddForce(force);
        }



        float distance = Vector2.Distance(enemyRigidBody.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    public float getVelocity()
    {
        return enemyRigidBody.velocity.y;
    }

    private bool isGroundedCheck()
    {

        float extraHeight = 0.05f;
        if (boxCollider != null)
        {
            RaycastHit2D raycastHit = Physics2D.Raycast((boxCollider.bounds.center - new Vector3(0f, boxCollider.bounds.extents.y - extraHeight, 0f)), -Vector3.up, jumpCheckOffset, platformLayerMask);
            return raycastHit.collider != null;
        }
        else if (capsCollider != null)
        {
            RaycastHit2D raycastHit = Physics2D.Raycast((capsCollider.bounds.center - new Vector3(0f, capsCollider.bounds.extents.y - extraHeight, 0f)), -Vector3.up, jumpCheckOffset, platformLayerMask);
            return raycastHit.collider != null;
        }
        else
        {
            //Debug.LogError("No collider found");
            return false;
        }
    }
}
