using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningArea : MonoBehaviour
{
    [SerializeField] private Transform _leftPos;
    [SerializeField] private Transform _rightPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    public Vector3 GetLeftPosition()
    {
        return _leftPos.position;
    }

    public Vector3 GetRightPosition()
    {
        return _rightPos.position;
    }
}
