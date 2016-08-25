using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform player;
    public float smooth; //0.5f
    private Vector2 velocity;

	Vector3 position;

	// Use this for initialization
	void Start () {
		position = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		position.x = Mathf.SmoothDamp(position.x, player.position.x + 2f, ref velocity.x, smooth);
		position.y = Mathf.SmoothDamp(position.y, player.position.y + 2.3f, ref velocity.y, smooth);

		transform.position = position;
    }
}
