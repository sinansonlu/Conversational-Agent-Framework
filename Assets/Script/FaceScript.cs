using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceScript : MonoBehaviour {

    [HideInInspector] public bool freeze;
    [HideInInspector] public float expressFactor;

    [HideInInspector] public bool talkingNow;

    [HideInInspector] public SkinnedMeshRenderer meshRenderer;
    [HideInInspector] public SkinnedMeshRenderer meshRendererEyes;
    [HideInInspector] public SkinnedMeshRenderer meshRendererEyelashes;
    [HideInInspector] public SkinnedMeshRenderer meshRendererBeards;
    [HideInInspector] public SkinnedMeshRenderer meshRendererMoustaches;

    private int[] shapeKey_mapBeard;
    private int[] shapeKey_mapMoustache;
    private int shapeKeyCount;
    
    private float[] values;
    private float[] valuesDisp;
    [HideInInspector] public float[] targets;
    private float[] speeds;

    private bool blinkingNow;
    private float blinkTimer;
    [HideInInspector] public float blinkCloseSpeed;
    [HideInInspector] public float blinkOpenSpeed;

    private static readonly int blink_left = 0;
    private static readonly int blink_right = 1;
    private static readonly int browsDown_left = 2;
    private static readonly int browsDown_right = 3;
    private static readonly int browsIn_left = 4;
    private static readonly int browsIn_right = 5;
    private static readonly int browsOuterLower_left = 6;
    private static readonly int browsOuterLower_right = 7;
    private static readonly int browsUp_left = 8;
    private static readonly int browsUp_right = 9;
    private static readonly int cheekPuff_left = 10;
    private static readonly int cheekPuff_right = 11;
    private static readonly int eyesWide_left = 12;
    private static readonly int eyesWide_right = 13;
    private static readonly int frown_left = 14;
    private static readonly int frown_right = 15;
    private static readonly int jawBackward = 16;
    private static readonly int jawForeward = 17;
    private static readonly int jawRotateY_left = 18;
    private static readonly int jawRotateY_right = 19;
    private static readonly int jawRotateZ_left = 20;
    private static readonly int jawRotateZ_right = 21;
    private static readonly int jawDown = 22;
    private static readonly int jawLeft = 23;
    private static readonly int jawRight = 24;
    private static readonly int jawUp = 25;
    private static readonly int lowerLipDown_left = 26;
    private static readonly int lowerLipDown_right = 27;
    private static readonly int lowerLipIn = 28;
    private static readonly int lowerLipOut = 29;
    private static readonly int midmouth_left = 30;
    private static readonly int midmouth_right = 31;
    private static readonly int mouthDown = 32;
    private static readonly int mouthNarrow_left = 33;
    private static readonly int mouthNarrow_right = 34;
    private static readonly int mouthOpen = 35;
    private static readonly int mouthUp = 36;
    private static readonly int mouthWhistle_left = 37;
    private static readonly int mouthWhistle_right = 38;
    private static readonly int noseScrunch_left = 39;
    private static readonly int noseScrunch_right = 40;
    private static readonly int smileLeft = 41;
    private static readonly int smileRight = 42;
    private static readonly int squint_left = 43;
    private static readonly int squint_right = 44;
    private static readonly int toungeUp = 45;
    private static readonly int upperLipIn = 46;
    private static readonly int upperLipOut = 47;
    private static readonly int upperLipUp_left = 48;
    private static readonly int upperLipUp_right = 49;

    [Header("Expression Parameters")]
    [HideInInspector] public float exp_happy;
    [HideInInspector] public float exp_sad;
    [HideInInspector] public float exp_angry;
    [HideInInspector] public float exp_shock;
    [HideInInspector] public float exp_disgust;
    [HideInInspector] public float exp_fear;

    private Renderer bodyRenderer;

    public bool blinkOff = false;

    void Update()
    {
        if(!blinkOff) Blink();
        ExpressionPass();

        WrinklePass();

        if(talkingNow)
        {
            VisemesPass();
        }

        if (blinkOff)
        {
         /*   targets[browsOuterLower_left] = 8;
            targets[browsOuterLower_right] = 8;
            targets[browsUp_left] = 25;
            targets[browsUp_right] = 25;
            targets[mouthOpen] = 33;
            targets[smileLeft] = 100;
            targets[smileRight] = 100;
            targets[squint_left] = 27;
            targets[squint_right] = 27;
            targets[toungeUp] = 7;
            targets[upperLipIn] = 35;
            //targets[mouthOpen] = 25;
            //targets[toungeUp] = 20;

            /*v[10] = 0.5f;
            v[13] = 0.2f; // 0.25f;
            v[14] = 0.1f; // 0.25f;
            //v[11] = 0.05f; // 0.5f;
            //v[12] = 0.02f;

            targets[cheekPuff_left] = 10 * v[1];
            targets[cheekPuff_right] = 10 * v[1];
            targets[jawBackward] = 10 * v[2];
            targets[lowerLipDown_left] = 25 * v[3]
                + 15 * v[4]
                + 15 * v[5]
                + 40 * v[6]
                + 15 * v[7]
                + 30 * v[8]
                + 5 * v[9]
                + 10 * v[11]
                + 30 * v[12];
            targets[lowerLipDown_right] = 25 * v[3]
                + 15 * v[4]
                + 15 * v[5]
                + 40 * v[6]
                + 15 * v[7]
                + 30 * v[8]
                + 5 * v[9]
                + 10 * v[11]
                + 30 * v[12];
            targets[lowerLipIn] = 100 * v[1]
                + 75 * v[2];
            targets[lowerLipOut] = 20 * v[6]
                + 20 * v[7]
                + 20 * v[11]
                + 30 * v[12]
                + 10 * v[13]
                + 30 * v[14];
            targets[midmouth_left] = 45 * v[13]
                + 70 * v[14];
            targets[midmouth_right] = 45 * v[13]
                + 70 * v[14];
            targets[mouthUp] = 10 * v[1]
                + 5 * v[2];
            targets[mouthDown] = 10 * v[3]
                + 5 * v[4]
                + 10 * v[5]
                + 5 * v[11]
                + 10 * v[12];
            targets[mouthNarrow_left] = 40 * v[2]
                + 10 * v[3]
                + 30 * v[6];
            targets[mouthNarrow_right] = 40 * v[2]
                + 10 * v[3]
                + 30 * v[6];
            targets[mouthOpen] = 15 * v[2]
                + 20 * v[3]
                + 15 * v[4]
                + 15 * v[5]
                + 10 * v[6]
                + 5 * v[7]
                + 20 * v[8]
                + 15 * v[9]
                + 50 * v[10]
                + 15 * v[11]
                + 5 * v[12]
                + 40 * v[13]
                + 15 * v[14];
            targets[mouthWhistle_left] = 50 * v[4]
                + 55 * v[5]
                + 50 * v[6]
                + 50 * v[7]
                + 20 * v[8]
                + 10 * v[9]
                + 50 * v[11]
                + 60 * v[12];
            targets[mouthWhistle_right] = 50 * v[4]
                + 55 * v[5]
                + 50 * v[6]
                + 50 * v[7]
                + 20 * v[8]
                + 10 * v[9]
                + 50 * v[11]
                + 60 * v[12];
            targets[upperLipIn] = 100 * v[1]
                + 20 * v[11]
                + 40 * v[12];
            targets[upperLipOut] = 40 * v[2]
                + 20 * v[6]
                + 10 * v[7]
                + 10 * v[13]
                + 10 * v[14];
            targets[toungeUp] = 20 * v[3]
                + 20 * v[8]
                + 10 * v[9];
            targets[upperLipUp_left] = 20 * v[6]
                + 5 * v[7]
                + 5 * v[9];
            targets[upperLipUp_right] = 20 * v[6]
                + 5 * v[7]
                + 5 * v[9];*/
        }

        StepTargets();
        SetShapeKeys();
    }

    void Start () {
        // talkingNow = false;
        blinkingNow = false;

        bodyRenderer = meshRenderer.gameObject.GetComponent<Renderer>();

        values = new float[50];
        for(int i = 0; i < 50; i++)
        {
            values[i] = 0.0f;
        }

        valuesDisp = new float[50];
        for (int i = 0; i < 50; i++)
        {
            valuesDisp[i] = 0.0f;
        }

        targets = new float[50];
        for (int i = 0; i < 50; i++)
        {
            targets[i] = 0.0f;
        }

        speeds = new float[50];
        for (int i = 0; i < 50; i++)
        {
            speeds[i] = 3.0f;
        }

        blinkCloseSpeed = 12f;
        blinkOpenSpeed = 8f;

        expressFactor = 1f;
        blink_max = 4f;
        blink_min = 2f;
    }

    float rand_factor = 3f;
    float rand_min = -10f;
    float rand_max = 10f;

    void RandomMimicks()
    {
        valuesDisp[blink_left] = Mathf.Clamp(valuesDisp[blink_left]  + (Random.value - 0.5f) * rand_factor,rand_min,rand_max);
        valuesDisp[blink_right] = Mathf.Clamp(valuesDisp[blink_right]  + (Random.value - 0.5f) * rand_factor, rand_min, rand_max);
        valuesDisp[eyesWide_left] = Mathf.Clamp(valuesDisp[eyesWide_left] + (Random.value - 0.5f) * rand_factor, rand_min, rand_max);
        valuesDisp[eyesWide_right] = Mathf.Clamp(valuesDisp[eyesWide_right] + (Random.value - 0.5f) * rand_factor, rand_min, rand_max);

        valuesDisp[browsDown_left] = Mathf.Clamp(valuesDisp[browsDown_left] + (Random.value - 0.5f) * rand_factor, rand_min, rand_max);
        valuesDisp[browsDown_right] = Mathf.Clamp(valuesDisp[browsDown_right] + (Random.value - 0.5f) * rand_factor, rand_min, rand_max);

        valuesDisp[browsUp_left] = Mathf.Clamp(valuesDisp[browsUp_left] + (Random.value - 0.5f) * rand_factor, rand_min, rand_max);
        valuesDisp[browsUp_right] = Mathf.Clamp(valuesDisp[browsUp_right] + (Random.value - 0.5f) * rand_factor, rand_min, rand_max);

        valuesDisp[smileLeft] = Mathf.Clamp(valuesDisp[smileLeft] + (Random.value - 0.5f) * rand_factor, rand_min, rand_max);
        valuesDisp[smileRight] = Mathf.Clamp(valuesDisp[smileRight] + (Random.value - 0.5f) * rand_factor, rand_min, rand_max);
    }

    private int tmpKey1, tmpKey2;

    public void InitShapeKeys()
    {
        shapeKeyCount = meshRenderer.sharedMesh.blendShapeCount;
        Dictionary<string, int> shapeKeyDict_body = new Dictionary<string, int>();
        for (int i = 0; i < shapeKeyCount; i++)
        {
            shapeKeyDict_body.Add(meshRenderer.sharedMesh.GetBlendShapeName(i), i);
        }

        if (meshRendererMoustaches != null)
        {
            int scMoustaches = meshRendererMoustaches.sharedMesh.blendShapeCount;
            Dictionary<string, int> shapeKeyDict_moustaches = new Dictionary<string, int>();
            shapeKey_mapMoustache = new int[shapeKeyCount];
            for (int i = 0; i < shapeKeyCount; i++)
            {
                shapeKey_mapMoustache[i] = -1;
            }
            for (int i = 0; i < scMoustaches; i++)
            {
                shapeKeyDict_moustaches.Add(meshRendererMoustaches.sharedMesh.GetBlendShapeName(i), i);
            }
            for (int i = 0; i < scMoustaches; i++)
            {
                tmpKey1 = -1;
                tmpKey2 = -1;
                shapeKeyDict_body.TryGetValue(meshRenderer.sharedMesh.GetBlendShapeName(i), out tmpKey1);
                shapeKeyDict_moustaches.TryGetValue(meshRenderer.sharedMesh.GetBlendShapeName(i), out tmpKey2);
                if (tmpKey1 != -1 && tmpKey2 != -1)
                {
                    shapeKey_mapMoustache[tmpKey1] = tmpKey2;
                }
            }
            shapeKeyDict_moustaches.Clear();
        }

        if (meshRendererBeards != null)
        {
            int scBeards = meshRendererBeards.sharedMesh.blendShapeCount;
            Dictionary<string, int> shapeKeyDict_beards = new Dictionary<string, int>();
            shapeKey_mapBeard = new int[shapeKeyCount];
            for (int i = 0; i < shapeKeyCount; i++)
            {
                shapeKey_mapBeard[i] = -1;
            }
            for (int i = 0; i < scBeards; i++)
            {
                shapeKeyDict_beards.Add(meshRendererBeards.sharedMesh.GetBlendShapeName(i), i);
            }
            for (int i = 0; i < scBeards; i++)
            {
                tmpKey1 = -1;
                tmpKey2 = -1;
                shapeKeyDict_body.TryGetValue(meshRenderer.sharedMesh.GetBlendShapeName(i), out tmpKey1);
                shapeKeyDict_beards.TryGetValue(meshRenderer.sharedMesh.GetBlendShapeName(i), out tmpKey2);
                if (tmpKey1 != -1 && tmpKey2 != -1)
                {
                    shapeKey_mapBeard[tmpKey1] = tmpKey2;
                }
            }
            shapeKeyDict_beards.Clear();
        }
        shapeKeyDict_body.Clear();
    }

    void StepTargets()
    {
        for (int i = 0; i < 50; i++)
        {
            if( Mathf.Abs( values[i] - targets[i] ) <= speeds[i])
            {
                values[i] = targets[i];
            } else
            {
                values[i] -= (speeds[i] * Mathf.Sign(values[i] - targets[i]));
            } 
        }

        if (Mathf.Abs(w_happy - exp_happy) <= 3)
        {
            w_happy = exp_happy;
        }
        else
        {
            w_happy -= (3 * Mathf.Sign(w_happy - exp_happy));
        }

        if (Mathf.Abs(w_angry - exp_angry) <= 3)
        {
            w_angry = exp_angry;
        }
        else
        {
            w_angry -= (3 * Mathf.Sign(w_angry - exp_angry));
        }

        if (Mathf.Abs(w_shock - exp_shock) <= 3)
        {
            w_shock = exp_shock;
        }
        else
        {
            w_shock -= (3 * Mathf.Sign(w_shock - exp_shock));
        }

        if (Mathf.Abs(w_sad - exp_sad) <= 3)
        {
            w_sad = exp_sad;
        }
        else
        {
            w_sad -= (3 * Mathf.Sign(w_sad - exp_sad));
        }
    }

    private void SetShapeKeys()
    {
        meshRenderer.SetBlendShapeWeight(blink_left, values[blink_left] + valuesDisp[blink_left]);
        meshRenderer.SetBlendShapeWeight(blink_right, values[blink_right] + valuesDisp[blink_right]);
        meshRenderer.SetBlendShapeWeight(browsDown_left, values[browsDown_left] + valuesDisp[browsDown_left]);
        meshRenderer.SetBlendShapeWeight(browsDown_right, values[browsDown_right] + valuesDisp[browsDown_right]);
        meshRenderer.SetBlendShapeWeight(browsIn_left, values[browsIn_left] + valuesDisp[browsIn_left]);
        meshRenderer.SetBlendShapeWeight(browsIn_right, values[browsIn_right] + valuesDisp[browsIn_right]);
        meshRenderer.SetBlendShapeWeight(browsOuterLower_left, values[browsOuterLower_left] + valuesDisp[browsOuterLower_left]);
        meshRenderer.SetBlendShapeWeight(browsOuterLower_right, values[browsOuterLower_right] + valuesDisp[browsOuterLower_right]);
        meshRenderer.SetBlendShapeWeight(browsUp_left, values[browsUp_left] + valuesDisp[browsUp_left]);
        meshRenderer.SetBlendShapeWeight(browsUp_right, values[browsUp_right] + valuesDisp[browsUp_right]);
        meshRenderer.SetBlendShapeWeight(cheekPuff_left, values[cheekPuff_left] + valuesDisp[cheekPuff_left]);
        meshRenderer.SetBlendShapeWeight(cheekPuff_right, values[cheekPuff_right] + valuesDisp[cheekPuff_right]);
        meshRenderer.SetBlendShapeWeight(eyesWide_left, values[eyesWide_left] + valuesDisp[eyesWide_left]);
        meshRenderer.SetBlendShapeWeight(eyesWide_right, values[eyesWide_right] + valuesDisp[eyesWide_right]);
        meshRenderer.SetBlendShapeWeight(frown_left, values[frown_left] + valuesDisp[frown_left]);
        meshRenderer.SetBlendShapeWeight(frown_right, values[frown_right] + valuesDisp[frown_right]);
        meshRenderer.SetBlendShapeWeight(jawBackward, values[jawBackward] + valuesDisp[jawBackward]);
        meshRenderer.SetBlendShapeWeight(jawForeward, values[jawForeward] + valuesDisp[jawForeward]);
        meshRenderer.SetBlendShapeWeight(jawRotateY_left, values[jawRotateY_left] + valuesDisp[jawRotateY_left]);
        meshRenderer.SetBlendShapeWeight(jawRotateY_right, values[jawRotateY_right] + valuesDisp[jawRotateY_right]);
        meshRenderer.SetBlendShapeWeight(jawRotateZ_left, values[jawRotateZ_left] + valuesDisp[jawRotateZ_left]);
        meshRenderer.SetBlendShapeWeight(jawRotateZ_right, values[jawRotateZ_right] + valuesDisp[jawRotateZ_right]);
        meshRenderer.SetBlendShapeWeight(jawDown, values[jawDown] + valuesDisp[jawDown]);
        meshRenderer.SetBlendShapeWeight(jawLeft, values[jawLeft] + valuesDisp[jawLeft]);
        meshRenderer.SetBlendShapeWeight(jawRight, values[jawRight] + valuesDisp[jawRight]);
        meshRenderer.SetBlendShapeWeight(jawUp, values[jawUp] + valuesDisp[jawUp]);
        meshRenderer.SetBlendShapeWeight(lowerLipDown_left, values[lowerLipDown_left] + valuesDisp[lowerLipDown_left]);
        meshRenderer.SetBlendShapeWeight(lowerLipDown_right, values[lowerLipDown_right] + valuesDisp[lowerLipDown_right]);
        meshRenderer.SetBlendShapeWeight(lowerLipIn, values[lowerLipIn] + valuesDisp[lowerLipIn]);
        meshRenderer.SetBlendShapeWeight(lowerLipOut, values[lowerLipOut] + valuesDisp[lowerLipOut]);
        meshRenderer.SetBlendShapeWeight(midmouth_left, values[midmouth_left] + valuesDisp[midmouth_left]);
        meshRenderer.SetBlendShapeWeight(midmouth_right, values[midmouth_right] + valuesDisp[midmouth_right]);
        meshRenderer.SetBlendShapeWeight(mouthDown, values[mouthDown] + valuesDisp[mouthDown]);
        meshRenderer.SetBlendShapeWeight(mouthNarrow_left, values[mouthNarrow_left] + valuesDisp[mouthNarrow_left]);
        meshRenderer.SetBlendShapeWeight(mouthNarrow_right, values[mouthNarrow_right] + valuesDisp[mouthNarrow_right]);
        meshRenderer.SetBlendShapeWeight(mouthOpen, values[mouthOpen] + valuesDisp[mouthOpen]);
        meshRenderer.SetBlendShapeWeight(mouthUp, values[mouthUp] + valuesDisp[mouthUp]);
        meshRenderer.SetBlendShapeWeight(mouthWhistle_left, values[mouthWhistle_left] + valuesDisp[mouthWhistle_left]);
        meshRenderer.SetBlendShapeWeight(mouthWhistle_right, values[mouthWhistle_right] + valuesDisp[mouthWhistle_right]);
        meshRenderer.SetBlendShapeWeight(noseScrunch_left, values[noseScrunch_left] + valuesDisp[noseScrunch_left]);
        meshRenderer.SetBlendShapeWeight(noseScrunch_right, values[noseScrunch_right] + valuesDisp[noseScrunch_right]);
        meshRenderer.SetBlendShapeWeight(smileLeft, values[smileLeft] + valuesDisp[smileLeft]);
        meshRenderer.SetBlendShapeWeight(smileRight, values[smileRight] + valuesDisp[smileRight]);
        meshRenderer.SetBlendShapeWeight(squint_left, values[squint_left] + valuesDisp[squint_left]);
        meshRenderer.SetBlendShapeWeight(squint_right, values[squint_right] + valuesDisp[squint_right]);
        meshRenderer.SetBlendShapeWeight(toungeUp, values[toungeUp] + valuesDisp[toungeUp]);
        meshRenderer.SetBlendShapeWeight(upperLipIn, values[upperLipIn] + valuesDisp[upperLipIn]);
        meshRenderer.SetBlendShapeWeight(upperLipOut, values[upperLipOut] + valuesDisp[upperLipOut]);
        meshRenderer.SetBlendShapeWeight(upperLipUp_left, values[upperLipUp_left] + valuesDisp[upperLipUp_left]);
        meshRenderer.SetBlendShapeWeight(upperLipUp_right, values[upperLipUp_right] + valuesDisp[upperLipUp_right]);

        if(meshRendererEyelashes != null)
        {
            meshRendererEyelashes.SetBlendShapeWeight(blink_left, values[blink_left] + valuesDisp[blink_left]);
            meshRendererEyelashes.SetBlendShapeWeight(blink_right, values[blink_right] + valuesDisp[blink_right]);
            meshRendererEyelashes.SetBlendShapeWeight(browsDown_left, values[browsDown_left] + valuesDisp[browsDown_left]);
            meshRendererEyelashes.SetBlendShapeWeight(browsDown_right, values[browsDown_right] + valuesDisp[browsDown_right]);
            meshRendererEyelashes.SetBlendShapeWeight(browsIn_left, values[browsIn_left] + valuesDisp[browsIn_left]);
            meshRendererEyelashes.SetBlendShapeWeight(browsIn_right, values[browsIn_right] + valuesDisp[browsIn_right]);
            meshRendererEyelashes.SetBlendShapeWeight(browsOuterLower_left, values[browsOuterLower_left] + valuesDisp[browsOuterLower_left]);
            meshRendererEyelashes.SetBlendShapeWeight(browsOuterLower_right, values[browsOuterLower_right] + valuesDisp[browsOuterLower_right]);
            meshRendererEyelashes.SetBlendShapeWeight(browsUp_left, values[browsUp_left] + valuesDisp[browsUp_left]);
            meshRendererEyelashes.SetBlendShapeWeight(browsUp_right, values[browsUp_right] + valuesDisp[browsUp_right]);
            meshRendererEyelashes.SetBlendShapeWeight(cheekPuff_left, values[cheekPuff_left] + valuesDisp[cheekPuff_left]);
            meshRendererEyelashes.SetBlendShapeWeight(cheekPuff_right, values[cheekPuff_right] + valuesDisp[cheekPuff_right]);
            meshRendererEyelashes.SetBlendShapeWeight(eyesWide_left, values[eyesWide_left] + valuesDisp[eyesWide_left]);
            meshRendererEyelashes.SetBlendShapeWeight(eyesWide_right, values[eyesWide_right] + valuesDisp[eyesWide_right]);
            meshRendererEyelashes.SetBlendShapeWeight(frown_left, values[frown_left] + valuesDisp[frown_left]);
            meshRendererEyelashes.SetBlendShapeWeight(frown_right, values[frown_right] + valuesDisp[frown_right]);
            meshRendererEyelashes.SetBlendShapeWeight(jawBackward, values[jawBackward] + valuesDisp[jawBackward]);
            meshRendererEyelashes.SetBlendShapeWeight(jawForeward, values[jawForeward] + valuesDisp[jawForeward]);
            meshRendererEyelashes.SetBlendShapeWeight(jawRotateY_left, values[jawRotateY_left] + valuesDisp[jawRotateY_left]);
            meshRendererEyelashes.SetBlendShapeWeight(jawRotateY_right, values[jawRotateY_right] + valuesDisp[jawRotateY_right]);
            meshRendererEyelashes.SetBlendShapeWeight(jawRotateZ_left, values[jawRotateZ_left] + valuesDisp[jawRotateZ_left]);
            meshRendererEyelashes.SetBlendShapeWeight(jawRotateZ_right, values[jawRotateZ_right] + valuesDisp[jawRotateZ_right]);
            meshRendererEyelashes.SetBlendShapeWeight(jawDown, values[jawDown] + valuesDisp[jawDown]);
            meshRendererEyelashes.SetBlendShapeWeight(jawLeft, values[jawLeft] + valuesDisp[jawLeft]);
            meshRendererEyelashes.SetBlendShapeWeight(jawRight, values[jawRight] + valuesDisp[jawRight]);
            meshRendererEyelashes.SetBlendShapeWeight(jawUp, values[jawUp] + valuesDisp[jawUp]);
            meshRendererEyelashes.SetBlendShapeWeight(lowerLipDown_left, values[lowerLipDown_left] + valuesDisp[lowerLipDown_left]);
            meshRendererEyelashes.SetBlendShapeWeight(lowerLipDown_right, values[lowerLipDown_right] + valuesDisp[lowerLipDown_right]);
            meshRendererEyelashes.SetBlendShapeWeight(lowerLipIn, values[lowerLipIn] + valuesDisp[lowerLipIn]);
            meshRendererEyelashes.SetBlendShapeWeight(lowerLipOut, values[lowerLipOut] + valuesDisp[lowerLipOut]);
            meshRendererEyelashes.SetBlendShapeWeight(midmouth_left, values[midmouth_left] + valuesDisp[midmouth_left]);
            meshRendererEyelashes.SetBlendShapeWeight(midmouth_right, values[midmouth_right] + valuesDisp[midmouth_right]);
            meshRendererEyelashes.SetBlendShapeWeight(mouthDown, values[mouthDown] + valuesDisp[mouthDown]);
            meshRendererEyelashes.SetBlendShapeWeight(mouthNarrow_left, values[mouthNarrow_left] + valuesDisp[mouthNarrow_left]);
            meshRendererEyelashes.SetBlendShapeWeight(mouthNarrow_right, values[mouthNarrow_right] + valuesDisp[mouthNarrow_right]);
            meshRendererEyelashes.SetBlendShapeWeight(mouthOpen, values[mouthOpen] + valuesDisp[mouthOpen]);
            meshRendererEyelashes.SetBlendShapeWeight(mouthUp, values[mouthUp] + valuesDisp[mouthUp]);
            meshRendererEyelashes.SetBlendShapeWeight(mouthWhistle_left, values[mouthWhistle_left] + valuesDisp[mouthWhistle_left]);
            meshRendererEyelashes.SetBlendShapeWeight(mouthWhistle_right, values[mouthWhistle_right] + valuesDisp[mouthWhistle_right]);
            meshRendererEyelashes.SetBlendShapeWeight(noseScrunch_left, values[noseScrunch_left] + valuesDisp[noseScrunch_left]);
            meshRendererEyelashes.SetBlendShapeWeight(noseScrunch_right, values[noseScrunch_right] + valuesDisp[noseScrunch_right]);
            meshRendererEyelashes.SetBlendShapeWeight(smileLeft, values[smileLeft] + valuesDisp[smileLeft]);
            meshRendererEyelashes.SetBlendShapeWeight(smileRight, values[smileRight] + valuesDisp[smileRight]);
            meshRendererEyelashes.SetBlendShapeWeight(squint_left, values[squint_left] + valuesDisp[squint_left]);
            meshRendererEyelashes.SetBlendShapeWeight(squint_right, values[squint_right] + valuesDisp[squint_right]);
            meshRendererEyelashes.SetBlendShapeWeight(toungeUp, values[toungeUp] + valuesDisp[toungeUp]);
            meshRendererEyelashes.SetBlendShapeWeight(upperLipIn, values[upperLipIn] + valuesDisp[upperLipIn]);
            meshRendererEyelashes.SetBlendShapeWeight(upperLipOut, values[upperLipOut] + valuesDisp[upperLipOut]);
            meshRendererEyelashes.SetBlendShapeWeight(upperLipUp_left, values[upperLipUp_left] + valuesDisp[upperLipUp_left]);
            meshRendererEyelashes.SetBlendShapeWeight(upperLipUp_right, values[upperLipUp_right] + valuesDisp[upperLipUp_right]);
        }

        if (meshRendererEyes != null)
        {
            meshRendererEyes.SetBlendShapeWeight(blink_left, values[blink_left] + valuesDisp[blink_left]);
            meshRendererEyes.SetBlendShapeWeight(blink_right, values[blink_right] + valuesDisp[blink_right]);
            meshRendererEyes.SetBlendShapeWeight(browsDown_left, values[browsDown_left] + valuesDisp[browsDown_left]);
            meshRendererEyes.SetBlendShapeWeight(browsDown_right, values[browsDown_right] + valuesDisp[browsDown_right]);
            meshRendererEyes.SetBlendShapeWeight(browsIn_left, values[browsIn_left] + valuesDisp[browsIn_left]);
            meshRendererEyes.SetBlendShapeWeight(browsIn_right, values[browsIn_right] + valuesDisp[browsIn_right]);
            meshRendererEyes.SetBlendShapeWeight(browsOuterLower_left, values[browsOuterLower_left] + valuesDisp[browsOuterLower_left]);
            meshRendererEyes.SetBlendShapeWeight(browsOuterLower_right, values[browsOuterLower_right] + valuesDisp[browsOuterLower_right]);
            meshRendererEyes.SetBlendShapeWeight(browsUp_left, values[browsUp_left] + valuesDisp[browsUp_left]);
            meshRendererEyes.SetBlendShapeWeight(browsUp_right, values[browsUp_right] + valuesDisp[browsUp_right]);
            meshRendererEyes.SetBlendShapeWeight(cheekPuff_left, values[cheekPuff_left] + valuesDisp[cheekPuff_left]);
            meshRendererEyes.SetBlendShapeWeight(cheekPuff_right, values[cheekPuff_right] + valuesDisp[cheekPuff_right]);
            meshRendererEyes.SetBlendShapeWeight(eyesWide_left, values[eyesWide_left] + valuesDisp[eyesWide_left]);
            meshRendererEyes.SetBlendShapeWeight(eyesWide_right, values[eyesWide_right] + valuesDisp[eyesWide_right]);
            meshRendererEyes.SetBlendShapeWeight(frown_left, values[frown_left] + valuesDisp[frown_left]);
            meshRendererEyes.SetBlendShapeWeight(frown_right, values[frown_right] + valuesDisp[frown_right]);
            meshRendererEyes.SetBlendShapeWeight(jawBackward, values[jawBackward] + valuesDisp[jawBackward]);
            meshRendererEyes.SetBlendShapeWeight(jawForeward, values[jawForeward] + valuesDisp[jawForeward]);
            meshRendererEyes.SetBlendShapeWeight(jawRotateY_left, values[jawRotateY_left] + valuesDisp[jawRotateY_left]);
            meshRendererEyes.SetBlendShapeWeight(jawRotateY_right, values[jawRotateY_right] + valuesDisp[jawRotateY_right]);
            meshRendererEyes.SetBlendShapeWeight(jawRotateZ_left, values[jawRotateZ_left] + valuesDisp[jawRotateZ_left]);
            meshRendererEyes.SetBlendShapeWeight(jawRotateZ_right, values[jawRotateZ_right] + valuesDisp[jawRotateZ_right]);
            meshRendererEyes.SetBlendShapeWeight(jawDown, values[jawDown] + valuesDisp[jawDown]);
            meshRendererEyes.SetBlendShapeWeight(jawLeft, values[jawLeft] + valuesDisp[jawLeft]);
            meshRendererEyes.SetBlendShapeWeight(jawRight, values[jawRight] + valuesDisp[jawRight]);
            meshRendererEyes.SetBlendShapeWeight(jawUp, values[jawUp] + valuesDisp[jawUp]);
            meshRendererEyes.SetBlendShapeWeight(lowerLipDown_left, values[lowerLipDown_left] + valuesDisp[lowerLipDown_left]);
            meshRendererEyes.SetBlendShapeWeight(lowerLipDown_right, values[lowerLipDown_right] + valuesDisp[lowerLipDown_right]);
            meshRendererEyes.SetBlendShapeWeight(lowerLipIn, values[lowerLipIn] + valuesDisp[lowerLipIn]);
            meshRendererEyes.SetBlendShapeWeight(lowerLipOut, values[lowerLipOut] + valuesDisp[lowerLipOut]);
            meshRendererEyes.SetBlendShapeWeight(midmouth_left, values[midmouth_left] + valuesDisp[midmouth_left]);
            meshRendererEyes.SetBlendShapeWeight(midmouth_right, values[midmouth_right] + valuesDisp[midmouth_right]);
            meshRendererEyes.SetBlendShapeWeight(mouthDown, values[mouthDown] + valuesDisp[mouthDown]);
            meshRendererEyes.SetBlendShapeWeight(mouthNarrow_left, values[mouthNarrow_left] + valuesDisp[mouthNarrow_left]);
            meshRendererEyes.SetBlendShapeWeight(mouthNarrow_right, values[mouthNarrow_right] + valuesDisp[mouthNarrow_right]);
            meshRendererEyes.SetBlendShapeWeight(mouthOpen, values[mouthOpen] + valuesDisp[mouthOpen]);
            meshRendererEyes.SetBlendShapeWeight(mouthUp, values[mouthUp] + valuesDisp[mouthUp]);
            meshRendererEyes.SetBlendShapeWeight(mouthWhistle_left, values[mouthWhistle_left] + valuesDisp[mouthWhistle_left]);
            meshRendererEyes.SetBlendShapeWeight(mouthWhistle_right, values[mouthWhistle_right] + valuesDisp[mouthWhistle_right]);
            meshRendererEyes.SetBlendShapeWeight(noseScrunch_left, values[noseScrunch_left] + valuesDisp[noseScrunch_left]);
            meshRendererEyes.SetBlendShapeWeight(noseScrunch_right, values[noseScrunch_right] + valuesDisp[noseScrunch_right]);
            meshRendererEyes.SetBlendShapeWeight(smileLeft, values[smileLeft] + valuesDisp[smileLeft]);
            meshRendererEyes.SetBlendShapeWeight(smileRight, values[smileRight] + valuesDisp[smileRight]);
            meshRendererEyes.SetBlendShapeWeight(squint_left, values[squint_left] + valuesDisp[squint_left]);
            meshRendererEyes.SetBlendShapeWeight(squint_right, values[squint_right] + valuesDisp[squint_right]);
            meshRendererEyes.SetBlendShapeWeight(toungeUp, values[toungeUp] + valuesDisp[toungeUp]);
            meshRendererEyes.SetBlendShapeWeight(upperLipIn, values[upperLipIn] + valuesDisp[upperLipIn]);
            meshRendererEyes.SetBlendShapeWeight(upperLipOut, values[upperLipOut] + valuesDisp[upperLipOut]);
            meshRendererEyes.SetBlendShapeWeight(upperLipUp_left, values[upperLipUp_left] + valuesDisp[upperLipUp_left]);
            meshRendererEyes.SetBlendShapeWeight(upperLipUp_right, values[upperLipUp_right] + valuesDisp[upperLipUp_right]);
        }

        if (meshRendererBeards != null)
        {
            for(int i = 0; i < shapeKeyCount; i++)
            {
                tmpKey1 = shapeKey_mapBeard[i];
                if(tmpKey1 != -1)
                {
                    meshRendererBeards.SetBlendShapeWeight(tmpKey1, values[i] + valuesDisp[i]);
                }
            }
        }

        if (meshRendererMoustaches != null)
        {
            for (int i = 0; i < shapeKeyCount; i++)
            {
                tmpKey1 = shapeKey_mapMoustache[i];
                if (tmpKey1 != -1)
                {
                    meshRendererMoustaches.SetBlendShapeWeight(tmpKey1, values[i] + valuesDisp[i]);
                }
            }
        }
    }

    private void ExpressionPass()
    {
        targets[browsDown_left] = (exp_angry + exp_disgust * 0.5f) * expressFactor;
        targets[browsDown_right] = targets[browsDown_left];
        targets[cheekPuff_left] = exp_angry * 0.02f * expressFactor;
        targets[cheekPuff_right] = targets[cheekPuff_left];
        targets[frown_left] = (exp_angry * 0.4f + exp_sad * 0.85f + exp_disgust * 0.1f + exp_fear * 0.6f) * expressFactor;
        targets[frown_right] = targets[frown_left];
        targets[mouthDown] = (exp_angry * 0.1f + exp_sad * 0.1f + exp_fear * 0.05f) * expressFactor;
        targets[mouthNarrow_left] = (exp_angry * 0.2f + exp_shock * 0.2f + exp_fear * 0.5f ) * expressFactor;
        targets[mouthNarrow_right] = targets[mouthNarrow_left];
        targets[squint_left] = (exp_angry * 0.3f + exp_sad * 0.1f + exp_happy * 0.4f + exp_disgust * 0.3f) * expressFactor;
        targets[squint_right] = targets[squint_left];
        
        targets[browsUp_left] = (exp_happy * 0.3f + exp_shock + exp_fear * 0.9f + exp_sad * 0.05f) * expressFactor;
        targets[browsUp_right] = targets[browsUp_left];
        targets[eyesWide_left] = (exp_shock * 0.8f + exp_fear * 0.9f) * expressFactor;
        targets[eyesWide_right] = targets[eyesWide_left];
        targets[mouthOpen] = (exp_happy * 0.12f + exp_shock * 0.3f + exp_fear * 0.5f) * expressFactor;
        // targets[upperLipIn] = exp_happy * 0.02f * expressFactor;
        // targets[lowerLipIn] = exp_happy * 0.02f * expressFactor;
        targets[smileLeft] = exp_happy * 0.95f * expressFactor;
        targets[smileRight] = targets[smileLeft];
        
        targets[browsOuterLower_left] = (exp_sad * 0.95f + exp_shock * 0.5f) * expressFactor;
        targets[browsOuterLower_right] = targets[browsOuterLower_left];
        targets[browsIn_left] = (exp_sad * 0.05f + exp_fear * 0.8f) * expressFactor;
        targets[browsIn_right] = targets[browsIn_left];
        targets[jawBackward] = exp_sad * 0.05f * expressFactor;
        
        targets[jawDown] = exp_disgust * 0.2f * expressFactor;
        targets[noseScrunch_left] = exp_disgust * 0.85f * expressFactor;
        targets[noseScrunch_right] = targets[noseScrunch_left];
        targets[mouthUp] = exp_disgust * 0.05f * expressFactor;
        targets[jawForeward] = exp_disgust * 0.1f * expressFactor;
        targets[upperLipOut] = exp_disgust * 0.45f * expressFactor;
        targets[lowerLipIn] = exp_disgust * 0.15f * expressFactor;
        targets[mouthWhistle_left] = (exp_disgust * 0.1f) * expressFactor;
        targets[mouthWhistle_right] = targets[mouthWhistle_left];
        targets[midmouth_left] = exp_disgust * 0.15f * expressFactor;
        targets[midmouth_right] = targets[midmouth_left];

        targets[lowerLipDown_left] = exp_fear * 0.3f * expressFactor;
        targets[lowerLipDown_right] = targets[lowerLipDown_left];
    }

    private float w_happy;
    private float w_angry;
    private float w_shock;
    private float w_sad;

    private void WrinklePass()
    {
        bodyRenderer.material.SetFloat("_NF1", w_happy / 120f); // factor_happy
        bodyRenderer.material.SetFloat("_NF2", w_angry / 110f); // factor_angry
        bodyRenderer.material.SetFloat("_NF3", w_shock / 100f); // c_factor_shock - values[browsUp_left]
        bodyRenderer.material.SetFloat("_NF4", w_sad / 100f); // c_factor_sad

        bodyRenderer.material.SetFloat("_BF1", w_happy / 260f); // cheeck happy
        bodyRenderer.material.SetFloat("_BF2", w_angry / 140f); // cheeck angry
    }

    public void StartTalking()
    {
        talkingNow = true;
    }

    public void StopTalking()
    {
        talkingNow = false;
    }

    public void SetTargetsImmediate()
    {
        for (int i = 0; i < 50; i++)
        {
            values[i] = targets[i];
        }
        w_angry = exp_angry;
        w_happy = exp_happy;
        w_sad = exp_sad;
        w_shock = exp_shock;
    }

    [HideInInspector] public float blink_max;
    [HideInInspector] public float blink_min;
    private float blink_max_pre;

    void Blink()
    {
        if(blink_max != blink_max_pre)
        {
            // blinkTimer = 0f;
            blink_max_pre = blink_max;
        }

        blinkTimer -= Time.deltaTime;

        // time to blink
        if(!blinkingNow && blinkTimer <= 0)
        {
            targets[blink_left] = 100f;
            targets[blink_right] = 100f;
            speeds[blink_left] = blinkCloseSpeed;
            speeds[blink_right] = blinkCloseSpeed;

            blinkingNow = true;
            blinkTimer = 0.2f;
        }

        if(blinkingNow && blinkTimer <= 0)
        {
            targets[blink_left] = 0f;
            targets[blink_right] = 0f;
            speeds[blink_left] = blinkOpenSpeed;
            speeds[blink_right] = blinkOpenSpeed;

            blinkTimer = Random.Range(blink_min,blink_max);
            blinkingNow = false;
        }
    }

    public float[] v = new float[15];

    void VisemesPass()
    {
        targets[cheekPuff_left] = 10 * v[1];
        targets[cheekPuff_right] = 10 * v[1];
        targets[jawBackward] = 10 * v[2];
        targets[lowerLipDown_left] = 25 * v[3]
            + 15 * v[4]
            + 15 * v[5]
            + 40 * v[6]
            + 15 * v[7]
            + 30 * v[8]
            + 5 * v[9]
            + 10 * v[11]
            + 30 * v[12];
        targets[lowerLipDown_right] = 25 * v[3]
            + 15 * v[4]
            + 15 * v[5]
            + 40 * v[6]
            + 15 * v[7]
            + 30 * v[8]
            + 5 * v[9]
            + 10 * v[11]
            + 30 * v[12];
        targets[lowerLipIn] = 100 * v[1]
            + 75 * v[2];
        targets[lowerLipOut] = 20 * v[6]
            + 20 * v[7]
            + 20 * v[11]
            + 30 * v[12]
            + 10 * v[13]
            + 30 * v[14];
        targets[midmouth_left] = 45 * v[13]
            + 70 * v[14];
        targets[midmouth_right] = 45 * v[13]
            + 70 * v[14];
        targets[mouthUp] = 10 * v[1]
            + 5 * v[2];
        targets[mouthDown] = 10 * v[3]
            + 5 * v[4]
            + 10 * v[5]
            + 5 * v[11]
            + 10 * v[12];
        targets[mouthNarrow_left] = 40 * v[2]
            + 10 * v[3]
            + 30 * v[6];
        targets[mouthNarrow_right] = 40 * v[2]
            + 10 * v[3]
            + 30 * v[6];
        targets[mouthOpen] = 15 * v[2]
            + 20 * v[3]
            + 15 * v[4]
            + 15 * v[5]
            + 10 * v[6]
            + 5 * v[7]
            + 20 * v[8]
            + 15 * v[9]
            + 50 * v[10]
            + 15 * v[11]
            + 5 * v[12]
            + 40 * v[13]
            + 15 * v[14];
        targets[mouthWhistle_left] = 50 * v[4]
            + 55 * v[5]
            + 50 * v[6]
            + 50 * v[7]
            + 20 * v[8]
            + 10 * v[9]
            + 50 * v[11]
            + 60 * v[12];
        targets[mouthWhistle_right] = 50 * v[4]
            + 55 * v[5]
            + 50 * v[6]
            + 50 * v[7]
            + 20 * v[8]
            + 10 * v[9]
            + 50 * v[11]
            + 60 * v[12];
        targets[upperLipIn] = 100 * v[1]
            + 20 * v[11]
            + 40 * v[12];
        targets[upperLipOut] = 40 * v[2]
            + 20 * v[6]
            + 10 * v[7]
            + 10 * v[13]
            + 10 * v[14];
        targets[toungeUp] = 20 * v[3]
            + 20 * v[8]
            + 10 * v[9];
        targets[upperLipUp_left] = 20 * v[6]
            + 5 * v[7]
            + 5 * v[9];
        targets[upperLipUp_right] = 20 * v[6]
            + 5 * v[7]
            + 5 * v[9];
    }
}
