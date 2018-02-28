namespace Assets.Logic
{
    using UnityEngine;
    using System.Collections;
    using Assets.Logic.Blocks;
    using System.Collections.Generic;
    public class Player : MonoBehaviour
    {
        private Vector2 rotation;

        public Texture2D crosshairImage;

        public float speed = 0.6f;

        public float armLength = 100f;

        public IList<Block> inventory = new List<Block>();

        public World world;

        public void Start()
        {
            this.crosshairImage = (Texture2D)Resources.Load("crosshair");
        }

        public void Update()
        {
            // Destroy block by clicking left mouse.
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Debug.DrawRay(this.transform.position, this.transform.forward * this.armLength);
                if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, this.armLength))
                {
                    var destroyedBlock = TerrainEditor.GetBlock(hit);
                    TerrainEditor.SetBlock(hit, new Air());
                    this.inventory.Add(destroyedBlock);
                }
            }

            // Rotation of Camera
            this.rotation = new Vector2(
                this.rotation.x + Input.GetAxis("Mouse X") * 3,
                this.rotation.y + Input.GetAxis("Mouse Y") * 3);
            this.transform.localRotation = Quaternion.AngleAxis(this.rotation.x, Vector3.up);
            this.transform.localRotation *= Quaternion.AngleAxis(this.rotation.y, Vector3.left);

            // Player position
            this.transform.position += this.transform.forward * this.speed * Input.GetAxis("Vertical");
            this.transform.position += this.transform.right * this.speed * Input.GetAxis("Horizontal");
        }

        public void OnGUI()
        {
            var xMin = Screen.width / 2 - this.crosshairImage.width / 2;
            var yMin = Screen.height / 2 - this.crosshairImage.height / 2;
            GUI.DrawTexture(new Rect(xMin, yMin, this.crosshairImage.width, this.crosshairImage.height), this.crosshairImage);
        }
    }
}