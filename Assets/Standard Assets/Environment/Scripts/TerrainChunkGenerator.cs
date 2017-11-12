using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Terrain
{
    public class TerrainChunkGenerator : MonoBehaviour
    {
        public Material terrainMaterial1;
        public Material terrainMaterial2;

        private TerrainChunkSettings settings;

        public INoiseProvider noiseProvider1;
        public INoiseProvider noiseProvider2;

        private List<IFeatureGenerator> featureGenerator;

        public TerrainChunk terrainChunk1;
        public TerrainChunk terrainChunk2;

        private float seed;

        private void Awake()
        {
            settings = new TerrainChunkSettings(129, 200, 0.1f);
            noiseProvider1 = new SeussNoise0(true);
            noiseProvider2 = new SeussNoise0(false);
            featureGenerator = new List<IFeatureGenerator>();
            featureGenerator.Add(new TreePlacer(settings.length));

            seed = Random.Range(0.0f, 100000.0f);

            CreateTerrainChunk();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Generate"))
            {
                seed = Random.Range(0.0f, 100000.0f);
                RemoveTerrainChunk();
                CreateTerrainChunk();
            }
        }

        private void CreateTerrainChunk()
        {
            terrainChunk1 = new TerrainChunk(settings, noiseProvider1, seed, featureGenerator, terrainMaterial1);
            terrainChunk1.GenerateHeightmap();
            terrainChunk1.CreateTerrain();

            terrainChunk2 = new TerrainChunk(settings, noiseProvider2, seed, featureGenerator, terrainMaterial2);
            terrainChunk2.GenerateHeightmap();
            terrainChunk2.CreateTerrain();
        }

        private void RemoveTerrainChunk()
        {
            terrainChunk1.Destroy();
            terrainChunk2.Destroy();
        }

        public float GetHeight (float x, float z)
        {
            return terrainChunk1.GetTerrainHeight(x, z);
        }
    }
}