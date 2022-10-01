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

    private Vector3 _beforePosition;

    public Color c;

    // Use this for initialization
    void Start()
    {
        _mask = LayerMask.GetMask("Ground");

        _draw = new Material(_shader);
        _snow = _terrain.GetComponent<MeshRenderer>().material; // tesselation shader
        _splatmap = new RenderTexture(512, 512, 0, RenderTextureFormat.ARGBFloat);
        _snow.SetTexture("_Splatmap", _splatmap);
        _beforePosition = _sphere.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!Physics.Raycast(_sphere.position, Vector3.down, out _hit, Mathf.Infinity))
        {
            _beforePosition = _sphere.position;
            return;
        }
        
        // �A�N�e�B�u�ȃ����_�[�e�N�X�`�����L���b�V�����Ă���
        var currentRT = RenderTexture.active;

        // �A�N�e�B�u�ȃ����_�[�e�N�X�`�����ꎞ�I��Target�ɕύX����
        RenderTexture.active = _splatmap;

        // Texture2D.ReadPixels()�ɂ��A�N�e�B�u�ȃ����_�[�e�N�X�`���̃s�N�Z�������e�N�X�`���Ɋi�[����
        var texture = new Texture2D(1,1);
        texture.ReadPixels(new Rect(_hit.textureCoord.x * _splatmap.width, _splatmap.height-( _hit.textureCoord.y *_splatmap.height), 1,1), 0, 0);
        texture.Apply();

        // �^���̃s�N�Z�������擾����
        c = texture.GetPixels()[0];
        
            
        // �A�N�e�B�u�ȃ����_�[�e�N�X�`�������ɖ߂�
        RenderTexture.active = currentRT;

        // ���ɓh���Ă���Ȃ琬�����Ȃ�
        if(c.r >= 0.8f)
        {
            _beforePosition = _sphere.position;
            return;
        }

        // ���{�̂𐬒�������
        var distance = Vector3.Distance(_beforePosition, _sphere.position);
        _sphere.localScale += new Vector3(distance, distance, distance) * 0.05f;
        if (_sphere.localScale.x > 30)
            _sphere.localScale = new Vector3(30, 30, 30);
        _beforePosition = _sphere.position;

        // �n�ʂ�h��
        _draw.SetVector("_Coordinates", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
        _draw.SetFloat("_Strength", _bStrength);
        _draw.SetFloat("_Size", _bSize*this.transform.localScale.x);
        RenderTexture tmp = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(_splatmap, tmp);
        Graphics.Blit(tmp, _splatmap, _draw);
        RenderTexture.ReleaseTemporary(tmp);
    }
}
