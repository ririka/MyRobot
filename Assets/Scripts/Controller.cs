using UnityEngine;
public class Controller : MonoBehaviour
{
    public float wheelRadius = .5f;

    public float maxWheelAngularVelocity = 5;

    float distanceBetweenWheels;

    public Transform leftWheel;
    public Transform rightWheel;

    public Transform targetTransform;

    public float maxAngularVelocity; // maximum angular velocity the robot can have to rotate

    private void Start()
    {
        distanceBetweenWheels = Vector3.Distance(leftWheel.position, rightWheel.position);
        targetTransform.position = transform.position;
        targetTransform.rotation = transform.rotation;
        maxAngularVelocity = wheelRadius / distanceBetweenWheels * 2 * maxWheelAngularVelocity;
    }

    private void ApplyMotion(int leftWheelAction, float rightWheelAction, float deltaTime)
    {
        float angularVelocityLeftWheel = leftWheelAction * maxWheelAngularVelocity;
        float angularVelocityRightWheel = rightWheelAction * maxWheelAngularVelocity;

        float linearVelocity = wheelRadius / 2 * (angularVelocityRightWheel + angularVelocityLeftWheel);
        float angularVelocity = wheelRadius / distanceBetweenWheels * (angularVelocityRightWheel - angularVelocityLeftWheel);

        float leftWheelRotation = angularVelocityLeftWheel * deltaTime * Mathf.Rad2Deg;
        float rightWheelRotation = angularVelocityRightWheel * deltaTime * Mathf.Rad2Deg;

        leftWheel.Rotate(0, 0, leftWheelRotation, Space.Self); //rotate in the Z-axis.
        rightWheel.Rotate(0, 0, rightWheelRotation, Space.Self); //rotate in the Z-axis.

        transform.Translate(0, 0, linearVelocity * deltaTime, Space.Self);
        transform.Rotate(0, angularVelocity * deltaTime * Mathf.Rad2Deg, 0, Space.Self);
    }

    private void Update()
    {
        // 3D distance vector from the robot to the target
        var delta3D = targetTransform.position - transform.position;
        Vector2 delta = new Vector2(delta3D.x, delta3D.z);

        // angle the robot needs to turn to face the target directly
        float angle = Vector2.SignedAngle(
            new Vector2(transform.forward.x, transform.forward.z),
            delta
        );

        // check if the robot is close enough to the target
        if (delta.magnitude < 0.1)
        {
            // the orientation difference between the robot's forward direction and the target's
            float orientationDelta = Vector2.SignedAngle(
                new Vector2(transform.forward.x, transform.forward.z),
                new Vector2(targetTransform.forward.x, targetTransform.forward.z)
            );

            float turningTime = Mathf.Abs(orientationDelta) / maxAngularVelocity / Mathf.Rad2Deg;

            if (orientationDelta > 0)
                ApplyMotion(1, -1, Mathf.Min(turningTime, Time.deltaTime)); //turn left
            else
                ApplyMotion(-1, 1, Mathf.Min(turningTime, Time.deltaTime)); //turn right
        }
        else
        {
            // invert wheel movement to go in reverse if the angle is wider than 90 degrees
            int sign = Mathf.Abs(angle) < 90 ? 1 : -1;

            // remaining unsigned angle (including reverse case)
            float remainingAngle = 90 - Mathf.Abs(90 - Mathf.Abs(angle));

            if (remainingAngle < 0.01)
            {
                ApplyMotion(sign, sign, Time.deltaTime); // move straight towards the target
            }
            else
            {
                // turn until facing towards (or away from) the target
                float turningTime = Mathf.Abs(remainingAngle) / maxAngularVelocity / Mathf.Rad2Deg;

                if (angle > 0)
                    ApplyMotion(sign, -sign, Mathf.Min(turningTime, Time.deltaTime)); //turn left
                else
                    ApplyMotion(-sign, sign, Mathf.Min(turningTime, Time.deltaTime)); //turn right

                if (turningTime < Time.deltaTime)
                    ApplyMotion(sign, sign, Time.deltaTime - turningTime); // move forwards (or backwards) for the rest of the frame time
            }
        }
    }
}