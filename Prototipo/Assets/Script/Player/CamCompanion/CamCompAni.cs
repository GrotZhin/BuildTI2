using UnityEngine;
using UnityEngine.SceneManagement;

public class CamCompAni : MonoBehaviour
{
    public GameObject Cam;
    public Animator Canani;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Canani = Cam.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeLayersWeight();

    }
    
    public void ChangeLayersWeight()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {

            Canani.SetLayerWeight(1, 1);
            Canani.SetLayerWeight(0, 0);
        }
        else if (SceneManager.GetActiveScene().name == "MainMenu")
        {

            Canani.SetLayerWeight(1, 0);
            Canani.SetLayerWeight(0, 1);
        }

    }
}
