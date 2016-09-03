using UnityEngine;
using System.Collections;

public class HeroBullet : MonoBehaviour {

    public GameObject baozha;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        var hit = coll.gameObject;
        var health = hit.GetComponent<EnemyHealth>();
        if (health != null)
        {
            Instantiate(baozha, this.transform.position, Quaternion.identity    );
            health.TakeDmg(10);
            
            
        }
        Destroy(gameObject);

    }
}
