using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Terrain
{
    public class TerrainChunkGenerator : MonoBehaviour
    {
        public Material terrainMaterial;

        private TerrainChunkSettings settings;

        private INoiseProvider noiseProvider;

        private List<IFeatureGenerator> featureGenerator;

        private ChunkCache cache;

        private void Awake()
        {
            settings = new TerrainChunkSettings(17, 50, 0.1f);
            noiseProvider = new SeussNoise0();
            featureGenerator = new List<IFeatureGenerator>();
            featureGenerator.Add(new TreePlacer(settings.length));

            cache = new ChunkCache();
        }

        private void Update()
        {
            cache.Update();
        }

        private void GenerateChunk(int x, int z)
        {
            if (cache.ChunkCanBeAdded(x, z))
            {
                var chunk = new TerrainChunk(settings, noiseProvider, featureGenerator, x, z, terrainMaterial);
                cache.AddNewChunk(chunk);
            }
        }

        private void RemoveChunk(int x, int z)
        {
            if (cache.ChunkCanBeRemoved(x, z))
                cache.RemoveChunk(x, z);
        }

        private List<Vector2i> GetChunkPositionsInRadius(Vector2i chunkPosition, int radius)
        {
            var result = new List<Vector2i>();

            for (var zCircle = -radius; zCircle <= radius; zCircle++)
            {
                for (var xCircle = -radius; xCircle <= radius; xCircle++)
                {
                    if (xCircle * xCircle + zCircle * zCircle < radius * radius)
                        result.Add(new Vector2i(chunkPosition.X + xCircle, chunkPosition.Z + zCircle));
                }
            }

            return result;
        }

        public void UpdateTerrain(Vector3 worldPosition, int radius)
        {
            var chunkPosition = GetChunkPosition(worldPosition);
            var newPositions = GetChunkPositionsInRadius(chunkPosition, radius);

            var loadedChunks = cache.GetGeneratedChunks();
            var chunksToRemove = loadedChunks.Except(newPositions).ToList();

            var positionsToGenerate = newPositions.Except(chunksToRemove).ToList();
            foreach (var position in positionsToGenerate)
                GenerateChunk(position.X, position.Z);

            foreach (var position in chunksToRemove)
                RemoveChunk(position.X, position.Z);
        }

        public Vector2i GetChunkPosition(Vector3 worldPosition)
        {
            var x = (int)Mathf.Floor(worldPosition.x / settings.length);
            var z = (int)Mathf.Floor(worldPosition.z / settings.length);

            return new Vector2i(x, z);
        }

        public bool IsTerrainAvailable(Vector3 worldPosition)
        {
            var chunkPosition = GetChunkPosition(worldPosition);
            return cache.IsChunkGenerated(chunkPosition);
        }

        public float GetTerrainHeight(Vector3 worldPosition)
        {
            var chunkPosition = GetChunkPosition(worldPosition);
            var chunk = cache.GetGeneratedChunk(chunkPosition);
            if (chunkPosition != null)
                return chunk.GetTerrainHeight(worldPosition);

            return 0;
        }
    }
}