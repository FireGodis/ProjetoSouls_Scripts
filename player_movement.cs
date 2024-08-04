using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player_movement : MonoBehaviour
{

    private CharacterController controller;
    public float velocidade;
    private float ForcaY;
    public float Gravidade;
    public float forca_pulo;
    private Transform MinhaCamera;
    private Animator animacao;
    private Playercontrol controleplayer;

    private bool EstaNoChao;
    [SerializeField]private Transform pedopersonagem;
    [SerializeField]private LayerMask colisaolayer;

   private void Awake(){
    controleplayer = new Playercontrol();
    controleplayer.Enable();


   }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
       
       
        MinhaCamera = Camera.main.transform;
        animacao = GetComponent<Animator>();
    }

    private void OnPular(){
        Debug.Log("pulando");
         if(EstaNoChao){

            ForcaY = forca_pulo;
            animacao.SetTrigger("pula");



        }

    }

    // Update is called once per frame
    void Update()
    {
        /* float horizontal = Input.GetAxis("Horizontal");
        // float vertical = Input.GetAxis("Vertical");

        // float PS4_horizontal = Input.GetAxis("PS4_Horizontal");
        // float PS4_vertical = Input.GetAxis("PS4_Vertical");

         if (PS4_horizontal != 0)
        {
            Debug.Log("PS4 Horizontal Axis: " + PS4_horizontal);
        }

        if (PS4_vertical != 0)
        {
            Debug.Log("PS4 Vertical Axis: " + PS4_vertical);
        }
        */

         // float moveX = horizontal != 0 ? horizontal : PS4_horizontal;
         float moveZ = vertical != 0 ? vertical : PS4_vertical;
         //comentando 

         var movimento = controleplayer.player.move.ReadValue<Vector2>();

        // Vector3 movimento = new Vector3(moveX, 0, moveZ);
        movimento = MinhaCamera.TransformDirection(movimento);
        movimento.y = 0;

        controller.Move(movimento * Time.deltaTime * velocidade);
        

        if(movimento != Vector2.zero){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.deltaTime * 10);
        }


        animacao.SetBool("move", movimento != Vector2.zero);


        EstaNoChao = Physics.CheckSphere(pedopersonagem.position, 0.3f, colisaolayer);
        animacao.SetBool("nochao", EstaNoChao);

        

       

        if(ForcaY > Gravidade){
            ForcaY += Gravidade * Time.deltaTime;
        }

        controller.Move(new Vector3(0, ForcaY, 0) * Time.deltaTime);

        var look = controleplayer.player.look.ReadValue<Vector2>();

        MinhaCamera.IncrementLookRotation(new Vector2(look.y, look.x));

    }
}
