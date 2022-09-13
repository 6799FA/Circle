using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour {

    public static UiManager Instance { get; private set; }
    
    [SerializeField] UiAnimationGenerator[] actions = null;

    private void Start() {
        Instance = this;
    }

    public void Pause() {
        Time.timeScale = 0f;
    }

    public void ExitPause() {
        Time.timeScale = 1f;

    }

    public void Reset() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void StartAnimation(int i) {
        actions[i].Action();
    }

    public void StartAnimation(string aniName) {

        for (int i = 0; i < actions.Length; i++) {

            if (actions[i]._actionName == aniName)
                actions[i].Action();

        }

    }

    public void GoNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoScene(int i) {
        SceneManager.LoadScene(i);
    }

    public void OpenWeb(string link) {
        Application.OpenURL(link);
    }

}
