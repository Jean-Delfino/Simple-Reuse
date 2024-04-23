using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

//USING THIS AS MOST OF THE CODE https://github.com/tiago-peres/blog/blob/master/csvreader/CSVReader.cs
//What i need is, the first column is the key, and the next columns are values

namespace Reuse.CSV
{
    public static class CommaSeparatedValuesReader
    {
        public static Dictionary<string, string[]> ReadCommaSeparatedFile(TextAsset csvFile)
        {
            string[] lines = csvFile.text.Split('\n');

            // Create a dictionary to hold the parsed CSV data
            Dictionary<string, string[]> outputDict = new Dictionary<string, string[]>();

            // Process each line in the CSV
            foreach (string line in lines)
            {
                string[] row = SplitCsvLine(line);

                if (row.Length > 0)
                {
                    // Use the first column as the key and the rest as the value
                    string key = row[0];
                    string[] value = row.Length > 1 ? new List<string>(row).GetRange(1, row.Length - 1).ToArray() : new string[0];
                
                    // Add the key-value pair to the dictionary
                    outputDict[key] = value;
                }
            }

            return outputDict;
        }
        public static string[] SplitCsvLine(string line)
        {
            return line.Split('|');
        }
    }
}
