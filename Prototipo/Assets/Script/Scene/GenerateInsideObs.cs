using UnityEngine;

public class GenerateInsideObs : MonoBehaviour
{
     public GameObject[] boxPrefab;
    private bool Spawn = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Spawn)
        {
            var random = Random.Range(0, boxPrefab.Length);
            Instantiate(boxPrefab[random].gameObject, transform.position, Quaternion.identity);
            Spawn = true;
        }
    }
}
