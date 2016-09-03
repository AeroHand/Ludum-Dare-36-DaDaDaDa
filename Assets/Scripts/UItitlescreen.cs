using UnityEngine;
using System.Collections;

public class UItitlescreen : MonoBehaviour {

    public void playbtn()
    {
        GameManager.Instance.replay();
    }

    public void exist() 
    {
        GameManager.Instance.Exit();
    }
}
