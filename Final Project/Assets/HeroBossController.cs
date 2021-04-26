using UnityEngine;

public class HeroBossController : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    public Transform firePoint;
    Rigidbody2D rigBod;
    BoxCollider2D boxCollider;
    CapsuleCollider2D capsCollider;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("LaunchProjectile", 0f, 3f);
        boxCollider = GetComponent<BoxCollider2D>();
        capsCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        gameObject.transform.Rotate(0f, 180f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        bool grounded = isGroundedCheck();
        animator.SetBool("Grounded", grounded);
    }


    void LaunchProjectile()
    {
        GameObject bullet = Instantiate(Resources.Load("Rocket"), firePoint.position, firePoint.rotation) as GameObject;
        GameObject muzzleFlash = Instantiate(Resources.Load("BulletImpact"), firePoint.position, firePoint.rotation) as GameObject;
    }


    private bool isGroundedCheck()
    {
        float extraHeight = 0.1f;

        if (boxCollider != null)
        {
            Vector2 boxSize = new Vector2(boxCollider.bounds.size.x - 0.1f, extraHeight * 2);
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center - new Vector3(0f, boxCollider.bounds.extents.y - extraHeight, 0f), boxSize, 0f, Vector2.down, extraHeight, platformLayerMask);
            return raycastHit.collider != null;
        }
        else if (capsCollider != null)
        {
            Vector2 boxSize = new Vector2(capsCollider.bounds.size.x - 0.1f, extraHeight * 2);
            RaycastHit2D raycastHit = Physics2D.BoxCast(capsCollider.bounds.center - new Vector3(0f, capsCollider.bounds.extents.y - extraHeight, 0f), boxSize, 0f, Vector2.down, extraHeight, platformLayerMask);
            return raycastHit.collider != null;
        }
        else
        {
            Debug.LogError("No collider found");
            return false;
        }
    }
}


