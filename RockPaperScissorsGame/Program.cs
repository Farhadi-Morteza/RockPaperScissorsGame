using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissorsGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int gameMaxLevel = 3;
          
            var GameItems = new Dictionary<int, string>()
            {
                { 1, "Rock" },
                { 2, "Paper" },
                { 3, "Scissors" }
            };

            List<ActionResult> itemActionResults = new List<ActionResult>();
            itemActionResults.AddRange(new List<ActionResult>
            {
                new ActionResult{ChoiceItem1 = 1, ChoiceItem2 = 2 , WinnerItem = 2},
                new ActionResult{ChoiceItem1 = 1, ChoiceItem2 = 3 , WinnerItem = 1 },
                new ActionResult{ChoiceItem1 = 2, ChoiceItem2 = 3 , WinnerItem = 3 }
            });

            while (true)
            {
                ShowMenu();

                try
                {
                    var SelectedItem = Convert.ToInt32(Console.ReadLine());

                    switch (SelectedItem)
                    {
                        case (int)MenuItems.StartGame:
                            {
                                StartGame();
                                break;
                            }                            
                        case (int)MenuItems.AddGameItem:
                            {  
                                AddGameItem();                                
                                ShowItemActionResult();
                                break;
                            }
                        case (int)MenuItems.Setting:
                            {
                                GameSetting();
                                break;
                            }
                        case (int)MenuItems.About:
                            {
                                About();
                                break;
                            }
                        case (int)MenuItems.Exit:
                            {
                                Exit();
                                break;
                            }
                            
                        default:
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine(Messages.TypeCorrectNumberOfMenu);
                }
            }

            void ShowMenu()
            {
                Console.WriteLine();

                string menuItems = string.Empty;
                foreach (int i in Enum.GetValues(typeof(MenuItems)))
                {
                    menuItems += $"[{i}]: {Enum.GetName(typeof(MenuItems), i)} ║ ";
                }
                Console.WriteLine(menuItems.DrawInConsoleBox());
            }

            void AddGameItem()
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine(Messages.TypeNewGameItem);
                        string newItem = Console.ReadLine().Trim().ToLower();
                        GameItems.Add(GameItems.Count + 1, newItem);

                        AddItemActionResult(newItem);
                        break;
                    }
                    catch
                    {
                        Console.WriteLine(Messages.TypeCurrectNumber);
                    }
                }
            }

            void AddItemActionResult(string item)
            {
                var newItem = GameItems.Where(x => x.Value == item).FirstOrDefault();                
                var gameItemExcepNewItem = GameItems.Where(x => x.Key != newItem.Key).ToList();
                int winner;

                for (int i = 0; i < gameItemExcepNewItem.Count; i++)
                {
                    Console.WriteLine(String.Format(
                            Messages.BetweenTowItemsWinnerIs
                            , gameItemExcepNewItem.ElementAt(i).Key
                            , gameItemExcepNewItem.ElementAt(i).Value
                            , newItem.Key
                            , newItem.Value
                            ));

                    while (true)
                    {
                        try
                        {
                            winner = Convert.ToInt32(Console.ReadLine());

                            while (true)
                            {                        
                                try
                                {                            
                                    if (winner == gameItemExcepNewItem.ElementAt(i).Key || winner == newItem.Key)
                                    {
                                        itemActionResults
                                            .Add(new ActionResult 
                                                { 
                                                    ChoiceItem1 = gameItemExcepNewItem.ElementAt(i).Key
                                                    , ChoiceItem2 = newItem.Key, WinnerItem = Convert.ToInt32(winner) 
                                                });
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine(Messages.ChoiceCurrectNumber);
                                        winner = Convert.ToInt32(Console.ReadLine());
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine(Messages.ChoiceCurrectNumber);
                                    winner = Convert.ToInt32(Console.ReadLine());
                                }
                            }
                            break;
                        }
                        catch
                        {
                            Console.WriteLine(Messages.TypeCurrectNumber);
                            winner = Convert.ToInt32(Console.ReadLine());
                        }
                    }
                }
            }

            void ShowGameItems()
            {
                Console.WriteLine();
                for (int i = 0; i < GameItems.Count; i++)
                {
                    Console.WriteLine(string.Format(Messages.GameItem
                        , GameItems.ElementAt(i).Key
                        , GameItems.ElementAt(i).Value
                        )); 
                }
            }

            void ShowItemActionResult()
            {
                Console.WriteLine();
                for (int i = 0; i < itemActionResults.Count; i++)
                {
                    Console.WriteLine(string.Format(Messages.ItemActionResult
                        , GameItems.Where(x => x.Key == itemActionResults.ElementAt(i).ChoiceItem1).FirstOrDefault().Value
                        , GameItems.Where(x => x.Key == itemActionResults.ElementAt(i).ChoiceItem2).FirstOrDefault().Value
                        , GameItems.Where(x => x.Key == itemActionResults.ElementAt(i).WinnerItem).FirstOrDefault().Value
                        ));
                }
            }

            void StartGame()
            {
                Console.WriteLine();
                int selectedItem;
                int gameLevel = 1;
                int userRating = 0;
                int sysRating = 0;

                while(userRating < gameMaxLevel && sysRating < gameMaxLevel)
                {
                    ShowGameItems();
                    Console.WriteLine(Environment.NewLine + Messages.ChoiceNumber);

                    while (true)
                    {
                        try
                        {
                            selectedItem = Convert.ToInt32(Console.ReadLine());

                            while (true)
                            {
                                try
                                {
                                    var gameItem = GameItems.Where(x => x.Key == selectedItem).FirstOrDefault();
                                    if (gameItem.Key > 0 && gameItem.Key <= GameItems.Count)
                                    {
                                        Random random = new Random();
                                        int randomItem = random.Next(1, GameItems.Count);
                                        Console.WriteLine(string.Format(Messages.YouNumberSysNumber, selectedItem, randomItem));
                                        if (selectedItem == randomItem)
                                        {                                            
                                            Console.WriteLine(Messages.Equal);
                                            ShowGameResult(userRating, sysRating);
                                            break;
                                        }
                                        int win = winner(selectedItem, randomItem);                                        
                                        if (selectedItem == win)
                                        {
                                            userRating++;
                                            Console.WriteLine(Messages.YouWon);                                                                                       
                                        }
                                        else
                                        {
                                            sysRating++;
                                            Console.WriteLine(Messages.YouLost);
                                        }
                                        ShowGameResult(userRating, sysRating);
                                        gameLevel++;

                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine(String.Format(Messages.ChoiceBetweenTwoNumber, 1, GameItems.Count));
                                        selectedItem = Convert.ToInt32(Console.ReadLine());
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine(Messages.TypeCurrectNumber);
                                    selectedItem = Convert.ToInt32(Console.ReadLine());
                                }
                            }
                            break;
                        }
                        catch
                        {
                            Console.WriteLine(Messages.ChoiceNumber);
                            selectedItem = Convert.ToInt32(Console.ReadLine());
                        }
                    }

                    
                }
                if(userRating > sysRating)
                {                    
                    Console.WriteLine(Messages.Winner.DrawInConsoleBox());
                }
                else
                {
                    Console.WriteLine(Messages.Lost.DrawInConsoleBox());
                }
            }

            int winner(int choiceItem1, int choiceItem2)
            {
                int winner =
                    itemActionResults
                    .Where(x => x.ChoiceItem1 == choiceItem1 && x.ChoiceItem2 == choiceItem2 || x.ChoiceItem1 == choiceItem2 && x.ChoiceItem2 == choiceItem1)
                    .Select(s => s.WinnerItem)
                    .FirstOrDefault(); 

                return winner;
            }

            void GameSetting()
            {
                Console.WriteLine(Messages.GameMaxLevel);
                while (true)
                {
                    try
                    {
                        gameMaxLevel = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine(Messages.TypeCurrectNumber);
                    }
                }               
            }

            void About()
            {
                Console.WriteLine(Messages.About);
            }
           
            void Exit()
            {
                Console.WriteLine(Messages.Exit);
                var result = Console.ReadLine();

                if (result.Trim().ToLower() == "y")
                {
                    Environment.Exit(0);
                }
            }

            void ShowGameResult(int userRating, int sysRating)
            {
                Console.WriteLine(String.Format(Messages.GameResult, userRating, sysRating).DrawInConsoleBox());
            }
        }

        enum MenuItems
        {
            StartGame = 1,
            AddGameItem = 2,
            Setting = 3,
            About = 4,
            Exit = 5
        }

    }
}
