using UnityEngine;
using System.Collections;

public class GameControlScript : MonoBehaviour
{
    public static GameControlScript instance { get; private set; }

    GameObject target;

    public bool isOriginal;
    
    void Awake()
    {
        GameControlScript OG = GameObject.Find("GameController").GetComponent<GameControlScript>();
        if (instance != null && instance != this || OG.isOriginal && OG != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        isOriginal = true;
    }
    void Start()
    {

    }
    void Update()
    {

    }
}


//This object is a singleton. It can only exist in one instance and won't ever get destroyed even if the level is reloaded/changed.