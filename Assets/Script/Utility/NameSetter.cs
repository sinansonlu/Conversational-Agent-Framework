using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// [ExecuteInEditMode]
public class NameSetter : MonoBehaviour
{
    public AudioClip[] pp;
    public RecordPlayer rp;

    public AudioClip[] clip_e3c0;
    public TextAsset[] text_e3c0;
    public AudioClip[] clip_e3c1;
    public TextAsset[] text_e3c1;
    public AudioClip[] clip_e3c2;
    public TextAsset[] text_e3c2;
    public AudioClip[] clip_e3c3;
    public TextAsset[] text_e3c3;
    public AudioClip[] clip_e3c4;
    public TextAsset[] text_e3c4;
    public AudioClip[] clip_e3c5;
    public TextAsset[] text_e3c5;
    public AudioClip[] clip_e3c6;
    public TextAsset[] text_e3c6;
    public AudioClip[] clip_e3c7;
    public TextAsset[] text_e3c7;
    public AudioClip[] clip_e3c8;
    public TextAsset[] text_e3c8;
    public AudioClip[] clip_e3c9;
    public TextAsset[] text_e3c9;

    public AudioClip[] clip_e4c0;
    public TextAsset[] text_e4c0;
    public AudioClip[] clip_e4c1;
    public TextAsset[] text_e4c1;
    public AudioClip[] clip_e4c2;
    public TextAsset[] text_e4c2;
    public AudioClip[] clip_e4c3;
    public TextAsset[] text_e4c3;
    public AudioClip[] clip_e4c4;
    public TextAsset[] text_e4c4;
    public AudioClip[] clip_e4c5;
    public TextAsset[] text_e4c5;
    public AudioClip[] clip_e4c6;
    public TextAsset[] text_e4c6;
    public AudioClip[] clip_e4c7;
    public TextAsset[] text_e4c7;
    public AudioClip[] clip_e4c8;
    public TextAsset[] text_e4c8;
    public AudioClip[] clip_e4c9;
    public TextAsset[] text_e4c9;

