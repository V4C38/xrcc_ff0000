using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

public class SoundEmitter : MonoBehaviour
{
    public MusicTimelineController timelineController;
    public ENAudioTrack audioTrack;

    public Material trueStateMaterial;
    public Material falseStateMaterial;

    private MeshRenderer meshRenderer;

    public FrequencyBandAnalyser frequencyBandAnalyser;

    private Collider myCollider;
    private bool toggleState = false;
    private HashSet<Collider> overlappingColliders = new HashSet<Collider>();


    void Start()
    {
        myCollider = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();

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
        }
        else
        {
            if (meshRenderer != null)
            {
                meshRenderer.material = falseStateMaterial;
            }
        }

        if (timelineController != null)
        {
            timelineController.RequestTrackState(audioTrack, toggleState);
        }
        

    }
}
