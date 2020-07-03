using System;
using System.Collections.Generic;
using System.IO;

namespace CodeAnalyzer
{
    class LexicalAnalysis
    {
        private List<FiniteStateAutomaton> FiniteStateAutomatons;
        public LexicalAnalysis()
        {
            FiniteStateAutomatons = new List<FiniteStateAutomaton>();
        }

        public void LoadFiniteStateAutomatons(string[] paths)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                if (File.Exists(paths[i]))
                {
                    FiniteStateAutomatons.Add(new FiniteStateAutomaton(paths[i]));
                }
            }
        }

        public string Start(string inputCode)
        {
            string result = "";

            int k = 0;
            while (k < inputCode.Length)
            {
                bool Result = false;
                int M = 0;
                int Priority = -1;
                string FiniteStateAutomatonName = "";

                FiniteStateAutomatons.ForEach(item =>
                {
                    Tuple<bool, int> maxStringResult = item.maxString(inputCode, k);
                    if (M < maxStringResult.Item2 || (M == maxStringResult.Item2 && Priority > item.Priority))
                    {
                        FiniteStateAutomatonName = item.Name;
                        Priority = item.Priority;
                        M = maxStringResult.Item2;
                        Result = maxStringResult.Item1;
                    }
                });
                if (!Result)
                {
                    result += $"<\"Err\",{k}>";
                    k++;
                }
                else
                {
                    result += $"<\"{FiniteStateAutomatonName}\",{inputCode.Substring(k, M)}>";
                    k += M;
                }
            }

            return result;
        }
    }
}
