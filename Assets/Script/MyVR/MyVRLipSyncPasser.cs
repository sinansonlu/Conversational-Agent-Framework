using UnityEngine;

public class MyVRLipSyncPasser : MonoBehaviour
{
    public FaceScript currentFace;

    public int smoothAmount = 70;

    private OVRLipSyncContextBase lipsyncContext = null;

    void Start()
    {
        lipsyncContext = GetComponent<OVRLipSyncContextBase>();
        if (lipsyncContext == null)
        {
            Debug.LogError("LipSyncContextMorphTarget.Start Error: " +
                "No OVRLipSyncContext component on this object!");
        }
        else
        {
            lipsyncContext.Smoothing = smoothAmount;
        }
    }

    void Update()
    {
        if ((lipsyncContext != null) && currentFace != null)
        {
            OVRLipSync.Frame frame = lipsyncContext.GetCurrentPhonemeFrame();
            
            if (frame != null)
            {
                for (int i = 0; i < currentFace.v.Length; i++)
                {
                    currentFace.v[i] = frame.Visemes[i];
                }
            }

            if (smoothAmount != lipsyncContext.Smoothing)
            {
                lipsyncContext.Smoothing = smoothAmount;
            }
        }
    }
}
