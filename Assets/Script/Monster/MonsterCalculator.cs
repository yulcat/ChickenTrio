using UnityEngine;
using System.Collections.Generic;
using System.Collections;
//using System.Collections.IEnumerator;
using System;
using UnityEngine;

partial class MonsterCalculator : MonoBehaviour
{
    public bool isFinished;
    public bool isEggRain;
    public int chainingNum;
    public int combineRule = 1;
    public int SpawnOrder;  //일단 public
    public GUIText GameoverText;
}
partial class MonsterCalculator : MonoBehaviour
{
    private static MonsterCalculator instance = null;

    public static MonsterCalculator Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(MonsterCalculator)) as MonsterCalculator;
            return instance;
        }
    }
}

partial class MonsterCalculator : MonoBehaviour
{
    public void PrintMap()
    {
        Debug.Log("Monster data Start the Map");
        for (int height = Const.TileHeight - 1; height >= 0; height--)
        {
            string row = "";
            for (int width = 0; width < Const.TileWidth; width++)
            {
                if (row.Length != 0)
                {
                    row += " ";
                }
                //row += _monsterGroupData[width, height];
                row += _monsterData[width, height];
            }
            Debug.Log(row);

        }
        Debug.Log("End printing Map");
    }
}

partial class MonsterCalculator : MonoBehaviour
{ 
    private int[,] _monsterData;
    public int[,] MonsterData
    {
        get
        {
            return _monsterData;
        }
    }
    private int[,] _monsterGroupData;
    private List<int> _monsterGroupList;
    private bool is_removed;
    int finalMonsterRank;
    
