using UnityEngine;
using System.Collections;

public class lasercontrol : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        var hit = coll.gameObject;
        var health = hit.GetComponent<HeroHealth>();
        if (health != null)
        {
            health.TakeDmg(10);

            Destroy(gameObject);
        }


    }
}
