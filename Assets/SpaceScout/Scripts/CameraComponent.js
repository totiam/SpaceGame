
// variables
var speed : float = 0.1;
var zoomLimits : Vector2;
var turnableOn : boolean = false;

// constants
private var touchDeltaPosition : Vector2;
private var zoom : float;

function Update () {

	// basic rotation controls for mobile, please note there is no zoom implemented.
	if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {

		touchDeltaPosition = Input.GetTouch(0).deltaPosition;
		updateRotation ();
		
	}

	// left mousehold to rotate
	if (Input.GetMouseButton(0)) {

		touchDeltaPosition.x = speed * Input.GetAxis ("Mouse X");
		touchDeltaPosition.y = speed * Input.GetAxis ("Mouse Y");
		updateRotation ();
		
	}
	// right mousehold to zoom
	else if (Input.GetMouseButton(1)) {

		zoom = speed * Input.GetAxis ("Mouse Y");
		updateZoom ();
		
	}
	// slow turntable animation on idle
	else if (turnableOn) {
	
		transform.Rotate (Vector3.up* speed/100, Space.World);
	
	}
	
}

// function for rotation
function updateRotation () {

	transform.Rotate (Vector3.right * touchDeltaPosition.y * speed, Space.Self);
	transform.Rotate (Vector3.up * touchDeltaPosition.x * speed, Space.World);

}

// function for zoom
function updateZoom (){

	transform.localScale.x += zoom;
	transform.localScale.x = Mathf.Clamp (transform.localScale.x, zoomLimits.x, zoomLimits.y);

	transform.localScale.y += zoom;
	transform.localScale.y = Mathf.Clamp (transform.localScale.y, zoomLimits.x, zoomLimits.y);

	transform.localScale.z += zoom;
	transform.localScale.z = Mathf.Clamp (transform.localScale.z, zoomLimits.x, zoomLimits.y);

}