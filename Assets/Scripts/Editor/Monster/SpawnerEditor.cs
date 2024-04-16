using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor
{
    private void OnSceneGUI()
    {
        Spawner spawner = (Spawner)target;
        Handles.DrawWireArc(spawner.transform.position, Vector3.up, Vector3.forward, 360,spawner.SpawnDistance);
    }
}
