using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Progress", fileName = "Progress")]
public class Progress : ScriptableObject
{
    public Stage currentStage;
}
