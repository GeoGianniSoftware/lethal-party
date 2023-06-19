using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScript : MonoBehaviour
{
    [Header("Area Basics")]
    public string areaName;
    public Color areaColor;
    public Vector3 areaDimensions;
    public Vector3 areaOffset;

    [Header("Area Data")]
    public bool employOnly;
    public bool connectiveArea;

    static Vector3 origin;
    BoxCollider COL;
    MapManager MM;
    // Start is called before the first frame update
    void Start()
    {
        SetupCollider();
        MM = FindObjectOfType<MapManager>();
        if(!connectiveArea)
            MM.areas.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupCollider() {
        
        COL = gameObject.AddComponent<BoxCollider>();
        COL.center = areaOffset;
        COL.size = areaDimensions;
        COL.isTrigger = true;
    }

    private void OnDrawGizmos() {
        Gizmos.color = areaColor;
        Gizmos.DrawWireCube(transform.position + areaOffset, areaDimensions);
        Gizmos.DrawSphere(transform.position + areaOffset, .1f);
        Gizmos.DrawLine(transform.position + areaOffset + (Vector3.right * .5f), transform.position + areaOffset - (Vector3.right * .5f));
        Gizmos.DrawLine(transform.position + areaOffset + (Vector3.forward * .5f), transform.position + areaOffset - (Vector3.forward * .5f));
    }

}
