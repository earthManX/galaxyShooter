using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
	[SerializeField]
	private float speed = 4.0f ; 
	[SerializeField]
    private GameObject enemyExplosionPrefab ;
    
	private UIManager uiManager;
	[SerializeField]
	private AudioClip audioClip;
	
	void Start()
    {
	 uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
     Destroy( this.gameObject , 7f);   
	 
    }

    // Update is called once per frame
    void Update()
    {
	   movement();
	   reSpawn();
    }
	
	private void movement(){
		transform.Translate( Vector3.down * Time.deltaTime * speed );
	}
	
	private void reSpawn(){
		if( transform.position.y < - 6.07f ){
			transform.position = new Vector3( Random.Range( -8.5f , 8.5f ) , 7.0f , 0 )  ; 
		}
	}
	
	private void OnTriggerEnter2D( Collider2D other){
		if( other.tag == "Laser"){
			if( transform.parent != null ){
			Destroy( transform.parent.gameObject );
			}
			Destroy( other.gameObject );
			GameObject instantiateAnimation = (GameObject) Instantiate( enemyExplosionPrefab , transform.position , Quaternion.identity );
			Destroy( instantiateAnimation, 2f);
			
			uiManager.updateScore(10);
			AudioSource.PlayClipAtPoint( audioClip , Camera.main.transform.position);
			Destroy( this.gameObject );
		} else if (other.tag == "Player"){
			Player player = other.GetComponent<Player>();
			if( player != null){
				player.damage() ; 
			}
			GameObject animationInstance = (GameObject) Instantiate( enemyExplosionPrefab , transform.position , Quaternion.identity );
			Destroy( animationInstance , 2f);
			
			uiManager.updateScore(15);
			AudioSource.PlayClipAtPoint( audioClip , Camera.main.transform.position);
			Destroy( this.gameObject );
		}
		
	}
	
}
