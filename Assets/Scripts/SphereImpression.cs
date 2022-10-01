using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereImpression : MonoBehaviour
{
    public Shader _shader;
    public GameObject _terrain;
    public Transform _sphere;

    [Range(0, 2)]
    public float _bSize;

    [Range(0, 1)]
    public float _bStrength;

    private Material _snow;
    private Material _draw;
    private RenderTexture _splatmap;
    private RaycastHit _hit;
    private int _mask;

    // Use this for initialization
    void Start()
    {
        _mask = LayerMask.GetMask("Ground");

        _draw = new Material(_shader);
        _snow = _terrain.GetComponent<MeshRenderer>().material; // tesselation shader
        _splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        _snow.SetTexture("_Splatmap", _splatmap);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!Physics.Raycast(_sphere.position, Vector3.down, out _hit, Mathf.Infinity))
        {
            return;
        }
        _draw.SetVector("_Coordinates", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
        _draw.SetFloat("_Strength", _bStrength);
        _draw.SetFloat("_Size", _bSize);
        RenderTexture tmp = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(_splatmap, tmp);
        Graphics.Blit(tmp, _splatmap, _draw);
        RenderTexture.ReleaseTemporary(tmp);
    }
}
