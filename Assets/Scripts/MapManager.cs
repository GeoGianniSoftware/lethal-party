using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Vector3 mapDimensions;
    public Vector3 mapOffset;
    public Vector3 origin;
    public int crowdSize;

    public GameObject prefab;

    public List<AreaScript> areas = new List<AreaScript>();

    public int targetCount;
    List<NPCScript> civilians = new List<NPCScript>();
    List<NPCScript> staff = new List<NPCScript>();
    List<NPCScript> guards = new List<NPCScript>();
    List<NPCScript> targets = new List<NPCScript>();

    // Start is called before the first frame update
    void Start() {


        origin = transform.position + mapOffset;
        SpawnCrowd(crowdSize);

    }

    // Update is called once per frame
    void Update() {

    }

    void SpawnCrowd(int size) {
        for (int i = 0; i < crowdSize; i++) {
            GameObject spawn = SpawnRandomPerson();
            spawn.name = "" + i;

            float randomIndex = Random.Range(0f, 1f);
            float guardChance = .12f;
            float employChance = .5f;

            //Spawn Targets
            if (i < targetCount) {
                spawn.name += " :Target";
                spawn.GetComponent<NPCScript>().secDATA.secLEVEL = SecurityLevels.HIGHSECURITY;


                spawn.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
            }
            //Spawn Other NPCs
            else {
                if (randomIndex <= guardChance) {
                    spawn.name += " :Guard";
                    spawn.GetComponent<NPCScript>().secDATA.secLEVEL = SecurityLevels.PRIVATE;


                    spawn.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color = Color.black;
                }
                else if (randomIndex <= employChance) {
                    spawn.name += " :Staff";
                    spawn.GetComponent<NPCScript>().secDATA.secLEVEL = SecurityLevels.EMPLOY;


                    spawn.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color = Color.blue;
                }
                else {
                    spawn.GetComponent<NPCScript>().secDATA.secLEVEL = SecurityLevels.PUBLIC;

                    spawn.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color = Color.white;
                }
            }



        }
    }



    GameObject SpawnRandomPerson() {
        SpawnInformation info = getSpawnInfo();
        return Instantiate(prefab, info.spawnPos, Quaternion.identity);
    }



    public Vector3 getRandomPosOnMap() {
        int randomArea = Random.Range(0, areas.Count - 1);
        Vector3 newpos = areas[randomArea].transform.position;
        newpos = getRandomPosWithinRect(areas[randomArea].transform.position + areas[randomArea].areaOffset, areas[randomArea].areaDimensions, false);
        return newpos;


    }

    public Vector3 getRandomPosWithinRect(Vector3 center, Vector3 size, bool randomHeight) {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        Vector3 newPos;
        if (!randomHeight) {
            newPos = new Vector3(((randomX * (size.x / 2)) + center.x), center.y, ((randomZ * (size.z / 2)) + center.z));
        }
        else {
            newPos = new Vector3(((randomX * (size.x / 2)) + center.x), ((randomY * (size.y / 2)) + center.y), ((randomZ * (size.z / 2)) + center.z));
        }
         
        return newPos;
    }

    SpawnInformation getSpawnInfo() {


        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        Vector3 newPos = new Vector3(((randomX * (mapDimensions.x / 2)) + transform.position.x + mapOffset.x), ((randomY * (mapDimensions.y / 2)) + mapOffset.y), ((randomZ * (mapDimensions.z / 2)) + mapOffset.z));

        RaycastHit hit = new RaycastHit();
        SpawnInformation spawnInfo;
        if (Physics.Raycast(newPos, -Vector3.up, out hit)) {
            spawnInfo = new SpawnInformation(hit.point);
        }
        else {
            spawnInfo = new SpawnInformation(newPos);
        }

        return spawnInfo;
    }

    class SpawnInformation{
        public Vector3 spawnPos;
        public AreaScript spawnArea;
        public SpawnInformation(Vector3 pos) {
            spawnPos = pos;
        }
    }

    public  Vector3 getRandomPosWithinRadius(Vector3 center, float radius) {
        float randX = Random.Range(-1f, 1f) * radius;
        float randZ = Random.Range(-1f, 1f) * radius;

        return (center + new Vector3(randX, center.y, randZ));
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position+mapOffset, mapDimensions);
    }


}
