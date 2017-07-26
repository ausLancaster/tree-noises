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

        private TerrainChunk terrainChunk;

        private void Awake()
        {
            settings = new TerrainChunkSettings(65, 200, 0.1f);
            noiseProvider = new SeussNoise0();
            featureGenerator = new List<IFeatureGenerator>();
            featureGenerator.Add(new TreePlacer(settings.length));


            terrainChunk = new TerrainChunk(settings, noiseProvider, featureGenerator, terrainMaterial);
            terrainChunk.GenerateHeightmap();
            terrainChunk.CreateTerrain();
        }
    }
}