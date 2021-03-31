using UnityEngine;

public class RotatingMace : MonoBehaviour {
    public float speed;
    public bool isRotationClockwise = true;

    void FixedUpdate() {
        transform.Rotate(Vector3.forward * (isRotationClockwise ? 1 : -1), speed);
    }
}
