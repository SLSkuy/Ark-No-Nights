using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitZoneCheck : MonoBehaviour
{
    public Transform playerPosition;
    
    [SerializeField] private Material _edgeMaterial;
    private MeshRenderer _edgeMeshRenderer;

    [Header("Shader Reference")]
    [SerializeField] private string player = "_PlayerPosition";

    private void Awake()
    {
        _edgeMeshRenderer = GetComponent<MeshRenderer>();
        _edgeMaterial = new Material(_edgeMaterial);
        _edgeMeshRenderer.material = _edgeMaterial;
    }

    private void Update()
    {
        if (playerPosition)
        {
            _edgeMaterial.SetVector(player, playerPosition.position);
        }
    }
}
