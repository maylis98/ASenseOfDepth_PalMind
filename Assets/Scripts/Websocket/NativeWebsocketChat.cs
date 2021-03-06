
//using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NativeWebSocket;
using System;
using System.Text;
using TMPro;


// https://github.com/endel/NativeWebSocket

public class NativeWebsocketChat : MonoBehaviour
{
    public string hostname;
    public int port = 2567;
    public TextMeshProUGUI txt;

    WebSocket websocket;


    async void Start()
    {
        websocket = new WebSocket("ws://" + hostname + ":" + port);

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            /*Debug.Log(bytes.Length);
            Debug.Log(bytes[0]);*/
            //string str = Convert.ToString(bytes);
            //string str = BitConverter.ToString(bytes);
            //string str = Encoding.Default.GetString(bytes);

            //Debug.Log(str);

            // getting the message as a string
             var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("OnMessage! " + message);

            if (txt)
            {

                switch (message)
                {
                    //INTRO MANAGER
                    case "start intro text":
                        FindObjectOfType<DialogTrigger>().TriggerDialogue(1);
                        break;
                    case "enter main scene":
                        EventManager.TriggerEvent("show End Button", true);
                        break;

                    //SHOW THOUGHTS
                    case "look around":
                        FindObjectOfType<ThoughtsTrigger>().TriggerThoughts("Look around");
                        break;
                    case "bugs appear":
                        FindObjectOfType<VFXBugsManager>().BugsAppear();
                        break;
                    case "diving part 1 pre":
                        FindObjectOfType<ThoughtsTrigger>().TriggerThoughts("Diving part 1 pre");
                        FindObjectOfType<PPManager>().IsBlinking();
                        FindObjectOfType<CanvasManager>().showCapsuleButton(1);
                        break;
                    case "diving part 1 main":
                        FindObjectOfType<ThoughtsTrigger>().TriggerThoughts("Diving part 1 main");
                        FindObjectOfType<PPManager>().IsBlinking();
                        FindObjectOfType<MemoriesManager>().showFallingC();
                        break;
                    case "diving part 2 pre":
                        FindObjectOfType<PPManager>().IsBlinking();
                        FindObjectOfType<CanvasManager>().showCapsuleButton(2);
                        break;
                    case "so focus":
                        FindObjectOfType<ThoughtsTrigger>().TriggerThoughts("So focus");
                        FindObjectOfType<PPManager>().IsBlinking();
                        FindObjectOfType<MemoriesManager>().showFallingC();
                        break;
                    case "distortion field":
                        break;
                    case "front water":
                        FindObjectOfType<MemoriesManager>().showWaterFloor();
                        FindObjectOfType<ThoughtsTrigger>().TriggerThoughts("Front water");
                        break;
                    case "strange feet":
                        break;
                    case "water up":
                        FindObjectOfType<WaterPPManager>().PPPresence(1);
                        break;
                    case "water down":
                        FindObjectOfType<WaterPPManager>().PPPresence(0);
                        //FindObjectOfType<ThoughtsTrigger>().TriggerThoughts("Water is going up");
                        break;
                    case "finds box":
                        break;
                    case "feel myself":
                        EventManager.TriggerEvent("returnGame", true);
                        FindObjectOfType<ThoughtsTrigger>().TriggerThoughts("I feel myself again");
                        break;
                    case "end of Game":
                        FindObjectOfType<MemoriesManager>().showPalBody();
                        break;


                    //SHOW MEMORY
                    case "water up 1":
                        FindObjectOfType<WaterPPManager>().PPPresence(1);
                        FindObjectOfType<MemoriesManager>().showWaterFloor();
                        FindObjectOfType<MemoriesManager>().showUncompleteS();
                        FindObjectOfType<ThoughtsTrigger>().TriggerThoughts("Uncomplete memory");
                        break;
                    case "memory 1 is called":
                        FindObjectOfType<EmitterOrder>().SendData(0);
                        break;
                    case "memory 2 is called":
                        FindObjectOfType<EmitterOrder>().SendData(1);
                        break;
                    case "memory 3 is called":
                        FindObjectOfType<EmitterOrder>().SendData(2);
                        break;

                    default:
                        break;
                }
                
                //txt.text += "\n" + message;
            }


        };

        // Keep sending messages at every 1.0s
        //InvokeRepeating("SendWebSocketMessage", 0.0f, 1.0f);

        // waiting for messages
        await websocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    async void SendWebSocketMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Sending bytes
            await websocket.Send(new byte[] { 10, 20, 30 });

            // Sending plain text
            await websocket.SendText("plain text message");
        }
    }

    private async void OnApplicationQuit()
    {
        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            await websocket.Close();
        }
    }


    async public void SendChatMessage(string message)
    {

        if (websocket.State == WebSocketState.Open)
        {

            // Sending bytes
            //await websocket.Send(new byte[] { 10, 20, 30 });

            // Sending plain text
            await websocket.SendText(message);
        }

    }
}
