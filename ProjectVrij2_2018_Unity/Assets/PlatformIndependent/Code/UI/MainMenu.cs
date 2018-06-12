using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    [SerializeField] private string gameSceneName = "map2";
    [SerializeField] private Text text = null;
    [SerializeField] private string startInstructionText = "Press 'Start' or any key to face death.";
    [SerializeField] private List<string> loadingTips = new List<string>() { "" };
    private bool isLoading = false;


    private void Start() {
        this.text.text = this.startInstructionText;
    }


    public void StartGame(){

        StartCoroutine(LoadScene());

    }

    private IEnumerator LoadScene(){
        if(!this.isLoading){
            this.isLoading = true;

            this.text.text = "";

            if(this.loadingTips.Count > 0){
                this.text.text = this.loadingTips[Random.Range(0, this.loadingTips.Count - 1)];
            }

            var a = SceneManager.LoadSceneAsync(this.gameSceneName, LoadSceneMode.Single);
            a.allowSceneActivation = false;
            var wait = new WaitForFixedUpdate();
            string txt = this.text.text;
            int p = 0;
            float w = 0;
            do {
                yield return wait;
                w += Time.fixedDeltaTime;
                if(w >= 1f/3){
                    w = 0;
                    p++;
                    string t = txt;
                    for(int i=0; i<(p%3);i++){
                        t += ".";
                    }
                    this.text.text = t;
                }
                if(a.progress >= 0.9f){
                    break;
                }
            }
            while(true);
            a.allowSceneActivation = true;
        }
    }



    private void Update() {
        if(Input.anyKey){
            this.StartGame();
        }
    }

}
