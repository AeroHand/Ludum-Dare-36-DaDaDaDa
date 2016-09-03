using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeroHealth : MonoBehaviour {

    public float fullhealth;
    public float curhealth;

    public RectTransform UIhealthBar;

    // Use this for initialization
    void Start()
    {
        curhealth = fullhealth;
    }

    public void TakeDmg(float damagenum)
    {
        curhealth -= damagenum;

        UIhealthBar.offsetMax = new Vector2(-270 * (fullhealth - curhealth) / fullhealth, UIhealthBar.offsetMax.y);


        if (curhealth <= 0)
        {
            GameManager.Instance.lose();

        }

        //change UI
    }
}
