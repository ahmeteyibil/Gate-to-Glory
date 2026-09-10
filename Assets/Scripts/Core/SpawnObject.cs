using UnityEngine;

class SpawnObject
{
    public GameObject objectToSpawn;
    public Transform parent;
    public Vector3 spawnPosition;
    public GateSet gateSet;
    public CharacterData characterData;
    public SpawnObject(GameObject _objectToSpawn, Transform _parent, Vector3 _spawnPosition)
    {
        objectToSpawn = _objectToSpawn;
        spawnPosition = _spawnPosition;
        parent = _parent;
    }
}