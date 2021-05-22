using UnityEngine;

//invisble wall that destroys asteroids that spawn
public class DestroyOther : MonoBehaviour
{
   // private GameManager playerCtrl;

    void Start()
    {
        //playerCtrl = GameObject.Find("Player").GetComponent<GameManager>();
    }


    private void OnTriggerEnter(Collider other)
    {   //if object touches ceilings, object destroyed

     if (other.gameObject.CompareTag("hazard"))
        {
            Destroy(other.gameObject);
        }

    }
}
