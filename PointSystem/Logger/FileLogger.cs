﻿using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointSystem.Logger
{
    public class FileLogger : ILogger
    {
        private string filePath;
        private static object _lock = new object();
        public FileLogger(string path)
        {
            filePath = path;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Critical;
            if (logLevel == LogLevel.Warning || logLevel == LogLevel.Critical || logLevel == LogLevel.Error)
                return true;
            else 
                return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel))
                {
                if (formatter != null)
                {
                    lock (_lock)
                    {
                        File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
                    }
                }
            }
        }
    }
}