using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisMesh : MonoBehaviour
{
    [SerializeField] private List<GameObject> _debrisGO;
    [SerializeField] private float _rSizeMin = 0.5f;
    [SerializeField] private float _rSizeMax = 1.5f;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        if (_debrisGO.Count == 0)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        var rIndex = Random.Range(0, _debrisGO.Count);
        for (int i = 0; i < _debrisGO.Count; i++)
        {
            
            _debrisGO[i].SetActive(i == rIndex);
            if (i == rIndex)
            {
                _meshRenderer = _debrisGO[i].GetComponent<MeshRenderer>();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        var randomSize = new Vector3(Random.Range(_rSizeMin, _rSizeMax), Random.Range(_rSizeMin, _rSizeMax), Random.Range(_rSizeMin, _rSizeMax));
        var randomRotation = Quaternion.Euler(Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f), Random.Range(-180.0f, 180.0f));
        var rColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        _meshRenderer.material.SetColor("_BaseColor", rColor);
        transform.localScale = randomSize;
        transform.rotation = randomRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
