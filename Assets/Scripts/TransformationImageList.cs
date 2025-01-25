using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class TransformationImage
{
    public Transformation transformation;
    public Sprite image;
}

[Serializable]
[CreateAssetMenu(fileName = "TransformationImageList", menuName = "ScriptableObjects/TransformationImageList", order = 1)]
public class TransformationImageList: ScriptableObject
{
    public List<TransformationImage> transformationImages;
}
