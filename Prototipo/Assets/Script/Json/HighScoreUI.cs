using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject panelName;
    [SerializeField] GameObject HighScoreElement;
    [SerializeField] Transform elementWrapper;
    List<GameObject> uiElements = new List<GameObject>();



    private void OnEnable()
    {
        HighScore.onHighScoreListChanged += UpdateUI;
    }

    private void OnDisable()
    {
         HighScore.onHighScoreListChanged -= UpdateUI;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ShowPanel()
    {
        panel.SetActive(true);
    }
    public void ShowPanelName()
    { 
        panelName.SetActive(true);
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }
    private void UpdateUI(List<PlayerData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            PlayerData playerData = list[i];

            if (playerData.score > 0)
            {
                if (i >= uiElements.Count)
                {
                    var inst = Instantiate(HighScoreElement, Vector3.zero, Quaternion.identity);
                    inst.transform.SetParent(elementWrapper.transform, false);

                    uiElements.Add(inst);
                }
                var texts = uiElements[i].GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = playerData.playerName;
                texts[1].text = playerData.score.ToString();
            }
        }
     }
}
