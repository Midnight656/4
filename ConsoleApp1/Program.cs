using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization.Configuration;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[][] dates = {
                new string[] { "07.05.2222", "  1. Практика по БЖД", "  2. Практика по дискретной математике" },
                new string[] { "08.05.2222", "  1. Практика по шарпам", "  2. Практика по питону" },
                new string[] { "09.05.2222", "  1. Практика по БД" }
            };
            int i = 0;
            string[][] allinfo = {
                new string[] { "  1. Практика по БЖД", "--------------------", "Описание:", "Дата:" },
                new string[] { "  2. Практика по дискретной математике", "--------------------", "Описание:", "Дата:" },
                new string[] { "  1. Практика по шарпам", "--------------------", "Описание:", "Дата:" },
                new string[] { "  2. Практика по питону", "--------------------", "Описание:", "Дата:" },
                new string[] { "  1. Практика по БД", "--------------------", "Описание:", "Дата:" },
            };
            int position = writeListAndSetPosition(i, dates);
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(0, position);
                    Console.WriteLine("  ");
                    position--;
                    position = checkPosition(position, dates, i);
                    setPosition(position);
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(0, position);
                    Console.WriteLine("  ");
                    position++;
                    position = checkPosition(position, dates, i);
                    setPosition(position);
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    i++;
                    if (i > dates.Length - 1)
                    {
                        i = dates.Length - 1;
                    }
                    Console.Clear();
                    position = writeListAndSetPosition(i, dates);
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    i--;
                    if (i < 0)
                    {
                        i = 0;
                    }
                    Console.Clear();
                    position = writeListAndSetPosition(i, dates);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    string record = dates[i][position];
                    showMoreInformation(allinfo, record);
                    writeListAndSetPosition(i, dates);
                }
                else if (key.Key == ConsoleKey.C)
                {
                    Console.Clear();
                    Console.WriteLine("Что вы хотите добавить:дату,заметку?");
                    string comand = Console.ReadLine();
                    if (comand == "дату")
                    {
                        Console.Clear();
                        dates = setDate(dates, i);
                        writeListAndSetPosition(i, dates);
                    }
                    else if (comand == "заметку")
                    {
                        Console.Clear();
                        (dates, allinfo) = setRecord(dates, allinfo, i);
                        writeListAndSetPosition(i, dates);
                    }
                }
            }

        }
        static int writeListAndSetPosition(int i, string[][] dates)
        {
            foreach (string j in dates[i])
            {
                Console.WriteLine(j);
            }
            int position = 1;
            Console.SetCursorPosition(0, position);
            Console.WriteLine("->");
            return position;
        }
        static void setPosition(int position)
        {
            Console.SetCursorPosition(0, position);
            Console.WriteLine("->");
        }
        static int checkPosition(int position, string[][] dates, int i)
        {
            if (position > dates[i].Length - 1 || position < 1)
            {
                position = 1;
            }
            return position;
        }
        static void showMoreInformation(string[][] allinfo, string record)
        {
            Console.Clear();
            foreach (string[] task in allinfo)
            {
                var hash = new HashSet<string>(task);
                if (hash.Contains(record))
                {
                    for (int i = 0; i < task.Length; i++)
                    {
                        Console.WriteLine(task[i]);
                    }
                }
            }
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    break;
                }
            }
        }
        static string[][] setDate(string[][] dates, int i)
        {
            string[][] empty = { new string[] { } };
            dates = dates.Union(empty).ToArray();
            Console.WriteLine("Введите новую дату");
            string date = Console.ReadLine();
            dates[dates.Length - 1] = dates[dates.Length - 1].Append(date).ToArray();
            Console.Clear();
            return dates;
        }
        static (string[][], string[][]) setRecord(string[][] dates, string[][] allinfo, int i)
        {
            string[][] empty = { new string[] { } };
            Console.WriteLine("Введите название заметки");
            string record = "  " + dates[i].Length + ". " + Console.ReadLine();
            dates[i] = dates[i].Append(record).ToArray();
            allinfo = allinfo.Union(empty).ToArray();
            allinfo[allinfo.Length - 1] = allinfo[allinfo.Length - 1].Append(record).ToArray();
            allinfo[allinfo.Length - 1] = allinfo[allinfo.Length - 1].Append("--------------------").ToArray();
            Console.Clear();
            Console.WriteLine("Введите описание");
            string description = Console.ReadLine();
            allinfo[allinfo.Length - 1] = allinfo[allinfo.Length - 1].Append("Описание: " + description).ToArray();
            Console.WriteLine("Введите дату выполнения");
            string dateoffinish = Console.ReadLine();
            allinfo[allinfo.Length - 1] = allinfo[allinfo.Length - 1].Append("Дата:" + dateoffinish).ToArray();
            Console.Clear();
            return (dates, allinfo);
        }
    }
}
