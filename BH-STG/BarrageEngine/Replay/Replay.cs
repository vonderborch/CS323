/*
 * Component: Replay System
 * Version: 1.0.3
 * Created: March 27th, 2014
 * Created By: Christian
 * Last Updated: April 29th, 2014
 * Last Updated By: Christian
*/

using BH_STG.BarrageEngine.Input;

using Ionic.Zip;

using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace BH_STG.BarrageEngine.Replay
{
    class Replay
    {
        string currentReplay = "";
        int tick = 0;
        XmlNodeList[] replay = new XmlNodeList[6];
        const string ReplayVersion = "1.0.0";

        public void newReplay(int rseed, string level, string character, int difficulty)
        {
            currentReplay = "<replay><seed>" + rseed.ToString() + "</seed><level>"
                            + level + "</level><character>" + character + "</character><difficulty>"
                            + difficulty.ToString() + "</difficulty><ReplayVersion>" + ReplayVersion + "</ReplayVersion>";
        }

        public void addTick(InputCommon input)
        {
            currentReplay += "<tick><CommandX>" + input.CommandVector.X.ToString() + "</CommandX>";
            currentReplay += "<CommandY>" + input.CommandVector.Y.ToString() + "</CommandY>";
            currentReplay += "<UseItem>" + input.UseItem.pressed.ToString() + "</UseItem>";
            currentReplay += "<UseSafety>" + input.UseSafety.pressed.ToString() + "</UseSafety></tick>";
        }

        public bool saveReplay(string level, string character)
        {
            string filename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + "\\Team Christian\\BH-STG\\Replays\\";
            //// Remove Old XML File
            if (File.Exists(filename + "temp.xml"))
                File.Delete(filename + "temp.xml");

            //// Create XML File
            XmlDocument replaySave = new XmlDocument();
            replaySave.PreserveWhitespace = false;

            currentReplay += "</replay>";
            replaySave.LoadXml(currentReplay);
            replaySave.Save(filename + "temp.xml");

            // Compress File and Cleanup
            ZipFile zip = new ZipFile();
            zip.AddFile(filename + "temp.xml", "");
            string savefile = filename + level + "-" + character + "-" +
                DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Hour +
                DateTime.Now.Minute + DateTime.Now.Second + ".bhr";
            zip.Save(savefile);
            zip.Dispose();
            File.Delete(filename + "temp.xml");

            return true;
        }

        public bool loadReplay(string filename)
        {
            string basename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + "\\Team Christian\\BH-STG\\Replays\\";
            //// Remove Old XML File
            if (File.Exists(basename + "temp.xml"))
                File.Delete(basename + "temp.xml");

            //// uncompress the file
            ZipFile zip = ZipFile.Read(filename);
            foreach (ZipEntry entry in zip)
                entry.Extract(basename, ExtractExistingFileAction.OverwriteSilently);

            //// load and parse the file
            XmlDocument doc = new XmlDocument();
            doc.Load(basename + "temp.xml");

            replay[0] = doc.GetElementsByTagName("seed");
            replay[1] = doc.GetElementsByTagName("level");
            replay[2] = doc.GetElementsByTagName("character");
            replay[3] = doc.GetElementsByTagName("difficulty");
            replay[4] = doc.GetElementsByTagName("tick");
            replay[5] = doc.GetElementsByTagName("ReplayVersion");

            //// cleanup
            tick = 0;
            zip.Dispose();
            File.Delete(basename + "temp.xml");

            if (replay[5].Count > 0)
            {
                if (replay[5].Item(0).InnerText == ReplayVersion)
                    return true;
            }
            return false;
        }

        public static bool checkFileVersion(string filename)
        {
            string basename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString() + "\\Team Christian\\BH-STG\\Replays\\";
            //// Remove Old XML File
            if (File.Exists(basename + "temp.xml"))
                File.Delete(basename + "temp.xml");

            //// uncompress the file
            ZipFile zip = ZipFile.Read(filename);
            foreach (ZipEntry entry in zip)
                entry.Extract(basename, ExtractExistingFileAction.OverwriteSilently);

            //// load and parse the file
            XmlDocument doc = new XmlDocument();
            doc.Load(basename + "temp.xml");

            XmlNodeList[] tempreplay = new XmlNodeList[1];
            tempreplay[0] = doc.GetElementsByTagName("ReplayVersion");

            //// cleanup
            zip.Dispose();
            File.Delete(basename + "temp.xml");

            if (tempreplay[0].Count > 0)
            {
                if (tempreplay[0].Item(0).InnerText == ReplayVersion)
                    return true;
            }
            return false;
        }

        public int getSeed()
        {
            return Convert.ToInt32(replay[0].Item(0).InnerText);
        }

        public string getLevel()
        {
            return replay[1].Item(0).InnerText;
        }

        public string getCharacter()
        {
            return replay[2].Item(0).InnerText;
        }

        public int getDifficulty()
        {
            return Convert.ToInt32(replay[3].Item(0).InnerText);
        }

        public bool isDone()
        {
            if (tick >= replay[4].Count)
                return true;
            return false;
        }

        public InputCommon getNextTick()
        {
            InputCommon input = new InputCommon();
            
            foreach (XmlNode node in replay[4].Item(tick))
            {
                if (node.Name == "UseItem")
                {
                    if (Convert.ToBoolean(node.InnerText))
                        input.UseItem.isPressed();
                    else
                        input.UseItem.isNotPressed();
                }
                else if (node.Name == "UseSafety")
                {
                    if (Convert.ToBoolean(node.InnerText))
                        input.UseSafety.isPressed();
                    else
                        input.UseSafety.isNotPressed();
                }
                else if (node.Name == "CommandX")
                    input.CommandVector.X = float.Parse(node.InnerText, CultureInfo.InvariantCulture.NumberFormat);
                else if (node.Name == "CommandY")
                    input.CommandVector.Y = float.Parse(node.InnerText, CultureInfo.InvariantCulture.NumberFormat);
            }

            tick++;

            return input;
        }
    }
}
