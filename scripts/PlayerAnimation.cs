using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	
	private Animator anim;
 
	void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if( Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
			anim.SetBool("TurnLeft" , true );
			anim.SetBool("TurnRight" , false );
		}else if( Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) ){
			anim.SetBool("TurnLeft",  false );
			anim.SetBool("TurnRight" , false );
		}
		if( Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) ){
			anim.SetBool("TurnLeft" , false );
			anim.SetBool("TurnRight" , true );
		}else if( Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) ){
			anim.SetBool("TurnLeft",  false );
			anim.SetBool("TurnRight" , false );
		}
    }
}
