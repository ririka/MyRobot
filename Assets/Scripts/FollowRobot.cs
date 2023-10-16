using UnityEngine;

public class FollowRobot : MonoBehaviour
{
    public GameObject robot;
    Vector3 offset;
    void Start()
    {
        offset = transform.position - robot.transform.position;
    }

    void Update()
    {
        //we update the camera pos to be topdown and follow our robot
        transform.position = robot.transform.position + offset;
    }
}
