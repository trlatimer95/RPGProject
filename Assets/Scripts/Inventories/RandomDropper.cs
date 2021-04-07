﻿using GameDevTV.Inventories;
using RPG.Stats;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Inventories
{
    public class RandomDropper : ItemDropper
    {
        // CONFIG DATA
        [Tooltip("How far can teh pickups be scattered from the dropper")]
        [SerializeField] float scatterDistance = 1;
        [SerializeField] DropLibrary dropLibrary;

        const int ATTEMPTS = 30;

        public void RandomDrop()
        {
            var baseStats = GetComponent<BaseStats>();
            var drops = dropLibrary.GetRandomDrops(baseStats.GetLevel());
            foreach (var drop in drops)
            {
                DropItem(drop.item, drop.number);
            }
            
        }

        protected override Vector3 GetDropLocation()
        {
            for (int i = 0; i < ATTEMPTS; i++)
            {
                Vector3 randomPont = transform.position + Random.insideUnitSphere * scatterDistance;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPont, out hit, 0.1f, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }
            return transform.position;
        }

    }
}
