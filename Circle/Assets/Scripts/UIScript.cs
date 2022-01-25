using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{

    [SerializeField] Image[] Panels = null;
    [SerializeField] Text[] Texts = null;
    [SerializeField] Image[] Gauge = null;

    static UIScript singletone;

    public static UIScript GetUIScript() {
        if (!singletone)
            singletone = GameObject.FindWithTag("UI").GetComponent<UIScript>();

        return singletone;
    }

    public void ChangeText(int textNum, string textContent) {
        Texts[textNum].text = textContent;
    }

    public void OpenPanel(int panelNum) {
        Panels[panelNum].gameObject.SetActive(true);
    }

    public void ClosePanel(int panelNum)
    {
        Panels[panelNum].gameObject.SetActive(false);
    }

    public void GaugeImage(int idx, float now, float max) {
        Gauge[idx].fillAmount = now / max;
    }

    public void ChangeTimeScale(float changeTimeScale) {
        Time.timeScale = changeTimeScale;
    }

    public void GotoScene(int sceneNum) {
        SceneManager.LoadScene(sceneNum);
    }
}
