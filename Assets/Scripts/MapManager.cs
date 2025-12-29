using BETA7;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Serialization;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace BETA7
{
    [Serializable]
    public enum BlockName
    {
        Wall = 0, // 0, 0, 0
        NotWalkable = 1, // 255, 255, 255
        Walkble = 2, // 153, 217, 234
        Response = 3, // 237, 28, 36
        BuildingLand = 4, // 181, 230, 29
        DefenseBuliding = 5, // 255, 242, 0

        Build = 6, // 지어진 상태
        Monster = 7,
    }
    [Serializable]
    public class MapData
    {
        public BlockName BlockName;
        public int x;
        public int z;

        public MapData(int x, int z)
        {
            this.x = x;
            this.z = z;
        }
        public Vector2 Tovector()
        {
            return new Vector2(x, z);
        }
        public static MapData operator +(MapData a, MapData b)
            => new MapData(a.x + b.x, a.z + b.z);

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().IsValueType.Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                return x == ((MapData)obj).x && z == ((MapData)obj).z;
            }
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
    public class MapManager : MonoBehaviour
    {
        public Texture2D Mapinfo;
        public Color[] ColorBlock;
        public int mapWidth;
        public int mapHeight;
        public GameObject[] Block;
        public int blockScale = 1;
        public Transform Map;
        public List<MapData> mapData = new List<MapData>();
        public List<MapData> directions = new List<MapData>()
        {
            new MapData(1, 0),
            new MapData(0, 1),
            new MapData(-1, 0),
            new MapData(0, -1),
        };

        public void GenerateMap()
        {
            mapWidth = Mapinfo.width;
            mapHeight = Mapinfo.height;
            Debug.Log("mapWidth : " + mapWidth + " , mapHeight : " + mapHeight);
            Color[] pixels = Mapinfo.GetPixels();

            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    Color pixelColor = pixels[i * mapWidth + j];
                    MapData data = new MapData(j, i);
                    if (pixelColor == ColorBlock[(int)BlockName.Wall])
                    {
                        Instantiate(Block[(int)BlockName.Wall], new Vector3(blockScale * j, 0, blockScale * i), Quaternion.identity, Map);
                        data.BlockName = BlockName.Wall;
                    }
                    else if (pixelColor == ColorBlock[(int)BlockName.Walkble])
                    {
                        Instantiate(Block[(int)BlockName.Walkble], new Vector3(blockScale * j, 0, blockScale * i), Quaternion.identity, Map);
                        data.BlockName = BlockName.Walkble;
                    }
                    else if (pixelColor == ColorBlock[(int)BlockName.NotWalkable])
                    {
                        Instantiate(Block[(int)BlockName.NotWalkable], new Vector3(blockScale * j, 0, blockScale * i), Quaternion.identity, Map);
                        data.BlockName = BlockName.NotWalkable;
                    }
                    else if (pixelColor == ColorBlock[(int)BlockName.Response])
                    {
                        Instantiate(Block[(int)BlockName.Response], new Vector3(blockScale * j, 0, blockScale * i), Quaternion.identity, Map);
                        data.BlockName = BlockName.Response;
                    }
                    else if (pixelColor == ColorBlock[(int)BlockName.DefenseBuliding])
                    {
                        Instantiate(Block[(int)BlockName.DefenseBuliding], new Vector3(blockScale * j, 0, blockScale * i), Quaternion.identity, Map);
                        data.BlockName = BlockName.DefenseBuliding;
                    }
                    else if (pixelColor == ColorBlock[(int)BlockName.BuildingLand])
                    {
                        Instantiate(Block[(int)BlockName.BuildingLand], new Vector3(blockScale * j, 0, blockScale * i), Quaternion.identity, Map);
                        data.BlockName = BlockName.BuildingLand;
                    }
                    mapData.Add(data);
                }
            }
        }
        void Start()
        {
            Map = GameObject.Find("MAP").transform;
            GenerateMap();
        }
    }

}



