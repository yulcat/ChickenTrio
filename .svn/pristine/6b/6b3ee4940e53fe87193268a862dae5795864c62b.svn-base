using System.Collections;
using UnityEngine;

public partial class MonsterSpawn : MonoBehaviour
{
    //index 번째 라인의 spawing tile 은 spawningTiles[index - 1]이다.
    public GameObject[] spawningTiles;
    private MonsterCreator _monsterCreator;
}

public partial class MonsterSpawn : MonoBehaviour
{
    public void ClickLine(int lineNumber)
    {
        if (MonsterCalculator.Instance.getMonsterHeight(lineNumber) <= Const.TileHeight)
        {
            //0부터 시작이므로 1 뺀다.
        Vector3 spawning_position = spawningTiles[lineNumber - 1].transform.position;
        GameObject dequeued_monster = _monsterCreator.dequeue();
        dequeued_monster.transform.position = spawning_position;
        spawningTiles[lineNumber - 1].SendMessage("MonsterDrop", dequeued_monster);
        Tile t11 = TileContainer.Instance.getTile(1, 1);
        _monsterCreator.fullyCreateMonster();
        MonsterCalculator.Instance.isFinished = false;
        }
    }
}

public partial class MonsterSpawn : MonoBehaviour
{
    void Start()
    {
        _monsterCreator = (MonsterCreator)FindObjectOfType(typeof(MonsterCreator));
    }

    // Update is called once per frame
    void Update()
    {
    }
}