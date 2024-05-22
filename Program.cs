using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace MushroomPocket
{
    class Program
    {
        public static Dictionary<string, List<Character>> Characters = new Dictionary<string, List<Character>>();
        public static List<int> common_exp_book = new List<int> { 0 };
        public static List<int> rare_exp_book = new List<int> { 0 };
        public static List<int> legendary_exp_book = new List<int> { 0 };
        public static List<int> character_spawner = new List<int> { 3 };
        public static List<int> tokens = new List<int> { 20 };
        static void Main(string[] args)
        {
            //MushroomMaster criteria list for checking character transformation availability.   
            /*************************************************************************
                PLEASE DO NOT CHANGE THE CODES FROM LINE 15-19
            *************************************************************************/
            List<MushroomMaster> mushroomMasters = new List<MushroomMaster>(){
            new MushroomMaster("Daisy", 2, "Peach"),
            new MushroomMaster("Wario", 3, "Mario"),
            new MushroomMaster("Waluigi", 1, "Luigi")
            };

            //Use "Environment.Exit(0);" if you want to implement an exit of the console program
            //Start your assignment 1 requirements below.

            static void Menu()
            {
                Console.WriteLine();
                Console.WriteLine("********************************");
                Console.WriteLine("Welcome to Mushroom Pocket App");
                Console.WriteLine("********************************");
                Console.WriteLine("(1). Add Mushroom's Character to my Pocket");
                Console.WriteLine("(2). List Characters in my Pocket");
                Console.WriteLine("(3). Check if I can transform my Characters");
                Console.WriteLine("(4). Transform Character(s)");
                Console.WriteLine("(5). Gain EXP");
                Console.WriteLine("(6). Play a Game");
                Console.WriteLine("(7). Open Lootbox");
                Console.WriteLine("(8). Check Inventory");
                Console.WriteLine("(9). How To Play");
                Console.Write("Please only enter [1, 2, 3, 4, 5, 6, 7, 8, 9] or Q to quit: ");
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        AddCharacter();
                        Menu();
                        break;
                    case "2":
                        ListCharacter();
                        Menu();
                        break;
                    case "3":
                        CheckTransform();
                        Menu();
                        break;
                    case "4":
                        Transform();
                        Menu();
                        break;
                    case "5":
                        GainEXP();
                        Menu();
                        break;
                    case "6":
                        Game();
                        Menu();
                        break;
                    case "7":
                        OpenLootbox();
                        Menu();
                        break;
                    case "8":
                        Inventory();
                        Menu();
                        break;
                    case "9":
                        HowToPlay();
                        Menu();
                        break;
                    case "Q":
                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        Menu();
                        break;
                }
            }

            static void AddCharacter()
            {
                if (character_spawner[0] <= 0)
                {
                    Console.WriteLine("You do not have any Character Spawner");
                }
                else
                {
                    Console.WriteLine("You have the following characters to add to your pocket:");
                    Console.WriteLine("1. Waluigi");
                    Console.WriteLine("2. Daisy");
                    Console.WriteLine("3. Wario");
                    Console.Write("Enter the character you want to add to your pocket: ");
                    string character_name = Console.ReadLine();
                    if (Characters.ContainsKey(character_name))
                    {
                        Characters[character_name].Add(CreateCharacterInstance(character_name));
                        Console.WriteLine("Enter the HP of the character: ");
                        int hp = Convert.ToInt32(Console.ReadLine());
                        if (hp > 100)
                        {
                            hp = 100;
                        }
                        else if (hp <= 0)
                        {
                            hp = 1;
                        }
                        Characters[character_name][Characters[character_name].Count - 1].character_hp = hp;
                        Console.WriteLine("Enter the EXP of the character: ");
                        int exp = Convert.ToInt32(Console.ReadLine());
                        if (exp > 100)
                        {
                            exp = 100;
                        }
                        else if (exp < 0)
                        {
                            exp = 0;
                        }
                        Characters[character_name][Characters[character_name].Count - 1].character_exp = exp;
                    }
                    else
                    {
                        Characters[character_name] = new List<Character> { CreateCharacterInstance(character_name) };
                    }
                    character_spawner[0] -= 1;
                    Console.WriteLine($"{character_name} has been added to your pocket");
                }
            }

            static void OpenLootbox()
            {
                if (tokens[0] > 0)
                {
                    Console.Write("How many lootboxes do you want to open? ");
                    int num = Convert.ToInt32(Console.ReadLine());
                    if (num <= tokens[0])
                    {
                        for (int i = 0; i < num; i++)
                        {
                            Console.WriteLine("Opening a lootbox...");
                            Random random = new Random();
                            int lootbox = random.Next(1, 101);
                            if (lootbox <= 20)
                            {
                                string character_name = random.Next(1, 3) switch
                                {
                                    1 => "Waluigi",
                                    2 => "Daisy",
                                    _ => "Wario",
                                };
                                if (Characters.ContainsKey(character_name))
                                {
                                    Characters[character_name].Add(CreateCharacterInstance(character_name));
                                }
                                else
                                {
                                    Characters[character_name] = new List<Character> { CreateCharacterInstance(character_name) };
                                }
                                Character added_instance = Characters[character_name][Characters[character_name].Count - 1];
                                Console.WriteLine($"{character_name} with {added_instance.character_hp} HP, and {added_instance.character_exp} EXP has been added to your pocket");
                                Console.WriteLine();
                            }

                            else if (lootbox <= 25)
                            {
                                character_spawner[0] += 1;
                                Console.WriteLine("You got a Character Spawner!");
                                Console.WriteLine();
                            }

                            else if (lootbox <= 30)
                            {
                                legendary_exp_book[0] += 1;
                                Console.WriteLine("You got a Legendary Exp Book!");
                                Console.WriteLine();
                            }
                            else if (lootbox <= 60)
                            {
                                rare_exp_book[0] += 1;
                                Console.WriteLine("You got a Rare Exp Book!");
                                Console.WriteLine();
                            }
                            else
                            {
                                common_exp_book[0] += 1;
                                Console.WriteLine("You got a Common Exp Book!");
                                Console.WriteLine();
                            }
                            tokens[0] -= 1;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You do not have enough tokens");
                    }
                }
                else
                {
                    Console.WriteLine("You do not have enough tokens");
                }

            }

            static Character CreateCharacterInstance(string character_name)
            {
                return character_name switch
                {
                    "Waluigi" => new Character.Waluigi(),
                    "Daisy" => new Character.Daisy(),
                    "Wario" => new Character.Wario(),
                    _ => null,
                };
            }


            static void ListCharacter()
            {
                foreach (KeyValuePair<string, List<Character>> entry in Characters)
                {
                    Console.WriteLine("-------------------------");
                    foreach (Character character_instance in entry.Value)
                    {
                        Console.WriteLine("Name: " + character_instance.character_name);
                        Console.WriteLine("HP: " + character_instance.character_hp);
                        Console.WriteLine("EXP: " + character_instance.character_exp);
                        Console.WriteLine("Skill: " + character_instance.character_skill);
                        Console.WriteLine("-------------------------");
                    }
                }
            }

            static void CheckTransform()
            {
                int daisy_count = 0;
                int wario_count = 0;
                int waluigi_count = 0;

                foreach (List<Character> instances in Characters.Values)
                {
                    foreach (Character character_instance in instances)
                    {
                        if (character_instance.character_name == "Daisy" && character_instance.character_exp >= 100)
                        {
                            daisy_count += 1;
                        }
                        else if (character_instance.character_name == "Wario" && character_instance.character_exp >= 100)
                        {
                            wario_count += 1;
                        }
                        else if (character_instance.character_name == "Waluigi" && character_instance.character_exp >= 100)
                        {
                            waluigi_count += 1;
                        }
                    }
                }

                if (daisy_count >= 2)
                {
                    Console.WriteLine("Daisy --> Peach");
                }

                if (wario_count >= 3)
                {
                    Console.WriteLine("Wario --> Mario");
                }

                if (waluigi_count >= 1)
                {
                    Console.WriteLine("Waluigi --> Luigi");
                }
            }

            static void Transform()
            {
                foreach (KeyValuePair<string, List<Character>> entry in Characters)
                {
                    bool transformed = false;
                    List<Character> new_instances = new List<Character>();

                    foreach (Character character_instance in entry.Value)
                    {
                        if (character_instance.character_name == "Daisy" && character_instance.character_exp >= 100)
                        {
                            new_instances.Add(new Character.Peach());
                            Console.WriteLine("Daisy has been transformed to Peach");
                            transformed = true;
                        }
                        else if (character_instance.character_name == "Wario" && character_instance.character_exp >= 100)
                        {
                            new_instances.Add(new Character.Mario());
                            Console.WriteLine("Wario has been transformed to Mario");
                            transformed = true;
                        }
                        else if (character_instance.character_name == "Waluigi" && character_instance.character_exp >= 100)
                        {
                            new_instances.Add(new Character.Luigi());
                            Console.WriteLine("Waluigi has been transformed to Luigi");
                            transformed = true;
                        }
                        else
                        {
                            new_instances.Add(character_instance);
                        }
                    }

                    if (transformed)
                    {
                        Characters[entry.Key] = new_instances;
                    }
                }
                // Remove excess characters if transformed
                foreach (string character_name in new List<string>(Characters.Keys))
                {
                    if (Characters[character_name].Count == 0)
                    {
                        Characters.Remove(character_name);
                    }
                }
            }

            static void GainEXP()
            {
                Console.WriteLine("You have the following EXP books:");
                Console.WriteLine("Common EXP Book: " + common_exp_book[0]);
                Console.WriteLine("Rare EXP Book: " + rare_exp_book[0]);
                Console.WriteLine("Legendary EXP Book: " + legendary_exp_book[0]);
                Console.WriteLine();
                Console.Write("Enter the character name you want to give EXP to: ");
                string character_name = Console.ReadLine();
                if (Characters.ContainsKey(character_name))
                {
                    for (int i = 0; i < Characters[character_name].Count; i++)
                    {
                        Character character_instance = Characters[character_name][i];
                        Console.WriteLine($"{i + 1}. {character_instance.character_name} with {character_instance.character_hp} HP, and {character_instance.character_exp} EXP");
                    }
                    Console.Write("Enter the index of the character you want to give EXP to: ");
                    int character_index = Convert.ToInt32(Console.ReadLine());
                    Character selected_character = Characters[character_name][character_index - 1];
                    Console.Write("Enter the type of EXP book you want to give (Common, Rare, Legendary): ");
                    string exp_book = Console.ReadLine();
                    if (exp_book == "Common")
                    {
                        Console.Write($"Enter the amount of {exp_book} EXP books you want to use: ");
                        int amt = Convert.ToInt32(Console.ReadLine());
                        if (common_exp_book[0] >= amt)
                        {
                            selected_character.character_exp += amt * 5;
                            common_exp_book[0] -= amt;
                            Console.WriteLine($"{amt} Common EXP books has been used to give {selected_character.character_name} {amt * 5} EXP");
                        }
                        else
                        {
                            Console.WriteLine("You do not have enough Common EXP books");
                        }
                    }
                    else if (exp_book == "Rare")
                    {
                        Console.Write($"Enter the amount of {exp_book} EXP books you want to use: ");
                        int amt = Convert.ToInt32(Console.ReadLine());
                        if (rare_exp_book[0] >= amt)
                        {
                            selected_character.character_exp += amt * 10;
                            rare_exp_book[0] -= amt;
                            Console.WriteLine($"{amt} Rare EXP books has been used to give {selected_character.character_name} {amt * 10} EXP");
                        }
                        else
                        {
                            Console.WriteLine("You do not have enough Rare EXP books");
                        }
                    }
                    else if (exp_book == "Legendary")
                    {
                        Console.Write($"Enter the amount of {exp_book} EXP books you want to use: ");
                        int amt = Convert.ToInt32(Console.ReadLine());
                        if (legendary_exp_book[0] >= amt)
                        {
                            selected_character.character_exp += amt * 20;
                            legendary_exp_book[0] -= amt;
                            Console.WriteLine($"{amt} Legendary EXP books has been used to give {selected_character.character_name} {amt * 20} EXP");
                        }
                        else
                        {
                            Console.WriteLine("You do not have enough Legendary EXP books");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid EXP book type");
                    }
                }
                else
                {
                    Console.WriteLine("Character not found");
                }
            }

            static void Game()
            {
                Random random = new Random();
                int number_to_guess = random.Next(1, 101);
                int attempts = 0;
                int max_attempts = 5;
                Console.WriteLine("Welcome to the Number Guessing Game!");
                Console.WriteLine("Do you want to play the game with a character? (yes/no)");
                string play = Console.ReadLine().ToLower();
                Console.WriteLine("I have selected a number between 1 and 100. Try to guess it!");
                if (play == "no")
                {
                    while (attempts < max_attempts)
                    {
                        Console.Write($"Attempt {attempts + 1}/{max_attempts} - Enter your guess: ");
                        string guess = Console.ReadLine();

                        try
                        {
                            int guess_int = Convert.ToInt32(guess);
                            attempts += 1;

                            if (guess_int < number_to_guess)
                            {
                                Console.WriteLine("Too low!");
                            }
                            else if (guess_int > number_to_guess)
                            {
                                Console.WriteLine("Too high!");
                            }
                            else
                            {
                                tokens[0] += 100;
                                Console.WriteLine($"Congratulations! You've guessed the number in {attempts} attempts.");
                                Console.WriteLine("You have gained 100 tokens.");
                                break;
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input! Please enter a valid number.");
                        }
                    }
                    Console.WriteLine($"The number was {number_to_guess}.");
                }
                else if (play == "yes")
                {
                    Console.Write("Select a character to play the game with: ");
                    string character_name = Console.ReadLine();
                    if (!Characters.ContainsKey(character_name))
                    {
                        Console.WriteLine("Character not found");
                        Game();
                    }
                    else
                    {
                        for (int i = 0; i < Characters[character_name].Count; i++)
                        {
                            Character character_instance = Characters[character_name][i];
                            Console.WriteLine($"{i + 1}. {character_instance.character_name} with {character_instance.character_hp} HP, and {character_instance.character_exp} EXP");
                        }
                        Console.Write("Enter the index of the character you want to give EXP to: ");
                        int character_index = Convert.ToInt32(Console.ReadLine());
                        Character selected_character = Characters[character_name][character_index - 1];
                        if (selected_character.character_skill == "Agility") // Waluigi
                        {
                            max_attempts += 1;
                            Console.WriteLine("Using Agility, you have one extra attempt");
                            Console.WriteLine($"You have {max_attempts} attempts");
                        }
                        else if (selected_character.character_skill == "Leadership") // Daisy
                        {
                            if (number_to_guess % 2 == 0)
                            {
                                Console.WriteLine("Using Leadership, you know the number is odd or even");
                                Console.WriteLine("The number is even");
                                Console.WriteLine($"You have {max_attempts} attempts");
                            }
                            else
                            {
                                Console.WriteLine("Using Leadership, you know the number is odd or even");
                                Console.WriteLine("The number is odd");
                                Console.WriteLine($"You have {max_attempts} attempts");
                            }
                        }
                        else if (selected_character.character_skill == "Strength") // Wario
                        {
                            int lower_bound = (number_to_guess / 10) * 10;
                            int upper_bound = lower_bound + 10;
                            if (upper_bound > 100)
                            {
                                upper_bound = 100;
                            }
                            Console.WriteLine("Using Strength, you can narrrow down the number to a range of 10");
                            Console.WriteLine($"The number is between {lower_bound} and {upper_bound}");
                            Console.WriteLine($"You have {max_attempts} attempts");
                        }
                        else if (selected_character.character_skill == "Precision and Accuracy") // Luigi
                        {
                            max_attempts += 2;
                            Console.WriteLine("Using Precision and Accuracy, you have two extra attempts");
                            Console.WriteLine($"You have {max_attempts} attempts");
                        }
                        else if (selected_character.character_skill == "Magic Abilities") // Peach
                        {
                            if (number_to_guess % 2 == 0)
                            {
                                Console.WriteLine("Using Magic Abilities, you know the number is odd or even and you gain one extra attempt");
                                Console.WriteLine("The number is even");
                                if (number_to_guess < 50)
                                {
                                    Console.WriteLine("The number is less than 50");
                                    Console.WriteLine($"You have {max_attempts} attempts");
                                }
                                else
                                {
                                    Console.WriteLine("The number is greater than 50");
                                    Console.WriteLine($"You have {max_attempts} attempts");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Using Magic Abilities, you know the number is odd or even and you gain one extra attempt");
                                Console.WriteLine("The number is odd");
                                if (number_to_guess < 50)
                                {
                                    Console.WriteLine("The number is less than 50");
                                    Console.WriteLine($"You have {max_attempts} attempts");
                                }
                                else
                                {
                                    Console.WriteLine("The number is greater than 50");
                                    Console.WriteLine($"You have {max_attempts} attempts");
                                }
                            }
                        }
                        else if (selected_character.character_skill == "Combat Skills") // Mario
                        {
                            max_attempts += 1;
                            int lower_bound = (number_to_guess / 10) * 10;
                            int upper_bound = lower_bound + 10;
                            if (upper_bound > 100)
                            {
                                upper_bound = 100;
                            }
                            Console.WriteLine("Using Combat Skills, you can narrrow down the number to a range of 10 and you gain one extra attempt");
                            Console.WriteLine($"The number is between {lower_bound} and {upper_bound}");
                            Console.WriteLine($"You have {max_attempts} attempts");
                        }


                        while (attempts < max_attempts)
                        {
                            Console.Write($"Attempt {attempts + 1}/{max_attempts} - Enter your guess: ");
                            string guess = Console.ReadLine();

                            try
                            {
                                int guess_int = Convert.ToInt32(guess);
                                attempts += 1;

                                if (guess_int < number_to_guess)
                                {
                                    Console.WriteLine("Too low!");
                                }
                                else if (guess_int > number_to_guess)
                                {
                                    Console.WriteLine("Too high!");
                                }
                                else
                                {
                                    selected_character.character_exp += 10;
                                    tokens[0] += 50;
                                    Console.WriteLine($"Congratulations! You've guessed the number in {attempts} attempts.");
                                    Console.WriteLine($"{selected_character.character_name} has gained 10 EXP.");
                                    Console.WriteLine("You have gained 50 tokens.");
                                    break;
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input! Please enter a valid number.");
                            }
                        }

                        Console.WriteLine($"The number was {number_to_guess}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    Game();
                }
            }


            static void Inventory()
            {
                Console.WriteLine("You have the following items in your inventory:");
                Console.WriteLine("Common EXP Book: " + common_exp_book[0]);
                Console.WriteLine("Rare EXP Book: " + rare_exp_book[0]);
                Console.WriteLine("Legendary EXP Book: " + legendary_exp_book[0]);
                Console.WriteLine("Tokens: " + tokens[0]);
                Console.WriteLine();
            }

            static void HowToPlay()
            {
                Console.WriteLine("********************************");
                Console.WriteLine("Rules");
                Console.WriteLine("********************************");
                Console.WriteLine("1. The game is called Mushroom Pocket");
                Console.WriteLine("2. You can add a character to your pocket using a Character Spawner");
                Console.WriteLine("3. You can only add the following characters:");
                Console.WriteLine("   - Waluigi with HP between 1 and 100, EXP between 0 and 100, and Agility skill");
                Console.WriteLine("   - Daisy with HP between 1 and 100, EXP between 0 and 100, and Leadership skill");
                Console.WriteLine("   - Wario with HP between 1 and 100, EXP between 0 and 100, and Strength skill");
                Console.WriteLine("4. You are given 3 free Character Spawners and 20 tokens at the start of the game");
                Console.WriteLine("5. One token can be used to open One lootbox");
                Console.WriteLine("6. Lootboxes contain characters, EXP books and Character Spawners");
                Console.WriteLine("7. The rates of getting characters and EXP books from Lootboxes are as follows:");
                Console.WriteLine("   - Characters: 20%");
                Console.WriteLine("   - Common EXP Book: 40%");
                Console.WriteLine("   - Rare EXP Book: 30%");
                Console.WriteLine("   - Legendary EXP Book: 5%");
                Console.WriteLine("   - Character Spawner: 5%");
                Console.WriteLine("8. Characters have HP, EXP, and Skills");
                Console.WriteLine("9. Characters can be transformed when they reach 100 EXP and the required number of characters");
                Console.WriteLine("10. The required number of characters to transform are as follows:");
                Console.WriteLine("   - You need 1 Waluigi with 100 EXP can be transformed to Luigi");
                Console.WriteLine("   - You need 2 Daisy with 100 EXP can be transformed to Peach");
                Console.WriteLine("   - You need 3 Wario with 100 EXP can be transformed to Mario");
                Console.WriteLine("11. After transforming, the characters will be set to 100 HP, 0 EXP, and the skill is replaced with a new skill");
                Console.WriteLine("12. EXP books can be used to give EXP to characters");
                Console.WriteLine("13. The amount of EXP given by each EXP book is as follows:");
                Console.WriteLine("   - Common EXP Book: 5 EXP");
                Console.WriteLine("   - Rare EXP Book: 10 EXP");
                Console.WriteLine("   - Legendary EXP Book: 20 EXP");
                Console.WriteLine("14. Tokens can be earned by playing the game");
                Console.WriteLine("15. The game is a number guessing game");
                Console.WriteLine("16. The game can be played with or without a character");
                Console.WriteLine("17. Different characters have different skills that can help in the game");
                Console.WriteLine();
            }
            Menu();
        }
    }
}