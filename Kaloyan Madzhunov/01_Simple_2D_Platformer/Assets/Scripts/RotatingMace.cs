using UnityEngine;

public class RotatingMace : MonoBehaviour {
    public float speed;

    void FixedUpdate() {
        transform.Rotate(Vector3.forward, speed);
    }
}
