using UnityEngine;

//invisble wall that destroys asteroids that spawn
public class DestroyOther : MonoBehaviour
{
    private GameConditions playerCtrl;

    void Start()
    {
        playerCtrl = GameObject.Find("Player").GetComponent<GameConditions>();
    }


    private void OnTriggerEnter(Collider other)
    {   //if object touches ceilings, object destroyed


        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("bullet2"))
        {
        }


        else
        {
            Destroy(other.gameObject);
        }

    }
}
