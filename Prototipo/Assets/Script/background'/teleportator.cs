using UnityEngine;

public class MoverQuandoOutroAtinge : MonoBehaviour
{
    [Header("Refer�ncias")]
    [SerializeField] private GameObject player;           // Quem ser� monitorado
    [SerializeField] private GameObject objectAim;       // Quem ser� movido

    [Header("Configura��o de Posi��o")]
    [SerializeField] private float newPosition;   // Novo X do objectAim


    void Update()
    {
        if (player == null || objectAim == null) return;

        float posPlayer = player.transform.position.x;
        float posBuild = objectAim.transform.position.x;

        if (posPlayer >= posBuild + 40f)
        {
            Vector3 novaPosicao = objectAim.transform.position;
            novaPosicao.x = posBuild + newPosition;
            objectAim.transform.position = novaPosicao;
        }
    }
}