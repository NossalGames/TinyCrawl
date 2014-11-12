using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateWorld : MonoBehaviour
{
	public int width;
	public int height;
	public int minGap;
	public int maxGap;
	public int minPlatform;
	public int maxPlatform;
	public int spawnChance;
	public GameObject block;
	public GameObject vine;
	public GameObject player;
	void Start() {
		List<List<bool>> cells = new List<List<bool>>();
		for (int q = 0; q<width; q++) { //for the width of the map we have given
			List<bool> innerCell = new List<bool> (); //create a new bool List for that column
			for (int w = 0; w<height; w++) { //for the height of the map we have given 
				innerCell.Add (false); //add the row and make it filled
			}
			cells.Add (innerCell); // add the column to the map list
		}
		for (int q = minGap+1; q < width-minGap; q++) { //For the width, minus the barrier at begining and end
			for (int w = 3; w<height-3; w++) { //for the height, minus the barrier at the top and bottom
				if (Random.Range(0,spawnChance) == 1){ //if a 1 in 5 chance
					int numSolid = 0;
					for (int e = -minGap; e<=0;e++){ //for minGap columns behind to this row
						for (int r = -3; r<=3;r++){ //for 3 rows above to 3 rows below
							if (cells[q+e][w+r] == true){ //if that cell is solid
								numSolid++; //record it
							}
						}
					}
					if (numSolid == 0) { //if there are no solid blocks nearby
						int platformLength = Random.Range(minPlatform,maxPlatform); //choose a random platform length between the min and max values
						if (q + platformLength < width) { //if the platform is not going to go off the edge
							for (int e = 0; e <= platformLength; e++){ //for the length of the platform
								cells[q+e][w] = true; //make it solid
							}
						}
					}
				}
			}
		}
		CreateTile tiles = GetComponent<CreateTile>();
		tiles.map = cells;
		BroadcastMessage("InstantiateTiles",SendMessageOptions.DontRequireReceiver);
	}
}