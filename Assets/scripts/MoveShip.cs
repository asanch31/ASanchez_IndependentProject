using UnityEngine;

public class MoveShip : MonoBehaviour
{
    private GameManager Win;


    // Start is called before the first frame update
    void Start()
    {
        Win = GameObject.Find("Player").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Win.winCondition == true)
        {

            transform.Translate(Vector3.forward * Time.deltaTime);

        }
    }
}
