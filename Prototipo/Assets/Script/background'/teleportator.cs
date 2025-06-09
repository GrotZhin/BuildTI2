using UnityEngine;

public class MoverQuandoOutroAtinge : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GameObject player;           // Quem será monitorado
    [SerializeField] private GameObject objectAim;       // Quem será movido

    [Header("Configuração de Posição")]
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