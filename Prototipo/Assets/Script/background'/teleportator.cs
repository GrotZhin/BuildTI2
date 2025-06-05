using UnityEngine;

public class MoverQuandoOutroAtinge : MonoBehaviour
{
    [Header("Refer�ncias")]
    [SerializeField] private GameObject player;           // Quem ser� monitorado
    [SerializeField] private GameObject objetoAlvo;       // Quem ser� movido

    [Header("Configura��o de Posi��o")]
    [SerializeField] private float novaPosicaoXDoAlvo ;   // Novo X do objetoAlvo


    void Update()
    {
        if (player == null || objetoAlvo == null) return;

        float posPlayer = player.transform.position.x;
        float posBuild = objetoAlvo.transform.position.x;

        if (posPlayer >= posBuild + 40f)
        {
            Vector3 novaPosicao = objetoAlvo.transform.position;
            novaPosicao.x = posBuild + novaPosicaoXDoAlvo;
            objetoAlvo.transform.position = novaPosicao;
        }
    }
}