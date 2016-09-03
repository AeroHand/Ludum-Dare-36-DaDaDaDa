using UnityEngine;
using System.Collections;

public class CameraLerp : MonoBehaviour {

    public GameObject Target;
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Target.transform.position =new Vector3(Vector3.Lerp(Target.transform.position, this.transform.position, 1).x, Vector3.Lerp(Target.transform.position, this.transform.position, 1).y,-10);
	}
}
