using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovementView : MonoBehaviour
{
    [SerializeField] private float _ballSpeed = 1;
    private Vector3 _destination;
    private bool ableToMove = false;

    private void Update()
    {
        
    }

    public void MoveAt(Vector3 destination)
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, destination, Time.deltaTime * _ballSpeed);
    }

    public void GetNewDestination(Vector3 destination)
    {
        _destination = destination;
        ableToMove = true;
    }

    private bool IsArrived(Vector3 destination) => Vector3.Distance(gameObject.transform.position, destination) < 0.02f;
}

