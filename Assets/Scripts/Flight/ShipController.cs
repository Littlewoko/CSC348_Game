using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class ShipController : MonoBehaviour
{
    [SerializeField] private Transform myForward;
    [SerializeField] private float mySpeed;
    [SerializeField] private float maxRotationSpeed;
    [SerializeField] private Transform myTarget;

    private Rigidbody2D rb2D;
    private float currentRotationSpeed = 0f;
    private float signedAngle;
    private float speedPercentage;
    private Vector2 toTarget = new Vector2();
    private Transform myTransform;

    private void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
        myTransform = transform;
    }

    private void FixedUpdate() {
        if(myTarget.position.x != myTransform.position.x && myTarget.position.y != myTransform.position.y) {
            StopMoving();
            UpdateVelocity();
            updateRotation();
        } else {
            UpdateVelocity();
            updateRotation();
        }
    }

    private void UpdateVelocity() {
        signedAngle = GetAngleToTarget();
        speedPercentage = signedAngle / 180;
        if (speedPercentage < 0) {
            speedPercentage = -speedPercentage; 
        }
        if (speedPercentage < 0.4) {
            speedPercentage = (float)0.4;
        }
        rb2D.velocity = (mySpeed * speedPercentage) * myForward.up;
    }

    private void updateRotation() {
        signedAngle = GetAngleToTarget();

        currentRotationSpeed = signedAngle / 90f;

        rb2D.angularVelocity = currentRotationSpeed * maxRotationSpeed;
    }

    private void StopMoving() {
        rb2D.velocity = 0 * myForward.up;
        currentRotationSpeed = 0;
    }

    private float GetAngleToTarget() {
        toTarget.x = myTarget.position.x - myTransform.position.x;
        toTarget.y = myTarget.position.y - myTransform.position.y;

        return Vector2.SignedAngle(myForward.up, toTarget);
    }
}