    void Update()
    {
        /*
        int c = 0;
        int l = 0;
        clip_e3c0 = new AudioClip[12];
        clip_e3c0[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c0[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c +"_L" + l + "_A_TTS.wav"); l++;
        clip_e3c0[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c0[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c +"_L" + l + "_A_TTS.wav"); l++;
        clip_e3c0[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c0[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c +"_L" + l + "_A_TTS.wav"); l++;
        clip_e3c0[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c0[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c +"_L" + l + "_A_TTS.wav"); l++;
        clip_e3c0[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c0[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c +"_L" + l + "_A_TTS.wav"); l++;
        clip_e3c0[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c0[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c +"_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e3c1 = new AudioClip[12];
        clip_e3c1[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c1[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c1[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c1[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c1[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c1[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c1[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c1[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c1[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c1[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c1[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c1[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e3c2 = new AudioClip[12];
        clip_e3c2[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c2[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c2[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c2[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c2[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c2[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c2[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c2[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c2[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c2[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c2[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c2[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e3c3 = new AudioClip[12];
        clip_e3c3[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c3[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c3[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c3[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c3[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c3[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c3[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c3[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c3[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c3[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c3[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c3[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e3c4 = new AudioClip[12];
        clip_e3c4[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c4[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c4[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c4[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c4[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c4[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c4[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c4[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c4[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c4[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c4[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c4[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e3c5 = new AudioClip[12];
        clip_e3c5[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c5[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c5[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c5[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c5[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c5[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c5[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c5[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c5[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c5[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c5[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c5[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e3c6 = new AudioClip[12];
        clip_e3c6[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c6[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c6[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c6[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c6[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c6[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c6[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c6[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c6[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c6[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c6[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c6[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e3c7 = new AudioClip[12];
        clip_e3c7[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c7[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c7[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c7[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c7[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c7[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c7[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c7[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c7[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c7[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c7[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c7[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e3c8 = new AudioClip[12];
        clip_e3c8[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c8[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c8[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c8[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c8[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c8[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c8[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c8[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c8[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c8[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c8[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c8[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e3c9 = new AudioClip[12];
        clip_e3c9[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c9[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c9[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c9[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c9[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c9[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c9[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c9[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c9[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c9[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e3c9[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e3c9[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c = 0; l = 0;
        clip_e4c0 = new AudioClip[12];
        clip_e4c0[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c0[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c0[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c0[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c0[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c0[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c0[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c0[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c0[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c0[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c0[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c0[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e4c1 = new AudioClip[12];
        clip_e4c1[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c1[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c1[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c1[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c1[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c1[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c1[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c1[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c1[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c1[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c1[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c1[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e4c2 = new AudioClip[12];
        clip_e4c2[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c2[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c2[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c2[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c2[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c2[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c2[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c2[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c2[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c2[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c2[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c2[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e4c3 = new AudioClip[12];
        clip_e4c3[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c3[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c3[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c3[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c3[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c3[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c3[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c3[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c3[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c3[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c3[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c3[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e4c4 = new AudioClip[12];
        clip_e4c4[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c4[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c4[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c4[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c4[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c4[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c4[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c4[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c4[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c4[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c4[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c4[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e4c5 = new AudioClip[12];
        clip_e4c5[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c5[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c5[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c5[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c5[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c5[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c5[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c5[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c5[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c5[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c5[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c5[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e4c6 = new AudioClip[12];
        clip_e4c6[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c6[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c6[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c6[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c6[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c6[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c6[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c6[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c6[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c6[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c6[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c6[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e4c7 = new AudioClip[12];
        clip_e4c7[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c7[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c7[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c7[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c7[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c7[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c7[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c7[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c7[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c7[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c7[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c7[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e4c8 = new AudioClip[12];
        clip_e4c8[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c8[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c8[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c8[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c8[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c8[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c8[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c8[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c8[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c8[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c8[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c8[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c++; l = 0;
        clip_e4c9 = new AudioClip[12];
        clip_e4c9[0] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c9[1] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c9[2] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c9[3] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c9[4] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c9[5] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c9[6] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c9[7] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c9[8] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c9[9] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;
        clip_e4c9[10] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_TTS.wav"); l++;
        clip_e4c9[11] = (AudioClip)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_TTS.wav"); l++;

        c = 0; l = 0;
        text_e3c0 = new TextAsset[12];
        text_e3c0[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c0[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        Debug.Log(text_e3c0[1]);
        text_e3c0[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c0[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c0[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c0[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c0[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c0[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c0[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c0[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for(int i = 0; i < 12; i++) { if (text_e3c0[i] == null) text_e3c0[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\NULL.txt"); }

        c++; l = 0;
        text_e3c1 = new TextAsset[12];
        text_e3c1[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c1[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c1[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c1[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c1[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c1[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c1[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c1[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c1[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c1[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e3c1[i] == null) text_e3c1[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\NULL.txt"); }

        c++; l = 0;
        text_e3c2 = new TextAsset[12];
        text_e3c2[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c2[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c2[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c2[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c2[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c2[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c2[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c2[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c2[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c2[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e3c2[i] == null) text_e3c2[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\NULL.txt"); }

        c++; l = 0;
        text_e3c3 = new TextAsset[12];
        text_e3c3[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c3[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c3[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c3[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c3[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c3[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c3[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c3[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c3[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c3[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e3c3[i] == null) text_e3c3[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\NULL.txt"); }

        c++; l = 0;
        text_e3c4 = new TextAsset[12];
        text_e3c4[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c4[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c4[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c4[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c4[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c4[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c4[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c4[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c4[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c4[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e3c4[i] == null) text_e3c4[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\NULL.txt"); }

        c++; l = 0;
        text_e3c5 = new TextAsset[12];
        text_e3c5[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c5[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c5[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c5[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c5[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c5[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c5[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c5[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c5[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c5[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e3c5[i] == null) text_e3c5[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\NULL.txt"); }

        c++; l = 0;
        text_e3c6 = new TextAsset[12];
        text_e3c6[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c6[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c6[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c6[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c6[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c6[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c6[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c6[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c6[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c6[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e3c6[i] == null) text_e3c6[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\NULL.txt"); }

        c++; l = 0;
        text_e3c7 = new TextAsset[12];
        text_e3c7[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c7[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c7[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c7[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c7[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c7[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c7[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c7[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c7[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c7[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e3c7[i] == null) text_e3c7[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\NULL.txt"); }

        c++; l = 0;
        text_e3c8 = new TextAsset[12];
        text_e3c8[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c8[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c8[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c8[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c8[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c8[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c8[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c8[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c8[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c8[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e3c8[i] == null) text_e3c8[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\NULL.txt"); }

        c++; l = 0;
        text_e3c9 = new TextAsset[12];
        text_e3c9[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c9[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c9[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c9[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c9[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c9[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c9[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c9[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e3c9[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C0_L" + l + "_S_NLU.txt"); l++;
        text_e3c9[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\E3_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e3c9[i] == null) text_e3c9[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E3\\NULL.txt"); }

        c = 0; l = 0;
        text_e4c0 = new TextAsset[12];
        text_e4c0[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c0[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c0[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c0[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c0[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c0[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c0[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c0[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c0[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c0[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e4c0[i] == null) text_e4c0[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\NULL.txt"); }

        c++; l = 0;
        text_e4c1 = new TextAsset[12];
        text_e4c1[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c1[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c1[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c1[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c1[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c1[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c1[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c1[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c1[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c1[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e4c1[i] == null) text_e4c1[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\NULL.txt"); }

        c++; l = 0;
        text_e4c2 = new TextAsset[12];
        text_e4c2[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c2[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c2[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c2[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c2[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c2[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c2[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c2[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c2[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c2[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e4c2[i] == null) text_e4c2[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\NULL.txt"); }

        c++; l = 0;
        text_e4c3 = new TextAsset[12];
        text_e4c3[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c3[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c3[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c3[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c3[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c3[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c3[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c3[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c3[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c3[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e4c3[i] == null) text_e4c3[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\NULL.txt"); }

        c++; l = 0;
        text_e4c4 = new TextAsset[12];
        text_e4c4[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c4[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c4[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c4[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c4[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c4[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c4[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c4[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c4[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c4[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e4c4[i] == null) text_e4c4[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\NULL.txt"); }

        c++; l = 0;
        text_e4c5 = new TextAsset[12];
        text_e4c5[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c5[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c5[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c5[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c5[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c5[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c5[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c5[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c5[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c5[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e4c5[i] == null) text_e4c5[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\NULL.txt"); }

        c++; l = 0;
        text_e4c6 = new TextAsset[12];
        text_e4c6[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c6[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c6[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c6[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c6[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c6[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c6[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c6[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c6[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c6[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e4c6[i] == null) text_e4c6[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\NULL.txt"); }

        c++; l = 0;
        text_e4c7 = new TextAsset[12];
        text_e4c7[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c7[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c7[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c7[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c7[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c7[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c7[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c7[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c7[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c7[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e4c7[i] == null) text_e4c7[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\NULL.txt"); }

        c++; l = 0;
        text_e4c8 = new TextAsset[12];
        text_e4c8[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c8[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c8[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c8[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c8[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c8[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c8[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c8[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c8[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c8[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e4c8[i] == null) text_e4c8[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\NULL.txt"); }

        c++; l = 0;
        text_e4c9 = new TextAsset[12];
        text_e4c9[0] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c9[1] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c9[2] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c9[3] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c9[4] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c9[5] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c9[6] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c9[7] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        text_e4c9[8] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C0_L" + l + "_S_NLU.txt"); l++;
        text_e4c9[9] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\E4_C" + c + "_L" + l + "_A_NLU.txt"); l++;
        for (int i = 0; i < 12; i++) { if (text_e4c9[i] == null) text_e4c9[i] = (TextAsset)EditorGUIUtility.Load("Assets\\0_SOUNDDB\\E4\\NULL.txt"); }

        Debug.Log("Editor causes this Update");
        this.enabled = false;
        */
    }
}
