/*
 * 用户自定义识别，可以由用户自己拍摄照片
 * 
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class UserEnent : MonoBehaviour ,IUserDefinedTargetEventHandler{

    private UserDefinedTargetBuildingBehaviour mUseTargetBehavior;   //用户自定义行为脚本
    private ObjectTracker objectTracker;                             //追踪器
    private  DataSet dataSet;                                        //数据集
    public ImageTargetBehaviour ImageTarget;                         //图片脚本
    private int count;                                               //图片名称后缀
    private FrameQuality frameQualityDisplay;                        //图片质量脚本 

    ImageTargetBuilder.FrameQuality frameQuality = ImageTargetBuilder.FrameQuality.FRAME_QUALITY_NONE;

    void Start () {
        mUseTargetBehavior = this.GetComponent<UserDefinedTargetBuildingBehaviour>();
        if (mUseTargetBehavior)
        {
            mUseTargetBehavior.RegisterEventHandler(this);
        }
        frameQualityDisplay = FindObjectOfType<FrameQuality>();
	}
	
	
	void Update () {
		
	}
    public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
    {//获取当前场景的质量，负责创建数据集
        this.frameQuality = frameQuality;
        if (frameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_LOW)
        {
            Debug.Log("Low");
        }

        frameQualityDisplay.SetQuality(this.frameQuality);
    }

    public void OnInitialized()
    {//初始化 ,获取对象追踪器，创建并激活数据集
        objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (objectTracker != null)
        {
            dataSet = objectTracker.CreateDataSet();
            objectTracker.ActivateDataSet(dataSet);
        }
    }

    public void OnNewTrackableSource(TrackableSource trackableSource)
    {//将新的目标添加到数据集
        count++;
        objectTracker.DeactivateDataSet(dataSet);//禁用数据集
        if (dataSet.HasReachedTrackableLimit())  //数据集上限
        {
            IEnumerable<Trackable> trackables = dataSet.GetTrackables(); //拿到数据集
            Trackable oldTrackable = null;
            foreach(Trackable trackable in trackables)
            {
                if (oldTrackable.ID > trackable.ID||oldTrackable==null)
                {
                    oldTrackable = trackable;
                }
            }
            if (oldTrackable != null)
            {
                dataSet.Destroy(oldTrackable, true);//删除最早的数据
            }
        }
        ImageTargetBehaviour imageTargetCopy = Instantiate(ImageTarget);
        imageTargetCopy.gameObject.name = "UseTarget_" + count;//修改名字
        dataSet.CreateTrackable(trackableSource, imageTargetCopy.gameObject);//创建到数据集
        objectTracker.ActivateDataSet(dataSet);//激活数据库
    }
    /// <summary>
    /// 拍摄按钮的点击
    /// </summary>
    public void BulidNewTarget()
    {
        if (frameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_LOW|| frameQuality==ImageTargetBuilder.FrameQuality.FRAME_QUALITY_NONE)
        {
            Debug.Log("Cannot Build");
        }
        else
        {
            string name = "UseTarget_" + count;
            mUseTargetBehavior.BuildNewTarget(name, ImageTarget.GetSize().x);
        }
       
    }
}
