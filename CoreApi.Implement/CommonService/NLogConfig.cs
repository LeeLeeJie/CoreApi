using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using NLog.Targets.Wrappers;

namespace CoreApi.Implement.CommonService
{
    public static class NLogConfig
    {
        public static void ConfigNLog()
        {
            var fileTarget = new FileTarget("file")
            {
                Encoding = Encoding.UTF8,
                FileName = @"${basedir}/logs/${date:format=yyyy\-MM\-dd}${logger}.log",
                ArchiveFileName = @"${basedir}/logs/${date:format=yyyy\-MM\-dd}.{#####}.log",
                ArchiveAboveSize = 10240000,
                ArchiveEvery = FileArchivePeriod.Day,
                MaxArchiveFiles = 200,
                Layout =
                    @"${date:format=yyyy\-MM\-dd HH\:mm\:ss\:fff}|${logger}|${level}|${stacktrace}${newline}${message}"
            };
            var asyncFileWrapper = new AsyncTargetWrapper("async_file", fileTarget)
            {
                OverflowAction = AsyncTargetWrapperOverflowAction.Grow
            };

            LogManager.Configuration.AddTarget(fileTarget);
            LogManager.Configuration.AddRuleForAllLevels(asyncFileWrapper);

            var nullTarget = new NullTarget("null");
            LogManager.Configuration.AddTarget(nullTarget);
            LogManager.Configuration.AddRuleForAllLevels(nullTarget, "Microsoft.*");

            var consoleTarget = new ColoredConsoleTarget("console")
            {
                Layout =
                    @"${date:format=yyyy\-MM\-dd HH\:mm\:ss\:fff}|${stacktrace}${newline}${logger}|${level}|${message}"
            };
            var asyncConsoleWrapper = new AsyncTargetWrapper("async_console", consoleTarget)
            {
                OverflowAction = AsyncTargetWrapperOverflowAction.Grow
            };
            LogManager.Configuration.AddTarget(asyncConsoleWrapper);
            LogManager.Configuration.AddRule(LogLevel.Warn, LogLevel.Fatal, asyncConsoleWrapper);

            LogManager.Configuration.Reload();
        }
    }
}
