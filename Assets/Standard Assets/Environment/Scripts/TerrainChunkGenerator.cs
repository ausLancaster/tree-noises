using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


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
        public TreePlacer treePlacer;

        public TerrainChunk terrainChunk1;
        public TerrainChunk terrainChunk2;

        private float seed;

        public void Start()
        {
            seed = Random.Range(0.0f, 100000.0f);

            settings = new TerrainChunkSettings(129, 200, 0.1f);
            noiseProvider1 = new SeussNoise0(true);
            noiseProvider2 = new SeussNoise0(false);
            featureGenerator = new List<IFeatureGenerator>();

            treePlacer.length = settings.length;
            featureGenerator.Add(treePlacer);

        }

        public void CreateTerrainChunk()
        {
            seed = Random.Range(0.0f, 100000.0f);

            terrainChunk1 = new TerrainChunk(settings, noiseProvider1, seed, featureGenerator, terrainMaterial1);
            terrainChunk1.GenerateHeightmap();
            terrainChunk1.CreateTerrain();

            terrainChunk2 = new TerrainChunk(settings, noiseProvider2, seed, featureGenerator, terrainMaterial2);
            terrainChunk2.GenerateHeightmap();
            terrainChunk2.CreateTerrain();

            foreach (IFeatureGenerator fg in featureGenerator)
            {
                fg.Generate(seed, noiseProvider1, settings);
            }
        }

        public void RemoveTerrainChunk()
        {
            terrainChunk1.Destroy();
            terrainChunk2.Destroy();
            foreach (IFeatureGenerator fg in featureGenerator)
            {
                fg.Destroy();
            }

        }

        public float GetHeight (float x, float z)
        {
            return terrainChunk1.GetTerrainHeight(x, z);
        }
    }
}