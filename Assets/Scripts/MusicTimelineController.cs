using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public enum ENAudioTrack { None, Vocals, Bass, Drums, Kick, FX, Synth }

public class TrackMuteController : MonoBehaviour
{
    private PlayableDirector playableDirector;
    private TimelineAsset timelineAsset;

    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        if (playableDirector != null)
        {
            timelineAsset = (TimelineAsset)playableDirector.playableAsset;
        }
        MuteAll();
    }

    public void MuteAll()
    {
        foreach (var track in timelineAsset.GetOutputTracks())
        {
            if (track is AudioTrack audioTrack)
            {
                SetTrackMute(audioTrack, true);
            }
        }
    }

    public void RequestTrackState(ENAudioTrack InAudioTrack, bool InState)
    {
        if (timelineAsset == null)
        {
            return;
        }


        foreach (var track in timelineAsset.GetOutputTracks())
        {
            if (track is AudioTrack audioTrack)
            {
                switch (InAudioTrack)
                {
                    case ENAudioTrack.None:
                        break;
                    case ENAudioTrack.Vocals:
                        if (audioTrack.name == "Vocals") SetTrackMute(audioTrack, InState);
                        break;
                    case ENAudioTrack.Bass:
                        if (audioTrack.name == "Bass") SetTrackMute(audioTrack, InState);
                        break;
                    case ENAudioTrack.Drums:
                        if (audioTrack.name == "Drums") SetTrackMute(audioTrack, InState);
                        break;
                    case ENAudioTrack.Kick:
                        if (audioTrack.name == "Kick") SetTrackMute(audioTrack, InState);
                        break;
                    case ENAudioTrack.FX:
                        if (audioTrack.name == "FX") SetTrackMute(audioTrack, InState);
                        break;
                    case ENAudioTrack.Synth:
                        if (audioTrack.name == "Synth") SetTrackMute(audioTrack, InState);
                        break;
                }
            }
        }
    }

    private void SetTrackMute(AudioTrack audioTrack, bool mute)
    {
        foreach (var clip in audioTrack.GetClips())
        {
            var audioPlayableAsset = (AudioPlayableAsset)clip.asset;
            if (audioPlayableAsset != null)
            {
                var binding = playableDirector.GetGenericBinding(audioPlayableAsset) as AudioSource;
                if (binding != null)
                {
                    binding.mute = mute;
                }
            }
        }
    }
}
