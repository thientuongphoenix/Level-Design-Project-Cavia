using System.Collections;
using UnityEngine;

public class CubeRoll : MonoBehaviour
{
    public float rollSpeed = 5f;
    private bool isRolling = false;

    void Update()
    {
        if (isRolling) return;

        if (Input.GetKeyDown(KeyCode.W))
            StartCoroutine(Roll(Vector3.forward));
        else if (Input.GetKeyDown(KeyCode.S))
            StartCoroutine(Roll(Vector3.back));
        else if (Input.GetKeyDown(KeyCode.A))
            StartCoroutine(Roll(Vector3.left));
        else if (Input.GetKeyDown(KeyCode.D))
            StartCoroutine(Roll(Vector3.right));
    }

    IEnumerator Roll(Vector3 direction)
    {
        isRolling = true;

        float remainingAngle = 90;
        float rotationAngle = 0;
        float radius = 0.5f; // half size of the cube (assuming cube size = 1 unit)
        Vector3 anchor = transform.position + (Vector3.down + direction) * radius;
        Vector3 axis = Vector3.Cross(Vector3.up, direction);

        while (remainingAngle > 0)
        {
            float angle = Mathf.Min(Time.deltaTime * rollSpeed * 90, remainingAngle);
            transform.RotateAround(anchor, axis, angle);
            rotationAngle += angle;
            remainingAngle -= angle;
            yield return null;
        }

        // Snap to grid (avoid floating point error)
        Vector3 position = transform.position;
        transform.position = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), Mathf.Round(position.z));

        isRolling = false;
    }
}
