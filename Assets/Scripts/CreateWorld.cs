using UnityEngine;
using System.Collections;

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
	Object curVine;
	void Start() {
		int[,] cells = new int[width,height]; //initialise the cells array
		for (int q = 0; q < width; q++) { //for the width of the level
			for (int w = 0; w<height; w++) { //for the height of the column
				cells[q,w] = 0; //make it empty
			}
		}
		for (int q = 0; q < width; q++){ //for the width of the level
			cells[q,0] = 1; //make the bottom a wall block
			cells[q,height-1] = 1; //make the top a wall block
		}
		for (int q = 3; q < height; q++){ //for the height
			cells[0,q] = 1; //make the left edge solid
			cells[width-1,q] = 1; //make the right edge solid
		}
		for (int q = minGap+1; q < width-minGap; q++) { //For the width, minus the barrier at beggining and end
			for (int w = 3; w<height-3; w++) { //for the height, minus the barrier at the top and bottom
				if (Random.Range(0,spawnChance) == 1){ //if a 1 in 5 chance
					int numSolid = 0;
					for (int e = -minGap; e<=0;e++){ //for minGap columns behind to this row
						for (int r = -3; r<=3;r++){ //for 3 rows above to 3 rows below
							if (cells[q+e,w+r] != 0){ //if that cell is solid
								numSolid++; //record it
							}
						}
					}
					if (numSolid == 0) { //if there are no solid blocks nearby
						int platformLength = Random.Range(minPlatform,maxPlatform); //choose a random platform length between the min and max values
						if (q + platformLength < width) { //if the platform is not going to go off the edge
							for (int e = 0; e <= platformLength; e++){ //for the length of the platform
								cells[q+e,w] = 1; //make it solid
							}
						}
					}
				}
			}
		}
		for (int q = 0; q < width; q++) { //for the width of the level
			for (int w = 0; w<height; w++) { //for the height of the level
				if (cells[q,w] == 1) { //if the cell is solid
					Instantiate(block,new Vector3(q*1f,w*1f,0f),Quaternion.identity); //place a block there
				}else if (cells[q,w] == 1) {
					curVine = Instantiate(vine,new Vector3(q*1f,w*1f,0f),Quaternion.identity); //place a vine there

				}
			}
		}
		Instantiate(player,new Vector3(1f,1f,0f), Quaternion.identity); //create the player
	}
}