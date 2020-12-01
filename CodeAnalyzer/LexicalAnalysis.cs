using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CodeAnalyzer
{
    public class LexicalAnalysis
    {
        private List<FiniteStateAutomaton> FiniteStateAutomatons;
        private DataGridView outAutomatons;
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
            ShowLoadAutomatons();
        }

        public void RemoveFiniteStateAutomatonByName(string name)
        {
            FiniteStateAutomaton automaton = FiniteStateAutomatons.Find(a => a.Name == name);
            if(automaton != null)
            {
                FiniteStateAutomatons.Remove(automaton);
                ShowLoadAutomatons();
            }
            else
            {
                throw new ArgumentException($"Ошибка удаления: конечный автомат с именем \"{name}\" - не найден.");
            }
        }

        public void ShowLoadAutomatons()
        {
            if (outAutomatons != null)
            {
                outAutomatons.DataSource = null;
                outAutomatons.Rows.Clear();
                outAutomatons.DataSource = FiniteStateAutomatons;
            }
        }

        public void SetOutAutomatons(DataGridView dgv)
        {
            outAutomatons = dgv;
        }

        public string Lexer(string inputCode, bool testMode = false)
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
                    if (testMode)
                    {
                        return "error";
                    }    
                    result += $"<\"Err\",{k},{inputCode[k]}>";
                    k++;
                }
                else
                {
                    string sub = inputCode.Substring(k, M), 
                        res = $"<\"{FiniteStateAutomatonName}\",";
                    switch(sub)
                    {
                        case " ":
                            res += "spase>";
                            break;
                        case "\t":
                            res += "horizontal tab>";
                            break;
                        case "\v":
                            res += "vertical tab>";
                            break;
                        case "\f":
                            res += "form feed>";
                            break;
                        case "\n":
                            res += "line feed>\n";
                            break;
                        case "\r":
                            res += "form feed>\r";
                            break;
                        case "\u0085":
                            res += "next line>\u0085";
                            break;
                        case "\u2028":
                            res += "line separator>\u2028";
                            break;
                        case "\u2029":
                            res += "paragraph separator>\u2029";
                            break;
                        default:
                            res += sub + ">";
                            break;
                    }
                    result += res;
                    k += M;
                }
            }

            return result;
        }
    }
}
