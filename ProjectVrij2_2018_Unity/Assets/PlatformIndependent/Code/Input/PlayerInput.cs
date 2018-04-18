using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour {

	[System.Serializable]
    public class InputAction{
        public string title = "input title";
        public string axis = "axis";
        public float threshold = 0f;
        public UnityEvent onPassThreshold = null;
        public UnityEvent onStopPassThreshold = null;
        public UnityEvent onUpdate = null;
        private float lastInput = 0;

        private static InputAction m_current = null;
        public static InputAction Current{
            get{
                return m_current;
            }
        }

        private float value = 0f;
        public float Value{
            get{
                return value;
            }
        }
        public static float CurrentValue{
            get{ 
                if(Current != null){
                    return Current.value;
                }
                return 0f;
            }
        }

        public void Update(){
            m_current = this;
            value = Input.GetAxis(axis);
            if(value >= threshold && lastInput < threshold){
                onPassThreshold.Invoke();
            }
            else if(value < threshold && lastInput >= threshold){
                onStopPassThreshold.Invoke();
            }
            onUpdate.Invoke();
            lastInput = value;
        }
    }

    [SerializeField] private List<InputAction> inputActions = new List<InputAction>();


    private void Update() {
        for(int i=0; i<inputActions.Count; i++){
            inputActions[i].Update();
        }
    }


    /// <summary>
    /// An example void to display how it works.
    /// </summary>
    public void ExampleAction(){
        MonoBehaviour.print((
        InputAction.CurrentValue    //Get the current input value directly.
        + 
        InputAction.Current.Value   //Get the current input value via the current InputAction.
        ) * 0.5f);
    }


}
