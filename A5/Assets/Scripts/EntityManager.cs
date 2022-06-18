using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {

    [SerializeField] 
    private List<Entity> Entities;

    [SerializeField]
    private List<Sprite> AllEnemies;

    [SerializeField]
    private GameObject EnemyPrefab;

    [SerializeField]
    private GameObject EnemyContainer;


    [SerializeField]
    private int _currentIndex;

    public Entity ActiveEntity => Entities[_currentIndex];
    public Entity NextEntity => Entities[(_currentIndex+1) % Entities.Count];
    public Entity Next2Entity => Entities[(_currentIndex+2) % Entities.Count];

    public List<Entity> Friends;
    public List<Entity> Enemies;
    public List<Entity> FriendsNotSelf;

    public static event Action<Entity> OnNextEntity;
    public static event Action<Entity> OnSetPreviousEntity;

    public static event Action OnEndTurn;

    void OnEnable(){
        Fighter.OnDie += OnFighterDie;
    }

    void OnDisable(){
        Fighter.OnDie -= OnFighterDie;
    }

    void Awake(){
        if (Friends == null) Friends = new List<Entity>();
        if (Enemies == null) Friends = new List<Entity>();
        if (FriendsNotSelf == null) Friends = new List<Entity>();
    }

    void Start() {
        _currentIndex = -1;
        CheckEnemies();
        SetNextEntity();
        SetEntitiesFriendship();
    }

    public void OnFighterDie(Entity who){
        Entities.Remove(who); 
        if (_currentIndex >= Entities.Count) _currentIndex = 0;
        CheckEnemies(); 
    }

    public void SetEntitiesFriendship() {
        Friends.Clear();
        Enemies.Clear();
        FriendsNotSelf.Clear();
        foreach (Entity e in Entities){
            if (e.Team != ActiveEntity.Team){
                Enemies.Add(e);
            } else {
                if (e != ActiveEntity) FriendsNotSelf.Add(e);
                Friends.Add(e);
            }
        }
    }

    public void SetNextEntity() {
        _currentIndex++;
        _currentIndex = _currentIndex % Entities.Count;
        OnNextEntity?.Invoke(ActiveEntity);
        if (_currentIndex == 0) {
            OnEndTurn?.Invoke();
            ResetFighters();
        }
    }

    public void CheckEnemies(){
        bool enemiesLeft = false;
        foreach (Entity ent in Entities){
            if (((Fighter)ent).Team == Team.Enemy) {
                enemiesLeft = true;
            }
        }
        if (!enemiesLeft) CreateNewEnemiesWave();
    }

    public void ResetFighters(){
        foreach (Entity ent in Entities){
            if (ent is Fighter){
                ((Fighter)ent).ResetFighter();
            }
        }
    }

    public void CreateNewEnemiesWave(){
        int amount = UnityEngine.Random.Range(1, 4);
        for (int i = 0; i < amount; i++){
            CreateEnemy(i, AllEnemies[(UnityEngine.Random.Range(0, AllEnemies.Count - 1))]);
        }
    }

    public void CreateEnemy(int index, Sprite sprite){
        GameObject e = Instantiate(EnemyPrefab);
        e.transform.SetParent(EnemyContainer.transform);
        e.GetComponent<SpriteRenderer>().sprite = sprite;
        e.GetComponent<Fighter>().CreatePossibleCommands();
        e.GetComponent<Fighter>().Team = Team.Enemy;
        e.name = "Enemy";
        e.transform.position = new Vector3(e.transform.position.x - (index % 2 == 0 ? - 2 : 0), e.transform.position.y - (index * 1.5f), e.transform.position.z);
        Entities.Add(e.GetComponent<Fighter>());
    }

    public void SetPreviousEntity() {
        
        _currentIndex--;
        if (_currentIndex < 0)
            _currentIndex = Entities.Count - 1;

        OnSetPreviousEntity?.Invoke(ActiveEntity);
    }

}
