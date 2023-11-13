using UnityEngine;
using System.Collections;

namespace AstronautPlayer
{

	public class AstronautPlayer : MonoBehaviour {

		private Animator anim;
		private CharacterController controller;

		public float speed = 600.0f;
		public float turnSpeed = 400.0f;
		private Vector3 moveDirection = Vector3.zero;
		public float gravity = 20.0f;

		void Start () {
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
		}

        void Update()
        {
            // Kiểm tra nút W được nhấn hay không
            bool isMoving = Input.GetKey("w");

            // Thiết lập giá trị AnimationPar trong Animator dựa trên trạng thái di chuyển
            anim.SetInteger("AnimationPar", isMoving ? 1 : 0);

            // Kiểm tra player đang ở trên mặt đất
            if (controller.isGrounded)
            {
                // Tính toán vector di chuyển dựa trên đầu vào từ trục dọc (Vertical)
                moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
            }

            // Tính toán góc quay từ đầu vào trục ngang (Horizontal)
            float turn = Input.GetAxis("Horizontal");
            transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

            // Di chuyển player
            controller.Move(moveDirection * Time.deltaTime);

            // Áp dụng trọng lực
            moveDirection.y -= gravity * Time.deltaTime;
        }
    }
}
