using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateTile : MonoBehaviour {
	public List<List<bool>> map;
	public GameObject grassMiddle;
	public GameObject grassRight;
	public GameObject grassLeft;
	public GameObject grassBoth;
	public GameObject dirt;
	Transform cameraPos;
	Camera cameraCam;
	// Use this for initialization
	void InstantiateTiles () {
		for (int x = 1; x < map.Count-1; x++){ //for each column
			for (int y = 1; y < map[x].Count-1; y++){ //for each cell in each column
				if (map[x][y] == true){
					if (map[x][y-1] == false){ //if there is no block above it
						if (map[x-1][y] == false && map[x+1][y] == false){ //if there are no blocks on the right and left
							Instantiate(grassBoth, new Vector2(x,y),Quaternion.identity); //instantiate the double end grass block
						} else if (map[x-1][y] == false){ //if there are no blocks on the left
							Instantiate(grassLeft, new Vector2(x,y),Quaternion.identity); //instantiate the left end grass block
						} else if (map[x+1][y] == false){ //if there are no blocks on the right
							Instantiate(grassRight, new Vector2(x,y),Quaternion.identity); //instantiate the right end grass block
						} else { //if there is a block to the right and the left
							Instantiate(grassMiddle, new Vector2(x,y), Quaternion.identity);
						}
					} else if (map[x][y-1] == true){ //if there is a block above it
						Instantiate(dirt, new Vector2(x,y), Quaternion.identity);
					} else {
						Instantiate(dirt, new Vector2(x,y), Quaternion.identity);
					}
				}
			}
		}
		cameraPos = Camera.main.GetComponent<Transform>();
		cameraCam = Camera.main.GetComponent<Camera>();
	}
	void Update (){
		if (Input.GetKey(KeyCode.D)){
			cameraPos.Translate(new Vector2(1,0));
		} else if (Input.GetKey(KeyCode.A)){
			cameraPos.Translate(new Vector2(-1,0));
		} else if (Input.GetKey(KeyCode.W)){
			cameraPos.Translate(new Vector2(0,1));
		} else if (Input.GetKey(KeyCode.S)){
			cameraPos.Translate(new Vector2(0,-1));
		} else if (Input.GetKey(KeyCode.Q)){
			cameraCam.orthographicSize -= 1; 
		} else if (Input.GetKey(KeyCode.E)){
			cameraCam.orthographicSize += 1;
		}
	}
}
