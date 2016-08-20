using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform player;
    public float smooth; //0.5f
    private Vector2 velocidade;

	// Use this for initialization
	void Start () {
        velocidade = new Vector2(0.5f, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 novaPosicao2D = Vector2.zero;
        novaPosicao2D.x = Mathf.SmoothDamp(transform.position.x, player.position.x + 2f, ref velocidade.x, smooth);
        novaPosicao2D.y = Mathf.SmoothDamp(transform.position.y, player.position.y + 2.3f, ref velocidade.y, smooth);

        Vector3 novaPosicaoCamera = new Vector3(novaPosicao2D.x, novaPosicao2D.y, transform.position.z);

        transform.position = Vector3.Slerp(transform.position, novaPosicaoCamera, Time.time);
    }
}
