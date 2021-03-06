﻿// Unity SDK Controller Tracker|SDK_Unity|005
namespace VRTK
{
    using UnityEngine;
#if UNITY_2017_2_OR_NEWER
    using UnityEngine.XR;
#else
    using UnityEngine.VR;
    using XRNode = UnityEngine.VR.VRNode;
#endif

    /// <summary>
    /// The Controller Tracker enables the GameObject to track it's position/rotation to the available connected VR Controller via the `UnityEngine.VR` library.
    /// In Unity versions greater than 2019.1 this interface is simplified through the Unity XR Input System, but older versions have some hooks for accessing
    /// the standard Unity input system which is more difficult to configure.
    /// 
    /// Last Editor: Dan Duggan (dduggan@cra.com) Updated: September 2019
    /// </summary>
    /// <remarks>
    /// The Unity Controller Tracker is attached to the `[UnityBase_CameraRig]` prefab on the child `LeftHandAnchor` and `RightHandAnchor` to enable controller tracking.
    /// </remarks>
    public class SDK_UnityControllerTracker : MonoBehaviour
    {
        [Tooltip("The Unity VRNode to track.")]
        public XRNode handNodeType;
        [Tooltip("The unique index to assign to the controller.")]
        public uint index;
#if !UNITY_2019_1_OR_NEWER
        // note: the following defaults are for the left hand, replace Left with Right if creating a new right hand
        [Tooltip("The Unity Input name for the trigger axis.")]
        public string triggerAxisName = "LeftTrigger";
        [Tooltip("The Unity Input name for the grip axis.")]
        public string gripAxisName = "LeftGrip";
        [Tooltip("The Unity Input name for the touchpad horizontal axis.")]
        public string touchpadHorizontalAxisName = "LeftTrackpadHorizontal";
        [Tooltip("The Unity Input name for the touchpad vertical axis.")]
        public string touchpadVerticalAxisName = "LeftTrackpadVertical";

        protected virtual void OnEnable()
        {
            CheckAxisIsValid(triggerAxisName, "triggerAxisName");
            CheckAxisIsValid(gripAxisName, "gripAxisName");
            CheckAxisIsValid(touchpadHorizontalAxisName, "touchpadHorizontalAxisName");
            CheckAxisIsValid(touchpadVerticalAxisName, "touchpadVerticalAxisName");
        }

        protected virtual string GetVarName<T>(T item) where T : class
        {
            return VRTK_SharedMethods.GetPropertyFirstName<T>();
        }

        protected virtual void CheckAxisIsValid(string axisName, string varName)
        {
            try
            {
                Input.GetAxis(axisName);
            }
            catch (System.ArgumentException ae)
            {
                VRTK_Logger.Warn(ae.Message + " on index [" + index + "] variable [" + varName + "]");
            }
        }
#endif

        protected virtual void FixedUpdate()
        {
            transform.localPosition = InputTracking.GetLocalPosition(handNodeType);
            transform.localRotation = InputTracking.GetLocalRotation(handNodeType);
        }
    }
}