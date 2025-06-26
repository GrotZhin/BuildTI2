using UnityEngine;
using DG.Tweening;

public class TutoTrigger : MonoBehaviour
{
    Tutorial tutorial;
    
    public GameObject TutoMng;
    public GameObject Tutobox;
    public CanvasGroup tu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorial = GameObject.Find("Tuto1").GetComponent<Tutorial>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            TutoMng.SetActive(true);
            Time.timeScale = Mathf.Lerp(1,0.2f, 6);
            tu.DOFade(1,0.1f).SetUpdate(true);
       
            Destroy(Tutobox);
        }

    }
}
