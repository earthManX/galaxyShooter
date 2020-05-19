using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Sprite[] livesImage;
	private int score = 0;
	public Image livesImageObject;
	public Text scoreText;
	public Text playPrompt;
	public Image titleScreen;
	public GameObject playerPrefab;
	[SerializeField]
	private bool gameStarted = true; 
	private int scoreMultiplier = 1 ;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate( titleScreen , new Vector(0,0,0) , Quaternion.identity) ;
		gameStarted = true ;
		scoreText.enabled =false ; 
		livesImageObject.enabled = false ;

    }

    // Update is called once per frame
    void Update()
    {
		// Do this in a game manager script
        if( gameStarted && Input.GetKeyDown( KeyCode.Space) ){
			titleScreen.enabled = false ; 
			playPrompt.enabled = false ;
			score = 0 ;
			scoreText.text = "Score - 0";
			scoreText.enabled = true ; 
			livesImageObject.enabled = true;
			Instantiate( playerPrefab , new Vector3( 0 , -4f , 0 ) , Quaternion.identity);	
			gameStarted = false ;
		}
	}
	
	public void updateMultiplier( int multiplier ){
		scoreMultiplier = multiplier ;
	}
	
	public void updateLives(int lives){
		// Manager clases should not communicate with other scripts 
		livesImageObject.sprite = livesImage[lives];
	} 
	
	public void updateScore( int gain ){
		score = score + (gain*scoreMultiplier) ; 
		scoreText.text = "Score - " + score ; 
	}
	
	public void endGame(){
		//gameStarted = false ;
		titleScreen.enabled = true;
		scoreText.enabled = false ;
		livesImageObject.enabled = false ;
		playPrompt.enabled = true ;
		gameStarted = true;
	}
	
	public bool gameState(){
		return gameStarted;
	}

}
