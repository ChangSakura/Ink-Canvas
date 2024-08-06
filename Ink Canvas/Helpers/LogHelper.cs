﻿using System;
using System.IO;
using System.Reflection;

namespace Ink_Canvas.Helpers
{
    class LogHelper
    {
        public static string LogFile = "Log.txt";

        public static void NewLog(string str)
        {
            WriteLogToFile(str, LogType.Info);
        }

        public static void NewLog(Exception ex)
        {

        }

        public static void WriteLogToFile(string str, LogType logType = LogType.Info)
        {
            string strLogType = "Info";
            switch (logType)
            {
                case LogType.Event:
                    strLogType = "Event";
                    break;
                case LogType.Trace:
                    strLogType = "Trace";
                    break;
                case LogType.Error:
                    strLogType = "Error";
                    break;
            }
            try
            {
                var file = App.RootPath + LogFile;
                if (!Directory.Exists(App.RootPath))
                {
                    Directory.CreateDirectory(App.RootPath);
                }
                StreamWriter sw = new StreamWriter(file, true);
                sw.WriteLine(string.Format("{0} [{1}] {2}", DateTime.Now.ToString("O"), strLogType, str));
                sw.Close();
            }
            catch { }
        }

        public static void WriteObjectLogToFile(object obj, LogType logType = LogType.Info)
        {
            string strLogType = "Info";
            switch (logType)
            {
                case LogType.Event:
                    strLogType = "Event";
                    break;
                case LogType.Trace:
                    strLogType = "Trace";
                    break;
                case LogType.Error:
                    strLogType = "Error";
                    break;
            }
            try
            {
                var file = App.RootPath + LogFile;
                if (!Directory.Exists(App.RootPath))
                {
                    Directory.CreateDirectory(App.RootPath);
                }
                using (StreamWriter sw = new StreamWriter(file, true))
                {
                    sw.WriteLine($"{DateTime.Now:O} [{strLogType}] Object Log:");
                    if (obj != null)
                    {
                        Type type = obj.GetType();
                        PropertyInfo[] properties = type.GetProperties();
                        foreach (PropertyInfo property in properties)
                        {
                            object value = property.GetValue(obj, null);
                            sw.WriteLine($"{property.Name}: {value}");
                        }
                    }
                    else
                    {
                        sw.WriteLine("null");
                    }
                }
            }
            catch { }
        }

        public enum LogType
        {
            Info,
            Trace,
            Error,
            Event
        }
    }
}
