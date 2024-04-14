using UnityEngine;

[CreateAssetMenu(fileName = "TextData", menuName = "ScriptableObjects/NoteText")]
public class NoteScriptableObject : ScriptableObject
{
    [TextArea(10,50)] public string Text;
}