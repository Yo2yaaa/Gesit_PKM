using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KnowledgeItem")]
public class KnowledgeItemSO : ScriptableObject
{
    [TextArea(2, 6)] public string knowledgeText;
}
