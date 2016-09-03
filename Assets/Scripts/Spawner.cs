using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject Skeleton;
    public GameObject Eyeball;
    public GameObject GhostFire;

    private float spawnTimer=20;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= 20) {
            spawnTimer = 0;
            StartCoroutine(spawnbytime());
            
        }
	}
    IEnumerator spawnbytime()
    {
        Instantiate(Skeleton, this.transform.position + new Vector3(Random.Range(0, 4), Random.Range(0, 4), 0), Quaternion.identity);
        yield return null;
        Instantiate(GhostFire, this.transform.position + new Vector3(Random.Range(0, 4), Random.Range(0, 4), 0), Quaternion.identity);
        yield return null;
        Instantiate(Eyeball, this.transform.position + new Vector3(Random.Range(0, 4), Random.Range(0, 4), 0), Quaternion.identity);
    }
}
