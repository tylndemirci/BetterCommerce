using System.IO;
using System.Linq;
using BetterCommerce.Core.Utilities.Results;

namespace BetterCommerce.CustomerServiceUI.Infrastructure
{
    public static class TxtWriter
    {
        public static void WriteToTxt(string message)
        {
            
            string path = $@"c:\temp\{message.Substring(0, message.Contains("@")? message.IndexOf("@"): 9)}.txt";
            
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(message);
                }
            }
            
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(message);
            }	
        }
    }
}