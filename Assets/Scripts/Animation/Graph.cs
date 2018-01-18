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
    private float[] radianList;
    public int[] qualities = new int[9];
    private Transform[] qualitiesPoints = new Transform[9];


    // Material used for the connecting lines
    public Material lineMat;

    // Connect all of the `points` to the `mainPoint`
    // public GameObject mainPoint;
    // public GameObject[] points;

    // Fill in this with the default Unity Cylinder mesh
    // We will account for the cylinder pivot/origin being in the middle.
    public Mesh cylinderMesh;


    GameObject[] ringGameObjects;

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
            for (int j = 0; j < 70; j++)
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

    }

    // Use this for initialization
    void Start()
    {
        this.ringGameObjects = new GameObject[qualitiesPoints.Length];
        //this.connectingRings = new ProceduralRing[points.Length];
        for (int i = 0; i < qualitiesPoints.Length; i++)
        {
            // Make a gameobject that we will put the ring on
            // And then put it as a child on the gameobject that has this Command and Control script
            this.ringGameObjects[i] = new GameObject();
            this.ringGameObjects[i].name = "Connecting ring #" + i;
            this.ringGameObjects[i].transform.parent = this.gameObject.transform;

            // We make a offset gameobject to counteract the default cylindermesh pivot/origin being in the middle
            GameObject ringOffsetCylinderMeshObject = new GameObject();
            ringOffsetCylinderMeshObject.transform.parent = this.ringGameObjects[i].transform;

            // Offset the cylinder so that the pivot/origin is at the bottom in relation to the outer ring gameobject.
            ringOffsetCylinderMeshObject.transform.localPosition = new Vector3(0f, 1f, 0f);
            // Set the radius
            ringOffsetCylinderMeshObject.transform.localScale = new Vector3(radius, 1f, radius);

            // Create the the Mesh and renderer to show the connecting ring
            MeshFilter ringMesh = ringOffsetCylinderMeshObject.AddComponent<MeshFilter>();
            ringMesh.mesh = this.cylinderMesh;

            MeshRenderer ringRenderer = ringOffsetCylinderMeshObject.AddComponent<MeshRenderer>();
            ringRenderer.material = lineMat;

        }
    }

    // Update is called once per frame
    void Update()
    {
        float cylinderDistance = 0;
        for (int i = 0; i < nbQualities - 1; i++)
        {
            // Move the ring to the point
            this.ringGameObjects[i].transform.position = this.qualitiesPoints[i].transform.position;

            // Match the scale to the distance
            cylinderDistance = 0.5f * Vector3.Distance(this.qualitiesPoints[i].transform.position, this.qualitiesPoints[i+1].transform.position);
            this.ringGameObjects[i].transform.localScale = new Vector3(this.ringGameObjects[i].transform.localScale.x, cylinderDistance, this.ringGameObjects[i].transform.localScale.z);

            // Make the cylinder look at the main point.
            // Since the cylinder is pointing up(y) and the forward is z, we need to offset by 90 degrees.
            this.ringGameObjects[i].transform.LookAt(this.qualitiesPoints[i+1].transform, Vector3.up);
            this.ringGameObjects[i].transform.rotation *= Quaternion.Euler(90, 0, 0);
        }

        // Draw last line

        // Move the ring to the point
        this.ringGameObjects[nbQualities - 1].transform.position = this.qualitiesPoints[nbQualities - 1].transform.position;

        // Match the scale to the distance
        cylinderDistance = 0.5f * Vector3.Distance(this.qualitiesPoints[nbQualities - 1].transform.position, this.qualitiesPoints[0].transform.position);
        this.ringGameObjects[nbQualities - 1].transform.localScale = new Vector3(this.ringGameObjects[nbQualities - 1].transform.localScale.x, cylinderDistance, this.ringGameObjects[0].transform.localScale.z);

        // Make the cylinder look at the main point.
        // Since the cylinder is pointing up(y) and the forward is z, we need to offset by 90 degrees.
        this.ringGameObjects[nbQualities - 1].transform.LookAt(this.qualitiesPoints[0].transform, Vector3.up);
        this.ringGameObjects[nbQualities - 1].transform.rotation *= Quaternion.Euler(90, 0, 0);
    }
}
