using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class FFTSetMaterialColour : MonoBehaviour
{   
    public FrequencyBandAnalyser _FFT;
    public FrequencyBandAnalyser.Bands _FreqBands = FrequencyBandAnalyser.Bands.Eight;
    public int _FrequencyBandIndex = 0;

    //public string _ColourName = "_EmissionColor";
    public Color _Col;
    public float _StrengthScalar = 1;

    float max_encountered = 1;
    MeshRenderer _MeshRenderer;

    private void Start()
    {
        _MeshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {        
        float strength = _FFT.GetBandValue(_FrequencyBandIndex, _FreqBands) * _StrengthScalar;
        if (strength > max_encountered)
            max_encountered = strength;
        Debug.Log("Str: " + strength.ToString());
        _MeshRenderer.material.SetVector("_MinMax", new Vector2(0, max_encountered));
        _MeshRenderer.material.SetFloat("_Strength", strength);
        _MeshRenderer.material.SetColor("_Col", _Col * strength);   
        //_MeshRenderer.material.SetColor(_ColourName, _Col * strength);        
    }
}
