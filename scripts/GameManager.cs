using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool gameOver = true ;
	public GameObject player;
	
	void Update(){
	
	if(gameOver){
		if(Input.GetKeyDown(KeyCode.Space)){
			Instantiate( player , new Vector3( 0 , -4f , 0 ) , Quaternion.identity);
			gameOver = false ; 
		}
	}
	}
}
