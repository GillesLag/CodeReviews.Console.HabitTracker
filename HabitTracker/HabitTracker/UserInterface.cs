﻿using HabitTracker.Database;
using System.Globalization;
using HabitTracker.Models;

namespace HabitTracker;

internal class UserInterface
{
    internal void ShowMenu()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("Hello! what do you want to do in the habit tracker?\n");
            Console.WriteLine("i: Input data");
            Console.WriteLine("u: Update data");
            Console.WriteLine("d: Delete data");
            Console.WriteLine("v: View all data");
            Console.WriteLine("q: Quit");

            var userInput = Console.ReadLine();

            switch (userInput)
            {
                case "i":
                    Clear();
                    Insert();
                    break;

                case "u":
                    Clear();
                    Update();
                    break;

                case "d":
                    Clear();
                    Delete();
                    break;

                case "v":
                    Clear();
                    ViewAll();
                    break;

                case "q":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid input, choose a letter from the menu");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void ViewAll()
    {
        List<WaterDrinkingHabit> data;
        if (!DataExists(out data))
        {
            return;
        }

        PrintData(data);

        Console.WriteLine("Press any key to go back to the menu.");
        Console.ReadKey();
        return;
    }

    private void Delete()
    {
        List<WaterDrinkingHabit> data;
        if (!DataExists(out data))
        {
            return;
        }

        PrintData(data);

        Console.Write("Choose the id of the habit you want to delete: ");
        int id = ValidateNumber(Console.ReadLine());

        while (!DatatBaseOperations.Exists(id))
        {
            Console.WriteLine("this id does not exist, choose a different one!");
            id = ValidateNumber(Console.ReadLine());
        }

        DatatBaseOperations.Delete(id);
    }


    private void Update()
    {
        List<WaterDrinkingHabit> data;
        if (!DataExists(out data))
        {
            return;
        }

        PrintData(data);

        Console.Write("Choose the id of the habit you want to update: ");
        int id = ValidateNumber(Console.ReadLine());

        while (!DatatBaseOperations.Exists(id))
        {
            Console.WriteLine("this id does not exist, choose a different one!");
            id = ValidateNumber(Console.ReadLine());
        }

        Console.Write("Give a date in the format dd-mm-yyyy: ");
        string date = ValidateDate(Console.ReadLine());

        Console.Write("Give a quantity: ");
        int quantity = ValidateNumber(Console.ReadLine());

        DatatBaseOperations.Update(id, date, quantity);
    }
    private void Insert()
    {
        Console.Write("Give a date in the format dd-mm-yyyy: ");
        string date = ValidateDate(Console.ReadLine());

        Console.Write("Give a quantity: ");
        int quantity = ValidateNumber(Console.ReadLine());

        DatatBaseOperations.AddData(new WaterDrinkingHabit() { Date = DateTime.Parse(date), Quantity = quantity});
    }

    private void PrintData(List<WaterDrinkingHabit> data)
    {
        Console.WriteLine("Habits:\n");

        foreach (var item in data)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"{nameof(item.Id)}: {item.Id}");
            Console.WriteLine($"{nameof(item.Date)}: {item.Date.ToString("dd-MM-yyyy")}");
            Console.WriteLine($"{nameof(item.Quantity)}: {item.Quantity}");
            Console.WriteLine("-----------------------------\n");
        }
    }

    private string ValidateDate(string? date)
    {
        while (date == null || !DateTime.TryParse(date, CultureInfo.CurrentCulture, out _))
        {
            Console.WriteLine("Wrong date format. try again: ");
            date = Console.ReadLine();
        }

        return date;
    }

    private int ValidateNumber(string? number)
    {
        int parsedNumber;
        while(!int.TryParse(number, out parsedNumber))
        {
            Console.WriteLine("Wrong format, try again: ");
            number = Console.ReadLine();
        }

        return parsedNumber;
    }

    private bool DataExists(out List<WaterDrinkingHabit> data)
    {
        data = DatatBaseOperations.GetAll();

        if (data.Count == 0)
        {
            Console.WriteLine("There is no data. press any key to return to the main menu");
            Console.ReadKey();
        }

        return data.Count > 0;
    }

    private void Clear()
    {
        Console.Clear();
    }
}
