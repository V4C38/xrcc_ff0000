using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

public class SoundEmitter : MonoBehaviour
{
    public ENAudioTrack audioTrack;

    public Material trueStateMaterial;
    public Material falseStateMaterial;

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
        }

    }
}
