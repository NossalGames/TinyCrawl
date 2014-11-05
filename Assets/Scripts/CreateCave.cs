using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateCave : MonoBehaviour
{
	public GameObject player; //the player object
	public GameObject block; //the basic floor object
	public int width; //width of the map
	public int height; //height of the map
	public int xBorder; //minimum border on the x axis
	public int yBorder; //minimum border on the y axis
	public int deathRequire; //number of enpty cells around the miner for it to die
	public float minSize;
	public int minCleanup;
	public bool hollowed;
	public bool cleanUp;
	void Start ()
	{
		float minCells = (width*height)/minSize; //minimum cave size
		int numVoid = 0;
		List<List<bool>> cells = new List<List<bool>> (); //initalise the 2d bool List
		while (numVoid<minCells){
			cells.Clear();
			for (int q = 0; q<width; q++) { //for the width of the map we have given
				List<bool> innerCell = new List<bool> (); //create a new bool List for that column
				for (int w = 0; w<height; w++) { //for the height of the map we have given 
					innerCell.Add (true); //add the row and make it filled
				}
				cells.Add (innerCell); // add the column to the map list
			}
			List<List<int>> miners = new List<List<int>> (); //initalise the overaching miners List
			List<int> miner = new List<int>(); //initalise the single miner list
			miner.Add (Random.Range(xBorder+1, width-xBorder)); //add the x cord for the miner
			miner.Add (Random.Range(yBorder+1, width-xBorder)); //add the y cord for the miner
			miner.Add (5); //add the action for the miner
			miners.Add (miner); //add the miner List to the overaching miners List
			while (miners.Count > 0) { //while the miners List is not empty
				for (int q = miners.Count-1; q>=0; q--) { //for all the miners
					int numEmpty = 0; //reset the number of empty squares around the miner
					for (int x = -1; x <= 1; x++) { //start one column behind and go until one column infront
						for (int y = -1; y <= 1; y++) { //start one row above and go until one row below
							if ((miners[q][0] + x < width-xBorder && miners[q][1] < height-yBorder) && (miners[q][0] + x > xBorder && miners[q][1] > yBorder)) { //if that row is inside the borders
								if (!cells[miners[q][0]+x][miners[q][1]+y]) {//and if it is empty
									numEmpty++; //increment the number of empty cells by one
								}
							} else {
								numEmpty++; //increment the number of empty cells by one
							}
						}
					}
					if (numEmpty >= deathRequire){ //if the number of empty squares is equal to or more than the number needed to kill the miner
						miners.RemoveAt(q); //kill it
					} else {
						if (miners[q][2] == 1 && miners[q][0] < width-xBorder){ // if the action is move right and we are not at the right border
							miners[q][0]++;// move right
						} else if (miners[q][2] == 2 && miners[q][1] < height-yBorder){//if the action is move down and we are not at the bottom border
							miners[q][1]++;// move down
						} else if (miners[q][2] == 3 && miners[q][1] > xBorder){//if the action is move left and we are not at the left border
							miners[q][0]--;// move left
						} else if (miners[q][2] == 4 && miners[q][1] > yBorder){//if the action is move up and we are not at the top border
							miners[q][1]--; //move up
						} else if (miners[q][2] == 5){
							miners.Add (new List<int>());
							miners[miners.Count-1].Add (miners[q][0]);
							miners[miners.Count-1].Add (miners[q][1]);
							miners[miners.Count-1].Add (Random.Range(1,5)); //pick a direction to move in
						}
						miners[q][2] = Random.Range(1,6); //pick a new action
						cells[miners[q][0]][miners[q][1]] = false; //remove the cell the miner is on
					}
				}
			}
			for (int x = 0; x < width; x++){
				for (int y = 0; y < height; y++){
					if (!cells[x][y]){
						numVoid++;
					}
				}
			}
		}
		for (int x = 1; x<width-1; x++) { //for the width of the map
			for (int y = 1; y<height-1; y++) { //for the height of the map
				numVoid = 0; //reset the number of empty cells
				if (cells[x][y]) { //if the cell is not void
					for (int q = -1; q <=1; q++){
						for (int w = -1; w <= 1; w++){
							if (!cells[x+q][y+w]) { //if the cell is not void
								numVoid++;
							}
						}
					}
					bool spawnBlock = true;
					if (hollowed && numVoid == 0){
						spawnBlock = false;
					}
					if (cleanUp && numVoid >= minCleanup){
						spawnBlock = false;
					}
					if (spawnBlock){
						Instantiate (block, new Vector2 (x, y), Quaternion.identity); //create an actual block there
					}
				}
			}
		}
		bool playerSpawned = false;
		for (int x = xBorder; x < width-xBorder; x++){
			for (int y = yBorder; y < height-yBorder; y++){
				if (((!cells[x][y]&&!cells[x+1][y])&&(!cells[x][y+1]&&!cells[x+1][y+1]))&&(!cells[x][y+2]&&!cells[x+1][y+2])){
					Instantiate(player,new Vector2(x+1f,y+1.5f),Quaternion.identity);
					playerSpawned = true;
					break;
				}
			}
			if (playerSpawned){
				break;
			}
		}
	}
}
