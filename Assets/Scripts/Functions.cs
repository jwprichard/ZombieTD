using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

    public static class Functions
    {

        //Find the rotation of z to look from p1 to p2
    public static Quaternion LookAt(Vector3 p1, Vector3 p2)
        {

            float angle = 0;
            float opp;
            float adj;

            if (p1.x == p2.x)
            {
                //Looking down
                if (p1.y > p2.y)
                {
                    angle = 0;
                }
                //Looking up
                else
                {
                    angle = 180;
                }
            }

            else if (p1.y == p2.y)
            {
                //Looking right
                if (p1.x < p2.x)
                {
                    angle = 90;
                }
                //Looking left
                else
                {
                    angle = 270;
                }
            }
            else
            {
                if (p1.x < p2.x)
                {
                    //First quadrant angle +0
                    if (p1.y > p2.y)
                    {
                        angle = 0;
                        opp = Mathf.Abs(p1.x - p2.x);
                        adj = Mathf.Abs(p1.y - p2.y);
                    }
                    //Second quadrant angle +90
                    else
                    {
                        angle = 90;
                        adj = Mathf.Abs(p1.x - p2.x);
                        opp = Mathf.Abs(p1.y - p2.y);
                    }
                }
                //if (p1.x > p2.x)
                else
                {
                    //Third quadrant angle +180
                    if (p1.y <= p2.y)
                    {
                        angle = 180;
                        opp = Mathf.Abs(p1.x - p2.x);
                        adj = Mathf.Abs(p1.y - p2.y);
                    }
                    //Forth quadrant angle +270
                    else
                    {
                        angle = 270;
                        adj = Mathf.Abs(p1.x - p2.x);
                        opp = Mathf.Abs(p1.y - p2.y);
                    }
                }

                float a = Mathf.Atan(opp / adj) * 180f / Mathf.PI;
                angle += a;
            }

            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            return rotation;
        }

    public static Vector3 GetMouseScreenPosition()
    {
        return new Vector3();
       
    }

    public static Vector3 FindTilePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 pos = hit.transform.position;
            return pos;        
        }

        return new Vector3(0, 0, 0);
    }

    //Calculate the damage made by the game object
    public static bool CalculateDamage(GameObject gameObject1, GameObject gameObject2)
    {
        int hp;
        int damage;

        if (gameObject1.tag == "Zombie")
        {

            hp = ZombieScript.ZombieDictionary[gameObject1].Health;
            damage = gameObject2.GetComponent<Projectiles>().GetDamage();
            hp -= damage;
            ZombieScript.ZombieDictionary[gameObject1].Health = hp;
        }
        else
        {
            hp = BuildingScript.BuildingDictionary[gameObject1].Health;
            damage = gameObject2.GetComponent<ZombieInterface>().Damage;
            hp -= damage;
            BuildingScript.BuildingDictionary[gameObject1].Health = hp;
        }

        if (hp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static int RandomNumber(int min, int max)
    {
        int num = UnityEngine.Random.Range(min, max);

        return num;
    }

    public static float GetDistance(Vector3 pos1, Vector3 pos2)
    {
        float distance = Mathf.Abs(pos1.x - pos2.x) + Mathf.Abs(pos1.y - pos2.y);

        return distance;
    }

}
