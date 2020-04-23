using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverMode : MonoBehaviour
{
    private GameObject fpp;
    private Vector3 fppPosition;
    private Quaternion fppRotation;
    private bool ob;

    private Camera _camera;
    private FpsInput input;

    public float sen;

    //private SmoothRotation _rotationX;
    //private SmoothRotation _rotationY;

    // Start is called before the first frame update
    void Start()
    {
        ob = false;
        fpp = gameObject;
        input = new FpsInput();
        //GameObject fpp = GameObject.Find("m_FPP");
        _camera = fpp.GetComponentInChildren<Camera>();
        //_rotationX = new SmoothRotation(RotationXRaw);
        //_rotationY = new SmoothRotation(RotationYRaw);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12)) Observe();
        if (Input.GetKeyDown(KeyCode.F11)) UnObserve();

        if(ob)
        {
            
            //var direction = new Vector3(input.Move, 0f, input.Strafe).normalized;
            //var worldDirection = transform.TransformDirection(direction);
            if (Input.GetKey(KeyCode.W)) fpp.GetComponent<Transform>().position += _camera.transform.forward * sen;
            if (Input.GetKey(KeyCode.S)) fpp.GetComponent<Transform>().position += _camera.transform.forward * -sen;
            if (Input.GetKey(KeyCode.A)) fpp.GetComponent<Transform>().position += _camera.transform.right * -sen;
            if (Input.GetKey(KeyCode.D)) fpp.GetComponent<Transform>().position += _camera.transform.right * sen;
        }
    }

    void Observe()
    {
        Debug.Log("ob");
        fpp.GetComponent<Rigidbody>().isKinematic = true;
        fppPosition = fpp.GetComponent<Transform>().position;
        fppRotation = fpp.GetComponent<Transform>().rotation;
        //fpp.GetComponent<FPPController>().enabled = false;

        ob = true;

    }

    void UnObserve()
    {
        fpp.GetComponent<Rigidbody>().isKinematic = false;
        fpp.GetComponent<Transform>().position = fppPosition;
        fpp.GetComponent<Transform>().rotation = fppRotation;
        //fpp.GetComponent<FPPController>().enabled = true;

        ob = false;
    }



    //private void RotateCamera()
    //{
    //    if (_camera)
    //    {
    //        Quaternion cameraRotation = _camera.GetComponent<Transform>().rotation;
    //        float camXAngle = cameraRotation.eulerAngles.x;

    //        //将camXAngle转换至[-180,180]区间
    //        if (camXAngle < 180) camXAngle = -camXAngle;
    //        else camXAngle = 360 - camXAngle;

    //        float yRot = _rotationX.Update(RotationXRaw, 0.05f);
    //        float xRot = _rotationY.Update(RotationYRaw, 0.05f);
    //        this.transform.localRotation *= Quaternion.Euler(0f, yRot, 0f);

    //        camXAngle += xRot;

    //        //判断旋转角度
    //         _camera.transform.localRotation *= Quaternion.Euler(-xRot, 0f, 0f);

    //    }
    //}


    ///// Returns the target rotation of the camera around the y axis with no smoothing.
    //private float RotationXRaw
    //{
    //    get { return input.RotateX * 4; }
    //}

    ///// Returns the target rotation of the camera around the x axis with no smoothing.
    //private float RotationYRaw
    //{
    //    get { return input.RotateY * 4; }
    //}

    ///// A helper for assistance with smoothing the camera rotation.
    //private class SmoothRotation
    //{
    //    private float _current;
    //    private float _currentVelocity;

    //    public SmoothRotation(float startAngle)
    //    {
    //        _current = startAngle;
    //    }

    //    /// Returns the smoothed rotation.
    //    public float Update(float target, float smoothTime)
    //    {
    //        return _current = Mathf.SmoothDampAngle(_current, target, ref _currentVelocity, smoothTime);
    //    }

    //    public float Current
    //    {
    //        set { _current = value; }
    //    }
    //}

    ///// A helper for assistance with smoothing the movement.
    //private class SmoothVelocity
    //{
    //    private float _current;
    //    private float _currentVelocity;

    //    /// Returns the smoothed velocity.
    //    public float Update(float target, float smoothTime)
    //    {
    //        return _current = Mathf.SmoothDamp(_current, target, ref _currentVelocity, smoothTime);
    //    }

    //    public float Current
    //    {
    //        set { _current = value; }
    //    }
    //}

    /// Input mappings
    private class FpsInput
    {
        [Tooltip("The name of the virtual axis mapped to rotate the camera around the y axis."),
         SerializeField]
        private string rotateX = "Mouse X";

        [Tooltip("The name of the virtual axis mapped to rotate the camera around the x axis."),
         SerializeField]
        private string rotateY = "Mouse Y";

        [Tooltip("The name of the virtual axis mapped to move the character back and forth."),
         SerializeField]
        private string move = "Horizontal";

        [Tooltip("The name of the virtual axis mapped to move the character left and right."),
         SerializeField]
        private string strafe = "Vertical";

        [Tooltip("The name of the virtual button mapped to run."),
         SerializeField]
        private string run = "Fire3";

        [Tooltip("The name of the virtual button mapped to jump."),
         SerializeField]
        private string jump = "Jump";

        [Tooltip("The name of the virtual button mapped to glide."), // 定义滑翔按钮，类似于jump
         SerializeField]
        private string glide = "Glide";

        /// Returns the value of the virtual axis mapped to rotate the camera around the y axis.
        public float RotateX
        {
            get { return Input.GetAxisRaw(rotateX); }
        }

        /// Returns the value of the virtual axis mapped to rotate the camera around the x axis.        
        public float RotateY
        {
            get { return Input.GetAxisRaw(rotateY); }
        }

        /// Returns the value of the virtual axis mapped to move the character back and forth.        
        public float Move
        {
            get { return Input.GetAxisRaw(move); }
        }

        /// Returns the value of the virtual axis mapped to move the character left and right.         
        public float Strafe
        {
            get { return Input.GetAxisRaw(strafe); }
        }

        /// Returns true while the virtual button mapped to run is held down.          
        public bool Run
        {
            get { return Input.GetButton(run); }
        }

        /// Returns true during the frame the user pressed down the virtual button mapped to jump.          
        public bool Jump
        {
            get { return Input.GetButtonDown(jump); }
        }

        // 当玩家按下滑翔按钮时，返回true
        public bool isGlide
        {
            get { return Input.GetButton(glide); }
        }
    }
}
