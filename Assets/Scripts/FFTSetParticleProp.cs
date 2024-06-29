using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FFTSetParticleProp : MonoBehaviour
{   
    public FrequencyBandAnalyser _FFT;
    public FrequencyBandAnalyser.Bands _FreqBands = FrequencyBandAnalyser.Bands.Eight;
    public int _FrequencyBandIndex = 0;

    //public string _ColourName = "_EmissionColor";
    public Color _Col;
    public float _speedScalar = 1;

    ParticleSystem _particles = null;

    private void Start()
    {
        _particles = GetComponent<ParticleSystem>();
    }

    void Update()
    {        
        float speed = _FFT.GetBandValue(_FrequencyBandIndex, _FreqBands) * _speedScalar;
        ParticleSystem.MainModule _main = _particles.main;
        _main.simulationSpeed = speed;
    }
}
