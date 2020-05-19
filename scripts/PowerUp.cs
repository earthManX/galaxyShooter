using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	[SerializeField]
	private float speed = 3.0f ;
	[SerializeField]
	private int powerUpId ;
	
	private UIManager uiManager;
	[SerializeField]
	private AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
		uiManager = GameObject.Find("Canvas").GetComponent<UIManager>() ;
        Destroy(this.gameObject , 7f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( Vector3.down * Time.deltaTime * speed  );
    }
	
	private void OnTriggerEnter2D( Collider2D other){
		if( other.tag == "Player"){
			Player player = other.GetComponent<Player>();
			if( player != null){
				AudioSource.PlayClipAtPoint( audioClip , Camera.main.transform.position);	
				switch( powerUpId )
				{
					case 0:
						player.EnableTripleShot();
						break;
					case 1 : 
						player.EnableSpeedBoost();
						break;
					case 2 : 
						player.EnableShield();
						break;
					default:
						break;
				}
				uiManager.updateScore(5);
				uiManager.updateMultiplier(2);
			}
			
			Destroy( this.gameObject );
		}
	}
}
