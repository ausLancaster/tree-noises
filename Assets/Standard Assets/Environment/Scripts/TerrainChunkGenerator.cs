using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Terrain
{
    public class TerrainChunkGenerator : MonoBehaviour
    {
        public Material terrainMaterial;

        private TerrainChunkSettings settings;

        public INoiseProvider noiseProvider;

        private List<IFeatureGenerator> featureGenerator;

        public TerrainChunk terrainChunk;

        private void Awake()
        {
            settings = new TerrainChunkSettings(129, 200, 0.1f);
            noiseProvider = new SeussNoise0();
            featureGenerator = new List<IFeatureGenerator>();
            featureGenerator.Add(new TreePlacer(settings.length));

            CreateTerrainChunk();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Generate"))
            {
                RemoveTerrainChunk();
                CreateTerrainChunk();
            }
        }

        private void CreateTerrainChunk()
        {
            terrainChunk = new TerrainChunk(settings, noiseProvider, featureGenerator, terrainMaterial);
            terrainChunk.GenerateHeightmap();
            terrainChunk.CreateTerrain();
        }

        private void RemoveTerrainChunk()
        {
            terrainChunk.Destroy();
        }

        public float GetHeight (float x, float z)
        {
            return terrainChunk.GetTerrainHeight(x, z);
        }
    }
}