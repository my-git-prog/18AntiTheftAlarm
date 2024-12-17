using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheftMover : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    [SerializeField] private float _speed = 1f;

    private void Update()
    {
        float horizontal = Input.GetAxis(Horizontal);
        transform.Translate(Vector2.right * horizontal * _speed * Time.deltaTime);
    }
}
