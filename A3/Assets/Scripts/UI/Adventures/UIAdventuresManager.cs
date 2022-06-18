using System.Collections.Generic;
using UnityEngine;

public class UIAdventuresManager : MonoBehaviour {

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private GameObject _rewardsContainer;
    [SerializeField]
    private GameObject _rewardsSlotUI;

    public List<GameObject> _shownObjects;

    void OnEnable(){
        QuestNode.OnStartQuest += ShowAdventures;
        QuestNode.OnPickRewards += HideAdventures;

        Rewards.OnRewardsGenerated += ShowRewards;

        GameManager.GameReset += HideAdventures;
    }

    void OnDisable(){
        QuestNode.OnStartQuest -= ShowAdventures;
        QuestNode.OnPickRewards -= HideAdventures;

        Rewards.OnRewardsGenerated -= ShowRewards;

        GameManager.GameReset -= HideAdventures;
    }

    void Awake(){
        _animator = GetComponent<Animator>();
    }

    void Start(){
        HideAdventures();
    }

    public void ShowAdventures(){
        _animator.SetBool("adventures", true);
    }

    public void HideAdventures(){
        _animator.SetBool("adventures", false);
    }

    // Método para mostrar las recompensas generadas
    // @param List<InventrorySlot> rewards -> recompensas
    public void ShowRewards(List<InventorySlot> rewards){
        ClearRewardContainer();
        foreach (InventorySlot islot in rewards){
            MakeNewGOEntry().GetComponent<RewardsSlotUI>().LoadData(islot);
        }
    }

    // Método para limpiar todas las recompensas ya mostradas, solo los GO
    public void ClearRewardContainer(){
        if (_shownObjects == null) _shownObjects = new List<GameObject>();
        foreach (var item in _shownObjects) {
            if(item) Destroy(item);
        }
        _shownObjects.Clear();
    }

    // Método para crear el GO para mostrar las reocmpensas
    // @return GameObject -> útlima posición de la lista con el nuevo GameObject creado
    public GameObject MakeNewGOEntry(){
        _shownObjects.Add(Instantiate(_rewardsSlotUI, Vector3.zero, Quaternion.identity));
        _shownObjects[_shownObjects.Count - 1].transform.SetParent(_rewardsContainer.transform);
        return _shownObjects[_shownObjects.Count - 1];
    }

}