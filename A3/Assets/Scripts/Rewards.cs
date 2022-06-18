using System;
using System.Collections.Generic;
using UnityEngine;

public class Rewards : MonoBehaviour {

    private List<InventorySlot> _rewardsList;

    // Observer para determinar que pasa cuando se generan lasrecompensas
    public static event Action<List<InventorySlot>> OnRewardsGenerated;

    void OnEnable(){
        QuestNode.OnStartQuest += GenerateRewards;
    }

    void OnDisable(){
        QuestNode.OnStartQuest += GenerateRewards;
    }

    // Método para gener de forma aleatoria lasr ecompensas entre todos los items
    public void GenerateRewards(){
        if (_rewardsList == null) 
            _rewardsList = new List<InventorySlot>();
        else 
            _rewardsList.Clear();

        int limit = UnityEngine.Random.Range(1, 9);

        for (int i = 0; i < limit; i++){
            int amount = 1;
            Item it = ItemData.GetRandomItem();
            if (it.Type != IType.I_EQUIPMENT) amount = UnityEngine.Random.Range(6, 12);
            _rewardsList.Add(new InventorySlot(it, amount));
        }

        OnRewardsGenerated?.Invoke(_rewardsList);
    }

}
