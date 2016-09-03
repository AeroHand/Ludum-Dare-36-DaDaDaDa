using UnityEngine;
using System.Collections;

public class life : MonoBehaviour {

    public float lifetime;
    private float lifetimer;

	// Use this for initialization
	void Start () {
        lifetimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        lifetimer += Time.deltaTime;
        if (lifetimer >= lifetime) {
            Destroy(gameObject);
        }
	}
}
