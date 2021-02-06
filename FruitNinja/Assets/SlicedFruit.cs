using UnityEngine;

public class SlicedFruit : MonoBehaviour
{

    private void Awake() {
        Sliced();
    }

    private void Sliced() {
        Rigidbody[] rb = transform.GetComponentsInChildren<Rigidbody>();

        foreach (var r in rb) {
            r.transform.rotation = Random.rotation;
            r.AddExplosionForce(Random.Range(500, 1000), transform.position, 5);
        }
    }
}
