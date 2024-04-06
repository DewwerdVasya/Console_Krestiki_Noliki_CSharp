using System;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    public static char[,] cell = new char[3, 3];

    public static bool isGameOn = true;

    public static void Main(string[] args)
    {

        Random ran = new Random();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                cell[i, j] = '_';
            }
        }

        bool yourTurn = true;

        string result = "";

        int row = -1;
        int column = -1;

        Console.WriteLine("Игра началась\n\n" + CreateField());

        while (isGameOn)
        {
            if (yourTurn)
            {
                Console.WriteLine("Ваш ход");
                while (row > 2 || row < 0 || column > 2 || column < 0 || cell[row, column] != '_')
                {
                    Console.Write("Введи номер строки: ");
                    row = Convert.ToInt32(Console.ReadLine()) - 1;

                    Console.Write("Введи номер колонки: ");
                    column = Convert.ToInt32(Console.ReadLine()) - 1;

                }
                cell[row, column] = 'X';

                yourTurn = false;
            }
            else
            {
                Console.WriteLine("Ход компьютера");
                while (cell[row, column] != '_')
                {
                    row = ran.Next(3);
                    column = ran.Next(3);
                }

                cell[row, column] = '0';
                yourTurn = true;
            }

            Console.WriteLine(CreateField());

            if (WinCheck('X'))
                result = "Крестики победили!";
            else if (WinCheck('0'))
                result = "Нолики победили!";
            else if (DrawCheck())
                result = "Ничья!";
        }

        Console.WriteLine($"Игра окончена\n\n{result}");

    }

    public static string CreateField()
    {
        string field = "";
        int c = 0;

        foreach (char symb in cell)
        {
            field += "|" + symb;
            c++;

            if (c == 3)
            {
                field += "|\n";
                c = 0;
            }
        }

        return field;
    }

    public static bool WinCheck(char symb)
    {
        int c = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (cell[i, j] == symb)
                    c++;
                else
                    break;
                if (c == 3)
                {
                    isGameOn = false;
                    return true;
                }
            }

            c = 0;

            for (int j = 0; j < 3; j++)
            {
                if (cell[j, i] == symb)
                    c++;
                else
                    break;
                if (c == 3)
                {
                    isGameOn = false;
                    return true;
                }
            }

            c = 0;
        }

        for (int i = 0; i < 3; i++)
        {
            if (cell[i, i] == symb)
                c++;
            else
                break;

            if (c == 3)
            {
                isGameOn = false;
                return true;
            }
        }

        c = 0;

        for (int i = 0; i < 3; i++)
        {
            if (cell[2 - i, i] == symb)
                c++;
            else
                break;

            if (c == 3)
            {
                isGameOn = false;
                return true;
            }
        }

        return false;
    }

    public static bool DrawCheck()
    {
        foreach (char symb in cell)
        {
            if (symb == '_')
            {
                return false;
            }
        }

        isGameOn = false;
        return true;
    }

}
