using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    Player player;
    public Image batery;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        batery.fillAmount = 0;
      
    }


    void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            batery.fillAmount = 1;
        }
    }
    public void Boost()
    {
        player.speed.x *= 0.5f;
        batery.fillAmount = 0;

    }
}
