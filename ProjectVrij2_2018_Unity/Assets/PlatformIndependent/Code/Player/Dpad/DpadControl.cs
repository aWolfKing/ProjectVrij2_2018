using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DpadControl : MonoBehaviour {

	public void OnDpadLeft(){
        MonoBehaviour.print("dpad left");
    }
    
    public void OnDpadRight(){
        MonoBehaviour.print("dpad right");
    }

    public void OnDpadUp(){
        MonoBehaviour.print("dpad up");
    }

    public void OnDpadDown(){
        MonoBehaviour.print("dpad down");
    }

}
