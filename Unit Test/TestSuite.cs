using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TestSuite
{
    //Create a reference to the gamemanager class
    private GameManager game;

    //Setup Tests
    [SetUp]
    public void Setup()
    {
        //Create a object that has the game manager script attached to it and assign it in to a local variable
        GameObject mainGameClass = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"));

        //Get The Game Manager Script
        game = mainGameClass.GetComponent<GameManager>();
    }

    //Tests
    #region Player
    [UnityTest]
    public IEnumerator MovementUp()
    {
        //Get the rigidbody from the player's object
        Rigidbody rb = game.Player.GetComponent<Rigidbody>();

        //Get the set speed variable from the playercontroller script and assign it in to a local variable
        float speed = game.Player.GetComponent<PlayerController>().speed;

        //Get the original position of the player's object
        Vector3 origpos = rb.transform.position;

        //Create a vector3 movement variable increasing the z axis so the player moves forward
        Vector3 movement = new Vector3(0, 0, 500);

        //Add force to the player's rigidbody using the variables created above
        rb.AddForce(movement * speed);

        //Wait 100 milliseconds
        yield return new WaitForSeconds(0.5f);

        //Check if the player's z position has increased
        Assert.Greater(rb.transform.position.z, origpos.z);
    }

    [UnityTest]
    public IEnumerator MovementDown()
    {
        //Get the rigidbody from the player's object
        Rigidbody rb = game.Player.GetComponent<Rigidbody>();

        //Get the set speed variable from the playercontroller script and assign it in to a local variable
        float speed = game.Player.GetComponent<PlayerController>().speed;

        //Get the original position of the player's object
        Vector3 origpos = rb.transform.position;

        //Create a vector3 movement variable decreasing the z axis so the player moves backwards
        Vector3 movement = new Vector3(0, 0, -500);

        //Add force to the player's rigidbody using the variables created above
        rb.AddForce(movement * speed);

        //Wait 500 milliseconds
        yield return new WaitForSeconds(0.5f);

        //Check if the player's z position has decreased
        Assert.Less(rb.transform.position.z, origpos.z);
    }

    [UnityTest]
    public IEnumerator MovementLeft()
    {
        //Get the rigidbody from  the player's object
        Rigidbody rb = game.Player.GetComponent<Rigidbody>();

        //Get the set speed variable from the playercontroller script and assign it in to a local variable
        float speed = game.Player.GetComponent<PlayerController>().speed;

        //Get the original position of the player's object
        Vector3 origpos = rb.transform.position;

        //Create a vector3 movement variable decreasing the x axis so the player moves left
        Vector3 movement = new Vector3(-500, 0, 0);

        //Add force to the player's rigidbody using the variables created above
        rb.AddForce(movement * speed);

        //Wait 500 milliseconds
        yield return new WaitForSeconds(0.5f);

        //Check if the player's x position has decreased
        Assert.Less(rb.transform.position.x, origpos.x);
    }

    [UnityTest]
    public IEnumerator MovementRight()
    {
        //Get the rigidbody from  the player's object
        Rigidbody rb = game.Player.GetComponent<Rigidbody>();

        //Get the set speed variable from the playercontroller script and assign it in to a local variable
        float speed = game.Player.GetComponent<PlayerController>().speed;

        //Get the original position of the player's object
        Vector3 origpos = rb.transform.position;

        //Create a vector3 movement variable increasing the x axis so the player moves right
        Vector3 movement = new Vector3(500, 0, 0);

        //Add force to the player's rigidbody using the variables created above
        rb.AddForce(movement * speed);

        //Wait 500 milliseconds
        yield return new WaitForSeconds(0.5f);

        //Check if the player's x position has increased
        Assert.Greater(rb.transform.position.x, origpos.x);
    }

    [UnityTest]
    public IEnumerator CameraMovement()
    {
        //Get Camera Controller Script From The Camera Gameobject
        CameraController cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

        //Get Cameras Original Position
        Vector3 origpos = cam.transform.position;

        //Get player from game manager and assign the player in to a local variable
        GameObject player = game.Player;

        //Change players position
        player.transform.position = new Vector3(90, 0, 0);

        //Wait 100 milliseconds
        yield return new WaitForSeconds(0.1f);

        //Check if the cameras x position value has increased
        Assert.Greater(cam.transform.position.x, origpos.x);
    }
    #endregion Player

    #region Collectables
    [UnityTest]
    public IEnumerator CollectablesRotate()
    {
        //Get the first collectable in the collectable array and assign it in to a local variable
        GameObject collectable = game.Collectables[1];
        
        //Get the original rotation of the collectable
        Quaternion origrotation = collectable.transform.rotation;

        //Wait 100 milliseconds
        yield return new WaitForSeconds(0.1f);

        //Check if the object has rotated
        Assert.AreNotEqual(collectable.transform.rotation, origrotation);
    }

    [UnityTest]
    public IEnumerator CollectablesDisappearOnPlayerCollision()
    {
        //Get the player from the game manager and assign the player in to a local variable
        GameObject player = game.Player;

        //Set the players position to 0
        player.transform.position = Vector3.zero;
        
        //Get the first collectable within the game manager's collectable array and assign it in to a local variable
        GameObject collectable = game.Collectables[0];

        //Set the collectables position to 0
        collectable.transform.position = Vector3.zero;

        //Wait 100 milliseconds
        yield return new WaitForSeconds(0.1f);

        //Check if the collectable is disabled within the hierarchy
        Assert.IsTrue(!collectable.activeInHierarchy);
    }

    [UnityTest]
    public IEnumerator CollectingCollectablesIncreasesScore()
    {
        //Get the player from the game manager and assign the player in to a local variable
        GameObject player = game.Player;

        //Set the player's position to 0
        player.transform.position = Vector3.zero;

        //Get the first collectable in the collectable array and assign it in to a local variable
        GameObject collectable = game.Collectables[0];

        //Set the collectable's position to 0
        collectable.transform.position = Vector3.zero;

        //Wait 100 milliseconds
        yield return new WaitForSeconds(0.1f);

        //Check if the score has increased
        Assert.AreEqual("Score: 10", game.scoreText.text);
    }

    [UnityTest]
    public IEnumerator CollectableSoundPlays() 
    {
        //Get the player from the game manager and assign the player in to a local variable
        GameObject player = game.Player;

        //Set the players position to 0
        player.transform.position = Vector3.zero;

        //Get the first collectable within the game manager's collectable array and assign it in to a local variable
        GameObject collectable = game.Collectables[0];

        //Set the collectables position to 0
        collectable.transform.position = Vector3.zero;

        //Get the player's audio source component
        AudioSource audioSource = player.GetComponent<AudioSource>();

        //Wait 100 milliseconds
        yield return new WaitForSeconds(0.1f);

        //Check if the audio source plays a sound when a collectable is collected
        Assert.IsTrue(audioSource.isPlaying);
    }
    #endregion Collectables

    #region Win / Load Conditions
    [UnityTest]
    public IEnumerator WinCondition()
    {
        //Get the collectables in the collectable array and assign them in to a local variable
        GameObject[] collectables = game.Collectables;

        //Destroy all but one collectable 
        for (int i = 1; i < collectables.Length; i++)
        {
            Object.Destroy(collectables[i]);
        }

        //Set last collectable position to 0
        collectables[0].transform.position = Vector3.zero;

        //Get the player from the game manager and assign the player in to a local variable
        GameObject player = game.Player;

        //Set the player's position to 0
        player.transform.position = Vector3.zero;

        //Wait 100 milliseconds
        yield return new WaitForSecondsRealtime(0.1f);

        //Check if the win text is visible
        Assert.IsTrue(game.winText.activeInHierarchy);
    }

    [UnityTest]
    public IEnumerator LoseConditionOffPlatform()
    {
        //Get the player from the game manager and assign the player in to a local variable
        GameObject player = game.Player;

        //Set the player's position to under the lose position
        player.transform.position = new Vector3(0, -10, 0);

        //Wait 100 milliseconds
        yield return new WaitForSecondsRealtime(0.1f);

        //Check if the lose text is active in the hierarchy
        Assert.IsTrue(game.loseText.activeInHierarchy);
    }

    [UnityTest]
    public IEnumerator LoseConditionOutOfTime()
    {
        //Set the time remaining variable within the game manager to 100 milliseconds
        game.timeRemaining = 0.1f;

        //Wait 500 milliseconds
        yield return new WaitForSecondsRealtime(0.5f);

        //Check if the lose text is active in the hierarchy
        Assert.IsTrue(game.loseText.activeInHierarchy);
    }
    #endregion Win / Load Conditions

    #region Menu
    [UnityTest]
    public IEnumerator xRestartButtonFunctions()
    {
        //Get the menu script from the menu panel gameobject
        Menu menu = game.menuPanel.GetComponent<Menu>();

        //Use the menu script's restart method to restart the game
        menu.Restart();

        //Wait 100 milliseconds
        yield return new WaitForSeconds(0.1f);

        //Check if the scene was reloaded by checking if the instantiated gamemanager object has been removed from the scene
        Assert.That(game == null);
    }

    [UnityTest]
    public IEnumerator QuitGameButtonFunctions()
    {
        //Get the menu script from the menu panel gameobject
        Menu menu = game.menuPanel.GetComponent<Menu>();

        //Use the menu script's quit method
        menu.Quit();

        //Wait 100 milliseconds
        yield return new WaitForSeconds(0.1f);

        //Check if the quit method has been called
        Assert.IsTrue(menu.isQuit);
    }
    #endregion Menu

    #region Other
    [UnityTest]
    public IEnumerator TimerDecreases()
    {
        //Assign the time remaining variable in to a local variable
        float origtime = game.timeRemaining;

        //Wait 100 milliseconds
        yield return new WaitForSeconds(0.1f);

        //Check if the time has decreased since it was assigned into the local variable
        Assert.Less(game.timeRemaining, origtime);
    }

    [UnityTest]
    public IEnumerator BackgroundMusicPlays()
    {
        //Check if the background music is playing
        Assert.IsTrue(game.GetComponent<AudioSource>().isPlaying);

        return null;
    }
    #endregion Other

    //End Tests
    [TearDown]
    public void TearDown()
    {
        //Check that the game object is not null
        if (game != null)
        {
            //Destroy Gameobject
            Object.Destroy(game.gameObject);
        }
    }
}
