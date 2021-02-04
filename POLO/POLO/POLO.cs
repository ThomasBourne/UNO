using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLO
{
    class POLO
    {
        public bool validPath(string path)
        {
            POLOLogic PoloLogic = new POLOLogic();
            if (PoloLogic.IsPolo(path))
                return true;
            return false;
        }
        public List<char> Command(string file)
        {
            List<char> command = new List<char>();
            char currentChar = 'd';
            int i = 0;
            while (currentChar != ';')
            {
                currentChar = file[i];
                if (currentChar == ' ')
                { }
                else
                {
                    command.Add(file[i]);
                }
                currentChar = file[i];
            }
            return command;
        }
        public int ExecuteNumeric(List<char> command)
        {
            int? num1 = null;
            int? num2 = null;
            for (int i = 0; i < command.Count; i++)
            {
                char focus = command[i];
                if (int.TryParse(Convert.ToString(command[i]), out int asdasd))
                {
                    if (num1 == null)
                        num1 = int.Parse(Convert.ToString(command[i]));
                    else
                        num2 = int.Parse(Convert.ToString(command[i]));
                }
                else
                {
                    for (int j = 0; j < Store.mathsSymbols.Length; j++)
                    {
                        if (Store.mathsSymbols[j] == focus)
                        {
                            if (focus == ' ') { }
                            if (focus == '+')
                                return num1.Value + num2.Value;
                            if (focus == '-')
                                return num1.Value - num2.Value;
                            if (focus == '*')
                                return num1.Value * num2.Value;
                            if (focus == '/')
                                return num1.Value / num2.Value;
                            if (focus == '%')
                                return num1.Value % num2.Value;
                            if (focus == '^')
                            {
                                for (int x = 0; x < num2.Value; x++)
                                {
                                    num1 *= num1;
                                }
                                return num1.Value;
                            }
                        }
                    }
                }
            }
            return 0;
        }
    }
}
