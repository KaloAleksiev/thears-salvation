using UnityEngine;

public class RotatingMace : MonoBehaviour {
    public float speed = 1.4f;
    public bool isRotationClockwise = true;

    void FixedUpdate() {
        transform.Rotate(Vector3.forward * (isRotationClockwise ? 1 : -1), speed);
    }
}
