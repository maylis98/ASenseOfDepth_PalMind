using UnityEngine;

public class FollowMoveVibrate : MonoBehaviour
{
    public Transform target;

    [SerializeField]
    private float vibAmount = 8;

    [SerializeField]
    private float smoothSpeed = 0.125f;

    [SerializeField]
    private Vector3 offset;
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
     
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition + Random.insideUnitSphere * (Time.deltaTime * vibAmount);

        transform.LookAt(target);
    }


}
