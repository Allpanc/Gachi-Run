using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachiWall : MonoBehaviour
{
    [SerializeField] Texture[] _dungeonMasters;
    
    void Start()
    {
        GetComponent<Renderer>().material.SetTexture("_MainTex", _dungeonMasters[Random.Range(0, _dungeonMasters.Length - 1)]);
    }
}
