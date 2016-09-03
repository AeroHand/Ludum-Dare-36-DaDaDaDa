using UnityEngine;
using System.Collections;

public class EyeballAI : MonoBehaviour {


    public Animator animatorSkeleton;

    [SerializeField]
    private bool alert = false;
    [SerializeField]
    private GameObject target;

    public GameObject hero;
    public GameObject herobase;

    public float reloadtime = 1.5f;
    public float reloadtimer = 1;

    public GameObject laser;
    private GameObject laserentity;
    private Vector3 tempShootingV3;

    public int bulletspd;

    public float atkrange;
    // Use this for initialization
    void Start()
    {

        hero = GameObject.Find("Hero");
        herobase = GameObject.Find("herobase");

    }

    // Update is called once per frame
    void Update()
    {

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

        if (Vector3.Distance(target.transform.position, this.transform.position) <= atkrange)
        {
            if (reloadtimer >= reloadtime)
            {
                reloadtimer = 0;
                StartCoroutine(shootlaser());

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
            this.transform.position += movedir * Time.deltaTime * 0.4f;
        }

    }

    IEnumerator shootlaser()
    {
        yield return new WaitForSeconds(0.2f);
        if (target.transform.position.x <= transform.position.x)
        {
            laserentity=Instantiate(laser, transform.position + Vector3.left*0.3f, Quaternion.identity)as GameObject;

        }
        else
        {
            laserentity=Instantiate(laser, transform.position - Vector3.left * 0.3f, Quaternion.identity)as GameObject;

        }
        tempShootingV3=target.transform.position-this.transform.position;
        tempShootingV3.Normalize();
        Quaternion rotation = Quaternion.LookRotation
             (tempShootingV3, laserentity.transform.TransformDirection(Vector3.up));
        laserentity.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        laserentity.GetComponent<Rigidbody2D>().velocity = tempShootingV3 * bulletspd;
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
