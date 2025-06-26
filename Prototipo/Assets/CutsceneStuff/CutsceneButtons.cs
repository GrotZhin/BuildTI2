using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.SceneManagement;
public class CutsceneButtons : MonoBehaviour
{
    public GameObject Blocker;


    public CanvasGroup ani;
    public CanvasGroup fade;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Blocker.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void breakblocker()
    {
        Blocker.SetActive(false);
        ani.DOFade(1, 0.5f).SetUpdate(true);
    }

    public async void Menu()
    {
        await fade.DOFade(1, 1f).SetUpdate(true).AsyncWaitForCompletion();
        SceneManager.LoadScene("MainMenu");
    }
    
    
}
