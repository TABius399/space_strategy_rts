using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    private Ship ship = null;
    private Material[] originalMaterials = null;
    private Material[] selectedMaterials = null;
    [SerializeField]
    private Material selectedMaterial = null;
    [SerializeField]
    private MeshRenderer meshRenderer = null;

    private void Awake()
    {
        this.originalMaterials = this.meshRenderer.materials;
        this.selectedMaterials = (Material[]) this.originalMaterials.Clone();
        this.selectedMaterials[3] = selectedMaterial;
    }

    void Update()
    {
        this.ship.Tick();
        this.transform.position = this.ship.position;
        this.transform.rotation = Quaternion.AngleAxis(this.ship.direction, Vector3.up);
    }

    public void SetShip(Ship shipClass)
    {
        this.ship = shipClass;
    }

    public void GoTo(Vector3 target)
    {
        this.ship.GoTo(target);
    }


    public void OnSelect()
    {
        
        this.AssignMaterial(this.selectedMaterials);
    }

    public void OnDeselect()
    {
        this.AssignMaterial(this.originalMaterials);
    }

    private void AssignMaterial(Material[] materials)
    {
        this.meshRenderer.sharedMaterials = materials;
    }
}
