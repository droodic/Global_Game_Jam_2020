using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeReadyColor : MonoBehaviour
{
    [SerializeField] private Color _readyColor = Color.green;
    [SerializeField] private GameObject _cylinder;
    private MeshRenderer _meshRenderer;
    private Color _ogColor;
    private int _PlayerID = -1;

    private void Start()
    {
        _meshRenderer = _cylinder.GetComponent<MeshRenderer>();
        _ogColor = _meshRenderer.material.GetColor("_Color");
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("COLIDE COLOR!!");
        if (other.CompareTag("Player") && _PlayerID == -1)
        {
            //Debug.Log("CHARGE COLOR!!");
            _PlayerID = other.GetInstanceID();
            _meshRenderer.material.SetColor("_BaseColor", _readyColor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetInstanceID().Equals(_PlayerID))
        {
            //Debug.Log("NO COLOR!!");
            _meshRenderer.material.SetColor("_BaseColor", _ogColor);
            _PlayerID = -1;
        }
    }

    private void OnDisable()
    {
        _meshRenderer.material.SetColor("_BaseColor", _ogColor);
    }
}
