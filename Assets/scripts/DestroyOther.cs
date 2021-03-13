using UnityEngine;

public class DestroyOther : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Player")))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * -2);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
