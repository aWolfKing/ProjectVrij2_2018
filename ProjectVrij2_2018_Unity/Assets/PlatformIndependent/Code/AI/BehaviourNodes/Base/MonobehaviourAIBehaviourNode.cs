using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonobehaviourAIBehaviourNode : MonoBehaviour {   

    public virtual IEnumerator ExecuteNode(MonobehaviourAIBehaviourGraph caller) {
        yield return 0;
    }
    
}

