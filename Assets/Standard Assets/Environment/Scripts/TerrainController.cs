using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
    public class TerrainController : MonoBehaviour
    {

        public Transform player;
        public TerrainChunkGenerator generator;

        private int radius = 3;
        private Vector2i PreviousPlayerChunkPosition;

        // Use this for initialization
        void Start()
        {
            generator.UpdateTerrain(player.position, radius);
            PreviousPlayerChunkPosition = generator.GetChunkPosition(player.position);
        }

        // Update is called once per frame
        void Update()
        {
            var playerChunkPosition = generator.GetChunkPosition(player.position);
            if (!playerChunkPosition.Equals(PreviousPlayerChunkPosition))
            {
                generator.UpdateTerrain(player.position, radius);
                PreviousPlayerChunkPosition = playerChunkPosition;
            }
        }
    }

}
