using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform[] targets;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void LateUpdate()
    {
        if(targets == null || targets.Length == 0)
        {
            return;
        }

        Transform activeTarget = FindActiveTarget();
        if(activeTarget == null)
        {
            return;
        }

        Vector3 desiredPos = activeTarget.position + offset;
        desiredPos.y = transform.position.y;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothedPosition;
    }

    Transform FindActiveTarget()
    {
        foreach (Transform target in targets)
        {
            if (target.gameObject.activeInHierarchy)
            {
                return target;
            }
        }
        return null;
    }
}
