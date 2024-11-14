# TapTapClicker
## 목차
1. 프로젝트 소개
2. 트러블 슈팅
3. 개발 중 어려웠던 점

# 🎮프로젝트 소개
2D 클리커 게임으로 몬스터를 터치로 처치하고 얻은 골드로 플레이어를 강화하는 게임입니다.

# 🚀트러블 슈팅
1. 몬스터가 생성 후 사라지지 않는 이슈

   1-1. 몬스터 삭제 후 MonsterController컴퍼넌트가 생성된 몬스터의 MonsterController컴퍼넌트로 들어오지 않고 프리팹의 MonsterController컴퍼넌트로 들어오는 현상
#### 수정 전
void MonsterSpawn()

{
    
    int rndMonsterIndex = Random.Range(0, monsters.Length);
    monsterData = monsters[rndMonsterIndex];
    rndMonster = monsters[rndMonsterIndex].monsterPrefabs;

    Instantiate(rndMonster, GameManager.Instance.Monster.transform);
    GameManager.Instance.Monster.monsterController = GetComponent<MonsterController>();
}

#### 수정 후
void MonsterSpawn()

{
   
    int rndMonsterIndex = Random.Range(0, monsters.Length);
    monsterData = monsters[rndMonsterIndex];
    rndMonster = monsters[rndMonsterIndex].monsterPrefabs;

    GameObject go = Instantiate(rndMonster, GameManager.Instance.Monster.transform);
    GameManager.Instance.Monster.monsterController = go.GetComponent<MonsterController>();
}

# 🎯개발 중 어려웠던 점
이번 개인과제를 하면서 어려웠던 점은 객체지향입니다.
코드를 짜면서 계속해서 이게 객체지향이 맞는지 아닌지 고민을 많이 했던거 같습니다.
객체지향이란 무엇이고 또 어떻게 사용해야 하는지 공부를 더 하고 다음 개발 때 객체지향에 대한 깊은 이해로 참여하려고 합니다.
