using System.Collections;
using UnityCommunity.UnitySingleton;
using UnityEngine;

public class GameEventsManager : PersistentMonoSingleton<GameEventsManager>
{


    public void SlowDownEnemies(float factor, float duration)
    {
        
    }

    private IEnumerator SlowVisualEffect(float duration)
    {
        yield return null;
    }
}
