//#include <SoftwareSerial.h>
//SoftwareSerial BlModuloPinos(0,1);
#include <Stepper.h> 
 


//MOTOR1
int M1IN4 = 12;
int M1IN3 = 11;
int M1IN2 = 10;
int M1IN1 = 9;
//MOTOR2
int M2IN4 = 8;
int M2IN3 = 7;
int M2IN2 = 6;
int M2IN1 = 5;

const int stepsPerRevolution = 65;
const int motorSpeed = 300;
 
Stepper StepperM1(stepsPerRevolution, 9,11,10,12); 
Stepper StepperM2(stepsPerRevolution, 5,7,6,8);


const int distance =500;
int rotate =9;

void setup()
{

    StepperM1.setSpeed(motorSpeed);
    StepperM2.setSpeed(motorSpeed);
    pinMode(13, OUTPUT);



    
 //inicializando pino serial para o modulo bluetooth
    Serial.begin(9600); 

}





 //Funções movimentação individual dos motores 
  
//motor 1 gira para frente
void M1_Frente(){
  StepperM1.step(distance);   
  }
//motor 2 gira para frente
void M2_Frente(){
  StepperM2.step(distance);   
  }

//motor 1 gira para trás
void M1_Tras(){
  StepperM1.step(0-distance);      
  }
//motor 2 gira para trás
void M2_Tras(){
  StepperM2.step(0-distance);   
  }

  
  
  
//Funções movimentação movimentação do robô  
//Robô se movimenta para frente
void andarParaFrente(){
        M1_Frente();
        M2_Frente();
     
}

//Robô gira à esquerda 
void girarParaesquerda(){
        M1_Tras();
        M2_Frente();       
}

//Robô gira à direita 
void girarParaDireita(){
        M1_Frente();
        M2_Tras();
}




//função que controla o movimento para frente do robô
void walk(int argument, int movimento){

      switch(movimento)
         {                          
            case 3:
                andarParaFrente();                  
                break;
            case 2:
               girarParaesquerda();               
               break;
            case 1:
               girarParaDireita();                
               break; 
         }     

    delay(300);
    Serial.println('@');
    
  }




void loop() {



          //digitalWrite(13, digitalRead(TAC1));

          
 // if (Serial.available())  //verifica se tem dados diponível para leitura
  //{

    
         char byteRead = Serial.read();
       //char byteRead = BlModuloPinos.read();
         switch(byteRead)
         {                          
              case '3':
    andarParaFrente();
                  walk(distance, 3);
               break;
               case '2':
    girarParaesquerda();
                walk(rotate, 2);
               break;
               case '1':
          girarParaDireita();
                walk(rotate, 1);
               break;      
                defalut:
               Serial.println('@');
               break;
           }
            
//  }


}
