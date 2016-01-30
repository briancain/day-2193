﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

  public GameObject wall;
  public GameObject floor;
  public GameObject robot;

  //TODO: Remove this element as it is here for debugging purposes. We will
  // have a nicer display later
  public Text courseOffsetDisplay;
  public Text oxygenDisplay;
  public Text itemDisplay;

  private BoardManager boardManager;

  private static float courseOffset;
  private float courseRateOfIncrease;
  private float courseRateOfRestoration;

  private float currentOxygen;
  private float oxygenRateOfDecrease;
  private float oxygenRateOfRestoration;

  private float currentItems;
  private float itemsRateOfDecrease;
  private float itemsRateOfRestoration;

  private float timeLeft;

  private const int MAX_COURSE_OFFSET = 100;

  void StartGame() {
    boardManager.GenerateBoard(wall, floor);

    courseOffset = 15f;
    courseRateOfIncrease = 0.5f;
    courseRateOfRestoration = -1.0f;

    currentOxygen = 100f;
    oxygenRateOfDecrease = 1.0f;
    oxygenRateOfRestoration = 2.0f;

    currentItems = 20f;
    itemsRateOfDecrease = 0.2f;
    itemsRateOfRestoration = 0.3f;

    timeLeft = 60f;
  }

  void GameOver() {
    Debug.Log("Game Over");
    //Application.LoadLevel("GameOver");
  }

  bool IsGameOver() {
    // End conditions
    //

    if (timeLeft < 0f) {
      return true;
    } else if (currentOxygen <= 0) {
      return true;
    } else if (currentItems <= 0) {
      return true;
    } else if (courseOffset >= 100) {
      return true;
    } else {
      return false;
    }
  }

  void WinGame() {

  }

  void Awake() {
    boardManager = GetComponent<BoardManager>();
    StartGame();
  }

  void Update() {
    if (IsGameOver()) {
      GameOver();
    }

    // Decrease resources
    DecreaseResources();
  }

  void DecreaseResources() {
    courseOffset += courseRateOfIncrease * Time.deltaTime;
    currentOxygen -= Time.deltaTime * oxygenRateOfDecrease;
    currentItems -= Time.deltaTime * itemsRateOfDecrease;
    timeLeft -= Time.deltaTime;

    // TODO: Remove these in favor of a more interesting interface
    courseOffsetDisplay.text = "Course Offset: " + Mathf.FloorToInt(courseOffset);
    oxygenDisplay.text = "Oxygen: " + Mathf.CeilToInt(currentOxygen);
    itemDisplay.text = "Items: " + Mathf.CeilToInt(currentItems);
  }

  public void RestoreResource(GameObject restoringStation) {
    switch (restoringStation.name) {
      case RestorationStations.CONSOLE:
        courseOffset += courseRateOfRestoration * Time.deltaTime;
        break;
      case RestorationStations.OXYGEN_STATION:
        currentOxygen += oxygenRateOfRestoration * Time.deltaTime;
        break;
      case RestorationStations.ITEM_STATION:
        currentItems += itemsRateOfRestoration * Time.deltaTime;
        break;
      default:
        // Do nothing, as this was errantly triggered
        break;
    }
  }
}
