using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class NetworkScript : MonoBehaviour
{
    async void Start()
    {
        //If 401 error occurs with "valid ID Domain not specified" 
        //Go to unity editor -> edit -> project settings -> services
        //Make sure the project is linked.
        //ask Ian or Alex if the project is unable to be linked
        await UnityServices.InitializeAsync();

        //Check if user is signed in
        //Sign them in anonymously if they are not signed in
        try
        {

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }

        }
        catch (Exception error)
        {

            Debug.Log("User failed to sign in");
            Debug.Log(error);
            AuthenticationService.Instance.ClearSessionToken();

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
