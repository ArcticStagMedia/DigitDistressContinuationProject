﻿using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace DigitDistress.AI.ThoughtEngine
{

		public enum AiState
		{
				IDLE,
				SEARCHING,
				TRAVELLINGTOFOOD,
				TRAVELLINGTOENTER,
				EATING,
				PLAYING,
		BOTH
		}
		public class AICore : MonoBehaviour
		{
				//public List<MemoryBase> m_MemoryList { get; private set; }
				//public List<EmotionBase> m_EmotionList { get; private set; }
				//public List<DesireBase> m_DesireList { get; private set; }
				public List<GameObject> m_lBuildings { get; private set; }

				public float m_Entertainment = 100f;
				public float m_Hunger = 100f;
		public float m_Happiness = 100f;

				public AiState m_CurrentState = AiState.IDLE;

				Vector3 m_vTargetLocation = new Vector3 ();

				NavMeshAgent m_NavAg;
				NavMeshHit irrelevent;
				Animator animator;
					
				// Use this for initialization
				void Start ()
				{
						m_lBuildings = new List<GameObject> ();
						m_NavAg = this.GetComponent<NavMeshAgent> ();
						//NavMeshHit irrelevent = new NavMeshHit ();
						m_vTargetLocation = Vector3.zero;
						animator = GetComponent<Animator> ();

			m_Happiness = Random.Range (20.0f, 40.0f);
			Debug.Log ("<color=cyan> " + base.gameObject.transform + "</color>");
			GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CycleScenario> ().addDigit (this.gameObject);
						//		m_DesireList = new List<DesireBase> ();
						//		m_EmotionList = new List<EmotionBase> ();
						//		m_MemoryList = new List<MemoryBase> ();

				}
				
				// Update is called once per frame
				void Update ()
				{

						//Debug.Log ("Update");
						m_Entertainment = Mathf.Clamp (m_Entertainment, 0.0f, 100.0f);
						m_Hunger =  Mathf.Clamp (m_Hunger, 0.0f, 100.0f);
						m_Happiness = Mathf.Clamp (m_Happiness, -50.0f, 50.0f);

						switch (m_CurrentState) {
						case AiState.IDLE:
								{
										animator.SetBool ("Movement", false);
										if (m_vTargetLocation != Vector3.zero) {
												m_vTargetLocation = Vector3.zero;
										}
										if (Random.Range (0, 10) == 5) {
												m_CurrentState = AiState.SEARCHING;
												
										}
										if (m_Entertainment < 20.0f) {
												if (!m_lBuildings.Find (x => x.GetComponent<BuildingScript> ().m_AssociatedEmotion == "Fun")) {
														m_CurrentState = AiState.SEARCHING;
														
												} else {
														m_NavAg.SetDestination (getClosestBuilding ("Fun"));
														m_CurrentState = AiState.TRAVELLINGTOENTER;
														
						
												}
										}
										if (m_Hunger < 20.0f) {
												if (!m_lBuildings.Find (x => x.GetComponent<BuildingScript> ().m_AssociatedEmotion == "Hunger")) {
														m_CurrentState = AiState.SEARCHING;
												} else {
														m_NavAg.SetDestination (getClosestBuilding ("Hunger"));
														m_CurrentState = AiState.TRAVELLINGTOFOOD;
						
												}
										}
										break;
								}
						case AiState.SEARCHING:
								{
					
										if (m_vTargetLocation == Vector3.zero || m_NavAg.remainingDistance <= 1.0f) {
												//m_vTargetLocation = Vector3.zero;
												
												m_vTargetLocation = transform.position + Random.insideUnitSphere * 30.0f;
												m_vTargetLocation.y = transform.position.y;
												if (setNewDestination (m_vTargetLocation)) {
														animator.SetBool ("Movement", true);
						
												} else {
														Debug.Log ("No Path");
														m_CurrentState = AiState.IDLE;
														animator.SetBool ("Movement", false);
														
												}

										}

										//if (Random.Range (0, 100) == 49) {
										//m_vTargetLocation = Vector3.zero;
										//m_CurrentState = AiState.IDLE;
										//break;
										//}
										if (m_Entertainment < 20.0f) {
												if (!m_lBuildings.Find (x => x.GetComponent<BuildingScript> ().m_AssociatedEmotion.Contains("Fun"))) {
														
												} else {
														m_NavAg.SetDestination (getClosestBuilding ("Fun"));
														m_CurrentState = AiState.TRAVELLINGTOENTER;
														
						
												}
										}
										if (m_Hunger < 20.0f) {
												if (!m_lBuildings.Find (x => x.GetComponent<BuildingScript> ().m_AssociatedEmotion.Contains ("Hunger"))) {
														
												} else {
														m_NavAg.SetDestination (getClosestBuilding ("Hunger"));
														m_CurrentState = AiState.TRAVELLINGTOFOOD;
														
						
												}
										}
										m_NavAg.Move (Vector3.zero);
										break;
										
								}
						case AiState.TRAVELLINGTOFOOD:
								{
										m_NavAg.Move (Vector3.zero);
										break;
								}
						case AiState.TRAVELLINGTOENTER:
								{
										m_NavAg.Move (Vector3.zero);
										break;
								}
						case AiState.EATING:
								{
										animator.SetBool ("Movement", false);
										m_Hunger += Random.Range (0.2f, 0.5f);
										if (m_Hunger > 90.0f) {
												m_CurrentState = AiState.IDLE;
										}
										break;
								}
						case AiState.PLAYING:
								{
										animator.SetBool ("Movement", false);
										m_Entertainment += Random.Range (0.2f, 0.5f);
										if (m_Entertainment > 90.0f) {
												m_CurrentState = AiState.IDLE;
										}
										break;
								}
			case AiState.BOTH:
			{
				animator.SetBool ("Movement", false);
				m_Entertainment += Random.Range (0.2f, 0.5f);
				m_Hunger += Random.Range (0.2f, 0.5f);
				if (m_Entertainment > 80.0f && m_Hunger > 80.0f) {
					m_CurrentState = AiState.IDLE;
				}
				break;
			}

						default:
								{
//
										break;
								}
						}
//While Not Recharging
			if (m_Entertainment > 0.0f && (m_CurrentState != AiState.PLAYING || m_CurrentState != AiState.BOTH)) {
								m_Entertainment -= Random.Range (0.01f, 0.025f);
						} else {

						}

			if (m_Hunger > 0.0f && (m_CurrentState != AiState.EATING || m_CurrentState != AiState.BOTH) ) {
								m_Hunger -= Random.Range (0.01f, 0.025f);
						} else {
//
						}
			if (m_Hunger < 30.0f && m_Entertainment < 30.0f) {
				m_Happiness -= 0.03f;
						}
			else if (m_Hunger < 30.0f || m_Entertainment < 30.0f) {
				m_Happiness -= 0.01f;
			}

			if (m_Hunger > 70.0f && m_Entertainment < 70.0f) {
				m_Happiness += 0.03f;
			}
			else if (m_Hunger < 30.0f || m_Entertainment < 30.0f) {
				m_Happiness += 0.01f;
			}
				}

				public void addBuilding (GameObject obj)
				{
						m_lBuildings.Add (obj);
				}
				
				Vector3 getClosestBuilding (string bType)
				{
						Vector3 vect = Vector3.zero;
						if (m_lBuildings.Count <= 0) {
								return Vector3.zero;
						}
						foreach (GameObject obj in m_lBuildings.FindAll (x => x.GetComponent<BuildingScript> ().m_AssociatedEmotion.Contains(bType))) {
								if (vect == Vector3.zero) {
										vect = obj.transform.position;
								} else if (Vector3.Distance (vect, transform.position) > Vector3.Distance (obj.transform.position, transform.position)) {
										vect = obj.transform.position;
								}
						}
						return vect;
				}
				
				bool setNewDestination (Vector3 dest)
				{
						//Debug.Log ("Sidewalk: " + NavMesh.GetNavMeshLayerFromName ("sidewalk"));
						//Debug.Log ("Road: " + NavMesh.GetNavMeshLayerFromName ("road"));

						if (NavMesh.SamplePosition (dest, out irrelevent, 1.0f, 1 << NavMesh.GetNavMeshLayerFromName ("Walkable"))) {
								m_NavAg.SetDestination (irrelevent.position);
								//Debug.Log ("Path On path");
								return true;
						} else if (NavMesh.SamplePosition (dest, out irrelevent, 1.0f, 1 << NavMesh.GetNavMeshLayerFromName ("Jump"))) {
								m_NavAg.SetDestination (irrelevent.position);
								//Debug.Log ("Path On road");
								return true;
						} else {
								return false;
						}
						
				}

				public void setState (string sType)
				{
						switch (sType) {
						case "Fun":
								{
										m_CurrentState = AiState.PLAYING;
										break;
								}
						case "Hunger":
								{
										m_CurrentState = AiState.EATING;
										break;
								}
			case "Both":
			{
				m_CurrentState = AiState.BOTH;
				break;
			}
						default:
//
								break;
						}
						m_CurrentState = AiState.PLAYING;
				}
		}
}

