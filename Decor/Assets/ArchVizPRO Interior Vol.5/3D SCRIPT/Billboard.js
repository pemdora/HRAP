

#pragma strict
 var target : Transform;
 
 function Start () {
 target = GameObject.Find("Target_Billboard").transform;

var targetPostition : Vector3  = new Vector3(target.position.x, this.transform.position.y, target.position.z);
 transform.LookAt(targetPostition);
 transform.Rotate(0, 180, 0);
 }

