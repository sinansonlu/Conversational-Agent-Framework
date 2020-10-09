using System.Collections;
using System.Collections.Generic;
//using UnityEngine.PostProcessing;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public enum SCamMode { StreetCam, HeadFrontWall, OnSphere };

public class SCam : MonoBehaviour {

    public PostProcessVolume postProcessVolume;
    DepthOfField dof;

    public GameObject focusTarget;

    public Animator[] targetAnimators;
    public Transform[] positions;
    public Transform[] camPositions;

    private Transform frontDirection;

    private Vector3 cameraPosition;
    private Vector3 averagePosition;
    private Camera cam;

    //private PostProcessingBehaviour postPro;

    private float tmp_bestDistance;
    private float tmp_distance;

    public SCamMode mode;

    // camera fade
    Texture2D blk;
    public float alpha;

    private readonly bool alpha_Active = false;

    void OnGUI()
    {
        if(alpha_Active)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blk);
        }
    }

    public void ChangeFade(float f)
    {
        if(alpha_Active)
        {
            if (blk == null)
            {
                blk = new Texture2D(1, 1);
                blk.SetPixel(0, 0, new Color(0, 0, 0, 0));
                blk.Apply();
            }
            alpha = f;
            blk.SetPixel(0, 0, new Color(0, 0, 0, alpha));
            blk.Apply();
        }
    }

    public void CamTranslateToOriginal()
    {
        cam.transform.position = new Vector3(0.074f, 1.626f, -1973f);
        cam.transform.rotation = Quaternion.Euler(9f, -3.69f, 0f);
    }

    public void CamTranslateToFace()
    {
        cam.transform.position = new Vector3(0.106f, 1.881f, -1.504f);
        cam.transform.rotation = Quaternion.Euler(9f, -3.69f, 0f);
    }

    void Start ()
    {
        cam = GetComponent<Camera>();
        postProcessVolume.profile.TryGetSettings<DepthOfField>(out dof);
    }
	
	void Update ()
    {
        if(focusTarget != null) Focus(focusTarget.transform.position);

        /*
        switch(mode)
        {
            case SCamMode.StreetCam:
                FindAverage();
                Focus(averagePosition);
                break;
            case SCamMode.HeadFrontWall:
                FindHeadFrontWall();
                break;
            case SCamMode.OnSphere:
                OnSphere();
                Focus(frontDirection.position);
                break;
        }
        */
        
    }

    public void SetOneHead(Transform t)
    {
        mode = SCamMode.OnSphere;
        frontDirection = t;
    }

    private void OnSphere()
    {
        tmp_bestDistance = float.MaxValue;

        for (int i = 0; i < camPositions.Length; i++)
        {
            if (CanSee(camPositions[i].position, frontDirection.position))
            {
                tmp_distance = (camPositions[i].position - frontDirection.position).sqrMagnitude;
                if (tmp_distance < tmp_bestDistance)
                {
                    tmp_bestDistance = tmp_distance;
                    cameraPosition = camPositions[i].position;
                }
            }
        }

        cam.transform.position = cameraPosition + frontDirection.forward * 1.6f + frontDirection.right * 0.8f;
        cam.transform.LookAt(frontDirection.position);
    }

    private void FindHeadFrontWall()
    {
        if(targetAnimators != null && targetAnimators.Length == 1)
        {
            Transform tran = targetAnimators[0].GetBoneTransform(HumanBodyBones.Head);
            Vector3 pos = tran.position;
            Vector3 dir = tran.forward;
            Vector3 npos = pos + dir * 2;

            int count = 0;

            while(!CanSee(npos, pos))
            {
                npos = npos - dir * 0.1f;
                count++;
                if(count > 25)
                {
                    break;
                }
            }

            cam.transform.position = npos;
            cam.transform.LookAt(pos);
        }
    }

    private void Focus(Vector3 point)
    {
        dof.focusDistance.value = Vector3.Distance(cam.transform.position, point);
    }

    private void FindAverage()
    {
        averagePosition = Vector3.zero;

        for(int i = 0; i < positions.Length; i++)
        {
            averagePosition += positions[i].position;
        }

        if(positions.Length != 0)
        {
            averagePosition = averagePosition / positions.Length;
        }

        tmp_bestDistance = float.MaxValue;

        for (int i = 0; i < camPositions.Length; i++)
        {
            if(CanSee(camPositions[i].position, averagePosition))
            {
                tmp_distance = (camPositions[i].position - averagePosition).sqrMagnitude;
                if (tmp_distance < tmp_bestDistance)
                {
                    tmp_bestDistance = tmp_distance;
                    cameraPosition = camPositions[i].position;
                }
            }
        }
        cam.transform.position = cameraPosition;
        cam.transform.LookAt(averagePosition);
    }

    private bool CanSee(Vector3 point, Vector3 target)
    {
        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(point, target, out wallHit))
        {
            return false;
            // Vector3 wallHitVector3 = new Vector3(wallHit.point.x, wallHit.point.y, wallHit.point.z);
            // toTarget = new Vector3(wallHitVector3.x, toTarget.y, wallHitVector3.z);
        }
        else
        {
            return true;
        }
    }
}
