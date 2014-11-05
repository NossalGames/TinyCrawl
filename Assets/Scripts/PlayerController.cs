using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	Rigidbody2D play;
	Transform cam;
	BoxCollider2D box;
	public float forceStrength;
	SpriteRenderer image;
	// Use this for initialization
	void Start () {
		play = GetComponent<Rigidbody2D>();
		cam = Camera.main.GetComponent<Transform>();
		box = GetComponent<BoxCollider2D>();
		image = GetComponent<SpriteRenderer>();
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.D)){
			play.AddForce(new Vector2(forceStrength,0));
		}if (Input.GetKey(KeyCode.A)){
			play.AddForce(new Vector2(-forceStrength,0));
		}if (Input.GetKey(KeyCode.W)){
			play.AddForce(new Vector2(0,forceStrength));
		}if (Input.GetKey(KeyCode.S)){
			play.AddForce(new Vector2(0,-forceStrength));
		}if (Input.GetKeyDown (KeyCode.LeftControl)){
			box.size = new Vector2(2,1);
		}if (Input.GetKeyUp (KeyCode.LeftControl)){
			box.size = new Vector2(2,3);
		}if (Input.GetKeyDown (KeyCode.LeftShift)){
			forceStrength *= 2;
		}if (Input.GetKeyUp (KeyCode.LeftShift)){
			forceStrength /= 2;
		}
		cam.position = new Vector3(play.position.x, play.position.y,-1f);
	}
}
