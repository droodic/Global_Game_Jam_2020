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
            ChangeMaterialLerpValue(DEBUG_LerpValue);
        }
    }

#endif

    [SerializeField] MeshRenderer[] _meshRenderers;
    private RepairableBehaviour _repairableBehaviour;
    private void Start()
    {
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        _repairableBehaviour = GetComponent<RepairableBehaviour>();
    }

    private void LateUpdate()
    {
#if UNITY_EDITOR
        if (!DEBUG_Activate)
        { 
#endif
            ChangeMaterialLerpValue(_repairableBehaviour.RepairedPercentage);
#if UNITY_EDITOR
        }
#endif
    }

    private void ChangeMaterialLerpValue(float LerpValue)
    {
        foreach (var meshRenderer in _meshRenderers)
        {
            foreach (var material in meshRenderer.materials)
            {
                material.SetFloat("_LerpValue", LerpValue);
            }
        }
    }
}
