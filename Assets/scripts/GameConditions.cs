using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameConditions : MonoBehaviour

    //script where game is managed and controlled
    // player has to find keys(rock samples) and safely leave
    // player has health and can be damaged by enemies and asteroids
{
    //collect items and finish and health variables
    private int key;
    private int finish;
    public int health;
    int rockSamples;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI FindText;
    public GameObject findKey;
    public GameObject foundKey;
    public GameObject Lose;
    public GameObject Win;
    public GameObject finishKey;

    // Start is called before the first frame update
    void Start()
    {
        // Set the count to zero 
        key = 0;
        rockSamples = 4;
        finish = 0;

        

        FindKeyText();
        Health(); 
        //Foundfinish();
        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank

        //findKey.SetActive(true);
        Lose.SetActive(false);
        foundKey.SetActive(false);
        Win.SetActive(false);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("key"))
        {
            other.gameObject.SetActive(false);
            // Add one to the score variable 'count'
            rockSamples--;
            key = key + 1;
            

            // Run the 'SetCountText()' function (see below)
            FindKeyText();
        }
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("hazard"))
        {
            //random damage 
            int randomDMG = Random.Range(2, 8);
            health = health - randomDMG;
            print("this is an enemy");
            Health();
            if (other.gameObject.CompareTag("enemy"))
            {
                transform.Translate(Vector3.forward * -3);
            }
            //gameManager.PositionPlayer();
        }
        if (other.gameObject.CompareTag("ship"))
        {
            FoundFinish();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        finishKey.SetActive(false);


    }
    void FindKeyText()
    {

        if (key == 4)
        {
            // Set the text value of your 'winText'
            FindText.text = "";
            foundKey.SetActive(true);
            
            finish = finish + 1;

        }

        else
        {
            foundKey.SetActive(false);
            FindText.text = "Samples Left: " + rockSamples.ToString();
        }

    }
    void FoundFinish()
    {

        if (key > 3 && health > 0)
        {

            Win.SetActive(true);
            foundKey.SetActive(false);
            //gameManager.Level1();
            //SceneManager.LoadScene(1);
        }
        else
        {
            
            FindText.text = "Samples Left: " + health.ToString();

        }
    }
    void Health()
    {
        healthText.text = "Health: " + health.ToString();
        if (health <= 0)
        {
            Lose.SetActive(true);   

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
