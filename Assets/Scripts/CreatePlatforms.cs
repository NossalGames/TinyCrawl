using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatePlatforms : MonoBehaviour {
	public int length;
	public GameObject ground;
	// Use this for initialization
	void Start () {
		List<List<bool>> cells = new List<List<bool>>(); //initalise the cells List
		for (int q = 0; q<length*2+1; q++) { //for the width of the map we have given
			List<bool> innerCell = new List<bool> (); //create a new bool List for that column
			for (int w = 0; w<length*2+1; w++) { //for the height of the map we have given 
				innerCell.Add (false); //add the row and make it filled
			}
			cells.Add (innerCell); // add the column to the map list
		}
		List<List<int>> path = new List<List<int>>(); //make a new List for the path
		int x = length+1;
		int y = length+1;
		for (int q = 0; q<length; q++){ //loop for the number of times we have set
			int xPos = 0; //create the relation for the x position
			int yPos = 0; //create the relation for the y position
			int b = 0; //start the infinite loop breaker
			int dir = 0; //set the direction to 0
			do{
				b++;
				dir = Random.Range(1,5); //pick a random number from 1,2,3 and 4
				switch (dir){
				case 1: //if the dir is 1
					xPos = 0; //set the x relation to 0
					yPos = -1; //set the y relation to -1
					break; //end the switch statment
				case 2: //if the dir is 2
					xPos = 0; //set the x relation to 0
					yPos = 1; //set the y relation to 1
					break; //end the switch statment
				case 3: //if the dir is 3
					xPos = -1; //set the x relation to -1
					yPos = 0; //set the y relation to 0
					break; //end the switch statment
				case 4: //if the dir is 4
					xPos = 1; //set the x relation to 1
					yPos = 0; //set the y relation to 0
					break; //end the switch statment
				}
			} while (! cells[x+xPos][y+yPos]);
			x = x + xPos;
			y = y + yPos;
			cells[x][y] = true;
			List<int> segment = new List<int>();
			segment.Add (x);
			segment.Add (y);
			path.Add(segment);
		}
		for (int q = 0; q<path.Count; q++){
			Instantiate(ground,new Vector2(path[q][0],path[q][1]),Quaternion.identity);
		}
	}
}
