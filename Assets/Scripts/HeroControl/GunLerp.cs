using UnityEngine;
using System.Collections;

public class GunLerp : MonoBehaviour {


    public GameObject Target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Target.transform.position = Vector3.Lerp(Target.transform.position, this.transform.position + (new Vector3(0, -0.13f, 0)), 1);
    }
}
