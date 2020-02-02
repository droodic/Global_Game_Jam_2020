using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixMaterialLerpValue : MonoBehaviour
{

#if UNITY_EDITOR
    [Header("Debug")]
    public bool DEBUG_Activate = false;
    [Range(0.0f,1.0f)]
    public float DEBUG_LerpValue;

    private void Update()
    {
        if (DEBUG_Activate)
        {
            foreach (var meshRenderer in _meshRenderers)
            {
                foreach (var material in meshRenderer.materials)
                {
                    material.SetFloat("_LerpValue", DEBUG_LerpValue);
                }
            }
        }
    }
#endif

    [SerializeField] MeshRenderer[] _meshRenderers;

    private void Start()
    {
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }
}
