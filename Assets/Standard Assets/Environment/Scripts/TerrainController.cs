using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
    public class TerrainController : MonoBehaviour
    {

        public Transform player;
        public TerrainChunkGenerator generator;

        private int radius =6;
        private Vector2i previousPlayerChunkPosition;

        public void Start()
        {
            player.gameObject.SetActive(false);
            StartCoroutine(InitializeCoroutine());
        }

        private IEnumerator InitializeCoroutine()
        {
            var canActivateCharacter = false;

            generator.UpdateTerrain(player.position, radius);

            do
            {
                var exists = generator.IsTerrainAvailable(player.position);
                if (exists)
                    canActivateCharacter = true;
                yield return null;
            } while (!canActivateCharacter);

            previousPlayerChunkPosition = generator.GetChunkPosition(player.position);
            player.position = new Vector3(player.position.x, generator.GetTerrainHeight(player.position) + 0.98f, player.position.z);
            player.gameObject.SetActive(true);
        }

        void Update()
        {
            var playerChunkPosition = generator.GetChunkPosition(player.position);
            if (!playerChunkPosition.Equals(previousPlayerChunkPosition))
            {
                generator.UpdateTerrain(player.position, radius);
                previousPlayerChunkPosition = playerChunkPosition;
            }
        }
    }

}
