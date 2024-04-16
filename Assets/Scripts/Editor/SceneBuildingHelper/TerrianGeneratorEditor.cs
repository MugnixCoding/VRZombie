
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CircularTerrianGenerator), true)]
public class TerrianGeneratorEditor : Editor
{
    CircularTerrianGenerator generator;
    private void Awake()
    {
        generator = (CircularTerrianGenerator)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create Terrian"))
        {
            generator.Generate();
        }
        if (GUILayout.Button("Clear Terrian"))
        {
            generator.Clear();
        }
    }
}
