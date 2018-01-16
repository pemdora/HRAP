using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Graph : MonoBehaviour
{
    public Transform pointPrefabAxis;
    public Transform pointPrefabCandidate;
    public float size;
    private float radius;
    public Color[] cols = new Color[9];

    // Axis Values
    private int nbQualities;// Number of qualities
    Dictionary<int, List<Transform>> axis = new Dictionary<int, List<Transform>>(); // Dictionnary of each axis with points
    public float[] radianList;
    public int[] qualities = new int[9];
    private Transform[] qualitiesPoints = new Transform[9];
    void Awake()
    {
        radianList = new float[] { 0f, 0.689f, 1.396f, 2.094f, 2.792f, 3.490f, 4.188f, 4.886f, 5.585f };
        nbQualities = radianList.Length;
        this.radius = pointPrefabAxis.GetComponent<SphereCollider>().radius;
        Vector3 position;
        position.z = 0f;
        Transform point;
        List<Transform> points = new List<Transform>();
        for (int i = 0; i < nbQualities; i++)
        {
            points = new List<Transform>();
            for (int j = 0; j < 100; j++)
            {
                point = Instantiate(pointPrefabAxis);
                Material mymat = point.GetComponent<Renderer>().material;
                position.x = j * radius * Mathf.Cos(radianList[i]);
                position.y = j * radius * Mathf.Sin(radianList[i]);
                mymat.SetColor("_EmissionColor", cols[i]);
                point.localPosition = position;
                points.Add(point);
            }
            axis.Add(i, points);
        }

        for (int i = 0; i < nbQualities; i++)
        {
            if (axis.TryGetValue(i, out points)) // If the data exist in the dictionary with the given key
            {
                Transform candidatePoint = Instantiate(pointPrefabCandidate);
                position.x = points[qualities[i]*10].localPosition.x;
                position.y = points[qualities[i]*10].localPosition.y;
                candidatePoint.localPosition = position;
                qualitiesPoints[i] = candidatePoint;
            }
            else
            {
                Debug.Log("Error, empty key value in dictionnary");
            }
        }

        /*
        // Pour tracer les droites 
        // un peu de maths de seconde
        float m = 0; // coef directeur
        float p = 0; // ordonnée à l'origine
        float y = 0; // plus petit ordonnée entre les 2 points
        float resolution = 0;
        float num = 0, denum = 0;
        for (int i = 0; i < nbQualities -1; i++)
        {
            // trouver le coef directeur de la droite entre 2 points
            m = (qualitiesPoints[i].position.y - qualitiesPoints[i + 1].position.y) / (qualitiesPoints[i].position.x - qualitiesPoints[i + 1].position.x);
            p = qualitiesPoints[i].position.y - m * qualitiesPoints[i].position.x;

            y = 0;
            if(qualitiesPoints[i].position.y< qualitiesPoints[i + 1].position.y)
            {
                y = qualitiesPoints[i].position.y;
            }
            else
            {
                y = qualitiesPoints[i+1].position.y;
            }
            
            if ((Mathf.Abs(qualitiesPoints[i].position.y) + Mathf.Abs(qualitiesPoints[i + 1].position.y))> (Mathf.Abs(qualitiesPoints[i].position.x) + Mathf.Abs(qualitiesPoints[i + 1].position.x)))
            {
                num = Mathf.Abs(qualitiesPoints[i].position.y) + Mathf.Abs(qualitiesPoints[i + 1].position.y);
                denum = Mathf.Abs(qualitiesPoints[i].position.x) + Mathf.Abs(qualitiesPoints[i + 1].position.x);
            }
            else
            {
                denum = Mathf.Abs(qualitiesPoints[i].position.y) + Mathf.Abs(qualitiesPoints[i + 1].position.y);
                num = Mathf.Abs(qualitiesPoints[i].position.x) + Mathf.Abs(qualitiesPoints[i + 1].position.x);
            }

            resolution = num / denum;
            Debug.Log(resolution);
            for (int j = 0; j < 100; j++)
            {
                point = Instantiate(pointPrefabAxis);
                position.y = y + j*0.01f;
                position.x = (position.y - p) / m;
                point.localPosition = position;
                points.Add(point);
            
        }

        // Tracer la derniere droite
        m = (qualitiesPoints[nbQualities-1].position.y - qualitiesPoints[0].position.y) / (qualitiesPoints[nbQualities - 1].position.x - qualitiesPoints[0].position.x);
        p = qualitiesPoints[nbQualities - 1].position.y - m * qualitiesPoints[0].position.x;
        if (qualitiesPoints[nbQualities - 1].position.y < qualitiesPoints[0].position.y)
        {
            y = qualitiesPoints[nbQualities - 1].localPosition.y;
        }
        else
        {
            y = qualitiesPoints[0].localPosition.y;
        }
        for (int j = 0; j < 50; j++)
        {
            point = Instantiate(pointPrefabAxis);
            position.y = y + j * radius;
            position.x = (position.y - p) / m;
            point.localPosition = position;
            points.Add(point);
        }*/
        

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
