using UnityEngine;
using System.Collections;

public class SkeletonAI : MonoBehaviour {

    public Animator animatorSkeleton;

    [SerializeField]
    private bool alert = false;
    [SerializeField]
    private GameObject target;

    public GameObject hero;
    public GameObject herobase;

    public float reloadtime = 1;
    public float reloadtimer = 1;


    // Use this for initialization
    void Start () {

        hero = GameObject.Find("Hero");
        herobase = GameObject.Find("herobase");

	}
	
	// Update is called once per frame
	void Update () {

        //check which should be attacked now
        if (alert)
        {
            target = hero;
        }
        else
        {
            target = herobase;
        }

        reloadtimer += Time.deltaTime;
        //check if target is in attack range
        Debug.Log(Vector3.Distance(target.transform.position, this.transform.position));

        if (Vector3.Distance(target.transform.position, this.transform.position) <= 0.6f)
        {
            if (reloadtimer >= reloadtime)
            {
                reloadtimer = 0;
                target.GetComponent<HeroHealth>().TakeDmg(20);
                if (target.transform.position.x <= this.transform.position.x)
                {
                    animatorSkeleton.SetTrigger("AtkLeft");
                }
                else
                {
                    animatorSkeleton.SetTrigger("AtkRight");
                }
            }
        }
        else
        {
            Debug.Log("axxx");
            if (target.transform.position.x <= this.transform.position.x)
            {
                animatorSkeleton.SetTrigger("WalkLeft");
            }
            else
            {
                animatorSkeleton.SetTrigger("WalkRight");
            }
            Vector3 movedir = target.transform.position - this.transform.position;
            movedir.Normalize();
            this.transform.position += movedir * Time.deltaTime*0.4f;
        }
	
	}



    void OnTriggerEnter2D(Collider2D coll)
    {

        var hit = coll.gameObject;
        var team = hit.GetComponent<HeroControl>();
        if (team != null)
        {
            alert = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        var hit = coll.gameObject;
        var team = hit.GetComponent<HeroControl>();
        if (team != null)
        {
            alert = false;
        }
    }
}
