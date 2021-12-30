using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class ActorManager : MonoBehaviour
    {
        public readonly List<IActor> _actors = new List<IActor>();

        private static ActorManager _instance;
        public static ActorManager Instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        
        private void Start()
        {
            // Get all actors
            var foundActor = FindObjectsOfType<MonoBehaviour>().OfType<IActor>();
            foreach (var actor in foundActor)
            {
                _actors.Add(actor);
            }
        }

        public void RemoveActor(IActor actor)
        {
            _actors.Remove(actor);
        }

        public void UpdateAllActors()
        {
            UpdateAllActorsByCycle(1);
        }

        public void UpdateAllActorsByCycle(int cycle)
        {
            for (var i = 0; i < cycle; i++)
            {
                foreach (var actor in _actors)
                {
                    actor.Act();
                }
            }
        }
    }
}