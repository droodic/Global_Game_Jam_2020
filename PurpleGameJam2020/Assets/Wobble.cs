using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    private Vector3 _position;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    // Start is called before the first frame update
    void Start()
    {
        _position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = _position + (Vector3.up * Mathf.Sin((Time.time + gameObject.GetInstanceID()) * _speed) * _distance);
    }
}