    public int getMonsterData(int y, int x)
    {
        return _monsterData[x - 1, y - 1];
    }
    public void setMonsterData(int y, int x, int type)
    {
        _monsterData[x - 1, y - 1] = type;
    }
    public int getMonsterHeight(int x)
    {
        for (int y = Const.TileHeight; y > 0; y--)
        {
            if (getMonsterData(y, x) != 0) return y+1;
        }
        return 1;
    }
    public int getMonsterHoleHeight(int x)
    {
        for (int i = 1; i < Const.TileHeight + 1; i++)
        {
            if (getMonsterData(i, x) == 0) return i;
        }
        return -1;
    }
    public void groupingMonsterStarter()
    {
        StartCoroutine(groupingChicken());
       // StartCoroutine(groupingMonster());
    }
    public System.Collections.IEnumerator groupingMonster()
    {
        //isFinished = false;
        for (int i=0;i<Const.TileHeight;i++){
            for (int j=0;j<Const.TileWidth;j++){
                _monsterGroupData[i,j] = 0;
            }
        }
        _monsterGroupList = new List<int>();
    
        int group_num = 1;
        for (int i=0;i<Const.TileWidth;i++){
            for (int j=0;j<Const.TileHeight;j++){
                if (_monsterData[i, j] != 0)
                {
                    if (_monsterGroupData[i, j] == 0)
                    {
                        _monsterGroupData[i, j] = group_num;
                        _monsterGroupList.Add(1);
                        group_num++;
                    }

                    if (j + 1 < getMonsterHeight(i + 1) - 1)
                    {
                        //y 값 비교 : 두 개가 같은 타일이거나 둘 다 성장한 닭인 것을 찾는다.
                        if ( (_monsterData[i, j] + _monsterData[i, j + 1]) % 10 == 2 * finalMonsterRank)
                        {
                            if (_monsterGroupData[i, j + 1] == 0)
                            {
                                _monsterGroupData[i, j + 1] = _monsterGroupData[i, j];
                                int temp = _monsterGroupData[i, j] - 1;
                                int pastValue = _monsterGroupList[temp];
                                _monsterGroupList[temp] = pastValue + 1;
                            }
                            else
                            {
                                int temp = _monsterGroupData[i, j + 1] - 1;
                                int temp2 = _monsterGroupData[i, j] - 1;
                                if (temp != temp2)
                                {
                                    StartCoroutine(changeGroup(_monsterGroupData[i, j + 1], _monsterGroupData[i, j]));
                                    int pastValue = _monsterGroupList[temp];
                                    int pastValue2 = _monsterGroupList[temp2];
                                    _monsterGroupList[temp2] = pastValue + pastValue2;
                                    _monsterGroupList[temp] = 0;
                                }
                            }
                        }
                        else if (_monsterData[i, j+1] != 0)
                        {
                            if (_monsterGroupData[i, j + 1] == 0)
                            {
                                _monsterGroupData[i, j + 1] = group_num;
                                _monsterGroupList.Add(1);
                                group_num++;
                            }
                        }
                    }

                    if (i + 1 < Const.TileWidth)
                    {
                        if (_monsterData[i + 1, j] != 0)
                        {
                            //x 값 비교 : 두 개가 같은 타일이거나 둘 다 성장한 닭인 것을 찾는다.
                            if ( (_monsterData[i, j] + _monsterData[i + 1, j]) % 10 == 2 * finalMonsterRank)
                            {
                                if (_monsterGroupData[i + 1, j] == 0)
                                {
                                    _monsterGroupData[i + 1, j] = _monsterGroupData[i, j];
                                    int temp = _monsterGroupData[i, j] - 1;
                                    int pastValue = _monsterGroupList[temp];
                                    _monsterGroupList[temp] = pastValue + 1;
                                }
                                else
                                {
                                    int temp = _monsterGroupData[i + 1, j] - 1;
                                    int temp2 = _monsterGroupData[i, j] - 1;
                                    if (temp != temp2)
                                    {
                                        StartCoroutine(changeGroup(_monsterGroupData[i, j], _monsterGroupData[i + 1, j]));
                                        int pastValue = _monsterGroupList[temp];
                                        int pastValue2 = _monsterGroupList[temp2];
                                        _monsterGroupList[temp2] = pastValue + pastValue2;
                                        _monsterGroupList[temp] = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


    
        //is_removed = false;
        for (int i=0;i<_monsterGroupList.Count;i++)
        {
            //Debug.Log("List " + i + " : " + _monsterGroupList[i]);
            if (_monsterGroupList[i] > 2)
            {
                if (IsSameColorChicken3(i + 1))
                {
                    Debug.LogWarning("같은 색의 닭 3마리가 모였습니다. 세상이 멸망합니다.");
                }

                if (isBreak(i + 1))
                {

                    //  이 위까지가 합칠 지 여부를 결정하는 코드

                    #region 합치는 코드
                    if (combineRule == 0)
                    {
                        combineMonsterPlace(i + 1);
                    }
                    else if (combineRule == 1)
                    {
                        combineMonsterOrder(i + 1);
                    }
                    #endregion
                    //PrintMap();
                    /*
                    yield return new WaitForSeconds(0.4f);



                    StartCoroutine(dropMonster());  // 빈 공간을 메꾸는 코드
                     * */
                    //dropMonster();
                    is_removed = true;
                }


            }
        }

        if (is_removed)
        {
            yield return new WaitForSeconds(0.4f);



            StartCoroutine(dropMonster());  // 빈 공간을 메꾸는 코드
        }
        
        if (is_removed == false)
        {
            if (isGameOver() == true)
            {
                GameoverText.text = "GAMEOVER";
                isFinished = false;//임시?
            }
            else isFinished = true;

            chainingNum = 1;
            //Debug.Log("end");

            if (isEggRain)
            {
                Debug.LogWarning("닭갈비");
                isEggRain = false;
                StartCoroutine(eggRain());
            }
        }
        else
        {
            chainingNum++;
            //Debug.Log("chain up" + chainingNum);
            yield return new WaitForSeconds(0.6f);
            StartCoroutine(groupingChicken());
            //StartCoroutine(groupingMonster());
        }
    }
}

public class BirdData
{
    public Point position;
    public int kind;
    public int color;


    public BirdData(int kind, int color, Point position)
    {
        this.kind = kind;
        this.color = color;
        this.position = position;
    }
}

public struct Point
{
    public int x;
    public int y;

    public Point(int i, int j)
    {
        // TODO: Complete member initialization
        this.x = i;
        this.y = j;
    }
}

public class Group
{
    public int id = -1;
    public int kind;
    public int color;

    public int Count
    {
        get { return BirdList.Count; }
    }

    public List<BirdData> BirdList;

    public Group(int id, int kind, int color)
    {
        BirdList = new List<BirdData>();
        this.id = id;
        this.kind = kind;
        this.color = color;
    }

    public void Add(int x, int y)
    {
        BirdData birdData = new BirdData(id, kind, new Point(x, y));
        birdData.kind = this.kind;
        birdData.color = this.color;
        BirdList.Add(birdData);
    }

    public void Eat(Group prey)
    {
        BirdList.AddRange(prey.BirdList);
        prey.BirdList = null;
    }

    internal void Remove(int min_spawn_order_x, int min_spawn_order_y)
    {
        BirdData toRemove = null;
        foreach (BirdData bird in BirdList)
        {
            if (bird.position.x != min_spawn_order_x)
                continue;
            if (bird.position.y != min_spawn_order_y)
                continue;

            toRemove = bird;
        }
        Debug.LogWarning(BirdList.Count);
        BirdList.Remove(toRemove);
        Debug.LogWarning(BirdList.Count);
    }
}


partial class GroupManager
{
    private static GroupManager instance = null;

    public static GroupManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GroupManager();
            return instance;
        }
    }

    private int[,] monsterData
    {
        get { return MonsterCalculator.Instance.MonsterData; }
    }

    internal bool CompareToUp(int x, int y)
    {
        if (monsterData[x, y] == 41) return false;
        if (y + 1 < MonsterCalculator.Instance.getMonsterHeight(x + 1) - 1) // 이녀석 위에 다른 녀석이 존재한다면
        {
            if (monsterData[x, y] == monsterData[x, y + 1])
            {
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    internal bool CompareToRight(int x, int y)
    {
        if (monsterData[x, y] == 41) return false;
        if (x + 1 >= Const.TileWidth)
        {
            return false;
        }

        if (monsterData[x + 1, y] == 0)
        {
            return false;
        }

        if (monsterData[x, y] == monsterData[x + 1, y])
        {
            return true;
        }

        return false;
    }

    internal void ExpandGroupUp(int x, int y)
    {
        int upperGroupId = chickenGroupData[x, y + 1];
        int myGroupId = chickenGroupData[x, y];

        if (upperGroupId == myGroupId)
        {
            return;
        }

        if (chickenGroupData[x, y + 1] == NOT_GROUP)
        {
            chickenGroupData[x, y + 1] = chickenGroupData[x, y];

            Group group = chickenGroup[myGroupId];
            group.Add(x, y + 1);
        }
        else
        {
            Group myGroup = chickenGroup[myGroupId];
            Group upeerGroup = chickenGroup[upperGroupId];

            myGroup.Eat(upeerGroup);

            chickenGroup.Remove(upperGroupId);
            chickenGroupData[x, y + 1] = myGroupId;
        }
    }

    internal void ExpandGroupRight(int x, int y)
    {
        int rightGroupId = chickenGroupData[x + 1, y];
        int myGroupId = chickenGroupData[x, y];

        if (rightGroupId == myGroupId)
        {
            return;
        }

        if (chickenGroupData[x + 1, y] == NOT_GROUP)
        {
            chickenGroupData[x + 1, y] = chickenGroupData[x, y];

            Group group = chickenGroup[myGroupId];
            group.Add(x + 1, y);
        }
        else
        {
            Group myGroup = chickenGroup[myGroupId];
            Group upeerGroup = chickenGroup[rightGroupId];

            myGroup.Eat(upeerGroup);

            chickenGroup.Remove(rightGroupId);
            chickenGroupData[x, y + 1] = myGroupId;
        }
    }
}


public partial class GroupManager
{
    public int[,] chickenGroupData = new int[Const.TileWidth, Const.TileHeight];
    //public List<Group> chickenGroup;
    public Dictionary<int, Group> chickenGroup;
    private readonly int NOT_GROUP = -1;
    private int current_group_count = 0;

    public GroupManager()
    {
        InitializeChickenGrouping();
    }

    public void PrintMap()
    {
        Debug.Log("Start the Map");
        for (int height = Const.TileHeight - 1; height >= 0; height--)
        {
            string row = "";
            for (int width = 0; width < Const.TileWidth; width++)
            {
                if (row.Length != 0)
                {
                    row += " ";
                }
                row += chickenGroupData[width, height];
            }
            Debug.Log(row);

        }
        Debug.Log("End printing Map");
    }

    public void InitializeChickenGrouping()
    {
        current_group_count = 0;
        chickenGroup = new Dictionary<int, Group>();
        for (int i = 0; i < Const.TileWidth; i++)
        {
            for (int j = 0; j < Const.TileHeight; j++)
            {
                chickenGroupData[i, j] = NOT_GROUP;
            }
        }
    }

    int GetGroupId(int x, int y)
    {
        return chickenGroupData[x, y];
    }

    /*public bool isTileEmpty(int x, int y)
    {
        return chickenGroupData[x,y] == Constants.EMPTY_TILE;
    }*/

    public bool IsBelongedGroup(int x, int y)
    {
        if (chickenGroupData[x, y] == NOT_GROUP)
        {
            return false;
        }
        return true;
    }


    private void AddToGroup(int group_id, int number)
    {

    }

    private int getMonsterData(int y, int x)
    {
        return MonsterCalculator.Instance.getMonsterData(y,x);
    }

    private int getMonsterHeight(int x)
    {
        for (int y = Const.TileHeight; y > 0; y--)
        {
            if (getMonsterData(y, x) != 0) return y + 1;
        }
        return 1;
    }

    internal void MakeGroup(int i, int j, int chickenKind)
    {
        //GroupManager.Instance.MakeGroup(i, j);

        int new_id = current_group_count;
        Group group = new Group(new_id,chickenKind % 10, chickenKind / 10);
        group.Add(i,j);
        chickenGroup.Add(new_id, group);
        //group.Add(new chi
        
        /// TODO: 나중에 체크
        chickenGroupData[i, j] = new_id;

        current_group_count++;
    }

}

partial class MonsterCalculator : MonoBehaviour
{
    public List<int> chickenGroup;
    public int[,] chickenGroupData = new int[Const.TileWidth, Const.TileHeight];

    private readonly int NOT_GROUP = 0;

    private void InitializeChickenGrouping()
    {

        for (int i = 0; i < Const.TileWidth; i++)
        {
            for (int j = 0; j < Const.TileHeight; j++)
            {
                chickenGroupData[i, j] = NOT_GROUP;
            }
        }
    }

    private bool isTileEmpty(int chicken_id)
    {
        return chicken_id == Constants.EMPTY_TILE;
    }

    private bool IsBelongedGroup(int x, int y)
    {
        if (_monsterData[x, y] == 41) return false;
        if (chickenGroupData[x, y] == NOT_GROUP)
        {
            return false;
        }
        return true;
    }

   public System.Collections.IEnumerator groupingChicken()
    {
        int currentChickenKind = 0;
        is_removed = false;

        GroupManager.Instance.InitializeChickenGrouping();

        for (int x = 0; x < Const.TileWidth; x++)
        {
            for (int y = 0; y < Const.TileHeight; y++)
            {
                currentChickenKind = _monsterData[x, y];

                if (currentChickenKind == 0)
                {
                    continue;
                }

                //if (IsChicken(currentChickenKind) == false)
                //{
                //    continue;
                //}

                //그룹이 없다면 새로 만든다.
                if (GroupManager.Instance.IsBelongedGroup(x, y) == false)
                {
                    GroupManager.Instance.MakeGroup(x, y, currentChickenKind);
                }

                if (GroupManager.Instance.CompareToUp(x,y) == true)
                {
                    GroupManager.Instance.ExpandGroupUp(x, y);
                }

                if (GroupManager.Instance.CompareToRight(x, y) == true)
                {
                    GroupManager.Instance.ExpandGroupRight(x, y);
                }
            }
        }


       // Count >= 3 그룹들을 찾는다.

       // 그 그룹들을 합친다.

       // 빈 공간을 채운다.

        List<Group> over3match = new List<Group>();
        foreach (Group group in GroupManager.Instance.chickenGroup.Values)
        {
            if (group.Count >= 3)
            {
                if (group.kind == 3)
                {
                    isEggRain = true;
                    Debug.LogWarning("닭트리오!");
                }
                over3match.Add(group);
            }
        }

        foreach (Group group in over3match)
        {
            is_removed = true;
            combineChicken(group);

            //yield return new WaitForSeconds(0.4f);

            //yield return StartCoroutine(dropMonster());
        }
        //yield return StartCoroutine(dropMonster());
        GroupManager.Instance.PrintMap();

        //isFinished = true;
        yield return StartCoroutine(groupingMonster());


    }

   private void combineChicken(Group group)
   {
       int min_spawn_order = -1, min_spawn_order_x = -1, min_spawn_order_y = -1;
       foreach(BirdData bird in group.BirdList)
       {
           Tile temp_tile = TileContainer.Instance.getTile(bird.position.y + 1, bird.position.x + 1);
           Monster temp_monster = temp_tile.getMonster();
           int temp_spawn_order = temp_monster.getSpawnOrder();

           if (temp_spawn_order > min_spawn_order)
           {
               min_spawn_order = temp_spawn_order;
               min_spawn_order_x = bird.position.x;
               min_spawn_order_y = bird.position.y;
           }
       }

       if (min_spawn_order != -1)
       {
           //if (_monsterData[min_spawn_order_x, min_spawn_order_y] == 33) _monsterData[min_spawn_order_x, min_spawn_order_y] = 11;
           //else if (_monsterData[min_spawn_order_x, min_spawn_order_y] % 10 == finalMonsterRank) _monsterData[min_spawn_order_x, min_spawn_order_y] += 8;
           //else _monsterData[min_spawn_order_x, min_spawn_order_y]++;
           //알로 귀환

           if (_monsterData[min_spawn_order_x, min_spawn_order_y] % 10 == finalMonsterRank)
           {

               StartCoroutine(deleteGroup(group));        
               _monsterGroupData[min_spawn_order_x, min_spawn_order_y] = 0;
               return;
           }
           else
           {
               _monsterData[min_spawn_order_x, min_spawn_order_y]++;
           }

           _monsterGroupData[min_spawn_order_x, min_spawn_order_y] = 0;

           group.Remove(min_spawn_order_x, min_spawn_order_y);
           StartCoroutine(gatherMonster(group, min_spawn_order_x, min_spawn_order_y));
           
           //changeGroup(group_num, 0);

           monsterRankUp(min_spawn_order_x, min_spawn_order_y);
       }

   }
}
partial class MonsterCalculator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        finalMonsterRank = MonsterCreator.Instance.finalMonsterRank;
        isFinished = true;
        isEggRain = false;
        chainingNum = 1;
	    _monsterData = new int[Const.TileHeight, Const.TileHeight];
        _monsterGroupData = new int[Const.TileHeight, Const.TileHeight];
        for (int i = 0; i < Const.TileHeight; i++)
        {
            for (int j = 0; j < Const.TileWidth; j++)
            {
                _monsterData[i, j] = 0;
            }
        }
        SpawnOrder = 0;
        //var aSources = GetComponent(AudioSource);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

partial class MonsterCalculator : MonoBehaviour
{
    private System.Collections.IEnumerator changeGroup(int from_num, int to_num)
    {
        //Debug.Log("" + from_num + " to " + to_num);
        for (int i = 0; i < Const.TileHeight; i++)
        {
            for (int j = 0; j < Const.TileWidth; j++)
            {
                if (_monsterGroupData[i, j] == from_num)
                {
                    _monsterGroupData[i, j] = to_num;
                    if (to_num == 0)
                    {
                        _monsterData[i, j] = 0;
                        Tile temp_tile = TileContainer.Instance.getTile(j + 1, i + 1);
                        temp_tile.deleteMonster();
                    }
                }
            }
        }
        yield return new WaitForSeconds(0.0f); ///??
    }

    private System.Collections.IEnumerator deleteGroup(Group group)
    {
        foreach (BirdData bird in group.BirdList)
        {
            int i = bird.position.x;
            int j = bird.position.y;

            _monsterData[i, j] = 0;
            Tile temp_tile = TileContainer.Instance.getTile(j + 1, i + 1);
            temp_tile.deleteMonster();
        }
        yield return new WaitForSeconds(0.0f); ///??
    }

    private void combineMonsterPlace(int group_num)
    {
        bool fin = false;
        for (int j = 0; j < Const.TileWidth; j++)
        {
            for (int i = 0; i < Const.TileHeight; i++)
            {
                if (_monsterGroupData[i, j] == group_num)
                {
                    if (_monsterData[i,j] % 10 == finalMonsterRank)
                    {
                        StartCoroutine(changeGroup(group_num, 0));
                    }
                    else
                    {
                        _monsterData[i, j]++;
                        //Debug.Log("check : " + _monsterData);
                        _monsterGroupData[i, j] = 0;
                        StartCoroutine(changeGroup(group_num, 0));

                        monsterRankUp(i, j);
                    }
                    fin = true;
                    break;
                }
            }
            if (fin == true) break;
        }
    }

    private void combineMonsterOrder(int group_num)
    {
        int min_spawn_order = -1, min_spawn_order_x = -1, min_spawn_order_y = -1;
        for (int j = 0; j < Const.TileWidth; j++)
        {
            for (int i = 0; i < Const.TileHeight; i++)
            {
                //Debug.Log("MonsterData : " + i + j + " " + _monsterData[i, j]);
                if (_monsterGroupData[i, j] == group_num)
                {
                    //if (_monsterData[i, j] % 10 == finalMonsterRank)
                    //{
                    //    StartCoroutine(changeGroup(group_num, 0));
                    //}
                    //else
                    //{
                        Tile temp_tile = TileContainer.Instance.getTile(j + 1, i + 1);
                        Monster temp_monster = temp_tile.getMonster();
                        int temp_spawn_order = temp_monster.getSpawnOrder();
                        //Debug.Log("order : " + temp_spawn_order);
                        if (temp_spawn_order > min_spawn_order)
                        {
                            min_spawn_order = temp_spawn_order;
                            min_spawn_order_x = i;
                            min_spawn_order_y = j;
                        }
                    //}
                }
            }
        }

        if (min_spawn_order != -1)
        {
            //if (_monsterData[min_spawn_order_x, min_spawn_order_y] == 33) _monsterData[min_spawn_order_x, min_spawn_order_y] = 11;
            //else if (_monsterData[min_spawn_order_x, min_spawn_order_y] % 10 == finalMonsterRank) _monsterData[min_spawn_order_x, min_spawn_order_y] += 8;
            //else _monsterData[min_spawn_order_x, min_spawn_order_y]++;
            //알로 귀환

            if (_monsterData[min_spawn_order_x, min_spawn_order_y] % 10 == finalMonsterRank) _monsterData[min_spawn_order_x, min_spawn_order_y] = 41;
            else _monsterData[min_spawn_order_x, min_spawn_order_y]++;

            _monsterGroupData[min_spawn_order_x, min_spawn_order_y] = 0;
            
            StartCoroutine(gatherMonster(group_num, min_spawn_order_x, min_spawn_order_y));
            //changeGroup(group_num, 0);

            monsterRankUp(min_spawn_order_x, min_spawn_order_y);
        }
    }

    
    private System.Collections.IEnumerator dropMonster()
    {
        for (int i = 0; i < Const.TileWidth; i++)
        {
            for (int j = 0 ; j < Const.TileHeight; j++)
            {
                //Debug.Log("check : " + i + j);
                if (_monsterData[i, j] != Constants.EMPTY_TILE)
                {
                    int hole_height = getMonsterHoleHeight(i + 1) - 1;
                    if (hole_height == -2) break; //빈칸없음
                    if (j > hole_height)
                    {
                        moveMonster(i, j, i, hole_height);
                    }
                }
            }
        }
        yield return new WaitForSeconds(0.0f); ///??
    }
    
    private bool isGameOver()
    {
        for (int i = 0; i < Const.TileWidth; i++)
        {
            int height = getMonsterHeight(i + 1) - 1;
            if (height < 7) return false;
        }
        return true;
    }
    private void monsterRankUp(int i, int j)
    {
        audio.Play();

        Tile temp_tile = TileContainer.Instance.getTile(j + 1, i + 1);

        temp_tile.deleteMonster();
        int monsterListOrder = _monsterData[i, j] / 10;
        monsterListOrder = (monsterListOrder - 1) * 3 + _monsterData[i, j] % 10 - 1;
        GameObject combined_monster = MonsterCreator.Instance.getMonster(monsterListOrder);
        combined_monster.transform.position = temp_tile.transform.position + new Vector3(0, 0, -1);//z축 위치?
        Monster newMonster = gameObject.AddComponent<Monster>();
        newMonster.setMonsterSprite(combined_monster);
        newMonster.setSpawnOrder(MonsterCalculator.Instance.SpawnOrder);
        temp_tile.setMonster(newMonster);

        MonsterCalculator.Instance.SpawnOrder++;

        int addValue;
        if (monsterListOrder % 3 == 1) addValue = 1;
        else if (monsterListOrder % 3 == 2) addValue = 3;
        else addValue = 9;
        addValue *= chainingNum*chainingNum;

        if (monsterListOrder == 9) addValue += 100;

        int score = Score.Instance.getScore();
        Score.Instance.setScore(score + addValue);
        //Debug.Log("score display" + chainingNum);
        //Debug.Log("RankUp" + _monsterData[i,j] + " " +monsterListOrder);
    }
    private void moveMonster(int x1, int y1, int x2, int y2)
    {
        //Debug.Log("move : " + x1 + y1 + " " + x2 + y2);
        Tile from_tile = TileContainer.Instance.getTile(y1 + 1, x1 + 1);
        Tile to_tile = TileContainer.Instance.getTile(y2 + 1, x2 + 1);
        Monster temp_monster = from_tile.getMonster();
        GameObject temp_monster_sprite = temp_monster.getMonsterSprite();

        Hashtable ht = new Hashtable();
        ht.Add("position", to_tile.transform.position + new Vector3(0, 0, -1));//z축 위치?
        ht.Add("speed", 10);

        iTween.MoveTo(temp_monster_sprite, ht);
        _monsterData[x2, y2] = _monsterData[x1, y1];
        _monsterData[x1, y1] = 0;
        to_tile.setMonster(temp_monster);
        //from_tile.setMonster();  //?
    }

    private System.Collections.IEnumerator gatherMonster(Group group, int x, int y)
    {

        foreach (BirdData bird in group.BirdList)
        {
            int i = bird.position.x;
            int j = bird.position.y;

            Tile temp_tile = TileContainer.Instance.getTile(j + 1, i + 1);
            Tile gathering_tile = TileContainer.Instance.getTile(y + 1, x + 1);
            Monster temp_monster = temp_tile.getMonster();
            GameObject temp_monster_sprite = temp_monster.getMonsterSprite();

            Hashtable ht = new Hashtable();
            ht.Add("position", gathering_tile.transform.position + new Vector3(0, 0, 0));//z축 위치?
            //ht.Add("speed", 10);
            ht.Add("time", 0.4);

            iTween.MoveTo(temp_monster_sprite, ht);
        }

        yield return new WaitForSeconds(0.4f);
        StartCoroutine(deleteGroup(group));
    }

    private System.Collections.IEnumerator gatherMonster(int group_num, int x, int y)
    {
        for (int i = 0; i < Const.TileHeight; i++)
        {
            for (int j = 0; j < Const.TileWidth; j++)
            {
                if (_monsterGroupData[i, j] == group_num)
                {
                    Tile temp_tile = TileContainer.Instance.getTile(j + 1, i + 1);
                    Tile gathering_tile = TileContainer.Instance.getTile(y + 1, x + 1);
                    Monster temp_monster = temp_tile.getMonster();
                    GameObject temp_monster_sprite = temp_monster.getMonsterSprite();

                    Hashtable ht = new Hashtable();
                    ht.Add("position", gathering_tile.transform.position + new Vector3(0, 0, 0));//z축 위치?
                    //ht.Add("speed", 10);
                    ht.Add("time", 0.4);

                    iTween.MoveTo(temp_monster_sprite, ht);
                }
            }
        }
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(changeGroup(group_num, 0));
    }
    private bool isBreak(int group_num)
    {
        bool red = false, green = false, blue = false;
        for (int j = 0; j < Const.TileWidth; j++)
        {
            for (int i = 0; i < Const.TileHeight; i++)
            {
                if (_monsterGroupData[i, j] == group_num)
                {
                    if (_monsterData[i, j] / 10 == 4) return false;
                    else if (_monsterData[i,j] % 10 != finalMonsterRank) //최적화?
                    {
                        return true;
                    }
                    else
                    {
                        if (_monsterData[i, j] / 10 == 1) red = true;
                        else if (_monsterData[i, j] / 10 == 2) green = true;
                        else blue = true;
                    }
                }
            }
        }
        if (red && green && blue) return true;
        else return false;
    }

    private bool IsSameColorChicken3(int group_id)
    {
        // assert 여기에 도달한 그룹은 3개 이상의 타일이 모여있는 그룹입니다.
        int chicken_id = -1;
        for (int i = 0; i < Const.TileHeight; i++)
        {
            for (int j = 0; j < Const.TileWidth; j++)
            {
                int current_tile_group = _monsterGroupData[i, j];

                if (current_tile_group == group_id) // 원하는 그룹인가.
                {
                    if (chicken_id == -1)
                    {
                        chicken_id = _monsterData[i, j];
                        break;
                    }
                }
            }
        }

        if (IsChicken(chicken_id) == false)
        {
            return false;
        }

        for (int i = 0; i < Const.TileHeight; i++)
        {
            for (int j = 0; j < Const.TileWidth; j++)
            {
                int current_tile_group = _monsterGroupData[i, j];

                if (current_tile_group == group_id)
                {
                    if (chicken_id != _monsterData[i, j])
                    {
                        return false; //다른 색의 치킨입니다.
                    }
                }

            }
        }

        return true;
    }

    private bool IsChicken(int chicken_data)
    {
        return (chicken_data % 10) == MonsterCreator.Instance.finalMonsterRank;
    }

    private System.Collections.IEnumerator eggRain()
    {
        MonsterCreator _monsterCreator = (MonsterCreator)FindObjectOfType(typeof(MonsterCreator));
        for (int i = 0; i < Const.TileWidth; i++)
        {
            if (getMonsterHeight(i + 1) < Const.TileHeight + 1)
            {
                Tile tempTile = TileContainer.Instance.getTile(Const.TileHeight, i + 1);
                Vector3 spawning_position = tempTile.transform.position;
                int monster_index = UnityEngine.Random.Range(0, 3) * 3; //typeNum = 3
                GameObject egg_monster = _monsterCreator.getEgg(monster_index);
                egg_monster.transform.position = spawning_position;

                Monster eggMonster = gameObject.AddComponent<Monster>();
                eggMonster.setMonsterSprite(egg_monster);
                eggMonster.setSpawnOrder(MonsterCalculator.Instance.SpawnOrder);
                tempTile.setMonster(eggMonster);

                _monsterData[i, Const.TileHeight - 1] = monster_index / 3 * 10 + 11;

                MonsterCalculator.Instance.SpawnOrder++;
            }
        }
        isFinished = false;

        yield return new WaitForSeconds(0.4f);
        StartCoroutine(dropMonster());  // 빈 공간을 메꾸는 코드

        groupingMonsterStarter();
    }
}
