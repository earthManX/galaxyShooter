using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	private GameObject laserPrefab;
	
	[SerializeField]
	private GameObject tripleShotPrefab;
	
	[SerializeField]
    private GameObject playerExplosionPrefab ;
	
	[SerializeField]
	private GameObject shieldGameObject  ; 
	
	[SerializeField]
	private GameObject[] engineFaliure;
	private bool leftEngine;
	
	private float canFire = 0.0f ;
	
	[SerializeField]
	private float fireRate = 0.25f ;
	[SerializeField]	
    public float speed = 4.0f;
	
	public bool didPowerUp = false ;
	public bool speedPowerUp = false;
	public bool shieldPowerUp = false ;
	
	public int lives = 3	 ; 
	
	private UIManager uiManager;
	private SpawnManager spawnManager;
	private AudioSource audioSource;
	void Start()
    {
		//if(Input.GetKeyDown( KeyCode.Space)){
	    //transform.position= new Vector3( 0 , -4f , 0);
		//}
		uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
		if( uiManager != null){
			uiManager.updateLives(lives);
		}
		if(spawnManager != null){
			spawnManager.startSpawning();
		}
		audioSource = GetComponent<AudioSource>();
		leftEngine = false;
    }

    // Update is called once per frame
    void Update()
    {
		movement(	);
		
		if( Input.GetKeyDown( KeyCode.Space)  || Input.GetMouseButton(0) ){
			shoot();
		}
		
    }
	
	private void movement(){
		
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		
		if( speedPowerUp ){
			transform.Translate( Vector3.Scale(new Vector3( 1, 1 , 0 ) , new Vector3( horizontalInput , verticalInput , 0 )) * Time.deltaTime * speed * 1.5f );
		}
        else{
			transform.Translate( Vector3.Scale(new Vector3( 1, 1 , 0 ) , new Vector3( horizontalInput , verticalInput , 0 )) * Time.deltaTime * speed  );
		}
		// time between last and current frame.
		
		if( transform.position.y > 1 ){
			transform.position = new Vector3( transform.position.x , 1 , 0 );
		}else if( transform.position.y < -4.09f){
			transform.position = new Vector3( transform.position.x , -4.09f , 0 );
		}
		if( transform.position.x > 8.4f || transform.position.x < -8.7f ){
			transform.position = new Vector3( -1 * Math.Sign( transform.position.x ) * 8.3f , transform.position.y, 0 );
		}
		
	}
	
	private void shoot(){
			
		if( Time.time > canFire ){
			audioSource.Play(); 
			canFire = Time.time + fireRate;
				
			if( didPowerUp ){
					Instantiate( tripleShotPrefab , transform.position , Quaternion.identity	);
				//didPowerUp = false;
			} else {
				Instantiate( laserPrefab , transform.position + new Vector3( 0 , 1.59f , 0 ) , Quaternion.identity	);
			}
		}
	}
	
	public void EnableTripleShot(){
		didPowerUp = true ;
		StartCoroutine(TripleShotCoolDown());
	}
	
	public IEnumerator TripleShotCoolDown(){
		yield return new WaitForSeconds( 4.0f );
		uiManager.updateMultiplier(1);
		didPowerUp = false ;
	}
	
	public void EnableSpeedBoost(){
		speedPowerUp = true ;
		StartCoroutine(SpeedBoostCoolDown());
	}
	
	public IEnumerator SpeedBoostCoolDown(){
		yield return new WaitForSeconds( 4.0f );
		uiManager.updateMultiplier(1);
		speedPowerUp = false ;
	}
	
	public void EnableShield(){
		shieldPowerUp = true ;
		shieldGameObject.SetActive(true);
		StartCoroutine(ShieldCoolDown());
	}
	
	public IEnumerator ShieldCoolDown(){
		yield return new WaitForSeconds( 10.0f );
		uiManager.updateMultiplier(1);
		shieldPowerUp = false ;
		shieldGameObject.SetActive(false);

	}
	
	public void damage(){
		if(!shieldPowerUp){
			lives--;
			uiManager.updateLives(lives);
			
			if(leftEngine == false ){
				engineFaliure[0].SetActive(true);
				
			}else{
				engineFaliure[1].SetActive(true);
			}
			leftEngine = !leftEngine;
			
			if( lives < 1 ){
			Instantiate( playerExplosionPrefab , transform.position , Quaternion.identity );
			uiManager.endGame();
			Destroy( this.gameObject);
			} 
		}
		shieldPowerUp = false ;
		shieldGameObject.SetActive(false);
	}
	
}