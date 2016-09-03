using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public float fullhealth;
    public float curhealth;

    public Animator animatorEnemy;

    public GameObject gbar;
    public GameObject hbar;

    // Use this for initialization
    void Start () {
        curhealth = fullhealth;
	}

    public void TakeDmg(float damagenum)
    {
        curhealth -= damagenum;

        gbar.transform.localScale = new Vector3((curhealth * 1f / fullhealth), gbar.transform.localScale.y, gbar.transform.localScale.z);
        gbar.transform.position = hbar.transform.position + Vector3.left * (fullhealth - curhealth) / (fullhealth) * 0.16f;

        if (curhealth <= 0) {
            StartCoroutine(diediedie());



        }

        //change UI
    }

    IEnumerator diediedie()
    {
        animatorEnemy.SetTrigger("Die");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
