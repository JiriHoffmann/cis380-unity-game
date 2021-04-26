using UnityEngine;

public class JumperController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LayerMask platformLayerMask;
    Rigidbody2D rigBod;
    BoxCollider2D boxCollider;
    CapsuleCollider2D capsCollider;
    Animator animator;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        capsCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool grounded = isGroundedCheck();
        //Debug.LogFormat("grounded {0}", grounded ? 0 : 1);
        animator.SetBool("Grounded", grounded);
    }

    private bool isGroundedCheck()
    {
        float extraHeight = 0.05f;

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
