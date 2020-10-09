/************************************************************************************
Copyright : Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.

Licensed under the Oculus Utilities SDK License Version 1.31 (the "License"); you may not use
the Utilities SDK except in compliance with the License, which is provided at the time of installation
or download, or which otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at
https://developer.oculus.com/licenses/utilities-1.31

Unless required by applicable law or agreed to in writing, the Utilities SDK distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
ANY KIND, either express or implied. See the License for the specific language governing
permissions and limitations under the License.
************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_2017_2_OR_NEWER
using InputTracking = UnityEngine.XR.InputTracking;
using Node = UnityEngine.XR.XRNode;
#else
using InputTracking = UnityEngine.VR.InputTracking;
using Node = UnityEngine.VR.VRNode;
#endif

/// <summary>
/// A head-tracked stereoscopic virtual reality camera rig.
/// </summary>
[ExecuteInEditMode]
public class OVRCameraRig : MonoBehaviour
{
	/// <summary>
	/// The left eye camera.
	/// </summary>
	public Camera leftEyeCamera { get { return (usePerEyeCameras) ? _leftEyeCamera : _centerEyeCamera; } }
	/// <summary>
	/// The right eye camera.
	/// </summary>
	public Camera rightEyeCamera { get { return (usePerEyeCameras) ? _rightEyeCamera : _centerEyeCamera; } }
	/// <summary>
	/// Provides a root transform for all anchors in tracking space.
	/// </summary>
	public Transform trackingSpace { get; private set; }
	/// <summary>
	/// Always coincides with the pose of the left eye.
	/// </summary>
	public Transform leftEyeAnchor { get; private set; }
	/// <summary>
	/// Always coincides with average of the left and right eye poses.
	/// </summary>
	public Transform centerEyeAnchor { get; private set; }
	/// <summary>
	/// Always coincides with the pose of the right eye.
	/// </summary>
	public Transform rightEyeAnchor { get; private set; }
	/// <summary>
	/// Always coincides with the pose of the left hand.
	/// </summary>
	public Transform leftHandAnchor { get; private set; }
	/// <summary>
	/// Always coincides with the pose of the right hand.
	/// </summary>
	public Transform rightHandAnchor { get; private set; }
	/// <summary>
	/// Anchors controller pose to fix offset issues for the left hand.
	/// </summary>
	public Transform leftControllerAnchor { get; private set; }
	/// <summary>
	/// Anchors controller pose to fix offset issues for the right hand.
	/// </summary>
	public Transform rightControllerAnchor { get; private set; }
	/// <summary>
	/// Always coincides with the pose of the sensor.
	/// </summary>
	public Transform trackerAnchor { get; private set; }
	/// <summary>
	/// Occurs when the eye pose anchors have been set.
	/// </summary>
	public event System.Action<OVRCameraRig> UpdatedAnchors;
	/// <summary>
	/// If true, separate cameras will be used for the left and right eyes.
	/// </summary>
	public bool usePerEyeCameras = false;
	/// <summary>
	/// If true, all tracked anchors are updated in FixedUpdate instead of Update to favor physics fidelity.
	/// \note: If the fixed update rate doesn't match the rendering framerate (OVRManager.display.appFramerate), the anchors will visibly judder.
	/// </summary>
	public bool useFixedUpdateForTracking = false;

	protected bool _skipUpdate = false;
	protected readonly string trackingSpaceName = "TrackingSpace";
	protected readonly string trackerAnchorName = "TrackerAnchor";
	protected readonly string leftEyeAnchorName = "LeftEyeAnchor";
	protected readonly string centerEyeAnchorName = "CenterEyeAnchor";
	protected readonly string rightEyeAnchorName = "RightEyeAnchor";
	protected readonly string leftHandAnchorName = "LeftHandAnchor";
	protected readonly string rightHandAnchorName = "RightHandAnchor";
	protected readonly string leftControllerAnchorName = "LeftControllerAnchor";
	protected readonly string rightControllerAnchorName = "RightControllerAnchor";
	protected Camera _centerEyeCamera;
	protected Camera _leftEyeCamera;
	protected Camera _rightEyeCamera;

#region Unity Messages
	protected virtual void Awake()
	{
		_skipUpdate = true;
		EnsureGameObjectIntegrity();
	}

	protected virtual void Start()
	{
		UpdateAnchors();
	}

	protected virtual void FixedUpdate()
	{
		if (useFixedUpdateForTracking)
			UpdateAnchors();
	}

	protected virtual void Update()
	{
		_skipUpdate = false;

		if (!useFixedUpdateForTracking)
			UpdateAnchors();
	}
#endregion

	protected virtual void UpdateAnchors()
	{
		EnsureGameObjectIntegrity();

		if (!Application.isPlaying)
			return;

		if (_skipUpdate)
		{
			centerEyeAnchor.FromOVRPose(OVRPose.identity, true);
			leftEyeAnchor.FromOVRPose(OVRPose.identity, true);
			rightEyeAnchor.FromOVRPose(OVRPose.identity, true);

			return;
		}

		bool monoscopic = OVRManager.instance.monoscopic;
		bool hmdPresent = OVRNodeStateProperties.IsHmdPresent();

		OVRPose tracker = OVRManager.tracker.GetPose();

		trackerAnchor.localRotation = tracker.orientation;

		Quaternion emulatedRotation = Quaternion.Euler(-OVRManager.instance.headPoseRelativeOffsetRotation.x, -OVRManager.instance.headPoseRelativeOffsetRotation.y, OVRManager.instance.headPoseRelativeOffsetRotation.z);

		centerEyeAnchor.localRotation = hmdPresent ? InputTracking.GetLocalRotation(Node.CenterEye) : emulatedRotation;
		leftEyeAnchor.localRotation = (!hmdPresent || monoscopic) ? centerEyeAnchor.localRotation : InputTracking.GetLocalRotation(Node.LeftEye);
		rightEyeAnchor.localRotation = (!hmdPresent || monoscopic) ? centerEyeAnchor.localRotation : InputTracking.GetLocalRotation(Node.RightEye);

		//Need this for controller offset because if we're on OpenVR, we want to set the local poses as specified by Unity, but if we're not, OVRInput local position is the right anchor
		if (OVRManager.loadedXRDevice == OVRManager.XRDevice.OpenVR)
		{
			leftHandAnchor.localPosition = InputTracking.GetLocalPosition(Node.LeftHand);
			rightHandAnchor.localPosition = InputTracking.GetLocalPosition(Node.RightHand);
			leftHandAnchor.localRotation = InputTracking.GetLocalRotation(Node.LeftHand);
			rightHandAnchor.localRotation = InputTracking.GetLocalRotation(Node.RightHand);
		}
		else
		{
			leftHandAnchor.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
			rightHandAnchor.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
			leftHandAnchor.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
			rightHandAnchor.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
		}

		trackerAnchor.localPosition = tracker.position;

		centerEyeAnchor.localPosition = hmdPresent ? InputTracking.GetLocalPosition(Node.CenterEye) : OVRManager.instance.headPoseRelativeOffsetTranslation;
		leftEyeAnchor.localPosition = (!hmdPresent || monoscopic) ? centerEyeAnchor.localPosition : InputTracking.GetLocalPosition(Node.LeftEye);
		rightEyeAnchor.localPosition = (!hmdPresent || monoscopic) ? centerEyeAnchor.localPosition : InputTracking.GetLocalPosition(Node.RightEye);

		OVRPose leftOffsetPose = OVRPose.identity;
		OVRPose rightOffsetPose = OVRPose.identity;
		if (OVRManager.loadedXRDevice == OVRManager.XRDevice.OpenVR)
		{
			leftOffsetPose = OVRManager.GetOpenVRControllerOffset(Node.LeftHand);
			rightOffsetPose = OVRManager.GetOpenVRControllerOffset(Node.RightHand);

			//Sets poses of left and right nodes, local to the tracking space.
			OVRManager.SetOpenVRLocalPose(trackingSpace.InverseTransformPoint(leftControllerAnchor.position),
				trackingSpace.InverseTransformPoint(rightControllerAnchor.position),
				Quaternion.Inverse(trackingSpace.rotation) * leftControllerAnchor.rotation,
				Quaternion.Inverse(trackingSpace.rotation) * rightControllerAnchor.rotation);
		}
		rightControllerAnchor.localPosition = rightOffsetPose.position;
		rightControllerAnchor.localRotation = rightOffsetPose.orientation;
		leftControllerAnchor.localPosition = leftOffsetPose.position;
		leftControllerAnchor.localRotation = leftOffsetPose.orientation;

		RaiseUpdatedAnchorsEvent();
	}

	protected virtual void RaiseUpdatedAnchorsEvent()
	{
		if (UpdatedAnchors != null)
		{
			UpdatedAnchors(this);
		}
	}

	public virtual void EnsureGameObjectIntegrity()
	{
		bool monoscopic = OVRManager.instance != null ? OVRManager.instance.monoscopic : false;

		if (trackingSpace == null)
			trackingSpace = ConfigureAnchor(null, trackingSpaceName);

		if (leftEyeAnchor == null)
			leftEyeAnchor = ConfigureAnchor(trackingSpace, leftEyeAnchorName);

		if (centerEyeAnchor == null)
			centerEyeAnchor = ConfigureAnchor(trackingSpace, centerEyeAnchorName);

		if (rightEyeAnchor == null)
			rightEyeAnchor = ConfigureAnchor(trackingSpace, rightEyeAnchorName);

		if (leftHandAnchor == null)
			leftHandAnchor = ConfigureAnchor(trackingSpace, leftHandAnchorName);

		if (rightHandAnchor == null)
			rightHandAnchor = ConfigureAnchor(trackingSpace, rightHandAnchorName);

		if (trackerAnchor == null)
			trackerAnchor = ConfigureAnchor(trackingSpace, trackerAnchorName);

		if (leftControllerAnchor == null)
			leftControllerAnchor = ConfigureAnchor(leftHandAnchor, leftControllerAnchorName);

		if (rightControllerAnchor == null)
			rightControllerAnchor = ConfigureAnchor(rightHandAnchor, rightControllerAnchorName);

		if (_centerEyeCamera == null || _leftEyeCamera == null || _rightEyeCamera == null)
		{
			_centerEyeCamera = centerEyeAnchor.GetComponent<Camera>();
			_leftEyeCamera = leftEyeAnchor.GetComponent<Camera>();
			_rightEyeCamera = rightEyeAnchor.GetComponent<Camera>();

			if (_centerEyeCamera == null)
			{
				_centerEyeCamera = centerEyeAnchor.gameObject.AddComponent<Camera>();
				_centerEyeCamera.tag = "MainCamera";
			}

			if (_leftEyeCamera == null)
			{
				_leftEyeCamera = leftEyeAnchor.gameObject.AddComponent<Camera>();
				_leftEyeCamera.tag = "MainCamera";
			}

			if (_rightEyeCamera == null)
			{
				_rightEyeCamera = rightEyeAnchor.gameObject.AddComponent<Camera>();
				_rightEyeCamera.tag = "MainCamera";
			}

			_centerEyeCamera.stereoTargetEye = StereoTargetEyeMask.Both;
			_leftEyeCamera.stereoTargetEye = StereoTargetEyeMask.Left;
			_rightEyeCamera.stereoTargetEye = StereoTargetEyeMask.Right;
		}

		if (monoscopic && !OVRPlugin.EyeTextureArrayEnabled)
		{
			// Output to left eye only when in monoscopic mode
			if (_centerEyeCamera.stereoTargetEye != StereoTargetEyeMask.Left)
			{
				_centerEyeCamera.stereoTargetEye = StereoTargetEyeMask.Left;
			}
		}
		else
		{
			if (_centerEyeCamera.stereoTargetEye != StereoTargetEyeMask.Both)
			{
				_centerEyeCamera.stereoTargetEye = StereoTargetEyeMask.Both;
			}
		}

		// disable the right eye camera when in monoscopic mode
		if (_centerEyeCamera.enabled == usePerEyeCameras ||
			_leftEyeCamera.enabled == !usePerEyeCameras ||
			_rightEyeCamera.enabled == !(usePerEyeCameras && (!monoscopic || OVRPlugin.EyeTextureArrayEnabled)))
		{
			_skipUpdate = true;
		}

		_centerEyeCamera.enabled = !usePerEyeCameras;
		_leftEyeCamera.enabled = usePerEyeCameras;
		_rightEyeCamera.enabled = (usePerEyeCameras && (!monoscopic || OVRPlugin.EyeTextureArrayEnabled));
	}

	protected virtual Transform ConfigureAnchor(Transform root, string name)
	{
		Transform anchor = (root != null) ? root.Find(name) : null;

		if (anchor == null)
		{
			anchor = transform.Find(name);
		}

		if (anchor == null)
		{
			anchor = new GameObject(name).transform;
		}

		anchor.name = name;
		anchor.parent = (root != null) ? root : transform;
		anchor.localScale = Vector3.one;
		anchor.localPosition = Vector3.zero;
		anchor.localRotation = Quaternion.identity;

		return anchor;
	}

	public virtual Matrix4x4 ComputeTrackReferenceMatrix()
	{
		if (centerEyeAnchor == null)
		{
			Debug.LogError("centerEyeAnchor is required");
			return Matrix4x4.identity;
		}

		// The ideal approach would be using UnityEngine.VR.VRNode.TrackingReference, then we would not have to depend on the OVRCameraRig. Unfortunately, it is not available in Unity 5.4.3

		OVRPose headPose;

		headPose.position = InputTracking.GetLocalPosition(Node.Head);
		headPose.orientation = InputTracking.GetLocalRotation(Node.Head);

		OVRPose invHeadPose = headPose.Inverse();
		Matrix4x4 invHeadMatrix = Matrix4x4.TRS(invHeadPose.position, invHeadPose.orientation, Vector3.one);

		Matrix4x4 ret = centerEyeAnchor.localToWorldMatrix * invHeadMatrix;

		return ret;
	}
}
