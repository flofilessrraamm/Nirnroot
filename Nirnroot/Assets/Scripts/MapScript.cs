using UnityEngine;
using System.Collections;

public class MapScript : MonoBehaviour {

    public GameObject MasterHex;
    public CameraScript camScript;
    private Vector2 HexSize;

    const int WIDTH_MAP = 15, HEIGHT_MAP = 11;
    const float MAP_PADDING = 1f;

    // Use this for initialization
    void Start () {
        HexSize = MasterHex.GetComponent<SpriteRenderer>().sprite.rect.size / 100;
        CreateMap();
    }
	

    //Creates the Hex Map
    void CreateMap()
    {
        for (int j = 0; j < HEIGHT_MAP; j++)
        {
            for (int i = 0; i < WIDTH_MAP; i++)
            {
                CreateHex(i, j);

            }
        }
        camScript.normalSize = WIDTH_MAP * Camera.main.aspect + MAP_PADDING;
        camScript.normalPos = new Vector2((WIDTH_MAP) * HexSize.x / 2, (HEIGHT_MAP) * HexSize.y * 3 / 8);
        camScript.ResetCamera();
        //camScript.InitiateFraming("Hex 2, -5, 3", "Hex 7, -14, 7");
    }

    //Builds an Hex (called for each Hex)
    void CreateHex(int i, int j)
    {
        GameObject Hex = Instantiate(MasterHex);
        Hex.transform.position = new Vector2(((HexSize.x) * i + ((j) % 2) * (HexSize.x/2)),HEIGHT_MAP - ( j * (3*HexSize.y/4)));
        Hex.GetComponent<HexScript>().CreateCoordinates(i, j);
        Hex.tag = "Hex";
        Hex.GetComponent<HexScript>().Instantiate();
        Hex.GetComponent<HexScript>().size = HexSize;
    }
    
}
