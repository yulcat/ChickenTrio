using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class MonsterCreator : MonoBehaviour
{
    private static MonsterCreator instance = null;

    public static MonsterCreator Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(MonsterCreator)) as MonsterCreator;
            return instance;
        }
    }
}

public partial class MonsterCreator : MonoBehaviour
{
    public GameObject[] randomlyGeneratedMonsterList;
    public int maximumQueueSize = 5;
    //���� object�� position�� �������� �ϱ� ������ List�� ����Ѵ�.
    public List<GameObject> _queue;
    public int finalMonsterRank = 3; //���� �Ⱦ�??
    public int monsterTypeNum = 3;
}

partial class MonsterCreator : MonoBehaviour
{
    public GameObject getMonster(int monster_type)
    {
        GameObject monster = Instantiate(randomlyGeneratedMonsterList[monster_type]) as GameObject;
        return monster;
    }
}

public partial class MonsterCreator : MonoBehaviour
{
    //���� �ٸ����� �߰��� ��Ұ� �����Ƿ� private�� ������ �д�.
    private GameObject create()
    {
        int monster_index = Random.Range(0, monsterTypeNum) * 3;
        int monster_type_prob = Random.Range(0, 13);
        if (monster_type_prob == 12)
        {
            monster_index += 2;
        }
        else if (monster_type_prob > 8)
        {
            monster_index += 1;
        }

        /*
        int monster_index = Random.Range(0, 3 * monsterTypeNum - 1);
        while (monster_index%3 >= deleteMonsterRank)
        {
            monster_index = Random.Range(0, 3 * monsterTypeNum - 1); //randomlyGeneratedMonsterList.Length);
        }
        */
        
        GameObject monster = Instantiate(randomlyGeneratedMonsterList[monster_index]) as GameObject;
        monster.transform.parent = transform;
        return monster;
    }
    //������ ����� ���� ���� �� ���Ƽ� ����� ����.
    private GameObject create(GameObject specialMonster)
    {
        GameObject monster = Instantiate(specialMonster) as GameObject;
        monster.transform.parent = transform;
        return monster;
    }
    public void fullyCreateMonster()
    {
        while (_queue.Count < maximumQueueSize)
        {
            GameObject monster = create();
            //�߾������� ����
            _queue.Add(monster);
        }
        arrange();
    }
    public void arrange()
    {
        GameObject monster;
        for (int i = 0; i < _queue.Count; i++)
        {
            monster = _queue[i];
            //ġŲ ���� ����
            float distance = 1.5503863f;
            monster.transform.localPosition = Vector3.right * ((float)(i) - (float)(_queue.Count - 1) / 2.0f) * distance;
        }
    }
    public GameObject dequeue()
    {
        GameObject removed_monster = _queue[0];
        _queue.RemoveAt(0);
        return removed_monster;
    }
    public GameObject getEgg(int monster_index)
    {
        GameObject monster = Instantiate(randomlyGeneratedMonsterList[monster_index]) as GameObject;
        monster.transform.parent = transform;
        return monster;
    }
}

public partial class MonsterCreator : MonoBehaviour
{
    void Start()
    {
        _queue = new List<GameObject>(maximumQueueSize);
        fullyCreateMonster();
    }
}