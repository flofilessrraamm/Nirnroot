using System.Collections.Generic;
using UnityEngine;




public class HexScript : MonoBehaviour {

    public Vector3 coordinates { get; set; }

    public Vector2 size { get; set; }

    Color colorNormal, colorOvered;

    bool overing;

    float quadrant;

    float angle;

    bool hasBanner = false;

    public GameObject banner;


    // Use this for initialization, after the hex is given his properties
    public void Instantiate ()
    {
        size = GetComponent<SpriteRenderer>().sprite.rect.size / 100;

        //invisible alpha for Hexes centers by default
        colorNormal = gameObject.GetComponent<SpriteRenderer>().color;
        colorNormal.a = 0.1f;
        colorOvered = gameObject.GetComponent<SpriteRenderer>().color;
        colorOvered.a = 0.3f;
        gameObject.GetComponent<SpriteRenderer>().color = colorNormal;

    }

    // Update is called once per frame
    void Update()
    {
        if (overing)
        {
            FindOveredQuadrant();
        }
    }

    public void CreateCoordinates(int i, int j)
    {

        coordinates = new Vector3(/*x*/i - ((j - (j % 2)) / 2),/*y*/-(i - ((j - (j % 2)) / 2)) - j,/* */j);
        gameObject.name = "Hex " + (i - ((j - (j % 2)) / 2))+ ", " + (-(i - ((j - (j % 2)) / 2)) - j) + ", " + (j);
    }

 
    /*
      IN: an hex HexScript  || OUT: bool isNeighbor
      This function takes an hex as an argument.
      When you call this function you it will return
      a boolean that indicates if the hex in the argument
      is an immediate neighbor of the hex from which the
      function is called.
    */
    public bool IsNeighbor(HexScript hex)
    {
        bool isNeighbor = false;

        if(
           (hex.coordinates.x == coordinates.x + 1 && hex.coordinates.y == coordinates.y - 1 && hex.coordinates.z == coordinates.z) ||
           (hex.coordinates.x == coordinates.x - 1 && hex.coordinates.y == coordinates.y + 1 && hex.coordinates.z == coordinates.z) ||
           (hex.coordinates.x == coordinates.x && hex.coordinates.y == coordinates.y + 1 && hex.coordinates.z == coordinates.z - 1) ||
           (hex.coordinates.x == coordinates.x && hex.coordinates.y == coordinates.y - 1 && hex.coordinates.z == coordinates.z + 1) ||
           (hex.coordinates.x == coordinates.x + 1 && hex.coordinates.y == coordinates.y && hex.coordinates.z == coordinates.z - 1) ||
           (hex.coordinates.x == coordinates.x - 1 && hex.coordinates.y == coordinates.y && hex.coordinates.z == coordinates.z + 1)
           )
        {
            isNeighbor = true;
        }

        return isNeighbor;
    }


    /*
    IN: HexScript || OUT: int distance
    this function will return the number
    of hex between the hex in the argument
    and the hex from which the function is called.
    The number is an integer.
    */
    public int hexDistance(HexScript hex)
    {
        return (int)((Mathf.Abs(coordinates.x - hex.coordinates.x) + Mathf.Abs(coordinates.y - hex.coordinates.y) + Mathf.Abs(coordinates.z - hex.coordinates.z))/2);
    }

    /*

    */

    public List<HexScript> neighborList() {
        List<HexScript> list = new List<HexScript>();
        list.Add(GameObject.Find("Hex " + coordinates.x + (1) + ", " + coordinates.y + (-1) + ", " + coordinates.z + (0)).GetComponent<HexScript>());
        list.Add(GameObject.Find("Hex " + coordinates.x + (1) + ", " + coordinates.y + (0) + ", " + coordinates.z + (-1)).GetComponent<HexScript>());
        list.Add(GameObject.Find("Hex " + coordinates.x + (0) + ", " + coordinates.y + (1) + ", " + coordinates.z + (-1)).GetComponent<HexScript>());
        list.Add(GameObject.Find("Hex " + coordinates.x + (-1) + ", " + coordinates.y + (1) + ", " + coordinates.z + (0)).GetComponent<HexScript>());
        list.Add(GameObject.Find("Hex " + coordinates.x + (-1) + ", " + coordinates.y + (0) + ", " + coordinates.z + (1)).GetComponent<HexScript>());
        list.Add(GameObject.Find("Hex " + coordinates.x + (0) + ", " + coordinates.y + (-1) + ", " + coordinates.z + (1)).GetComponent<HexScript>());

        return list;
    }
        



    public void OnMouseEnter()
    {
        gameObject.GetComponent<SpriteRenderer>().color = colorOvered;
        overing = true;
    }

    public void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = colorNormal;
        overing = false;
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (hasBanner == true)
            {
                banner.GetComponent<BannerGenerator>().BannerDisappear();
                hasBanner = false;
                Debug.Log(hasBanner);
            }
            else
            {
                banner.GetComponent<BannerGenerator>().BannerAppear();
                hasBanner = true;
                Debug.Log(hasBanner);
            }
        }
    }
        void FindOveredQuadrant()
    {
        angle = Vector2.Angle(Vector2.up, new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y));
            if ((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x) > 0)
                angle = 360 - angle;
        quadrant = Mathf.Floor(angle / 60);
        Debug.Log(quadrant);
    }
}
