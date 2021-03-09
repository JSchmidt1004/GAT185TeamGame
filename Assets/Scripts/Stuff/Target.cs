using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private enum eTargetType
    {
        Red,
        Yellow,
        Green,
        Purple,
        Hidden
    }


    private eTargetType targetType = eTargetType.Red;
    public int points = 100;
    public Material[] materials;
    public GameObject destroyGameObject;
    public AudioSource hitSound;
    public bool useRandomTarget = true;

    private void Start()
    {
        Material material = materials[0];
        if (useRandomTarget)
        {
            eTargetType[] targetTypes = (eTargetType[])System.Enum.GetValues(typeof(eTargetType));
            int randMat = Random.Range(0, materials.Length);
            targetType = targetTypes[randMat];

            switch (targetType)
            {
                case eTargetType.Red:
                    material = materials[0];
                    points = 10;
                    break;
                case eTargetType.Yellow:
                    material = materials[1];
                    points = 20;
                    break;
                case eTargetType.Green:
                    material = materials[2];
                    points = 30;
                    break;
                case eTargetType.Purple:
                    material = materials[3];
                    points = 40;
                    break;
                case eTargetType.Hidden:
                    material = materials[4];
                    points = 100;
                    break;
                default:
                    material = materials[0];
                    points = 10;
                    break;
            }
        }

        GetComponent<Renderer>().material = material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Add score to game
        if(collision.gameObject.CompareTag("Projectile"))
        {
            Game.Instance.AddPoints(points);
            hitSound.Play();
            if (destroyGameObject != null)
            {
                Destroy(destroyGameObject, 0.5f);
            }
        }
    }
}
