using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField]
	private GameObject enemyPrefab;
	[SerializeField]
	private GameObject[] powerUps ;
	private UIManager uiManager;

    void Start()
    {
		
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		//StartCoroutine( enemySpawnCoRoutine());
		//StartCoroutine( powerUpSpawnCoroutine());

    }

    // Update is called once per frame
    void Update()
    {
       // if( uiManager.gameState() && Input.GetKeyDown( KeyCode.Space) ){
		
		//}
    }
		
	public void startSpawning(){
		StartCoroutine( enemySpawnCoRoutine());
		StartCoroutine( powerUpSpawnCoroutine());
	}
	
	private IEnumerator enemySpawnCoRoutine(){
		while( uiManager.gameState() == false ){
			Instantiate( enemyPrefab , new Vector3(Random.Range(-7.0f , 7.0f ) , 7.0f , 0) , Quaternion.identity  );
			yield return new WaitForSeconds(5.0f);
		}
	}
	
	private IEnumerator powerUpSpawnCoroutine(){
		while( uiManager.gameState() == false ){
			int randomPowerUp = Random.Range(0 , 3);
			Instantiate( powerUps[randomPowerUp] ,new Vector3( Random.Range(-7f , 7f ) , 7f, 0 ) , Quaternion.identity );
			yield return new WaitForSeconds(7.0f) ; 
		}
	}
	
}
