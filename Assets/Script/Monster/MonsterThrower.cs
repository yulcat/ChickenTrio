using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;

public partial class MonsterThrower : MonoBehaviour
{
    public void MonsterDrop(GameObject dequed_dequeued_monster)
    {
        //높이 찾기
        string temp_str = Regex.Replace(this.name, @"Tile", "");
        string[] splitted_str = Regex.Split(temp_str, "-");
        int lineNumber = int.Parse(splitted_str[0]);
        int height = MonsterCalculator.Instance.getMonsterHeight(lineNumber);

        //drop
        Tile top_tile = TileContainer.Instance.getTile(height, lineNumber);

        Hashtable ht = new Hashtable();
        ht.Add("position", top_tile.transform.position + new Vector3(0, 0, -1));//z축 위치?
        ht.Add("speed", 20);
        ht.Add("oncomplete", "groupingMonsterStarter");
        ht.Add("oncompletetarget", MonsterCalculator.Instance.gameObject);

        iTween.MoveTo(dequed_dequeued_monster, ht); //{"x":1}같은건 안되나?
        int monsterType = getMonsterType(dequed_dequeued_monster.name);
        MonsterCalculator.Instance.setMonsterData(height, lineNumber, monsterType); //몬스터에 숫자 할당

        dequed_dequeued_monster.AddComponent("Monster");

        Monster newMonster = gameObject.AddComponent<Monster>();
        newMonster.setMonsterSprite(dequed_dequeued_monster);
        newMonster.setSpawnOrder(MonsterCalculator.Instance.SpawnOrder);
        top_tile.setMonster(newMonster);

        

        MonsterCalculator.Instance.SpawnOrder++;


        //MonsterCalculator.Instance.PrintMap();
    }
}

public partial class MonsterThrower : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Debug.Log("start");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

partial class MonsterThrower : MonoBehaviour
{
    private int getMonsterType(string name)
    {
        if (name == "BlueEgg(Clone)") return 11;
        else if (name == "BlueChick(Clone)") return 12;
        else if (name == "BlueChicken(Clone)") return 13;
        else if (name == "GreenEgg(Clone)") return 21;
        else if (name == "GreenChick(Clone)") return 22;
        else if (name == "GreenChicken(Clone)") return 23;
        else if (name == "RedEgg(Clone)") return 31;
        else if (name == "RedChick(Clone)") return 32;
        else if (name == "RedChicken(Clone)") return 33;
        else return -1;
    }
}