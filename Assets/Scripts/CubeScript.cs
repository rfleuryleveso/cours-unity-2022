using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 1;
    [SerializeField] private float prefabCount = 20;
    [SerializeField] private float distance = 20;
    [SerializeField] private GameObject target;

    [SerializeField] private GameObject prefabAInstantier;

    private List<GameObject> cubes;

    void Start()
    {
        for (int i = 0; i < prefabCount; i++)
        {
            this.cubes.Add(Instantiate(prefabAInstantier));
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var it in this.cubes.Select((x, i) => new { Value = x, Index = i }))
        {
            float idx = it.Index;
            // On calcule la position
            float angle = (((2.0f * (float)Math.PI) / (float)this.cubes.Count) * idx) +
                          Time.fixedTime * (float)Math.Cos(idx) * this.horizontalSpeed;
            float x = Mathf.Cos(angle+ (float)Math.PI / 2 * idx) * this.distance;
            float y = Mathf.Sin(angle + (float)Math.PI / 3 * idx) * this.distance;
            float z = Mathf.Sin(angle +  (float)Math.PI / 4 * idx) * this.distance;
            it.Value.transform.position = this.target.transform.position + new Vector3(
                x,
                y,
                z
            );
            
            // On fait en sorte que le cube face toujours face au joueur
            it.Value.transform.LookAt(this.target.transform);
        }
    }
}