#pragma strict

var rotationsPerMinute : float = 10.0;
 function Update()
 {
     transform.Rotate(0,0,6.0*rotationsPerMinute*Time.deltaTime);
 }

