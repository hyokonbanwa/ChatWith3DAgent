using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using VRM;
using DG.Tweening;

public class CharacterBehaivar : MonoBehaviour
{

    private Animator animator;

    [SerializeField]
    private GameObject audioObject;
    private AudioSource audioSource;

    public delegate void AudioFinishCallback();
    private AudioFinishCallback audioFinishCallback;

    private FaceController faceController;

    private AutoBlink autoBlink;

    private bool speaking = false;
    // Start is called before the first frame update
    void Start()
    {
        faceController = this.gameObject.GetComponent<FaceController>();
        autoBlink = this.gameObject.GetComponent<AutoBlink>();
        audioSource = audioObject.GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        audioFinishCallback += () => { };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeFace(BlendShapeKey key, float startValue, float endValue, float duration)
    {
        faceController.ChangeFace(key, startValue, endValue, duration);
    }

    public void SetBlinkMax(float blinkMax)
    {
        autoBlink.SetBlinkMax(blinkMax);
    }
    public async UniTask Speak()
    {
        if (speaking == true) return;
        speaking = true;
        //Debug.Log("ON");
        animator.SetBool("Speaking", true);
        audioSource.Play();
        await AudioFinished(audioSource);
        animator.SetBool("Speaking", false);
        audioFinishCallback();
        //Debug.Log("OFF");
        speaking = false;

    }

    public void SetAudioCLip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
    }

    public void OnSpeakFinished(AudioFinishCallback callback)
    {
        audioFinishCallback += callback;
    }

    private async UniTask AudioFinished(AudioSource audioSource)
    {
        while (true)
        {
            await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
            if (!audioSource.isPlaying)
            {
                //Debug.Log("audioSource再生終了");
                break;
            }
        }
    }
}
