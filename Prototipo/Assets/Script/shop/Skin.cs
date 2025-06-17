using UnityEngine;

public class skin : MonoBehaviour
{

    [System.Serializable]
    public class Skin
    {
        public string nome;
        public Material hatMaterial;
        public Material bodyMaterial;
        public Mesh hatMesh;
        public Mesh bodyMesh;
        public int preco;
        public bool comprada;
    }
}
