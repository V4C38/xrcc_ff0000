using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.Specialized;

public class SoundEmitter : MonoBehaviour
{
    public ENAudioTrack audioTrack;

    public Material trueStateMaterial;
    public Material falseStateMaterial;

    public GameObject particlePrefab;
    private Vector3 particleSpawnPosition;
    public ParticleSystem[] particleSystems;

    private MeshRenderer meshRenderer;
    private AudioSource audioPlayer;

    private FrequencyBandAnalyser frequencyBandAnalyser;

    private Collider myCollider;
    private bool toggleState = false;
    private HashSet<Collider> overlappingColliders = new HashSet<Collider>();


    void Start()
    {
        myCollider = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
        frequencyBandAnalyser = GetComponent<FrequencyBandAnalyser>();
        audioPlayer = GetComponent<AudioSource>();

        if (myCollider == null)
        {
            return;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        particleSpawnPosition = other.transform.position;
        if (!overlappingColliders.Contains(other))
        {
            overlappingColliders.Add(other);
            SetToggleState(!toggleState);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (overlappingColliders.Contains(other))
        {
            overlappingColliders.Remove(other);
        }
    }

    private void SetToggleState(bool inState)
    {
        if (inState == toggleState)
        {
            return;
        }

        toggleState = inState;

        if (toggleState)
        {
            if (meshRenderer != null)
            {
                meshRenderer.material = trueStateMaterial;
            }
            if (audioPlayer != null)
            {
                audioPlayer.mute = false;
            }
            if (particlePrefab != null)
            {
                GameObject particleInstance = Instantiate(particlePrefab, particleSpawnPosition, Quaternion.identity);
                Destroy(particleInstance, 1.25f);
            }
            if (GetComponent<ParticleSystem>() != null)
            {
                GetComponent<ParticleSystem>().Play();
            }

        }
        else
        {
            if (meshRenderer != null)
            {
                meshRenderer.material = falseStateMaterial;
            }
            if (audioPlayer != null)
            {
                audioPlayer.mute = true;
            }
            if (GetComponent<ParticleSystem>() != null)
            {
                GetComponent<ParticleSystem>().Stop();
            }
        }

    }
}
