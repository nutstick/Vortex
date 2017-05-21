/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using System.Collections.Generic;
using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// This class serves both as an augmentation definition for an ImageTarget in the editor
    /// as well as a tracked image target result at runtime
    /// </summary>
    public class ImageTargetBehaviour : ImageTargetAbstractBehaviour
    {

        // Use this for initialization
        void Start()
        {
            VuforiaARController.Instance.RegisterVuforiaStartedCallback(LoadDataSet);
        }

        void LoadDataSet()
        {

            ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

            DataSet dataSet = objectTracker.CreateDataSet();

            if (dataSet.Load(this.mTrackableName))
            {

                objectTracker.Stop();  // stop tracker so that we can add new dataset

                if (!objectTracker.ActivateDataSet(dataSet))
                {
                    // Note: ImageTracker cannot have more than 100 total targets activated
                    Debug.Log("<color=yellow>Failed to Activate DataSet: " + this.mTrackableName + "</color>");
                }

                if (!objectTracker.Start())
                {
                    Debug.Log("<color=yellow>Tracker Failed to Start.</color>");
                }
            }
            else
            {
                Debug.LogError("<color=yellow>Failed to load dataset: '" + this.mTrackableName + "'</color>");
            }
        }
    }
}
