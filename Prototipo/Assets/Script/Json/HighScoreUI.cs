using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using RWM;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject panelName;
    [SerializeField] GameObject HighScoreElement;
    [SerializeField] Transform elementWrapper;
    List<GameObject> uiElements = new List<GameObject>();

    [SerializeField] Transform Panel;

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
        Panel.DOScale(0.81f, 0.08f).SetEase(Ease.InOutCubic);
        soundManager.PlaySound(SoundType.SettingsOp);
    }
    public void ShowPanelName()
    { 
        panelName.SetActive(true);
    }

    public async void HidePanel()
    {
        await Panel.DOScale(0.7f, 0.08f).SetEase(Ease.InOutCubic).SetUpdate(true).AsyncWaitForCompletion();
        soundManager.PlaySound(SoundType.SettingsClos);
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
                int distance = Mathf.FloorToInt(playerData.distance);;
                texts[2].text = distance.ToString();
            }
        }
     }
}
