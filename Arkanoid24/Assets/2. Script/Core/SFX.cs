using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFX : AudioSystem<SFX>
{
    #region Set SFX

    public AudioSource AudioSource { get; set; }
    private float volumeScale = 1.0f;
    public float VolumeScale
    {
        get
        {
            return this.volumeScale;
        }
        set
        {
            this.volumeScale = Mathf.Clamp01(value);
            SetVolume(this.volumeScale);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        this.AudioSource = GetComponent<AudioSource>();
    }

    protected override void SetVolume(float volumeScale)
    {
        SetVolume("SFX", volumeScale);
    }

    #endregion

    [Header("UI")]
    public AudioClip btnClick;

    [Header("Game")]
    public AudioClip brickHit;
    public AudioClip paddleHit;
    public AudioClip itemPickup;
    public AudioClip ballDeath;
    public AudioClip laser;

    [Header("End")]
    public AudioClip gameover;
    public AudioClip nextstage;

    public void PlayOneShot(AudioClip clip, float volumeScale = 1.0f)
    {
        this.AudioSource.PlayOneShot(clip, volumeScale);
    }
}