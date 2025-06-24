using UnityEngine;

public class dontdestroyonload : MonoBehaviour
{
    public static dontdestroyonload instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // opcional, só se quiser manter entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
