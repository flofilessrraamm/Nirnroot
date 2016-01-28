using UnityEngine;
using System.Collections;

public class BannerGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SpriteRenderer banner = GetComponent<SpriteRenderer>();
    
        banner.enabled= false;

    }
	
	// Update is called once per frame
	void Update () {
    
	}
    // Makes a banner appear
    public void BannerAppear()
    {
        SpriteRenderer banner = GetComponent<SpriteRenderer>();
        banner.enabled = true;
    }

    // Makes a banner disappar
    public void BannerDisappear()
    {
        SpriteRenderer banner = GetComponent<SpriteRenderer>();
        banner.enabled = false;
    }
}
