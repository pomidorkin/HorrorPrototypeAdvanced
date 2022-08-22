using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class GetFMODSpectrumData : MonoBehaviour
{
    [FMODUnity.EventRef] public string _eventPath = null;
    public int _windowSize = 512;
    public FMOD.DSP_FFT_WINDOW _windowShape = FMOD.DSP_FFT_WINDOW.RECT;

    private FMOD.Studio.EventInstance _event; // Event istance is a copy of your event, you can play lots of copies
    private FMOD.ChannelGroup _channelGroup;
    private FMOD.DSP _dsp; // Digital Signal Processor
    private FMOD.DSP_PARAMETER_FFT _fftparam;
    [SerializeField] FMODUnity.StudioEventEmitter loudTrack;
    [SerializeField] AnimationClip animationClip;

    public static float[] bandBuffer = new float[8];
    [SerializeField] public static float[] frequencyBands = new float[8];
    float[] bufferDecrease = new float[8];

    //private float animLength = 0.05f;
    private float timecCounter = 0f;
    private bool isLoudTrackPlaying = false;


    public float[] _samples;

    private void Start()
    {
        //Prepare FMOD event
        PrepareFMODeventInstance();
        _samples = new float[_windowSize];
    }

    private void PrepareFMODeventInstance()
    {
        _event = FMODUnity.RuntimeManager.CreateInstance(_eventPath);
        _event.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        _event.start();

        FMODUnity.RuntimeManager.CoreSystem.createDSPByType(FMOD.DSP_TYPE.FFT, out _dsp);
        _dsp.setParameterInt((int)FMOD.DSP_FFT.WINDOWTYPE, (int)_windowShape);
        _dsp.setParameterInt((int)FMOD.DSP_FFT.WINDOWSIZE, _windowSize * 2);

        _event.getChannelGroup(out _channelGroup);
        _channelGroup.addDSP(0, _dsp);

    }

    private void Update()
    {
        if (timecCounter < animationClip.length && !isLoudTrackPlaying)
        {
            timecCounter += Time.deltaTime;
        }
        else if (timecCounter >= animationClip.length && !isLoudTrackPlaying)
        {
            loudTrack.Play();
            isLoudTrackPlaying = true;
        }
        GetSpectrumData();
        MakeFrequencyBands();
        BandBuffer();
    }

    private void BandBuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (frequencyBands[i] > bandBuffer[i])
            {
                bandBuffer[i] = frequencyBands[i];
                bufferDecrease[i] = 0.005f;
            }
            if (frequencyBands[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
        }
    }

    private void GetSpectrumData()
    {
        System.IntPtr _data;
        uint _length;

        _dsp.getParameterData(2, out _data, out _length);
        _fftparam = (FMOD.DSP_PARAMETER_FFT)Marshal.PtrToStructure(_data, typeof(FMOD.DSP_PARAMETER_FFT));


        if (_fftparam.numchannels == 0)
        {
            _event.getChannelGroup(out _channelGroup);
            _channelGroup.addDSP(0, _dsp);
            //Debug.Log("wait I'm not ready yet!");
        }
        else if (_fftparam.numchannels >= 1)
        {
            for (int s = 0; s < _windowSize; s++)
            {
                float _totalChannelData = 0f;
                for (int c = 0; c < _fftparam.numchannels; c++)
                    _totalChannelData += _fftparam.spectrum[c][s];
                _samples[s] = _totalChannelData / _fftparam.numchannels;
            }
            //Debug.Log("working with: " + fftparam.numchannels + " channels here baby!");
        }
    }

    private void MakeFrequencyBands()
    {
        // 22050Hz / 512 = 43Hz per sample

        /*
        20 - 60 Hz
        60 - 250 Hz
        250 - 500 Hz
        500 - 2000 Hz
        2000 - 4000 Hz
        4000 - 6000 Hz
        6000 - 20000 Hz
        */

        int counter = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }
            for (int ii = 0; ii < sampleCount; ii++)
            {
                average += _samples[counter] * (counter + 1);
                counter++;
            }

            average /= counter;
            frequencyBands[i] = average * 10;
        }

    }
}
