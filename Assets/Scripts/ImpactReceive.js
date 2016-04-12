#pragma strict

 var velBack:float = 15;
 private var character: CharacterController;
 
 function Start(){
     character = GetComponent(CharacterController);
 }
 

 
 function Update(){

 }

 function OnControllerColliderHit(col:ControllerColliderHit){
     if (col.gameObject.name == "Player"){
         var dir = (col.transform.position - transform.position).normalized;
         print("HELP");
         character.SimpleMove(dir*velBack);
     }
 }