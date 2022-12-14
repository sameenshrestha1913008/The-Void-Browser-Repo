
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds;               // Array (list) of all the back- and foregrounds to be parallaxed
    private float[] parallaxScales;               // The propotion of the Camera's movement to move the backgounds by
    public float smoothing = 1f;                  // How smooth the parallax is going to be. Make sure to set this above 0.

    private Transform cam;                        // reference to the main cameras transform
    private Vector3 previousCamPos;               // the position of the camera in the previous frame

    // Is called before Start(). Great for references.
    void Awake()
    {
        // set up camera the reference
        cam = Camera.main.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        // The previous frame had the curent frames camera postion
        previousCamPos = cam.position;

        // assigning corresponding  parallaxScales
        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z*-1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // for each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            //the parallax is the opposite of the camera movement because the previous frame multiplied by the scale
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // set a target x position which is the current position plus the parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //create  a target postion which is the  background's current position with it's target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // fade between current postion and the target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // set the previousCamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;
    }
}
