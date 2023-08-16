using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Message", menuName = "Message Data")]
public class MessageData: ScriptableObject
{
    public List<string> messageTexts = new List<string>();
}
