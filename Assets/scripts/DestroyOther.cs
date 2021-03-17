using UnityEngine;

//invisble wall that destroys asteroids that spawn
public class DestroyOther : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {   //if object touches ceilings, object destroyed
        
            Destroy(other.gameObject);
        
    }
}
