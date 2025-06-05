using UnityEngine;

public class AniTest : MonoBehaviour
{
    public Animator Ranani;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void RannaAniWdb()
    {
       
            Ranani.SetBool("IsCustom", true);
        
    }
    public void RannaAniMenu()
    {
            Ranani.SetBool("IsCustom", false);
        
    }
}
