/*
 * Component: High Scores System - Offline Scores
 * Version: 1.0.3
 * Last Updated: March 28th, 2014
 * Created By: Christian
 * Last Updated: May 1st, 2014
 * Last Updated By: Christian
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace BH_STG.BarrageEngine.HighScores
{
    public class OfflineScores
    {
        #region ScoreStruct
        public struct Score
        {
            public string user;
            public string level;
            public string character;
            public int difficulty;
            public int score;

            public Score (string nUser, string nLevel, string nCharacter, int nDifficulty, int nScore)
            {
                user = nUser;
                level = nLevel;
                character = nCharacter;
                difficulty = nDifficulty;
                score = nScore;
            }
        }

        #endregion

        List<Score> Scores = new List<Score>();
        string basename;

        public OfflineScores()
        {
            basename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + "\\Team Christian\\BH-STG\\highscores.bhs";
        }

        public void loadScores()
        {
            createScoresFile();

            XmlDocument doc = new XmlDocument();
            XmlNodeList[] ScoresList = new XmlNodeList[1];
            doc.Load(basename);

            ScoresList[0] = doc.GetElementsByTagName("hscore");

            Scores = new List<Score>();
            for (int i = 0; i < ScoresList[0].Count; i++)
            {
                string nUser = "", nLevel = "", nCharacter = "";
                int nDifficulty = 0, nScore = 0;
                foreach (XmlNode sc in ScoresList[0].Item(i))
                {
                    if (sc.Name == "user")
                        nUser = sc.InnerText;
                    else if (sc.Name == "level")
                        nLevel = sc.InnerText;
                    else if (sc.Name == "character")
                        nCharacter = sc.InnerText;
                    else if (sc.Name == "difficulty")
                        nDifficulty = Convert.ToInt32(sc.InnerText);
                    else if (sc.Name == "score")
                        nScore = Convert.ToInt32(sc.InnerText);
                }
                Scores.Add(new Score(nUser, nLevel, nCharacter, nDifficulty, nScore));
            }

            Scores.Sort((x, y) => x.score.CompareTo(y.score));
            Scores.Reverse();
        }

        public void saveScore(string nUser, string nLevel, string nCharacter, int nDifficulty, int nScore)
        {
            // load scores
            loadScores();
            // add the new score to the scores list
            Scores.Add(new Score(nUser, nLevel, nCharacter, nDifficulty, nScore));

            // create the scores xml string
            string scorestring = "<highscores>";
            foreach (Score score in Scores)
            {
                scorestring += "<hscore><user>" + score.user + "</user><level>" +
                                                  score.level + "</level><character>" +
                                                  score.character + "</character><difficulty>" +
                                                  score.difficulty + "</difficulty><score>" +
                                                  score.score + "</score></hscore>";
            }
            scorestring += "</highscores>";

            // delete the old scores file
            File.Delete(basename);

            // save a new scores file
            XmlDocument replaySave = new XmlDocument();
            replaySave.PreserveWhitespace = false;
            replaySave.LoadXml(scorestring);
            replaySave.Save(basename);
        }

        public bool createScoresFile()
        {
            if (File.Exists(basename))
                return false;
            else
            {
                string text = "<highscores></highscores>";
                XmlDocument replaySave = new XmlDocument();
                replaySave.PreserveWhitespace = false;

                replaySave.LoadXml(text);
                replaySave.Save(basename);

                return true;
            }
        }

        public int GetTopScore(string level)
        {
            int score = 0;

            foreach (Score sc in Scores)
            {
                if (sc.level == level)
                {
                    if (sc.score > score)
                        score = sc.score;
                }
            }
            return score;
        }

        public List<Score> getTwentyScores(string level)
        {
            List<Score> tenScores = new List<Score>();
            int scoreCount = 0;

            foreach (Score sc in Scores)
            {
                if (sc.level == level)
                {
                    tenScores.Add(sc);
                    scoreCount++;
                    if (scoreCount == 19)
                        break;
                }
            }

            return tenScores;
        }
    }
}
