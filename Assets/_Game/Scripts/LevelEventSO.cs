using UnityEngine;

[CreateAssetMenu(fileName = "New Level Event", menuName = "Wave Event/LevelEventSO")]
public class LevelEventSO : ScriptableObject
{
    public WaveEventType WaveEventType;
    public int NumberOfEnemies;
    public GameObject[] possibleEnemyPrefabs;
}
