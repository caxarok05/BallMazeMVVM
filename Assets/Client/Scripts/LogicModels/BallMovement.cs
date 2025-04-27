using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement
{
    private readonly IInputService _inputService;

    public BallMovement(IInputService inputService)
    {
        _inputService = inputService;
    }
}
