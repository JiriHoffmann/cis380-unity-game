
using UnityEngine;
public class EnemyBulletController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigBod;
    private SpriteRenderer sprite;
    void Start()
    {
        rigBod = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5.0f);

        sprite = GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 0;
        sprite.sortingLayerName = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        rigBod.velocity = rigBod.transform.right * 20f;
    }


    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerController player = hitInfo.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(15, player.transform.position.x < transform.position.x ? 1 : -1);
        }
        Destroy(gameObject);
        Instantiate(Resources.Load("BulletImpact"), transform.position, transform.rotation);

    }
}


