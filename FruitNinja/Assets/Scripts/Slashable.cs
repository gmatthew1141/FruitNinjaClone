using UnityEngine;

public class Slashable : MonoBehaviour {

    private Rigidbody2D rb;
    private GameManager gameManager;
    private float velocityUp;

    [SerializeField] private int index;
    [SerializeField] private float pointsGranted;
    [SerializeField] private bool bomb;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        Invoke("DestroyAfter", 5f);
        ShootUp();
    }

    protected void Slashed() {
        Debug.Log("Fruit slashed");

        // game over if player hit a bomb
        if (bomb) {
            gameManager.GameOver();
            return;
        }

        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, 5f);
        Instantiate(gameManager.slashedPrefabs[index], spawnPos, transform.rotation);
        // destroy gameobject
        Destroy(gameObject);
        // add points to score
        gameManager.AddScore(pointsGranted);
    }

    protected void DestroyAfter() {
        Destroy(gameObject);
    }

    protected void ShootUp() {
        //rb.AddForce(Vector3.up * velocityUp, ForceMode.Impulse);
        velocityUp = Random.Range(gameManager.minVelocityUp, gameManager.maxVelocityUp);
        rb.velocity = Vector3.up * velocityUp + Vector3.right * Random.Range(-5, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Slasher s = collision.GetComponent<Slasher>();

        if (s == null) {
            return;
        }

        Slashed();
    }
}
