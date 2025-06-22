using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Wave", menuName = "Waves/WaveData")]
public class WaveData : ScriptableObject
{
    //satu wave terdiri dari serangkai action-action yaitu spawning
    public List<SpawnAction> actions;
}

[System.Serializable]
public class SpawnAction
{
    [Tooltip("Prefab enemy untuk dispawn")]
    public GameObject enemyPrefab;

    [Tooltip("Jumlah enemy untuk dispawn")]
    public int count = 1;

    [Tooltip("Jeda waktu antar-spawn")]
    public float spawnInterval = 1f;

    [Tooltip("Delay sebelum wave dimulai")]
    public float delayBeforeAction = 0f;
}
