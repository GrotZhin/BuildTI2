using UnityEngine;
using TMPro;


public class RandomTextDisplay : MonoBehaviour
{
    public TextMeshProUGUI TMPJ;
    public string[] ReactionsJ;
    public TextMeshProUGUI TMPS;
    public string[] ReactionsS;
    public TextMeshProUGUI TMPG;
    public string[] ReactionsG;

    public TextMeshProUGUI TMPN;
    public string[] ReactionsN;



    void Start()
    {

        int randobJ = Random.Range(0, ReactionsJ.Length);

        TMPJ.text = ReactionsJ[randobJ];

        int randobS = Random.Range(0, ReactionsS.Length);

        TMPS.text = ReactionsS[randobS];

        int randobG = Random.Range(0, ReactionsG.Length);

        TMPG.text = ReactionsG[randobG];
        
        int randobN = Random.Range(0, ReactionsN.Length);
        
        TMPN.text = ReactionsN[randobN];

    }
    
     
}
