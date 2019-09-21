using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text livesText;
    public Text winText;
    public Text loseText;
    private Rigidbody2D rb2d;
    private int count;
    private int lives;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        count = 0;
        lives = 3;
        winText.text = "";
        loseText.text = "";
        SetLivesText();
        SetCountText();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        { 
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText();
        }

       else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives =  lives - 1;
            SetLivesText();
        }

        if (count == 12)
        {
            transform.position = new Vector2(100f, 0f); 
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 20)
        {
            winText.text = "You won! Game created by Hannah Parks";
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            loseText.text = "You lost!";
        }
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (lives > 3)
            lives = 3;
    }

}
