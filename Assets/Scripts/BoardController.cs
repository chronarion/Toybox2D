using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {    
    public int rows = 4;
    public GameObject blackTile;
    public GameObject whiteTile;

	// Use this for initialization
	void Start () {
        addTiles();
    }

    void addTiles()
    {
        Renderer r = GetComponent<Renderer>();
       
        float height = r.bounds.size.y;
        
        float tileHeight = height / rows;

        for (int i = 0; i < rows; i++)
        {
            float y = (i * tileHeight) + r.bounds.min.y + tileHeight/2;
            float x = 0; //-width/2;
            GameObject tile = Instantiate(blackTile, new Vector3(x, y, 0f), Quaternion.identity);
            tile.transform.localScale = new Vector3(transform.localScale.x, 1,1);
            tile.transform.SetParent(transform.parent);
        }        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
