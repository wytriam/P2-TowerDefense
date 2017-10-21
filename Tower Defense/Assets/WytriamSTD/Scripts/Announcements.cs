/*
 * The general idea behind this class is to have a Singleton Announcement Class that 
 *      - easily makes onscreen announcements appear
 *      - records all announcements made in a log
 * This script should be attached to a Canvas Object named "Announcements"
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WytriamSTD
{
    public class Announcements : MonoBehaviour
    {
        private static Announcements instance;
        //private Announcements() {} //Singleton Implementation. No extra announcements can be made
        public static Announcements getInstance()
        {
            //if (instance == null)
                //instance = new Announcements();
            return instance;
        }

        public float announcementDuration = 5.0f;

        private List<string> announcementLog;
        private Text anncmntText;
        private float timer = 0.0f;

        void Awake()
        {
            if (instance == null)
                instance = this;
        }

        // Use this for initialization
        void Start()
        {
            anncmntText = GetComponentInChildren<Text>();
            anncmntText.enabled = false;
            announcementLog = new List<string>();
        }

        // Update is called once per frame
        void Update()
        {
            if (anncmntText.enabled)
            {
                timer += Time.fixedDeltaTime;
                if (timer >= announcementDuration)
                {
                    anncmntText.enabled = false;
                    timer = 0.0f;
                }
            }
        }

        // Add an announcement to the announcement log. This will not display the announcement on screen. 
        public void WriteNewAnnoucement(string announcement)
        {
            announcementLog.Add(announcement);
        }

        // Display an announcement to the screen. If this announcement is not on the log, it will be added to the log
        public void DisplayAnnouncement(string announcement)
        {
            Debug.Log("Announcements::DisplayAnnouncement() - " + announcement);
            anncmntText.text = announcement;
            anncmntText.enabled = true;
            if (!announcementLog.Contains(announcement))
                announcementLog.Add(announcement);
        }
        
        // Display an announcement from the announcementLog at location index to the screen.
        public void DisplayAnnouncement(int index)
        {
            anncmntText.text = announcementLog[index];
            anncmntText.enabled = true;
        }
    }
}
