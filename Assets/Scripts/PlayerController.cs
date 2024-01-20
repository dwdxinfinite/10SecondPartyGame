using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour


{
public TextMeshProUGUI countText;
public TextMeshProUGUI winText;
public TextMeshProUGUI timerText;
public TextMeshProUGUI objectiveText;

public ParticleSystem pickupEffect;
 public bool playing;

public float targetTime = 12.0f ;
private int count;
private float time;
   // Start is called before the first frame update
   void Start()
   {
    count = 0;
    
	SetCountText ();
    
    time = targetTime;

    // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
    winText.text = "";
    
    objectiveText.text = "Collect 5 Balls";

    playing = (true);
   }
   // Update is called once per frame
   void FixedUpdate()
   {
    if(playing == true){
  
	  
	  int minutes = Mathf.FloorToInt(targetTime / 60F);
	  int seconds = Mathf.FloorToInt(targetTime % 60F);
	  int milliseconds = Mathf.FloorToInt((targetTime * 100F) % 100F);
	  timerText.text = minutes.ToString ("00") + ":" + seconds.ToString ("00") + ":" + milliseconds.ToString("00");
	

       float horizontal = Input.GetAxis("Horizontal");
       float vertical = Input.GetAxis("Vertical");
       Vector2 position = transform.position;
       position.x = position.x + 6.0f * horizontal * Time.deltaTime;
       position.y = position.y + 6.0f * vertical * Time.deltaTime;
       transform.position = position;
       targetTime -= Time.deltaTime;


		if (targetTime <= 0.0f) {

			timerEnded();

		}
        if (targetTime <= 10.0f) {
            disableObjective();
        }
    }
   }
   void OnTriggerEnter2D(Collider2D other) 
	{
		// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("PickUp"))
		{
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText ();
            pickupEffect.Play();
		}
	}
       void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 5) 
		{
                    // Set the text value of your 'winText'
                    winText.text = "You Win!";
                    playing = (false);


		}
	}
    void disableObjective ()
    {
        objectiveText.text = "";
    }
    void timerEnded ()
    {

		winText.text = "You Lost!";
        gameObject.SetActive (false);

	}
}
