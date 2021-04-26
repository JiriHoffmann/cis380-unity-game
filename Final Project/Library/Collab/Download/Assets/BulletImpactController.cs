using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpactController : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        
        sprite.sortingOrder = 0;
        sprite.sortingLayerName = "Player";
        Destroy(gameObject, 0.3f);
    }

}
