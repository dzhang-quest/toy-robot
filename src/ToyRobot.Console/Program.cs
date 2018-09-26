using System;
using System.Configuration;
using ToyRobot.Commands;

namespace ToyRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            var tableDimension = new TableDimension(
                width: int.Parse(ConfigurationManager.AppSettings["table_width"]),
                hight: int.Parse(ConfigurationManager.AppSettings["table_hight"]));
            var context = new Context(new Robert(tableDimension), Console.Out);
            string commandLine;
            while (!string.IsNullOrEmpty((commandLine = Console.ReadLine())))
            {
                CommandFactory
                    .Parse(commandLine)
                    .Do(context);
            }
        }
    }
}
