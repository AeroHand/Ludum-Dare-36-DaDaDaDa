using UnityEngine;
using System.Collections;

public class HeroControl : MonoBehaviour {

    //public variables

    //private variables
    private Vector3 tempShootingV3;
    private Vector3 tempMovingV3;

    private float inputX;
    private float inputY;

    private bool faceleft = true;
    private bool curfaceleft = true;

    private bool moving = false;
    private bool curmoving = false;

    private bool firing = false;
    private bool curfiring = false;

    public Animator animatorHero;
    public Animator animatorGun;
    public Animator animatorSpark;

    public Transform gunposition;
    public GameObject gameobjSpark;
    public GameObject bulletprefableft;
    public GameObject bulletprefabright;
    public GameObject tempBullet;
    public GameObject danke;

    private float loadtime = 0.2f;
    private float loadtimer = 0.2f;
    public AudioClip dadada;
    public AudioSource audiosource;

    // Use this for initialization
    void Start () {
        gameobjSpark.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        //get shooting vector
        inputX = Input.GetAxis("HorizontalFire");
        inputY = Input.GetAxis("VerticalFire");
        tempShootingV3 = new Vector3(inputX, inputY, 0);
        tempShootingV3.Normalize();

        //get moving vector
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        Vector3 tempMovingV3 = new Vector3(inputX, inputY, 0);
        tempMovingV3.Normalize();

        //generate facing, left or right?

        
        
        Debug.Log(tempShootingV3.magnitude);
        if (tempShootingV3.magnitude > 0.4f)
        {
            Debug.Log("AS");
            firing = true;
            if (tempShootingV3.x >= 0)
            {
                //right
                faceleft = false;
            }
            else
            {
                //left
                faceleft = true;
            }
 
        }
        else
        {
            firing = false;
            if (tempMovingV3.magnitude > 0.4f)
            {
                //moving

                moving = true;
                if (tempMovingV3.x >= 0)
                {
                    //right
                    faceleft = false;
                }
                else
                {
                    //left
                    faceleft = true;
                }


            }
            else {
                //not moving
                moving = false;
            }
        }

        if (tempMovingV3.magnitude > 0.4f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        //adjust facing
        Debug.Log("firing");
        Debug.Log(firing);
        Debug.Log(curfiring);
        if (faceleft)
        {
            //face left
            if (curfaceleft != faceleft)
            {
                invertspark();
                //was right before
                curfaceleft = faceleft;
                if (firing)
                {
                    animatorGun.SetTrigger("fire_left");
                    gameobjSpark.SetActive(true);
                    animatorSpark.SetTrigger("left");
                }
                else
                {
                    animatorGun.SetTrigger("stop_left");
                    gameobjSpark.SetActive(false);
                }
                curfiring = firing;

                if (moving)
                {
                    animatorHero.SetTrigger("MoveLeft");
                }
                else
                {
                    animatorHero.SetTrigger("IdleLeft");
                }
                curmoving = moving;
            }
            else
            {
                //was left before
                if (firing != curfiring)
                {

                    curfiring = firing;
                    if (firing)
                    {
                        animatorSpark.SetTrigger("left");
                        gameobjSpark.SetActive(true);
                        animatorGun.SetTrigger("fire_left");
                    }
                    else
                    {
                        gameobjSpark.SetActive(false);
                        animatorGun.SetTrigger("stop_left");
                    }
                }

                if (moving != curmoving)
                {
                    curmoving = moving;
                    if (moving)
                    {
                        animatorHero.SetTrigger("MoveLeft");
                    }
                    else
                    {
                        animatorHero.SetTrigger("IdleLeft");
                    }
                }
            }
        }
        else
        {
            if (curfaceleft != faceleft)
            {
                invertspark();
                curfaceleft = faceleft;
                if (firing)
                {
                    animatorGun.SetTrigger("fire_right");
                    animatorSpark.SetTrigger("right");
                    gameobjSpark.SetActive(true);
                    
                }
                else
                {
                    animatorGun.SetTrigger("stop_right");
                    gameobjSpark.SetActive(false);
                }
                curfiring = firing;

                if (moving)
                {
                    animatorHero.SetTrigger("MoveRight");
                }
                else
                {
                    animatorHero.SetTrigger("IdleRight");
                }
                curmoving = moving;
            }
            else
            {
                if (firing != curfiring)
                {
                    curfiring = firing;
                    if (firing)
                    {
                        animatorSpark.SetTrigger("right");
                        gameobjSpark.SetActive(true);
                        
                        animatorGun.SetTrigger("fire_right");
                    }
                    else
                    {
                        gameobjSpark.SetActive(false);
                        animatorGun.SetTrigger("stop_right");
                    }
                }

                if (moving != curmoving)
                {
                    curmoving = moving;
                    if (moving)
                    {
                        animatorHero.SetTrigger("MoveRight");
                    }
                    else
                    {
                        animatorHero.SetTrigger("IdleRight");
                    }
                }
            }
        }


        //shoot

        if (tempShootingV3.y <= 0.5f && tempShootingV3.y >= -0.5f)
        {
            Quaternion rotation = Quaternion.LookRotation
             (tempShootingV3, gunposition.transform.TransformDirection(Vector3.up));
            gunposition.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }


        loadtimer += Time.deltaTime;
        if (firing)
        {

            if (loadtimer >= loadtime)
            {
                audiosource.PlayOneShot(dadada);
                //adjust the face of bullet
                Vector3 bulletdir = tempShootingV3 + Random.Range(-0.1f, 0.1f) * (new Vector3(tempShootingV3.x + tempShootingV3.y, tempShootingV3.y - tempShootingV3.x, 0));
                Instantiate(danke, gunposition.transform.position, Quaternion.identity);
                if (!faceleft)
                {
                    tempBullet = Instantiate(bulletprefabright, gameobjSpark.transform.position, Quaternion.identity) as GameObject;
                }
                else
                {
                    tempBullet = Instantiate(bulletprefableft, gameobjSpark.transform.position, Quaternion.identity) as GameObject;
                }
                Quaternion rotationbullet = Quaternion.LookRotation
                         (bulletdir, tempBullet.transform.TransformDirection(Vector3.up));
                tempBullet.transform.rotation = new Quaternion(0, 0, rotationbullet.z, rotationbullet.w);
                //add speed to bullet
                tempBullet.GetComponent<Rigidbody2D>().velocity = bulletdir * 10;
            }
        }
        //move

        this.transform.position += tempMovingV3 * Time.deltaTime;

    }

    void invertspark() {
        gameobjSpark.transform.position = gunposition.position - (gameobjSpark.transform.position - gunposition.position);
    }
}
