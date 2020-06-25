using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Target;

    private Vector3 startDistance;
    private Vector3 moveVec;
    
    void Start()
    {
        startDistance = transform.position - Target.position;
    }

    void Update()
    {
        moveVec = Target.position + startDistance;
        moveVec.z = 0;
        moveVec.y = startDistance.y;

        transform.position = Vector3.Lerp(transform.position, moveVec, 3f * Time.deltaTime);
    }
}
