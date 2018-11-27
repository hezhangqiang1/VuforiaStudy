/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using Vuforia;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
///
/// Changes made to this file could be overwritten when upgrading the Vuforia version.
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
public class MyDefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{

    public GameObject SwordGameObject;   //人物模型
    //public GameObject Effect1;
    public GameObject Effect2;
   
    private AudioSource audio;
    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        //GameObject go= GameObject.Find("SwordPrefab");
        // go.SetActive(false);
        audio = this.GetComponent<AudioSource>();
        
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS
    /// <summary>
    /// 当图片找到的时候
    /// </summary>
    protected virtual void OnTrackingFound()
    {
        if (!audio.isPlaying)
        {
            audio.Play();
        }
        GameObject Sword = GameObject.Instantiate(SwordGameObject,transform.position,transform.rotation);
        
        Sword.transform.parent = this.transform;
        Sword.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

        //GameObject e1 = GameObject.Instantiate(Effect1, transform.position,Quaternion.identity );
        //e1.transform.parent = this.transform;
        //e1.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        //Destroy(e1, 5f);

        GameObject e2 = GameObject.Instantiate(Effect2, transform.position, Quaternion.identity);
        e2.transform.parent = this.transform;
        e2.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        Destroy(e2, 5f);
        //var rendererComponents = GetComponentsInChildren<Renderer>(true);
        //var colliderComponents = GetComponentsInChildren<Collider>(true);
        //var canvasComponents = GetComponentsInChildren<Canvas>(true);

        //// Enable rendering:
        //foreach (var component in rendererComponents)
        //    component.enabled = true;

        //// Enable colliders:
        //foreach (var component in colliderComponents)
        //    component.enabled = true;

        //// Enable canvas':
        //foreach (var component in canvasComponents)
        //    component.enabled = true;
    }

    /// <summary>
    ///   当图片丢失的时候
    /// </summary>
    protected virtual void OnTrackingLost()
    {
        Destroy(GameObject.Find("SwordPrefab(Clone)"));
        Destroy(GameObject.Find("RFX_Blood_Puddle(Clone)"));
        Destroy(GameObject.Find("RFX_Tonado_Flame(Clone)"));
       
        
        //var rendererComponents = GetComponentsInChildren<Renderer>(true);
        //var colliderComponents = GetComponentsInChildren<Collider>(true);
        //var canvasComponents = GetComponentsInChildren<Canvas>(true);

        //// Disable rendering:
        //foreach (var component in rendererComponents)
        //    component.enabled = false;

        //// Disable colliders:
        //foreach (var component in colliderComponents)
        //    component.enabled = false;

        //// Disable canvas':
        //foreach (var component in canvasComponents)
        //    component.enabled = false;
    }

    
    #endregion // PROTECTED_METHODS

}
