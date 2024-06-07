using System.Media;
using Spectre.Console;

//      USING THE FOLLOWING EXTENSIONS AND LIBRARIES.       // 
//      - Spectre Console [Console NET Library providing tons of useful features, free to use.]     //
//          https://github.com/spectreconsole/spectre.console#installing        //
//          Documentation - https://spectreconsole.net/     //
//      - System.Windows.Extensions [Provides many useful features, including SoundPlayer that was exclusive to WPF/WinForms.       //

//      DISCLAIMER: A LOT OF THE INITIALLY PLANNED FEATURES WERE CUT FROM THE GAME DUE TO TIME CONSTRAINTS.     //


namespace No_Hope___Text_Based_Console_Game
{
    internal class Program
    {
        //public SoundPlayer BackgroundPlayer = new SoundPlayer();
        
        static void Main(string[] args)
        {
            PlayGame playGame = new PlayGame();

        Start: // This is a label, that you can apply anywhere in the code. Labels enable you to jump to a certain part of the code. I will be using this temporarily, before moving to making most of the labels into functions with their respective code.
            //SoundPlayer MainMenuPlayer = new SoundPlayer("Resident_Evil_3_Nemesis_OST_Feel_the_Tension.wav");

            //MainMenuPlayer.Load();
            //MainMenuPlayer.PlayLooping();

            //AnsiConsole.Markup("[underline red]Hello[/] World!");

            AnsiConsole.Write(new FigletText("Welcome to No Hope!").Centered().Color(Color.Blue));

            // ForegroundColor suprisingly works with AnsiConsole text.
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.Title = "No Hope - Console-Based Game";

            //Console.WriteLine("Welcome to No Hope! A Console-Based Game.");
            //Console.WriteLine("Here are all the options you can choose from:\n1 - Play the Game\n2 - How to Play?\n3 - Store\n4 - Quit\n\nEnter any corresponding value to Continue...");

            //AnsiConsole.WriteLine("Welcome to No Hope! A Console-Based Adventure Game.");
            //AnsiConsole.MarkupLine("A Console-Based Adventure Game.");
            
            AnsiConsole.Write(new Markup("A Console-Based Adventure Game.\n").Centered());

            //AnsiConsole.WriteLine("Here are the options that you can choose from: ");

            //Console.WriteLine("\nEnter any corresponding value to Continue...");
            /*int menuValue = Convert.ToInt16(Console.ReadLine());

            switch (menuValue)
            {
                case 1:
                    MainMenuPlayer.Stop();
                    MainMenuPlayer.Dispose(); // No longer need this when the player starts the game.
                    playGame.BackStory();
                    break;

                case 2:
                    Console.Title = "No Hope - Console-Based Game: How To Play";
                    Console.WriteLine("To play this game, you always have to choose between the options that are displayed on the screen. \n");
                    Console.WriteLine("To advanced through this game, you must proceed in different directions to reach the goal. \n");
                    Console.WriteLine("Throughout the game, you will encounter enemies, which varies between difficulty. Defeating enemies will grant you items, both usable and consumable items.\n Furthermore, you can level up, increasing your stats. \n");
                    Console.WriteLine("The game also features a Store. Where you can buy permanent upgrades or items.\n\nIt is recommended to wear Headphones while playing this game, as there is audio support for this game.");
                    Console.ReadKey();
                    goto Start;

                case 4:
                    Console.WriteLine("Exiting game...");
                    System.Threading.Thread.Sleep(750);
                    Environment.Exit(0);
                    break;
            }
            */

            //      CREATES A THIN LINE     //
            Rule Underline = new Rule("Choose any option below to continue.\n").LeftAligned();
            Underline.Style = Style.Parse("blue");
            AnsiConsole.Write(Underline);

            //      CREATES A SELECTION PROMPT      //

            /*
             * This allows the player to scroll through different choices that they can make.
             */
            string MenuValue = AnsiConsole.Prompt(new SelectionPrompt<string>()
                //.Title("\nChoose an [green]option[/] below to continue.")
                .PageSize(4) // Displays this many choices, if there is more choices displayed than the number defined, the selections will scroll up and down accordingly.
                //.MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(new[] { "Play the Game", "Set Printout Animation Speed" , "How to Play?", "Quit" })); // Choices are stored in an array.

            switch (MenuValue) 
            {
                case "Play the Game":
                    //MainMenuPlayer.Stop();
                    //MainMenuPlayer.Dispose(); // No longer need this when the player starts the game.
                    playGame.BackStory();
                    break;

                case "Set Printout Animation Speed":
                    Console.WriteLine("Enter the Typing Speed you wish to set.");
                    AnsiConsole.MarkupLine("[grey]The higher the number, the slower the animation is.\n20 is the default.\n0 - Completely disables the animation.[/]");
                    playGame.PlayerDefinedTypingSpeed = Convert.ToInt32(Console.ReadLine());
                    AnsiConsole.MarkupLine("[green]Your Typing Speed preference has been saved![/]");
                    Thread.Sleep(750);

                    AnsiConsole.Status().Start("\n[white]Please wait for a moment, so you can be directed back to the main menu...[/]", LoadingStatus =>
                    {
                        // ctx is a local variable is creates a new instance of StatusContext.

                        // Simulate some work
                        AnsiConsole.MarkupLine("[white]Cleaning up the console and loading the main menu.[/]");
                        Thread.Sleep(1250);

                        // Update the status and spinner
                        LoadingStatus.Status("[white]Now loading...[/]");
                        LoadingStatus.Spinner(Spinner.Known.Star2);
                        LoadingStatus.SpinnerStyle(Style.Parse("green"));
                        Thread.Sleep(1000);

                        //AnsiConsole.MarkupLine("Done!");
                        LoadingStatus.Status("[green]Done![/]");
                        Thread.Sleep(350);

                        AnsiConsole.Clear();
                        Console.Clear();
                    });

                    goto Start;

                case "How to Play?":
                    Console.Title = "No Hope - Console-Based Game: How To Play";

                    Rule HowToPlayUnderline = new Rule("How to Play.\n").LeftAligned();
                    HowToPlayUnderline.Style = Style.Parse("blue");
                    AnsiConsole.Write(HowToPlayUnderline);

                    Console.WriteLine("To play this game, you always have to choose between the options that are displayed on the screen. \n");
                    Console.WriteLine("To advanced through this game, you must proceed in different directions to reach the goal. \n");
                    Console.WriteLine("Throughout the game, you will encounter enemies, which varies between difficulty. Defeating enemies will grant you items, both usable and consumable items.\nFurthermore, you can level up, increasing your stats. \n");
                    Console.WriteLine("The game also features a Store. Where you can buy permanent upgrades or items.\n\nIt is recommended to wear Headphones while playing this game, as there is audio support for this game.\n\n");
                    Console.ReadKey();
                    AnsiConsole.Clear();
                    goto Start;

                case "Quit":
                    Console.WriteLine("Exiting game...");
                    System.Threading.Thread.Sleep(750);
                    Environment.Exit(0);
                    break;
            }
        }
    }

    class PlayGame
    {
        //      PUZZLE & STORY PROGRESSION RELATED      //
        bool GotCrossBow = false;
        protected bool GotCombinationPaper = false;
        protected bool GotPuzzlePaper1 = false;
        protected string PuzzlePaper1Solution = "Eagle.";
        protected bool GotPuzzlePaper2 = false;
        protected string PuzzlePaper2Solution = "Bear.";
        protected bool GotPuzzlePaper3 = false;
        protected string PuzzlePaper3Solution = "Snake.";

        //      ENEMY INIT RELATED      //
        public string EnemyEncountered = "";

        //      PREFERENCES         //
        public int PlayerDefinedTypingSpeed = 20;

        //      CLASSES     //
        public Player player = new Player();
        public Player.PlayerInventory.ConsumableItems ConsumableItems = new Player.PlayerInventory.ConsumableItems();
        //public PlayerInventory.ConsumableItems ConsumableItems = new PlayerInventory.ConsumableItems();


        public void BackStory()
        {
            ClearConsole();

            Console.Title = "The Prologue";

            AnimatedTextPrintout("You are The Man - who's identity remains a mystery, after years of being isolated from the society after losing His family to a car crash.\n\nHe decided to isolate himself from everyone after the incident, in doing so, made him purchase a land in a middle of a forested area, where he had a house built.\n", PlayerDefinedTypingSpeed);

            //Console.WriteLine("You are The Man - who's identity remains a mystery, after years of being isolated from the society after losing His family to a car crash.\n\nHe decided to isolate himself from everyone after the incident, in doing so, made him purchase a land in a middle of a forested area, where he had a house built.\n");

            AnimatedTextPrintout("One of the recent days, The Man was watching the news on the TV, until the broadcast has been interrupted with a severe weather warning.\n\nThe weather warning was of heavy thunderstorm that will be sweeping across the entire country.\n\nThe Man quickly recognised the threat, making his main objective to secure his house his top priority.\n\nHowever, he cannot do this alone. He does not have the supplies to prepare for the storm, so he needs to make his way to the closest telephone booth, located outskirts of the forest...", PlayerDefinedTypingSpeed);

            Console.ForegroundColor = ConsoleColor.Red;

            AnimatedTextPrintout("\nUsing Headphones is heavily recommended. The game uses royalty free sound to give you a better gameplay experience.", PlayerDefinedTypingSpeed);

            AnimatedTextPrintout("\n\nPress any key to start the game...", PlayerDefinedTypingSpeed);
            
            Console.ReadKey();
            Console.CursorVisible = true;
            MainGameInit();
        }

        public void MainGameInit()
        {
            // Some of these might be unnecessary, need to go back into this...
            RandomisedRewards randomisedRewards = new RandomisedRewards();
            
            //Player.PlayerInventory playerInventory = new Player.PlayerInventory();
            //Player.PlayerInventory.PlayerWeapons Weapons = new Player.PlayerInventory.PlayerWeapons();
            //Player.PlayerInventory.ConsumableItems consumableItems = new Player.PlayerInventory.ConsumableItems();
            //string FunctionName = "";

            // Curse you Perry the Platypus - Moving this under the class so it can be used in all functions under this class, causes the entire game to have a mental breakdown.
            BattleSetup battleSetup = new BattleSetup();

            /*
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(FunctionName, BindingFlags.NonPublic | BindingFlags.Instance);
            methodInfo.Invoke(this, null);
            */

            // Clears the console
            ClearConsole();

            Console.Title = "Start of the Game - Location: The House";
            Console.ForegroundColor = ConsoleColor.Yellow;
            AnimatedTextPrintout(Console.Title + "\n", PlayerDefinedTypingSpeed);
            
            // Background Sound
            SoundPlayer MainGameInitBackgroundPlayer = new SoundPlayer("lit-fireplace.wav");
            MainGameInitBackgroundPlayer.Load();
            MainGameInitBackgroundPlayer.PlayLooping();

            Console.ForegroundColor = ConsoleColor.White;
            //AnsiConsole.MarkupLine("\nPress Enter to Continue.\n");
            //Console.WriteLine("\nPress Enter to continue.\n");
            //Console.ReadKey();

            player.DisplayPlayerStats();

            /*
            //      Old Code        //
            Console.WriteLine("You are currently in Your House, what do you want to do?" +
            "\n\n1 - Check the Storage Box[?]" +
            "\n\n2 - Turn off the TV and Lights, then leave the House." +
            "\n\n3 - Exit the house." +
            "\n\n4 - Change Weapons [WIP]." +
            "\n\n5 - Use Items [WIP].");
            */


            //      New Code        //
            string SelectionValue = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Choose an [blue]option[/] below.")
            .PageSize(5)
            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
            .AddChoices(new[] {"Check the Storage Box for Items.", "Turn off the TV and Lights, then leave the House.", "Exit the House.", "Jump locations."}));


            switch (SelectionValue)
            {
                case "Check the Storage Box for Items.":
                    randomisedRewards.StorageBox1Rewards();
                    break;

                case "Turn off the TV and Lights, then leave the House.":
                    player.PlayerDef -= 1;
                    AnimatedTextPrintout("Turning off the TV and Lights has made you anxious and nervous, reducing your DEF by 1.",PlayerDefinedTypingSpeed);
                    Console.ReadKey();
                    MainGameInitBackgroundPlayer.Stop();
                    MainGameInitBackgroundPlayer.Dispose();
                    OutSide1A();
                    break;

                case "Exit the House.":
                    MainGameInitBackgroundPlayer.Stop();
                    MainGameInitBackgroundPlayer.Dispose();
                    OutSide1A();
                    break;

                    /*
                case "Jump locations.": //- Can't let anyone cheat through the game.
                    JumpLocations();
                    break;
                
                    case "Change Weapons.":
                        Weapons.DisplayWeapons();
                        break;

                    case "Use Items.":
                        consumableItems.UseConsumableItem();
                        break;
                    */
            }

            /*
            void JumpLocations()
            {
                //      New Code        //
                string LocationSelection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Choose an [blue]option[/] below.")
                .PageSize(5)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(new[] { "OutSide1A", "OutSide2A", "OutSide2B", "OutSide3A", "OutSide3B", "OutSide3C", "OutSide4A", "OutSide4B", "OutSide4C", "AbandonedBuilding1A", "OutSide5AFinale" }));
            
                switch (LocationSelection)
                {
                    case "OutSide1A":
                        OutSide1A();
                        break;

                    case "OutSide2A":
                        OutSide2A();
                        break;

                    case "OutSide2B":
                        OutSide2B();
                        break;

                    case "OutSide3A":
                        OutSide3A();
                        break;

                    case "OutSide3B":
                        OutSide3B();    
                        break;

                    case "OutSide3C":
                        OutSide3C();
                        break;

                    case "OutSide4A":
                        OutSide4A();
                        break;

                    case "OutSide4B":
                        OutSide4B();
                        break;

                    case "OutSide4C":
                        OutSide4C();
                        break;

                    case "AbandonedBuilding1A":
                        AbandonedBuilding1A();
                        break;

                    case "OutSide5AFinale":
                        OutSide5AFinale();
                        break;
                }
            }*/

            /*try
            {
                int decision0A = Convert.ToInt16(Console.ReadLine());

                switch (decision0A)
                {
                    case 1:
                        randomisedRewards.StorageBox1Rewards();
                        break;

                    case 2:
                        player.PlayerDef -= 1;
                        break;

                    case 3:
                        MainGameInitBackgroundPlayer.Stop();
                        MainGameInitBackgroundPlayer.Dispose();
                        OutSide1A();
                        break;

                    case 4:
                        Weapons.EquipWeapon();
                        break;

                    case 5:
                        consumableItems.DisplayConsumableItem();
                        break;
                }
            }

            catch (FormatException ExceptionInfo)
            {
                Console.WriteLine("Invalid input detected. Please try again.\n{0}", ExceptionInfo);
                ClearConsole();
            }*/

        }

        //          LOCATION FUNCTIONS           //

        public void OutSide1A()
        {
            BattleSetup battleSetup = new BattleSetup();

            ClearConsole();

            Console.ForegroundColor = ConsoleColor.Blue;

            /*battleSetup.EngagedWithWolf = true;
            
            if (battleSetup.EngagedWithWolf == true)
            {
                battleSetup.EnemyIsAliveBattle = true;
            }*/

            EnemyEncountered = "Wolf";
            battleSetup.EnemyName = EnemyEncountered;

            // Checking to see if EnemyEncountered has the correct value.
            //Console.WriteLine(EnemyEncountered);


            //if (battleSetup.EnemyIsAliveBattle == true && battleSetup.EngagedWithWolf == true)
            
            if (EnemyEncountered == "Wolf")
            {
                SoundPlayer BushSoundPlayer = new SoundPlayer("bushmovement.wav");
                SoundPlayer WolfHowl = new SoundPlayer("wolf-howl.wav");

                Console.WriteLine("Hold on...");
                
                BushSoundPlayer.Load();
                BushSoundPlayer.PlayLooping();
                Console.ReadKey();
                Console.WriteLine("There seem to be something rustling in the bushes...\n\nThe rustling in the bushes, catches your attention and you have found out there was a {0} lurking in there!", EnemyEncountered);
                battleSetup.InitiateBattle = true;

                WolfHowl.Load();
                WolfHowl.Play();

                Console.WriteLine("Press Enter to initiate the battle!");

                // Sound Effect from <a href="https://pixabay.com/?utm_source=link-attribution&amp;utm_medium=referral&amp;utm_campaign=music&amp;utm_content=6310">Pixabay</a>

                Console.ReadLine();

                BushSoundPlayer.Stop();
                WolfHowl.Stop();
                WolfHowl.Dispose();
                battleSetup.InitialSetup();
            }

            if (battleSetup.EnemyIsAliveBattle == false)
            {
                SoundPlayer OutSide1ABackgroundPlayer = new SoundPlayer("forest-wind-and-birds.wav");

                player.PlayerExp += 15;

                player.LevelSystem();

                OutSide1ABackgroundPlayer.Load();
                OutSide1ABackgroundPlayer.PlayLooping();

                Console.Title = "Location: Outside of the House";

                player.DisplayPlayerStats();

                /*Console.WriteLine("\nCurrent Stats:\n{0} HP - Health Points\n{1} DEF Rating - Defence Rating\n{2} ATK Rating - Attack Rating\n\nCurrent Level: {3}\nCurrent EXP: {4}\n", player.PlayerHP, player.PlayerDef, player.PlayerAtk, player.PlayerLevel, player.PlayerExp); - not sure what I was taking that made me replicate this in other functions.*/

                Console.WriteLine("Current Location: {0},\n\nthere are two paths that you can take. Aside from that, there is nothing worth looking into within the area.\n", Console.Title);

                string SelectionValue = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Choose an [green]option[/] below.")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(new[] {"Move Right.", "Move Down."}));


                //int decision1A = Convert.ToInt16(Console.ReadLine());

                switch (SelectionValue)
                {
                    case "Move Right.":
                        OutSide1ABackgroundPlayer.Stop();
                        OutSide1ABackgroundPlayer.Dispose();

                        OutSide2A();
                        break;
                    case "Move Down.":
                        OutSide1ABackgroundPlayer.Stop();
                        OutSide1ABackgroundPlayer.Dispose();

                        OutSide2B();
                        break;
                }
            }
        }

        public void OutSide2A()
        {
            BattleSetup battleSetup = new BattleSetup();
            SoundPlayer OutSide2ABackgroundPlayer = new SoundPlayer("forest-wind-and-birds.wav");
            SoundPlayer MousePlayer = new SoundPlayer("mouse.wav");

            ClearConsole();

            Console.Title = "Location: Forest 2A";

            OutSide2ABackgroundPlayer.Load();
            OutSide2ABackgroundPlayer.PlayLooping();

            player.DisplayPlayerStats();

            /* Console.WriteLine("\nCurrent Stats:\n{0} HP - Health Points\n{1} DEF Rating - Defence Rating\n{2} ATK Rating - Attack Rating\n\nCurrent Level: {3}\nCurrent EXP: {4}\n", player.PlayerHP, player.PlayerDef, player.PlayerAtk, player.PlayerLevel, player.PlayerExp); - I need to go touch grass. */


            Console.WriteLine("{0}.\n\nThe shadows casted from the trees grows larger.", Console.Title);
            Console.ReadKey();


            if (GotCrossBow == false)
            {
                Console.WriteLine("\nAhead of the path, there seem to be some booby traps that has been laid out, by yourself, as means to hunt for savage animals that lurks within the forest.\n\nYou need something to clear the traps, so you can go through...\n\nYour only options are:");
                
                string SelectionValue = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                //.Title("Choose an [green]option[/] below.")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(new[] { "Go Back.", "Take a look around." }));

                //int decision2A = Convert.ToInt16(Console.ReadLine());

                switch (SelectionValue)
                {
                    case "Go Back.":
                        OutSide2ABackgroundPlayer.Stop();
                        OutSide1A();
                        break;

                    case "Take a look around.":
                        Console.WriteLine("You have found a scrap of paper with four numbers printed on it. You take it, as you feel like it will be useful later.\n\nAfter picking the paper up, you see a new path being formed in front of you, so you head on to the path.");
                        GotCombinationPaper = true;
                        Console.ReadKey();
                        OutSide2ABackgroundPlayer.Stop();
                        OutSide2B();
                        break;
                }
            }

            else if (GotCrossBow == true)
            {
                Console.WriteLine("You have the Crossbow, which can be used to clear the armed booby traps.\n\nHit Enter to use the Crossbow.");
                Console.ReadLine();
                Console.WriteLine("You have used to Crossbow on the bear traps, allowing you to go through the path.");
                System.Threading.Thread.Sleep(1000); // A nice implementation that commands the console to wait for a period of time, defined in milliseconds, before heading to the next line of code.
                Console.WriteLine("\nAs the way is now clear, the breeze starts getting cold and hits onto your face. The breeze sends shivers down your spine, decreasing your defence by 1.\n\nPress Enter to continue.");
                Console.ReadLine();

                Console.WriteLine("But...\nYou hear something sqweeky, but you cannot determine where the noise is coming from...");
                Console.ReadKey();

                OutSide2ABackgroundPlayer.Stop();

                EnemyEncountered = "Black Rats";
                battleSetup.EnemyName = EnemyEncountered;

                MousePlayer.Load();
                MousePlayer.Play();
                Thread.Sleep(750); // Let the mouse squeak before getting cut off from another sound being played.

                // Checking to see if EnemyEncountered has the correct value.
                Console.WriteLine(EnemyEncountered);


                //if (battleSetup.EnemyIsAliveBattle == true && battleSetup.EngagedWithWolf == true)

                if (EnemyEncountered == "Black Rats")
                {
                    Console.WriteLine("A swarm of {0} is surrounding you!\n\nBattle is initiating.", EnemyEncountered);
                    battleSetup.InitiateBattle = true;
                    MousePlayer.Stop();
                    MousePlayer.Dispose();
                    battleSetup.InitialSetup();
                }

                OutSide2ABackgroundPlayer.Play();

                player.PlayerExp += 10;

                player.LevelSystem();

                Thread.Sleep(750);

                OutSide3A();

            }
        }

        public void OutSide2B()
        {
            SoundPlayer OutSide2BBackgroundPlayer = new SoundPlayer("forest-wind-and-birds.wav");

            ClearConsole();

            Console.Title = "Location: Forest 2B";

            OutSide2BBackgroundPlayer.Load();
            OutSide2BBackgroundPlayer.PlayLooping();

            player.DisplayPlayerStats();

            /*Console.WriteLine("\nCurrent Stats:\n{0} HP - Health Points\n{1} DEF Rating - Defence Rating\n{2} ATK Rating - Attack Rating\n\nCurrent Level: {3}\nCurrent EXP: {4}\n", player.PlayerHP, player.PlayerDef, player.PlayerAtk, player.PlayerLevel, player.PlayerExp);*/

            Console.WriteLine("{0}.\n\nThe wind howls, the bushes rustles as they are being blown by the wind.", Console.Title);
            Console.ReadKey();


            Console.WriteLine("\nThe environment that surrounds you, gives you the chills - making you feel uneasy and jumpy.\n\nYou look around and see a small destructed crate.\n\nDo you want to investigate the crate?\n\n");

            string SelectionValue = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            //.Title("Choose an [green]option[/] below.")
            .PageSize(3)
            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
            .AddChoices(new[] { "Yes.", "No." }));

            //int decision2B = Convert.ToInt16(Console.ReadLine());

            switch (SelectionValue)
            {
                case "Yes.":

                    Console.WriteLine("\nYou found out that the crate is partially destroyed, but there is a firm lock in place, preventing you from discovering its contents.\nThe lock requires a combination of four numbers. Would you like to enter the numbers?\n\n");

                    string CrateDecision = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    //.Title("Choose an [green]option[/] below.")
                    .PageSize(3)
                    .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices(new[] { "Yes.", "No." }));

                    //int decision2BLock = Convert.ToInt16(Console.ReadLine());

                    if (CrateDecision == "Yes." && GotCombinationPaper == false)
                    {
                        // Make the player go crazy by changing the combination if the player does not have the combination paper. Or in the future I can randomise the combination of the puzzle solution to be more mean.
                        int decision2BUnlockNum1 = 4;
                        int decision2BUnlockNum2 = 1;
                        int decision2BUnlockNum3 = 0;
                        int decision2BUnlockNum4 = 9;

                        Console.WriteLine("\nPlease enter the first number.");
                        int decision2BUnlockNumInput1 = Convert.ToInt16(Console.ReadLine());

                        if (decision2BUnlockNumInput1 == decision2BUnlockNum1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green; // Dictates the input is correct.
                            Console.WriteLine(decision2BUnlockNumInput1);
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        else if (decision2BUnlockNumInput1 != decision2BUnlockNum1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red; // Dictates the input is incorrect. 
                            Console.WriteLine(decision2BUnlockNumInput1 + " - WRONG!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        Console.WriteLine("\nPlease enter the second number.");
                        int decision2BUnlockNumInput2 = Convert.ToInt16(Console.ReadLine());

                        if (decision2BUnlockNumInput2 == decision2BUnlockNum2)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(decision2BUnlockNumInput2);
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        else if (decision2BUnlockNumInput2 != decision2BUnlockNum2)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(decision2BUnlockNumInput2 + " - WRONG!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        Console.WriteLine("\nPlease enter the third number.");
                        int decision2BUnlockNumInput3 = Convert.ToInt16(Console.ReadLine());

                        if (decision2BUnlockNumInput3 == decision2BUnlockNum3)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(decision2BUnlockNumInput3);
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        else if (decision2BUnlockNumInput3 != decision2BUnlockNum3)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(decision2BUnlockNumInput3 + " - WRONG!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        Console.WriteLine("\nPlease enter the fourth number.");
                        int decision2BUnlockNumInput4 = Convert.ToInt16(Console.ReadLine());

                        if (decision2BUnlockNumInput4 == decision2BUnlockNum4)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(decision2BUnlockNumInput4);
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        else if (decision2BUnlockNumInput4 != decision2BUnlockNum4)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(decision2BUnlockNumInput4 + " - WRONG!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        if (decision2BUnlockNumInput1 == decision2BUnlockNum1 && decision2BUnlockNumInput2 == decision2BUnlockNum2 && decision2BUnlockNumInput3 == decision2BUnlockNum3 && decision2BUnlockNumInput4 == decision2BUnlockNum4)
                        {
                            // Add line of code to add a wooden crossbow to deal with the booby traps
                            GotCrossBow = true;
                            Console.WriteLine("The lock on the crate has been disengaged. You have found a Crossbow inside of the crate, with few arrows.\n\n This will help you to proceed on your journey.");
                            Console.WriteLine("Press Enter to head on back.");
                            Console.ReadLine();
                            OutSide2A();
                        }

                        else
                        {
                            Console.WriteLine("The combination was wrong. Please try again.");
                            goto case "Yes.";
                        }

                    }

                    else if (CrateDecision == "Yes." && GotCombinationPaper == true)
                    {
                        int decision2BUnlockNum1 = 5;
                        int decision2BUnlockNum2 = 8;
                        int decision2BUnlockNum3 = 1;
                        int decision2BUnlockNum4 = 4;

                        Console.WriteLine("\nOn the piece of paper you have picked, there are four numbers smudged on the paper, these are:\n\n4, 8, 5, 1\n\nFrom the looks of the paper, it seems like these numbers have been re-written several times in different order...");

                        System.Threading.Thread.Sleep(1000);

                        Console.WriteLine("\nPlease enter the first number.");
                        int decision2BUnlockNumInput1 = Convert.ToInt16(Console.ReadLine());

                        if (decision2BUnlockNumInput1 == decision2BUnlockNum1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(decision2BUnlockNumInput1);
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        else if (decision2BUnlockNumInput1 != decision2BUnlockNum1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(decision2BUnlockNumInput1 + " - WRONG!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        Console.WriteLine("\nPlease enter the second number.");
                        int decision2BUnlockNumInput2 = Convert.ToInt16(Console.ReadLine());

                        if (decision2BUnlockNumInput2 == decision2BUnlockNum2)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(decision2BUnlockNumInput2);
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        else if (decision2BUnlockNumInput2 != decision2BUnlockNum2)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(decision2BUnlockNumInput2 + " - WRONG!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        Console.WriteLine("\nPlease enter the third number.");
                        int decision2BUnlockNumInput3 = Convert.ToInt16(Console.ReadLine());

                        if (decision2BUnlockNumInput3 == decision2BUnlockNum3)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(decision2BUnlockNumInput3);
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        else if (decision2BUnlockNumInput3 != decision2BUnlockNum3)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(decision2BUnlockNumInput3 + " - WRONG!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        Console.WriteLine("\nPlease enter the fourth number.");
                        int decision2BUnlockNumInput4 = Convert.ToInt16(Console.ReadLine());

                        if (decision2BUnlockNumInput4 == decision2BUnlockNum4)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(decision2BUnlockNumInput4);
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        else if (decision2BUnlockNumInput4 != decision2BUnlockNum4)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(decision2BUnlockNumInput4 + " - WRONG!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        if (decision2BUnlockNumInput1 == decision2BUnlockNum1 && decision2BUnlockNumInput2 == decision2BUnlockNum2 && decision2BUnlockNumInput3 == decision2BUnlockNum3 && decision2BUnlockNumInput4 == decision2BUnlockNum4)
                        {
                            // Add line of code to add a wooden crossbow to deal with the booby traps
                            GotCrossBow = true;
                            Console.WriteLine("\nThe lock on the crate has been disengaged. You have found a Crossbow inside of the crate, with some arrows.\n\nThis will help you to proceed on your journey.");
                            Console.WriteLine("Press Enter to head on back.");
                            Console.ReadLine();
                            OutSide2BBackgroundPlayer.Stop();
                            OutSide2BBackgroundPlayer.Dispose();
                            OutSide2A();
                        }

                        else
                        {
                            Console.WriteLine("The combination was wrong. Please try again.");
                            ClearConsole();
                            goto case "Yes.";
                        }
                    }

                    if (CrateDecision == "No.")
                    {
                        goto case "No.";
                    }

                    break;

                case "No.":
                    Console.WriteLine("Would you like to go back to the previous area?");

                    string DeclinedCrateDecision = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    //.Title("Choose an [green]option[/] below.")
                    .PageSize(3)
                    .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices(new[] { "Yes.", "No." }));

                    //int decision2BGoBack = Convert.ToInt16(Console.ReadLine());

                    switch (DeclinedCrateDecision)
                    {
                        case "Yes.":
                            OutSide2BBackgroundPlayer.Stop();
                            OutSide2BBackgroundPlayer.Dispose();

                            OutSide2A();
                            break;

                        case "No.":
                            OutSide2BBackgroundPlayer.Stop();
                            OutSide2BBackgroundPlayer.Dispose();

                            OutSide2B();
                            break;
                    }
                    break;
            }
        }

        public void OutSide3A()
        {
            SoundPlayer OutSide3ABackgroundPlayer = new SoundPlayer("howling-wind.wav");

            ClearConsole();

            Console.Title = "Location: Forest 3A";

            OutSide3ABackgroundPlayer.Load();
            OutSide3ABackgroundPlayer.PlayLooping();

            player.DisplayPlayerStats();

            /*Console.WriteLine("\nCurrent Stats:\n{0} HP - Health Points\n{1} DEF Rating - Defence Rating\n{2} ATK Rating - Attack Rating\n\nCurrent Level: {3}\nCurrent EXP: {4}\n", player.PlayerHP, player.PlayerDef, player.PlayerAtk, player.PlayerLevel, player.PlayerExp);*/

            Console.WriteLine("{0}\n\nThere is three different paths that you could have taken from here. Except the path in front of you has mysteriously been blocked by foliage and tree trunks.\n\nWhich path do you want to take?\n\nNOTE: THERE IS NO GOING BACK ONCE YOU HAVE TAKEN A PATH.\n", Console.Title);

            string SelectionValue = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            //.Title("Choose an [green]option[/] below.")
            .PageSize(3)
            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
            .AddChoices(new[] { "Take Path 1.", "Take Path 2." }));

            //int decision3A = Convert.ToInt16(Console.ReadLine());

            switch (SelectionValue)
            {
                case "Take Path 1.":
                    OutSide3ABackgroundPlayer.Stop();
                    OutSide3ABackgroundPlayer.Dispose();

                    OutSide3B();
                    break;

                case "Take Path 2.":
                    OutSide3ABackgroundPlayer.Stop();
                    OutSide3ABackgroundPlayer.Dispose();

                    OutSide3C();
                    break;
            }
        }

        public void OutSide3B()
        {
            SoundPlayer OutSide3BBackgroundPlayer = new SoundPlayer("epic-storm-thunder-rainwindwaves.wav");
            SoundPlayer BearPlayer = new SoundPlayer("bear-growl.wav");
            BattleSetup battleSetup = new BattleSetup();

            ClearConsole();

            Console.Title = "Location: Forest 3B [Path 1]";

            OutSide3BBackgroundPlayer.Load();
            OutSide3BBackgroundPlayer.PlayLooping();

            /*Console.WriteLine("\nCurrent Stats:\n{0} HP - Health Points\n{1} DEF Rating - Defence Rating\n{2} ATK Rating - Attack Rating\n\nCurrent Level: {3}\nCurrent EXP: {4}\n", player.PlayerHP, player.PlayerDef, player.PlayerAtk, player.PlayerLevel, player.PlayerExp);*/

            player.DisplayPlayerStats();

            Console.WriteLine("{0}\n\nThe rain has started to pour in, along with occasional lightning strikes in the distance.\nYou should try to get through the forest quickly.\n\nThe path that lies ahead seems to be sealed shut by a chained fence.", Console.Title);
            Console.ReadKey();
            Console.WriteLine("\n\nThere is an unidentified enemy that is surrounding the fence. Their attention is being caught by something.\n\nYou need to check it out, but first you need to defeat the enemies...\n");
            // Insert function to initiate a battle here. Boss Fight 1
            OutSide3BBackgroundPlayer.Stop();


            EnemyEncountered = "Grizzly Bear";
            battleSetup.EnemyName = EnemyEncountered;

            BearPlayer.Load();
            BearPlayer.Play();

            Thread.Sleep(1000);

            // Checking to see if EnemyEncountered has the correct value.
            //Console.WriteLine(EnemyEncountered);


            //if (battleSetup.EnemyIsAliveBattle == true && battleSetup.EngagedWithWolf == true)

            if (EnemyEncountered == "Grizzly Bear")
            {
                Console.WriteLine("Turns out the unidentified enemy was a {0}, who has detected you and have been provoked to take the offensive!", EnemyEncountered);
                battleSetup.InitiateBattle = true;
                BearPlayer.Stop();
                BearPlayer.Dispose();
                //OutSide3BBackgroundPlayer.Stop();
                battleSetup.InitialSetup();
            }

            if (battleSetup.InitiateBattle == false)
            {
                OutSide3BBackgroundPlayer.PlayLooping();

                player.PlayerExp += 62;

                player.LevelSystem();

                OutSide3BBackgroundPlayer.PlayLooping();
                Console.WriteLine("After defeating the enemies, you have discovered that the enemies were interested in a key with an awful foul odor.\n\nThe Key unlocks the gate and you proceed forward.");
                Console.ReadLine();

                OutSide3BBackgroundPlayer.Stop();
                OutSide3BBackgroundPlayer.Dispose();

                OutSide4A();
            }
        }

        public void OutSide3C()
        {
            SoundPlayer OutSide3CBackgroundPlayer = new SoundPlayer("epic-storm-thunder-rainwindwaves.wav");
            SoundPlayer BearGrowlPlayer = new SoundPlayer("bear-growl.wav");
            BattleSetup battleSetup = new BattleSetup();

            ClearConsole();

            OutSide3CBackgroundPlayer.Load();
            OutSide3CBackgroundPlayer.PlayLooping();

            Console.Title = "Location: Forest 3C [Path 2]";

            /*Console.WriteLine("\nCurrent Stats:\n{0} HP - Health Points\n{1} DEF Rating - Defence Rating\n{2} ATK Rating - Attack Rating\n\nCurrent Level: {3}\nCurrent EXP: {4}\n", player.PlayerHP, player.PlayerDef, player.PlayerAtk, player.PlayerLevel, player.PlayerExp);*/

            player.DisplayPlayerStats();

            Console.WriteLine("{0}\n\nIn front of you, lies a Brown Bear, who is unaware of your presence and currently is asleep.", Console.Title);
            
            Console.ReadKey();

            Console.WriteLine("\n\nThe gate is obstructed by the Brown Bear. In this instance, you have two choices.\n\n");

            string BearDecision = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            //.Title("Choose an [green]option[/] below.")
            .PageSize(3)
            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
            .AddChoices(new[] { "Fight the Brown Bear.", "Attempt to sneak past the Brown Bear." }));

            //string decision3C = Console.ReadLine();

            switch (BearDecision)
            {
                case "Fight the Brown Bear.":
                    OutSide3CBackgroundPlayer.Stop();

                    BearGrowlPlayer.Load();
                    BearGrowlPlayer.Play();

                    Thread.Sleep(1000);

                    BearGrowlPlayer.Dispose();

                    EnemyEncountered = "Brown Bear";
                    battleSetup.EnemyName = EnemyEncountered;

                    // Checking to see if EnemyEncountered has the correct value.
                    //Console.WriteLine(EnemyEncountered);


                    //if (battleSetup.EnemyIsAliveBattle == true && battleSetup.EngagedWithWolf == true)
                    // Initiate fight here.
                    if (EnemyEncountered == "Brown Bear")
                    {
                        Console.WriteLine("A {0} has detected you and has been provoked to take the offensive!", EnemyEncountered);

                        Console.WriteLine("Press Enter to continue.");
                        Console.ReadLine();

                        battleSetup.InitiateBattle = true;
                        battleSetup.InitialSetup();
                    }

                    if (battleSetup.InitiateBattle == false)
                    {
                        player.PlayerExp += 72;

                        player.LevelSystem();

                        OutSide3CBackgroundPlayer.PlayLooping();
                        Console.WriteLine("You have defeated the bear, but ran out of breath.");
                        System.Threading.Thread.Sleep(1000); // A nice implementation that commands the console to wait for a period of time, defined in milliseconds, before heading to the next line of code.
                        Console.WriteLine("After catching a short breath, you have proceeded to move forward.");
                        Console.ReadKey();
                        OutSide3CBackgroundPlayer.Stop();
                        OutSide3CBackgroundPlayer.Dispose();
                        OutSide4A();
                    }
                    break;

                case "Attempt to sneak past the Brown Bear.":
                    Random SneakRND = new Random();
                    int SneakValue = SneakRND.Next(1, 2);

                    if (SneakValue == 1)
                    {
                        AnsiConsole.WriteLine("You have successfully sneaked past the Brown Bear without making any noise!");
                        Thread.Sleep(750);
                        OutSide3CBackgroundPlayer.Stop();
                        OutSide3CBackgroundPlayer.Dispose();
                        OutSide4A();
                    }

                    if (SneakValue == 2)
                    {
                        EnemyEncountered = "Brown Bear";
                        battleSetup.EnemyName = EnemyEncountered;

                        // Checking to see if EnemyEncountered has the correct value.
                        //Console.WriteLine(EnemyEncountered);

                        BearGrowlPlayer.Load();
                        BearGrowlPlayer.Play();

                        Thread.Sleep(1000);

                        BearGrowlPlayer.Dispose();

                        //if (battleSetup.EnemyIsAliveBattle == true && battleSetup.EngagedWithWolf == true)
                        // Initiate fight here.
                        if (EnemyEncountered == "Brown Bear")
                        {
                            Console.WriteLine("Your footsteps has produced audible noise that have woken up the {0}! You have no choice other than to defeat the {0} to advance through the forest!", EnemyEncountered);
                            battleSetup.InitiateBattle = true;
                            OutSide3CBackgroundPlayer.Stop();
                            battleSetup.InitialSetup();
                        }

                        if (battleSetup.InitiateBattle == false)
                        {
                            OutSide3CBackgroundPlayer.PlayLooping();

                            player.PlayerExp += 72;

                            player.LevelSystem();

                            Console.WriteLine("You have defeated the bear and proceed to move forward.");
                            System.Threading.Thread.Sleep(750); // A nice implementation that commands the console to wait for a period of time, defined in milliseconds, before heading to the next line of code.
                            OutSide3CBackgroundPlayer.Stop();
                            OutSide3CBackgroundPlayer.Dispose();
                            OutSide4A();
                        }
                    }
                    break;
            }
        }

        public void OutSide4A()
        {
            SoundPlayer OutSide4ABackgroundPlayer = new SoundPlayer("epic-storm-thunder-rainwindwaves.wav");

            ClearConsole();

            Console.Title = "Location: Forest 4A";

            OutSide4ABackgroundPlayer.Load();
            OutSide4ABackgroundPlayer.PlayLooping();

            player.DisplayPlayerStats();

            if (GotPuzzlePaper1 == false && GotPuzzlePaper2 == false && GotPuzzlePaper3 == false)
            {
                Console.WriteLine("{0}\n\nThere seems to be another locked gate, but this time with barbed wires on top.\n\nThe barbed wires above the gate is preventing you from jumping over the gate as an alternative solution.\n\nThere are three rusty statues in front of you, which can be rotated. There are three faces etched to the sides of the statue, representing different animals.\n\nWhat would you like to do?\n\n", Console.Title);

                string SelectionValue = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                //.Title("Choose an [green]option[/] below.")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(new[] { "Look around for clues.", "Go to the path on the left.", "Go to the path on the right."}));

                //int decision4A = Convert.ToInt16(Console.ReadLine());

                switch (SelectionValue)
                {
                    case "Look around for clues.":
                        GotPuzzlePaper1 = true;
                        Console.WriteLine("You have found the first paper that shows a clue to the puzzle!");
                        Thread.Sleep(750);
                        Console.WriteLine("Where do you want to go now?");

                        string MenuDecision = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        //.Title("Choose an [green]option[/] below.")
                        .PageSize(3)
                        .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                        .AddChoices(new[] { "Go to the path on the left.", "Go to the path on the right." }));

                        switch (MenuDecision)
                        {
                            case "Go to the path on the left.":
                                Console.WriteLine("Moving to the Location: Forest 4B...");
                                System.Threading.Thread.Sleep(1000);
                                OutSide4ABackgroundPlayer.Stop();
                                OutSide4B();
                                break;

                            case "Go to the path on the right.":
                                Console.WriteLine("Moving to the Location: Forest 4C...");
                                System.Threading.Thread.Sleep(1000);
                                OutSide4ABackgroundPlayer.Stop();
                                OutSide4C();
                                break;
                        }
                        break;

                    case "Go to the path on the left.":
                        Console.WriteLine("Moving to the Location: Forest 4B...");
                        System.Threading.Thread.Sleep(1000);
                        OutSide4ABackgroundPlayer.Stop();
                        OutSide4B();
                        break;

                    case "Go to the path on the right.":
                        Console.WriteLine("Moving to the Location: Forest 4C...");
                        System.Threading.Thread.Sleep(1000);
                        OutSide4ABackgroundPlayer.Stop();
                        OutSide4C();
                        break;
                }
            }

            else if (GotPuzzlePaper1 == true && GotPuzzlePaper2 == false || GotPuzzlePaper3 == false)
            {
                Console.WriteLine("{0}\n\nThere seems to be another locked gate, but this time with barbed wires on top.\n\nThe barbed wires above the gate is preventing you from jumping over the gate as an alternative solution.\n\nThere are three rusty statues in front of you, which can be rotated. There are three faces etched to the sides of the statue, representing different animals.\n\nWhat would you like to do?\n\n", Console.Title);

                string SelectionValue = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                //.Title("Choose an [green]option[/] below.")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(new[] { "Go to the path on the left.", "Go to the path on the right." }));

                switch (SelectionValue)
                {
                    case "Go to the path on the left.":
                        Console.WriteLine("Moving to the Location: Forest 4B...");
                        System.Threading.Thread.Sleep(1000);
                        OutSide4ABackgroundPlayer.Stop();
                        OutSide4B();
                        break;

                    case "Go to the path on the right.":
                        Console.WriteLine("Moving to the Location: Forest 4C...");
                        System.Threading.Thread.Sleep(1000);
                        OutSide4ABackgroundPlayer.Stop();
                        OutSide4C();
                        break;
                }    
            }

            if (GotPuzzlePaper1 == true && GotPuzzlePaper2 == true && GotPuzzlePaper3 == true)
            {
                Console.WriteLine("You have found all of the pieces of paper that will help you with solving the puzzle.");
                System.Threading.Thread.Sleep(750);

                OutSide4APuzzle();

                void OutSide4APuzzle()
                {
                    Console.Write("The paper you have gathered suggests that the solution to the puzzle is: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("{0}, {1}, {2}", PuzzlePaper1Solution, PuzzlePaper2Solution, PuzzlePaper3Solution);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine("Would you like to solve the puzzle so you can proceed through the gate? Yes or No?");

                    string PuzzleDecision = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    //.Title("Choose an [green]option[/] below.")
                    .PageSize(3)
                    .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices(new[] { "Yes.", "No."}));

                    //string OutSide4APuzzleDecision = Console.ReadLine();

                    switch (PuzzleDecision)
                    {
                        case "Yes.":
                            ClearConsole();

                            Console.Write("Remember; the solution of the puzzle is: ");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("{0}, {1}, {2}", PuzzlePaper1Solution, PuzzlePaper2Solution, PuzzlePaper3Solution);
                            Console.WriteLine();
                            
                            Console.ForegroundColor = ConsoleColor.White;

                            System.Threading.Thread.Sleep(750);

                            Console.WriteLine("Enter in the first solution to the puzzle.\n");

                            string PuzzleInput1 = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            //.Title("Choose an [green]option[/] below.")
                            .PageSize(3)
                            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                            .AddChoices(new[] { "Bear.", "Eagle.", "Snake." }));

                            //int OutSide4APuzzleInput1 = Convert.ToInt16(Console.ReadLine());

                            switch (PuzzleInput1)
                            {
                                case "Bear.":
                                    Console.Write("You have entered: ");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("Bear.");
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;

                                case "Eagle.":
                                    Console.Write("You have entered: ");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("Eagle.");
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;

                                case "Snake.":
                                    Console.Write("You have entered: ");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("Snake.");
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                            }

                            Console.WriteLine("Enter in the second solution to the puzzle.\n");

                            string PuzzleInput2 = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            //.Title("Choose an [green]option[/] below.")
                            .PageSize(3)
                            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                            .AddChoices(new[] { "Bear.", "Eagle.", "Snake." }));

                            //int OutSide4APuzzleInput2 = Convert.ToInt16(Console.ReadLine());

                            switch (PuzzleInput2)
                            {
                                case "Bear.":
                                    Console.Write("You have entered: ");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("Bear.");
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;

                                case "Eagle.":
                                    Console.Write("You have entered: ");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("Eagle.");
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;

                                case "Snake.":
                                    Console.Write("You have entered: ");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("Snake.");
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                            }

                            Console.WriteLine("Enter in the third solution to the puzzle.\n");

                            string PuzzleInput3 = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            //.Title("Choose an [green]option[/] below.")
                            .PageSize(3)
                            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                            .AddChoices(new[] { "Bear.", "Eagle.", "Snake." }));

                            //int OutSide4APuzzleInput3 = Convert.ToInt16(Console.ReadLine());

                            switch (PuzzleInput3)
                            {
                                case "Bear.":
                                    Console.Write("You have entered: ");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("Bear.");
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;

                                case "Eagle.":
                                    Console.Write("You have entered: ");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("Eagle.");
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;

                                case "Snake.":
                                    Console.Write("You have entered: ");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("Snake.");
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                            }



                            //      PUZZLE COMPLETED OUTCOME!       //

                            if (PuzzleInput1 == PuzzlePaper1Solution && PuzzleInput2 == PuzzlePaper2Solution && PuzzleInput3 == PuzzlePaper3Solution)
                            {
                                Console.WriteLine("The lock on the gate has been disengaged, allowing you to go through to the FINAL AREA...");

                                OutSide4ABackgroundPlayer.Stop();
                                OutSide4ABackgroundPlayer.Dispose();

                                OutSide5AFinale();
                            }

                            break;
                            
                        case "No.":
                            Console.WriteLine("What else are you going to do? Go and solve the puzzle, you dummy. *Facepalm*");
                            goto case "Yes.";
                    }
                }
            }
        }

        public void OutSide4B()
        {
            SoundPlayer OutSide4BBackgroundPlayer = new SoundPlayer("epic-storm-thunder-rainwindwaves.wav");

            ClearConsole();

            Console.Title = "Location: Forest 4B";

            OutSide4BBackgroundPlayer.Load();
            OutSide4BBackgroundPlayer.PlayLooping();

            player.DisplayPlayerStats();

            if (GotPuzzlePaper2 == false)
            {
                Console.WriteLine("{0}\n\nUp ahead, there is a chest. Behind it and everywhere else, there is no path to be found.", Console.Title);

                System.Threading.Thread.Sleep(1000);

                Console.WriteLine("\n\nWould you like to investigate the chest?\n");

                string ChoiceValue = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                //.Title("Choose an [green]option[/] below.")
                .PageSize(3)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(new[] { "Yes.", "No." }));

                //int decision4B = Convert.ToInt16(Console.ReadLine());

                switch (ChoiceValue)
                {
                    case "Yes.":
                        GotPuzzlePaper2 = true;

                        Console.WriteLine("You found a paper containing a clue to the puzzle.");

                        Thread.Sleep(1000);

                        OutSide4BBackgroundPlayer.Stop();
                        OutSide4BBackgroundPlayer.Dispose();

                        OutSide4A();
                        // player finds a piece to the puzzle.
                        break;

                    case "No.":
                        Console.WriteLine("Since you hit No, you head on back to the previous location.");

                        Thread.Sleep(1000);

                        OutSide4BBackgroundPlayer.Stop();
                        OutSide4BBackgroundPlayer.Dispose();

                        OutSide4A();
                        // offer the player a choice to go back.
                        break;
                }
            }

            else if (GotPuzzlePaper2 == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("What are you doing back here? You have already collected this part of the solution to the puzzle!");
                Console.ForegroundColor = ConsoleColor.White;
                OutSide4A();
            }
        }

        public void OutSide4C()
        {
            SoundPlayer OutSide4CBackgroundPlayer = new SoundPlayer("epic-storm-thunder-rainwindwaves.wav");

            ClearConsole();

            Console.Title = "Location: Forest 4C";

            OutSide4CBackgroundPlayer.Load();
            OutSide4CBackgroundPlayer.PlayLooping();

            player.DisplayPlayerStats();

            if (GotPuzzlePaper3 == false)
            {
                Console.WriteLine("{0}\n\nAs you enter the area, you see a abandoned building, made of wood, painted white - while showing clear signs of the paint and the wood decaying...\n\n", Console.Title);
                Console.ReadKey();
                Console.WriteLine("There seem to be an accessible entrance that you can take, to get inside, which is where you go into.\n\nPress any key to go into the Abandoned Building.");
                Console.ReadKey();
                //Console.WriteLine("Entering the Abandoned Building...");
                //System.Threading.Thread.Sleep(1000);

                AnsiConsole.Status().Start("\n[white]Please wait for a moment...[/]", EnteringStatus =>
                {
                    // Simulate some work
                    AnsiConsole.MarkupLine("[white]Started making the way to the Abandoned Building...[/]");
                    Thread.Sleep(1250);

                    // Update the status and spinner
                    EnteringStatus.Status("[white]Walking...[/]");
                    EnteringStatus.Spinner(Spinner.Known.Star2);
                    EnteringStatus.SpinnerStyle(Style.Parse("green"));
                    Thread.Sleep(1000);

                    //AnsiConsole.MarkupLine("Done!");
                    EnteringStatus.Status("[green]Arrived![/]");
                    Thread.Sleep(450);
                });

                OutSide4CBackgroundPlayer.Stop();
                OutSide4CBackgroundPlayer.Dispose();

                AbandonedBuilding1A();
            }

            else if (GotPuzzlePaper3 == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("What are you doing back here? You have already collected this part of the solution to the puzzle!");
                Console.ForegroundColor = ConsoleColor.White;
                OutSide4A();
            }
        }

        public void AbandonedBuilding1A()
        {
            BattleSetup battleSetup = new BattleSetup();
            SoundPlayer AbandonedBuilding1ABackgroundPlayer = new SoundPlayer("wood-creaking.wav");
            SoundPlayer DoorSlamPlayer = new SoundPlayer("door-slam-sound-effect.wav");
            SoundPlayer BatPlayer = new SoundPlayer("bat-chirp-close-up.wav");

            ClearConsole();

            Console.Title = "Location: Abandoned Building 1A";

            player.DisplayPlayerStats();

            DoorSlamPlayer.Load();
            DoorSlamPlayer.Play();

            Thread.Sleep(1350);

            DoorSlamPlayer.Dispose();

            Console.WriteLine("{0}\n\nAs you entered the building, the door shuts behind you.", Console.Title);

            AbandonedBuilding1ABackgroundPlayer.Load();
            AbandonedBuilding1ABackgroundPlayer.PlayLooping();

            Console.ReadKey();
            Console.WriteLine("In front of you, there seems to be a piece of paper, that has been scrolled up. It might be helpful to take it with you.\n\nHit Enter to pick up the piece of paper.");
            Console.ReadLine();
            bool GotPaper3 = true;

            if (GotPaper3 == true)
            {
                Console.WriteLine("You got the piece of paper!");
                // Initiate boss fight
                System.Threading.Thread.Sleep(750);
                AbandonedBuilding1ABackgroundPlayer.Stop();

                BatPlayer.Load();
                BatPlayer.Play();
                Thread.Sleep(1000);

                AbandonedBuilding1ABackgroundPlayer.PlayLooping();

                Console.WriteLine("Out of no where, a Venomous Bat is engaging you into a battle!");

                Console.ReadKey();

                EnemyEncountered = "Venomous Bat";
                battleSetup.EnemyName = EnemyEncountered;



                // Checking to see if EnemyEncountered has the correct value.
                // Console.WriteLine(EnemyEncountered);

                // Initiate fight here.
                if (EnemyEncountered == "Venomous Bat")
                {
                    Console.WriteLine("A {0} has ensnared you into his trap! Initiating the battle...", EnemyEncountered);
                    battleSetup.InitiateBattle = true;

                    BatPlayer.Stop();
                    BatPlayer.Dispose();

                    AbandonedBuilding1ABackgroundPlayer.Stop();

                    battleSetup.InitialSetup();
                }

                if (battleSetup.InitiateBattle == false || battleSetup.EnemyHP < 0f)
                {
                    player.PlayerExp += 67;

                    player.LevelSystem();

                    AbandonedBuilding1ABackgroundPlayer.Play();

                    Console.WriteLine("There is nothing left for you to do here.\n\nHeading back...");
                    System.Threading.Thread.Sleep(1000);

                    AbandonedBuilding1ABackgroundPlayer.Stop();
                    AbandonedBuilding1ABackgroundPlayer.Dispose();

                    GotPuzzlePaper3 = true;

                    OutSide4A();
                }
            }
        }

        public void OutSide5AFinale()
        {
            SoundPlayer OutSide5AFinaleBackgroundPlayer = new SoundPlayer("howling-wind.wav");

            ClearConsole();

            Console.Title = "Location: The Telephone Booth, Exterior of the Forest - The Finale";

            OutSide5AFinaleBackgroundPlayer.Load();
            OutSide5AFinaleBackgroundPlayer.PlayLooping();

            player.DisplayPlayerStats();
            
            Console.WriteLine("After battling through the forest, you have finally made it to the destination - The Telephone Booth.");
            Console.ReadKey();
            Console.WriteLine("Straight away, you run to the booth, to contact for aid.\n\nYou got through the call and have been told that help is on its way to assist you!");

            Console.ReadKey();

            Console.WriteLine("Several hours has passed and many people has volunteered to help you and some constructors came to help you to fortify your house that will aid your house to survive for the worst case scenario.\n");

            Console.ReadKey();

            Console.WriteLine("In the near future, the storm has passed, with you and your house surviving. You take this moment to think about the past positively, along with coming to realisation that the events that has occurred in the past are long gone and to stop isolating yourself.\n\n" + "The End...");
    
            Console.ReadKey();

            Environment.Exit(0);
        }

        //          OTHER FUNCTIONS         //
        public static void ClearConsole()
        {
            /*
             * Aims to clear the console when the player initiates a fight or moves to another location. This stops the console from becoming cluttered.
             */


            // Old Code

            //Console.ForegroundColor = ConsoleColor.White;
            /*Console.WriteLine("Sweeping up the console, please wait...");
            System.Threading.Thread.Sleep(750);
            Console.Clear();*/

            // New Code

            AnsiConsole.Status().Start("\n[white]Please wait for a moment...[/]", LoadingStatus =>
            {
                // ctx is a local variable is creates a new instance of StatusContext.

                // Simulate some work
                AnsiConsole.MarkupLine("[white]Cleaning up and loading.[/]");
                Thread.Sleep(1250);

                // Update the status and spinner
                LoadingStatus.Status("[white]Now loading...[/]");
                LoadingStatus.Spinner(Spinner.Known.Star2);
                LoadingStatus.SpinnerStyle(Style.Parse("green"));
                Thread.Sleep(1000);

                //AnsiConsole.MarkupLine("Done!");
                LoadingStatus.Status("[green]Done![/]");
                Thread.Sleep(350);

                AnsiConsole.Clear();
                Console.Clear();
            });
        }

        public static void AnimatedTextPrintout(string PrintText, int TypingSpeed)
        {
            foreach (char Character in PrintText)
            {
                Console.Write(Character);
                System.Threading.Thread.Sleep(TypingSpeed);

                if (Char.IsWhiteSpace(Character))
                {
                    System.Threading.Thread.Sleep(TypingSpeed * 2);
                }
            }
            Console.WriteLine();
        }
    }

    class Player
    {
        public float PlayerMaxHP = 125f;
        public float PlayerHP = 125f;
        public float PlayerDef = 1.75f;
        public int PlayerAtk = 12;
        public float PlayerFear = 0;
        public float PlayerFearMax = 100;
        public float PlayerRage = 100;
        public float PlayerRageMax = 100;
        public int PlayerLevel = 1;
        public int PlayerExp = 0;
        public int PlayerExpReq = 0;
        //public int PlayerExpGained = 0;
        public bool PlayerIsAlive = true;
        public bool PlayerRageMode = false;
        public bool PlayerFearMode = false;

        public void LevelSystem()
        {
            switch (PlayerLevel)
            {
                case 1:
                    PlayerExpReq = 15;

                    if (PlayerExp >= PlayerExpReq)
                    {
                        PlayerLevel = 2;
                        Console.WriteLine("\nCongratulations for levelling up! You are now Level {0}!", PlayerLevel);
                        PlayerLevelStats();
                    }
                    break;

                case 2:
                    PlayerExpReq = 45;

                    if (PlayerExp >= PlayerExpReq)
                    {
                        PlayerLevel = 3;
                        Console.WriteLine("\nCongratulations for levelling up! You are now Level {0}!", PlayerLevel);
                    }
                    break;

                case 3:
                    PlayerExpReq = 70;

                    if (PlayerExp >= PlayerExpReq)
                    {
                        PlayerLevel = 4;
                        Console.WriteLine("\nCongratulations for levelling up! You are now Level {0}", PlayerLevel);
                    }
                    break;

                case 4:
                    PlayerExpReq = 150;

                    if (PlayerExp >= PlayerExpReq)
                    {
                        PlayerLevel = 5;
                        Console.WriteLine("\nCongratulations for levelling up! You are now Level {0}", PlayerLevel);
                    }
                    break;
            }
        }

        public void PlayerLevelStats()
        {
            if (PlayerLevel == 2)
            {
                PlayerMaxHP += 10f;
                PlayerHP = PlayerMaxHP;
                PlayerAtk += 1;
                PlayerDef += 0.2f;

                Console.WriteLine("\n\nYour HP has been increased by {0}.\nATK Rating has been increased by {1}.\nDEF Rating has been increased by {2}.\n", PlayerMaxHP, PlayerAtk, PlayerDef);
            }

            if (PlayerLevel == 3)
            {
                PlayerMaxHP += 11;
                PlayerHP = PlayerMaxHP;
                PlayerAtk += 1;
                PlayerDef += 0.21f;

                Console.WriteLine("\n\nYour HP has been increased by {0}.\nATK Rating has been increased by {1}.\nDEF Rating has been increased by {2}.\n", PlayerMaxHP, PlayerAtk, PlayerDef);
            }

            if (PlayerLevel == 3)
            {
                PlayerMaxHP += 15;
                PlayerHP = PlayerMaxHP;
                PlayerAtk += 2;
                PlayerDef += 0.15f;

                Console.WriteLine("\n\nYour HP has been increased by {0}.\nATK Rating has been increased by {1}.\nDEF Rating has been increased by {2}.\n", PlayerMaxHP, PlayerAtk, PlayerDef);
            }

            if (PlayerLevel == 4)
            {
                PlayerMaxHP += 16.25f;
                PlayerHP = PlayerMaxHP;
                PlayerAtk += 1;
                PlayerDef += 0.21f;

                Console.WriteLine("\n\nYour HP has been increased by {0}.\nATK Rating has been increased by {1}.\nDEF Rating has been increased by {2}.\n", PlayerMaxHP, PlayerAtk, PlayerDef);
            }
        }

        public void DisplayPlayerStats()
        {
            Grid PlayerStatsGrid = new Grid(); //.Alignment(Justify.Center) - Alignment doesn't seem to work.
            Rule Underline = new Rule("Your Stats").Centered();

            string PlayerHPString = PlayerHP.ToString();
            string PlayerDefString = PlayerDef.ToString();
            string PlayerAtkString = PlayerAtk.ToString();
            string PlayerLevelString = PlayerLevel.ToString();
            string PlayerExpString = PlayerExp.ToString() + " XP";

            /*------------------------------------------------Text------------------------------------------------*/

            // Text allows us to apply style where it cannot be possible in any other way in a specific use.
            Text PlayerHPHeading = new Text("HP [Health Points]", new Style(Color.Green)).Centered();
            Text PlayerDefHeading = new Text("DEF Rating [Defence]", new Style(Color.Blue)).Centered();
            Text PlayerATKHeading = new Text("ATK Rating [Attack]", new Style(Color.Red)).Centered();
            Text PlayerLevelHeading = new Text("Player Level", new Style(Color.SteelBlue1_1)).Centered();
            Text PlayerExpHeading = new Text("Accumulated EXP", new Style(Color.Orange1)).Centered();

            Text PlayerHPDisplay = new Text(PlayerHPString, new Style()).Centered();
            Text PlayerDefDisplay = new Text(PlayerDefString, new Style()).Centered();
            Text PlayerATKDisplay = new Text(PlayerAtkString, new Style()).Centered();
            Text PlayerLevelDisplay = new Text(PlayerLevelString, new Style()).Centered();
            Text PlayerExpDisplay = new Text(PlayerExpString, new Style()).Centered();

            /*-----------------------------------------------Padding----------------------------------------------*/

            Padder PlayerHPHeadingPadder = new Padder(PlayerHPHeading).PadLeft(8).PadRight(1).PadBottom(0).PadTop(0);
            Padder PlayerDefHeadingPadder = new Padder(PlayerDefHeading).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            Padder PlayerATKHeadingPadder = new Padder(PlayerATKHeading).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            Padder PlayerLevelHeadingPadder = new Padder(PlayerLevelHeading).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            Padder PlayerExpHeadingPadder = new Padder(PlayerExpHeading).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);

            Padder PlayerHPDisplayPadder = new Padder(PlayerHPDisplay).PadLeft(8).PadRight(1).PadBottom(0).PadTop(0);
            Padder PlayerDefDisplayPadder = new Padder(PlayerDefDisplay).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            Padder PlayerATKDisplayPadder = new Padder(PlayerATKDisplay).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            Padder PlayerLevelDisplayPadder = new Padder(PlayerLevelDisplay).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            Padder PlayerExpDisplayPadder = new Padder(PlayerExpDisplay).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);

            //string[] PlayerStatsArray = {PlayerHP.ToString(), PlayerDef.ToString(), PlayerAtk.ToString(),PlayerLevel.ToString()};

            //Console.WriteLine(PlayerStatsArray);


            Underline.Style = Style.Parse("blue");
            AnsiConsole.Write(Underline);

            AnsiConsole.WriteLine();

            PlayerStatsGrid.AddColumn();
            PlayerStatsGrid.AddColumn();
            PlayerStatsGrid.AddColumn();
            PlayerStatsGrid.AddColumn();
            PlayerStatsGrid.AddColumn();
            PlayerStatsGrid.AddRow(PlayerHPHeadingPadder, PlayerDefHeadingPadder, PlayerATKHeadingPadder, PlayerLevelHeadingPadder, PlayerExpHeadingPadder);

            PlayerStatsGrid.AddRow(PlayerHPDisplayPadder, PlayerDefDisplayPadder, PlayerATKDisplayPadder, PlayerLevelDisplayPadder, PlayerExpDisplayPadder);

            AnsiConsole.Write(PlayerStatsGrid);

            AnsiConsole.WriteLine();

            // Old Code
            // Stops me from having to continuously having to write the same line over again for every time the player moves location.
            //Console.WriteLine("\nCurrent Stats:\n{0} HP - Health Points\n{1} DEF Rating - Defence Rating\n{2} ATK Rating - Attack Rating\n\nCurrent Level: {3}\nCurrent EXP: {4}\n", PlayerHP, PlayerDef, PlayerAtk, PlayerLevel, PlayerExp);


        }

        //      AIMS TO PREVENT PLAYER FROM THE EXCEEDING MAX HP        //
        public void PlayerHPCheck()
        {
            if (PlayerHP > PlayerMaxHP)
            {
                PlayerHP = PlayerMaxHP;
            }
        }

        //      CONTAINS EVERYTHING THAT THE PLAYER HAS     //
        internal class PlayerInventory
        {
            internal class ConsumableItems
            {
                Player player = new Player();
                Grid ConsumableItemsInventory = new Grid();
                readonly Rule Underline = new Rule("Your Inventory").Centered();
                readonly SoundPlayer StomachRumbling = new SoundPlayer("stomach-rumble.wav");

                public int Chocolates = 4;  // Increases HP
                public int BottleOfWater = 2;   // Increases HP
                public int Coke = 1;    // Increases HP and ATK
                public int HolyTapWater = 0;    // Removes debuffs and slightly increases max HP

                public bool LactoseIntolerant = false;

                public void UseConsumableItem()
                {
                    string ChocolateQuantityString = Chocolates.ToString();
                    string BottleOfWaterQuantityString = BottleOfWater.ToString();
                    string CokeQuantityString = Coke.ToString();
                    string HolyTapWaterString = HolyTapWater.ToString();

                    /*-----------------------------------------------Padding----------------------------------------------*/

                    Padder ConsumableItemNameHeadingPadder = new Padder(new Text("Name").Centered()).PadLeft(15).PadRight(1).PadBottom(0).PadTop(0);
                    Padder ConsumableItemQuantityHeadingPadder = new Padder(new Text("Quantity").Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
                    Padder ConsumableItemDescriptionHeadingPadder = new Padder(new Text("Description").Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);

                    Padder ConsumableItemChocolateName = new Padder(new Text("Box of Cheap Chocolates", new Style(Color.Orange4_1)).Centered()).PadLeft(15).PadRight(1).PadBottom(0).PadTop(0);
                    Padder ConsumableItemChocolateQuantity = new Padder(new Text(ChocolateQuantityString).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
                    Padder ConsumableItemChocolateDescription = new Padder(new Text("Restores 50 HP [CHANCE OF TRIGGERING A BAD REACTION].").Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);

                    Padder ConsumableItemBottleOfWaterName = new Padder(new Text ("Bo'le of Wa'er", new Style(Color.DarkSlateGray1)).Centered()).PadLeft(15).PadRight(1).PadBottom(0).PadTop(1);
                    Padder ConsumableItemBottleOfWaterQuantity = new Padder(new Text(BottleOfWaterQuantityString).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(1);
                    Padder ConsumableItemBottleOfWaterDescription = new Padder(new Text("Restores 25 HP.").Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(1);

                    Padder ConsumableItemCokeName = new Padder(new Text("Can of Coke", new Style(Color.Red3)).Centered()).PadLeft(15).PadRight(1).PadBottom(0).PadTop(1);
                    Padder ConsumableItemCokeQuantity = new Padder(new Text(CokeQuantityString).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(1);
                    Padder ConsumableItemCokeDescription = new Padder(new Text("Restores 35 HP and Increases ATK by 3 permanently.").Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(1);

                    Padder ConsumableItemHolyTapWaterName = new Padder(new Text("Holy Tap Water", new Style(Color.PaleTurquoise1)).Centered()).PadLeft(15).PadRight(1).PadBottom(0).PadTop(1);
                    Padder ConsumableItemHolyTapWaterQuantity = new Padder(new Text(HolyTapWaterString).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(1);
                    Padder ConsumableItemHolyTapWaterDescription = new Padder(new Text("Increaes Max HP by 10 and Removes Debuffs.").Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(1);

                    //Console.WriteLine("\nHere are all of the consumable items that are at your disposal.\n\n- Chocolates: {0}       - Bo'le of Wa'er: {1}\n- Can of Coke: {2}       - Holy Tap Water: {3}", Chocolates, BottleOfWater, Coke, HolyTapWater);

                    ConsumableItemsInventory.AddColumn();
                    ConsumableItemsInventory.AddColumn();
                    ConsumableItemsInventory.AddColumn();
                    //ConsumableItemsInventory.AddColumn();

                    ConsumableItemsInventory.AddRow(ConsumableItemNameHeadingPadder, ConsumableItemQuantityHeadingPadder, ConsumableItemDescriptionHeadingPadder);
                    ConsumableItemsInventory.AddRow(ConsumableItemChocolateName, ConsumableItemChocolateQuantity, ConsumableItemChocolateDescription);
                    ConsumableItemsInventory.AddRow(ConsumableItemBottleOfWaterName, ConsumableItemBottleOfWaterQuantity, ConsumableItemBottleOfWaterDescription);
                    ConsumableItemsInventory.AddRow(ConsumableItemCokeName, ConsumableItemCokeQuantity, ConsumableItemCokeDescription);
                    ConsumableItemsInventory.AddRow(ConsumableItemHolyTapWaterName, ConsumableItemHolyTapWaterQuantity, ConsumableItemHolyTapWaterDescription);
                    Underline.Style = Style.Parse("blue");
                    AnsiConsole.Write(Underline);

                    AnsiConsole.WriteLine();

                    AnsiConsole.Write(ConsumableItemsInventory);

                    //      CREATES A SELECTION PROMPT      //
                    string MenuValue = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    //.Title("\nChoose an [green]option[/] below to continue.")
                    .PageSize(4)
                    //.MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices(new[] { "Box of Cheap Chocolates", "Bo'le of Wa'er", "Can of Coke", "Holy Tap Water" }));

                    System.Threading.Thread.Sleep(1000);


                    switch (MenuValue)
                    {
                        case "Box of Cheap Chocolates":

                            if (Chocolates > 0)
                            {
                                player.PlayerHP += 50f;
                                Chocolates--;
                                Random LactoseIntoleranceRND = new Random();
                                int LactoseIntoleranceRNG = LactoseIntoleranceRND.Next(0, 10);

                                Console.WriteLine(LactoseIntoleranceRNG);

                                if (LactoseIntoleranceRNG >= 5)
                                {
                                    LactoseIntolerant = true;

                                    if (LactoseIntolerant == true)
                                    {
                                        StomachRumbling.Load();
                                        StomachRumbling.Play();

                                        Console.WriteLine("Whatever you just ate, caused your stomach to rumble and hurt. Due to this, your ATK and HP has been decreased!");

                                        Console.ReadKey();

                                        Random LactoseEffectHP = new Random();
                                        int PlayerHPLactose = LactoseEffectHP.Next(1, 10);

                                        Random LactoseEffectAtk = new Random();
                                        int PlayerAtkLactose = LactoseEffectAtk.Next(1, 5);

                                        Console.Write("Your HP has been decreased by: ");
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("{0} ", PlayerHPLactose);
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("and decreased your ATK rating by ");
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("{0}", PlayerAtkLactose);
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write(".");
                                        Console.WriteLine();

                                        LactoseIntolerancePlayerStats(PlayerHPLactose, PlayerAtkLactose);

                                        void LactoseIntolerancePlayerStats(int LactoseCalcPlayerHP, int LactoseCalcPlayerAtk)
                                        {
                                            player.PlayerHP -= LactoseCalcPlayerHP;
                                            player.PlayerAtk -= LactoseCalcPlayerAtk;
                                        }

                                        Console.WriteLine("You now have {0} Box of Cheap Chocolates remaining.", Chocolates);

                                    }
                                }

                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Luckily, you have not suffered from side effects caused by the food that you have ate.");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }

                                player.PlayerHPCheck();
                                PlayerHealInfo();
                            }

                            else
                            {
                                Console.WriteLine("You do not have any of this item remaining to use...");
                            }
                            
                            break;

                        case "Bo'le of Wa'er":
                            
                            if (BottleOfWater > 0)
                            {
                                //Console.WriteLine("Drinking , makes you feel refreshed.\n");
                                BottleOfWater--;
                                player.PlayerHP += 25f;
                                player.PlayerHPCheck();
                                PlayerHealInfo();
                                
                            }

                            else
                            {
                                Console.WriteLine("You do not have any of this item remaining to use...");
                            }
                            break;

                        case "Can of Coke":

                            if (Coke > 0)
                            {
                                Coke--;
                                player.PlayerHP += 35f;
                                player.PlayerAtk += 3;
                                player.PlayerHPCheck();
                                PlayerHealInfo();
                            }

                            else
                            {
                                Console.WriteLine("You do not have any of this item remaining to use...");
                            }

                            break;

                        case "Holy Tap Water":

                            if (HolyTapWater > 0)
                            {
                                HolyTapWater--;
                                player.PlayerMaxHP += 10f;
                                player.PlayerHPCheck();
                                PlayerHealInfo();
                            }

                            else
                            {
                                Console.WriteLine("You do not have any of this item remaining to use...");
                            }
                            break;

                        // Does not exist
                        /*
                        case "The Elixer":

                            player.PlayerMaxHP += 25f;
                            player.PlayerHP += 50f;
                            player.PlayerDef += 1.5f;
                            player.PlayerHPCheck();
                            PlayerHealInfo();
                            break;
                        */
                    }

                    void PlayerHealInfo()
                    {
                        Console.Write("Your HP is now ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("{0}", player.PlayerHP);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(".");
                        Console.WriteLine();

                        if (MenuValue == "The Elixer")
                        {
                            Console.Write("All ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("debuffs ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("have been removed!");
                            Console.WriteLine();
                        }

                        if (MenuValue == "Can of Coke")
                        {
                            Console.Write("Your ATK has been increased by ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("3");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(".");
                            Console.WriteLine();
                        }
                    }

                    //UseConsumableItem();


                    /*void UseConsumableItem()
                    {
                        Console.WriteLine("Would you like to use an item?");

                        int ConsumableItemDecision = Convert.ToInt16(Console.ReadLine());

                        switch (ConsumableItemDecision)
                        {
                            case 1:
                                Console.WriteLine("\n\nYour HP is now: {0}.\n\nWhich item would you like to use?" +
                                    "\n1 - Chocolates : Restores 50 HP [CHANCE OF TRIGGERING A BAD REACTION]" +
                                    "\n2 - Bo'ale of Wa'er : Restores 25 HP" +
                                    "\n3 - Can of Coke : Restores 35 HP and Increases ATK Rating by 3 [Permanent Upgrade]." +
                                    "\n4 - Holy Tap Water : Increaes Max HP by 10 and Removes Debuffs." +
                                    "\n5 - The Elixer : Increases Max HP by 25, restores HP by 50 and increases DEF Rating by 1.5.", player.PlayerHP);

                                int ItemChosen = Convert.ToInt16(Console.ReadLine());

                                switch (ItemChosen)
                                {
                                    case 1:
                                        player.PlayerHP += 50f;

                                        Random LactoseIntoleranceRND = new Random();

                                        int LactoseIntoleranceRNG = LactoseIntoleranceRND.Next (1, 10);

                                        

                                        if (LactoseIntoleranceRNG >= 5)
                                        {
                                            LactoseIntolerant = true;
                                        }

                                        player.PlayerHPCheck();
                                        PlayerHealInfo();
                                        break;

                                    case 2:
                                        player.PlayerHP += 25f;
                                        player.PlayerHPCheck();
                                        PlayerHealInfo();
                                        break;

                                    case 3:
                                        player.PlayerHP += 35f;
                                        player.PlayerAtk += 3;
                                        player.PlayerHPCheck();
                                        PlayerHealInfo();
                                        break;

                                    case 4:
                                        player.PlayerMaxHP += 10f;
                                        break;

                                    case 5:
                                        player.PlayerMaxHP += 25f;
                                        player.PlayerHP += 50f;
                                        player.PlayerDef += 1.5f;
                                        player.PlayerHPCheck();
                                        PlayerHealInfo();
                                        break;
                                }
                                break;

                            case 2:

                                return;

                        }


                    }*/
                }
            }

            /*

            I'm not very articulate with Arrays, therefore, I will be avoiding them to avoid confusion.

            public string[] PlayerItems = { "Bat", "Plastic Bag" };

            public string[] ConsumableItems = { "Chocolates" };

            */

            // Player has lactose intolerance, causing him to randomly have loose stool when eating certain food, causing a temporary debuff.

            //      SETS ATTRIBUTES TO THE WEAPONS AND UNIQUE ID        //
            internal class PlayerWeapons
            {
                Player player = new Player();

                /*public string Weapon1 = "Wooden Bat";
                public string Weapon2 = "Steel Rusty Mace";
                public string Weapon3 = "Fidget Spinner";
                public string Weapon4 = "Magic Bouncy Stress Ball";*/
                string[] WeaponsArray = { "Wooden Bat", "Steel Rusty Mace", "Fidget Spinner", "Magic Bouncy Stress Ball" };

                public string WeaponEquipped = "Wooden Bat";
                public int WeaponAtk = 0; // Adds to PlayerAtk.
                public int WeaponDurability = 0; // May be used to add durability on weapons, that can be repaired using resources, e.g., Wood.


                public void DisplayWeapons()
                {
                    /*Console.WriteLine("These are the weapons that you have:" +
                        "\n{0}" +
                        "\n{1}" +
                        "\n{2}" +
                        "\n{3}", Weapon1, Weapon2, Weapon3, Weapon4);*/

                    Console.WriteLine(WeaponsArray[0] + " " + WeaponsArray[1] + " " + WeaponsArray[2] + " " + WeaponsArray[3]);

                    System.Threading.Thread.Sleep(750);

                    Console.WriteLine("What would you like to do now?\n");

                    //      CREATES A SELECTION PROMPT      //
                    string MenuValue = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    //.Title("\nChoose an [green]option[/] below to continue.")
                    .PageSize(4)
                    //.MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices(new[] { "Equip a weapon.", "Go back" }));

                    //int DisplayWeaponsDecision = Convert.ToInt16(Console.ReadLine());

                    switch (MenuValue)
                    {
                        case "Equip a weapon.":
                            EquipWeapon();
                            break;

                        case "Go back":
                            WeaponStats();
                            break;
                    }

                    //      AIMS TO SUGGEST WHAT WEAPON IS THE STRONGEST        //

                    /*
                    void WeaponSuggestion()
                    {
                        int WeaponAtkSuggestion = 0;
                    }
                    */

                    void EquipWeapon()
                    {
                        /*Console.WriteLine("Which weapon do you want to equip?" +
                        "\n1 - {0}" +
                        "\n2 - {1}" +
                        "\n3 - {2}" +
                        "\n4 - {3}", Weapon1, Weapon2, Weapon3, Weapon4);

                        int ChosenWeapon = Convert.ToInt16(Console.ReadLine());

                        switch (ChosenWeapon)
                        {
                            case 1:
                                WeaponEquipped = Weapon1;
                                Console.WriteLine("You have successfully equipped {0}!", Weapon1);
                                WeaponAtk = 2;
                                WeaponDurability = 0;
                                Console.WriteLine(Weapon1 +
                                    "\nAttack Rating: {0}", WeaponAtk);
                                player.PlayerAtk += WeaponAtk;
                                Console.WriteLine("Your total ATK rating is now: {0}!", player.PlayerAtk);

                                break;

                            case 2:
                                WeaponEquipped = Weapon2;
                                Console.WriteLine("You have successfully equipped {0}!", Weapon2);
                                break;

                            case 3:
                                WeaponEquipped = Weapon3;
                                Console.WriteLine("You have successfully equipped {0}!", Weapon3);
                                break;

                            case 4:
                                WeaponEquipped = Weapon4;
                                Console.WriteLine("You have successfully equipped {0}!", Weapon4);
                                break;
                        
                        }
                        */
                    }

                }

                public void WeaponStats()
                {
                    /*switch (WeaponEquipped)
                    {
                        case "Wooden Bat":
                            WeaponAtk = 2;
                            WeaponDurability = 0;
                            Console.WriteLine(Weapon1 +
                                "\nAttack Rating: {0}", WeaponAtk);
                            player.PlayerAtk += WeaponAtk;
                            break;

                        case "Steel Rusty Mace":
                            WeaponAtk = 4;
                            WeaponDurability = 0;
                            Console.WriteLine(Weapon2 +
                                "\nAttack Rating: {0}", WeaponAtk);
                            player.PlayerAtk += WeaponAtk;
                            break;

                        case "Fidget Spinner":
                            WeaponAtk = 3;
                            WeaponDurability = 0;
                            Console.WriteLine(Weapon3 +
                                "\nAttack Rating: {0}", WeaponAtk);
                            player.PlayerAtk += WeaponAtk;
                            break;

                        case "Magic Bouncy Stress Ball":
                            WeaponAtk = 6;
                            WeaponDurability = 0;
                            Console.WriteLine(Weapon3 +
                                "\nAttack Rating: {0}", WeaponAtk);
                            player.PlayerAtk += WeaponAtk;
                            break;
                    }
                    */
                }
            }
        }
    }


    //      BATTLE AND ENEMY SETUP WITH ATTRIBUTE ASSIGNMENTS       //
    class BattleSetup
    {
        Player player = new Player();
        PlayGame playGame1 = new PlayGame();

        public bool InitiateBattle = false;
        public bool PlayerIsAliveBattle = true;
        public bool EnemyIsAliveBattle = true;

        ConsoleColor DefaultColour = ConsoleColor.White;
        ConsoleColor PlayerAttacks = ConsoleColor.Green;
        ConsoleColor EnemyAttacks = ConsoleColor.Red;


        //      ENEMIES VARIABLES       //
        /*
         *  Enemy Name [EnemyName] string - The name allows us to call the right enemy into the battle.
         *  Enemy Defense [EnemyDef] float - Takes away from the Player Attack Damage.
         *  Enemy HP [EnemyHP] int - Enemy's Health Points
         *  Enemy Atk [EnemyAtk] int - Enemy Attack Points that will decrease the Player's HP.
         */

        /*public string WolfName = "Wolf";
        public int WolfHP = 50;
        public int WolfDef = 4;
        public int WolfAtk = 7;
        public string BearName = "Brown Bear";
        public string GBearName = "Grizzy Bear";
        public string BlackRatsName = "Black Rats";*/

        public string EnemyName = "";
        public float EnemyHP;
        public float EnemyDef;
        public int EnemyAtk;
        public int EnemyExp;
        public float EnemyPoisonDmg = 1.5f;
        public bool Poisoned = false;
        public int PoisonTimer = 10;

        // Sets up the Enemy stats
        public void InitialSetup()
        {
            PlayerIsAliveBattle = player.PlayerIsAlive;

            switch (EnemyName)
            {
                case "Wolf":
                    EnemyHP = 75f;
                    EnemyDef = 0.4f;
                    EnemyAtk = 5;
                    EnemyExp = 15;
                    break;

                case "Brown Bear":
                    EnemyHP = 125f;
                    EnemyDef = 3.5f;
                    EnemyAtk = 9;
                    break;

                case "Grizzly Bear":
                    EnemyHP = 150f;
                    EnemyDef = 5.1f;
                    EnemyAtk = 12;
                    break;

                case "Black Rats":
                    EnemyHP = 75f;
                    EnemyDef = 1.2f;
                    EnemyAtk = 5;
                    break;

                case "Venomous Bat":
                    EnemyHP = 95f;
                    EnemyDef = 2.11f;
                    EnemyAtk = 7;
                    break;
            }

            Console.WriteLine("BATTLE HAS BEEN INITIATED WITH {0}.\n", EnemyName);

            while (player.PlayerHP > 0 && EnemyHP > 0)
            {
                if (InitiateBattle == true)
                {
                    Battle();
                }
            };
        }

        public void Battle()
        {
            bool PlayerAttack = false;
            bool PlayerRageAttack = false;
            //float CurrentPlayerHP = 0f;

            Grid EnemyStats = new Grid();

            /*-----------------------------------------------Padding----------------------------------------------*/

            Padder EnemyNameHeading = new Padder(new Text("Enemy Name", new Style(Color.Blue)).Centered()).PadLeft(15).PadRight(1).PadBottom(0).PadTop(0);
            Padder EnemyHPHeading = new Padder(new Text("HP", new Style(Color.Green)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            Padder EnemyDEFHeading = new Padder(new Text("DEF Rating", new Style(Color.Yellow)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            Padder EnemyATKHeading = new Padder(new Text("ATK Rating", new Style(Color.Red)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            //Padder EnemyEXPHeading = new Padder(new Text("EXP", new Style(Color.White)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);


            Padder EnemyNameString = new Padder(new Text(EnemyName, new Style(Color.White)).Centered()).PadLeft(15).PadRight(1).PadBottom(0).PadTop(0);
            Padder EnemyHPString = new Padder(new Text(EnemyHP.ToString(), new Style(Color.White)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            Padder EnemyDEFString = new Padder(new Text(EnemyDef.ToString(), new Style(Color.White)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            Padder EnemyATKString = new Padder(new Text(EnemyAtk.ToString(), new Style(Color.White)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);
            //Padder EnemyEXPString = new Padder(new Text(EnemyExp.ToString(), new Style(Color.White)).Centered()).PadLeft(0).PadRight(1).PadBottom(0).PadTop(0);

            EnemyStats.AddColumns(4);
            EnemyStats.AddRow(EnemyNameHeading, EnemyHPHeading, EnemyDEFHeading, EnemyATKHeading);
            EnemyStats.AddRow(EnemyNameString, EnemyHPString, EnemyDEFString, EnemyATKString);

            AnsiConsole.Write(EnemyStats);

            /*
            CurrentPlayerHP = player.PlayerHP;

            if (CurrentPlayerHP != player.PlayerHP)
            {
                CurrentPlayerHP = player.PlayerHP;
            }*/

            while (player.PlayerHP > 0 && EnemyHP > 0)
            {
                if (player.PlayerRageMode == true)
                {
                    Console.WriteLine("You are now completely angry and agitated! Unlesh your anger to the enemies!\n");


                    //      CREATES A SELECTION PROMPT      //
                    string MenuValue = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("\nChoose an [green]command[/] below to continue.")
                    .PageSize(4)
                    //.MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices(new[] { "Attack.", "Rage Attack!", "Use a Consumable Item." }));

                    switch (MenuValue)
                    {
                        case "Attack.":
                            AnsiConsole.Status().Start("\n[white]Trying to attack.[/]", PlayerATKStatus =>
                            {
                                // PlayerATKStatus is a local variable is creates a new instance of StatusContext.

                                // Simulate some work
                                AnsiConsole.MarkupLine("[white]Attacking...[/]");
                                Thread.Sleep(1250);

                                Random PlayerATKStatusRND = new Random();
                                int PlayerATKStatusRNDValue = PlayerATKStatusRND.Next(1, 5);

                                switch (PlayerATKStatusRNDValue)
                                {
                                    case 1:
                                        // Update the status and spinner
                                        PlayerATKStatus.Status("[white]Trying to be strategic with the attack...[/]");
                                        PlayerATKStatus.Spinner(Spinner.Known.Star2);
                                        PlayerATKStatus.SpinnerStyle(Style.Parse("green"));
                                        Thread.Sleep(1000);
                                        break;

                                    case 2:
                                        // Update the status and spinner
                                        PlayerATKStatus.Status("[white]Tripped over a rock, but getting up to attack properly...[/]");
                                        PlayerATKStatus.Spinner(Spinner.Known.Star2);
                                        PlayerATKStatus.SpinnerStyle(Style.Parse("green"));
                                        Thread.Sleep(1500);
                                        break;

                                    case 3:
                                        // Update the status and spinner
                                        PlayerATKStatus.Status("[white]Wiping the dripping mucus away from my nose and attacking...[/]");
                                        PlayerATKStatus.Spinner(Spinner.Known.Star2);
                                        PlayerATKStatus.SpinnerStyle(Style.Parse("green"));
                                        Thread.Sleep(1500);
                                        break;

                                    case 4:
                                        // Update the status and spinner
                                        PlayerATKStatus.Status("[white]Sneezed loudly due to the pollen in the air, slightly frightening the enemy.[/]");
                                        PlayerATKStatus.Spinner(Spinner.Known.Star2);
                                        PlayerATKStatus.SpinnerStyle(Style.Parse("green"));
                                        Thread.Sleep(1500);
                                        break;
                                }

                                //AnsiConsole.MarkupLine("Done!");
                                PlayerATKStatus.Status("[green]Done![/]");
                                Thread.Sleep(350);

                                PlayerRageAttack = true;

                            });
                            break;

                        case "Rage Attack!":
                            AnsiConsole.Status().Start("\n[white]Building up the anger to let out onto the enemy.[/]", PlayerRageATKStatus =>
                            {
                                // PlayerATKStatus is a local variable is creates a new instance of StatusContext.

                                // Simulate some work
                                AnsiConsole.MarkupLine("[white]Rage attacking...[/]");
                                Thread.Sleep(1250);

                                // Update the status and spinner
                                PlayerRageATKStatus.Status($"[white]Stomping the head of the {EnemyName}...[/]");
                                PlayerRageATKStatus.Spinner(Spinner.Known.Star2);
                                PlayerRageATKStatus.SpinnerStyle(Style.Parse("green"));
                                Thread.Sleep(1000);

                                //AnsiConsole.MarkupLine("Done!");
                                PlayerRageATKStatus.Status($"Smashed the {EnemyName}'s head to bits.");
                                Thread.Sleep(350);

                                PlayerRageAttack = true;

                            });
                            break;

                        case "Use a Consumable Item.":
                            Player.PlayerInventory.ConsumableItems ConsumableItems = new();
                            ConsumableItems.UseConsumableItem();
                            break;

                    }
                }
                else
                {
                    Console.WriteLine("What do you want to do?\n");

                    //      CREATES A SELECTION PROMPT      //
                    string MenuValue = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("\nChoose an [green]command[/] below to continue.")
                    .PageSize(4)
                    //.MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices(new[] { "Attack.", "Use a Consumable Item." }));

                    switch (MenuValue)
                    {
                        case "Attack.":
                            AnsiConsole.Status().Start("\n[white]Trying to attack.[/]", PlayerATKStatus =>
                            {
                                // PlayerATKStatus is a local variable is creates a new instance of StatusContext.

                                // Simulate some work
                                AnsiConsole.MarkupLine("[white]Attacking...[/]");
                                Thread.Sleep(1250);

                                Random PlayerATKStatusRND = new Random();
                                int PlayerATKStatusRNDValue = PlayerATKStatusRND.Next(1, 5);

                                switch (PlayerATKStatusRNDValue)
                                {
                                    case 1:
                                        // Update the status and spinner
                                        PlayerATKStatus.Status("[white]Trying to be strategic with the attack...[/]");
                                        PlayerATKStatus.Spinner(Spinner.Known.Star2);
                                        PlayerATKStatus.SpinnerStyle(Style.Parse("green"));
                                        Thread.Sleep(1000);
                                        break;

                                    case 2:
                                        // Update the status and spinner
                                        PlayerATKStatus.Status("[white]Tripped over a rock, but getting up to attack properly...[/]");
                                        PlayerATKStatus.Spinner(Spinner.Known.Star2);
                                        PlayerATKStatus.SpinnerStyle(Style.Parse("green"));
                                        Thread.Sleep(1500);
                                        break;

                                    case 3:
                                        // Update the status and spinner
                                        PlayerATKStatus.Status("[white]Wiping the dripping mucus away from my nose and attacking...[/]");
                                        PlayerATKStatus.Spinner(Spinner.Known.Star2);
                                        PlayerATKStatus.SpinnerStyle(Style.Parse("green"));
                                        Thread.Sleep(1500);
                                        break;

                                    case 4:
                                        // Update the status and spinner
                                        PlayerATKStatus.Status("[white]Sneezed loudly due to the pollen in the air, slightly frightening the enemy.[/]");
                                        PlayerATKStatus.Spinner(Spinner.Known.Star2);
                                        PlayerATKStatus.SpinnerStyle(Style.Parse("green"));
                                        Thread.Sleep(1500);
                                        break;
                                }

                                //AnsiConsole.MarkupLine("Done!");
                                PlayerATKStatus.Status("[green]Done![/]");
                                Thread.Sleep(350);

                                PlayerAttack = true;

                            });
                            break;

                        case "Use a Consumable Item.":
                            Player.PlayerInventory.ConsumableItems ConsumableItems = new();
                            ConsumableItems.UseConsumableItem();
                            break;

                    }
                }

                /*
                //      WE DON'T USE THIS ANYMORE       //
                try
                {
                    int PlayerDecision = Convert.ToInt16(Console.ReadLine());

                    switch (PlayerDecision)
                    {
                        case 1:
                            PlayerAttack = true;
                            break;
                    }
                }

                catch (FormatException ExceptionInfo)
                {
                    Console.WriteLine("INVALID INPUT DETECTED, TRY AGAIN.\n" + ExceptionInfo);
                }
                */

                if (PlayerAttack == true)
                {
                    float CalculatedPlayerAtk;
                    CalculatedPlayerAtk = player.PlayerAtk - EnemyDef;

                    EnemyHP -= CalculatedPlayerAtk;
                    //Console.WriteLine("You have dealt {0} damage! Enemy now has {1} HP!", CalculatedPlayerAtk, EnemyHP);

                    Console.Write("You have dealt ");
                    Console.ForegroundColor = PlayerAttacks;
                    Console.Write("{0} ", MathF.Round(CalculatedPlayerAtk, 2, MidpointRounding.ToEven));
                    Console.ForegroundColor = DefaultColour;
                    Console.Write("damage! The {0} now has ", EnemyName);
                    Console.ForegroundColor = EnemyAttacks;
                    Console.Write("{0}", MathF.Round(EnemyHP, 2, MidpointRounding.ToEven));
                    Console.ForegroundColor = DefaultColour;
                    Console.Write(" HP!");
                    Console.WriteLine();

                    Random PlayerRageRND = new Random();

                    int PlayerRageValue = PlayerRageRND.Next(1, 11);

                    player.PlayerRage += PlayerRageValue;

                    Console.WriteLine("Your Rage level is now: {0}.\n", player.PlayerRage);

                    PlayerAttack = false;

                    if (EnemyHP <= 0)
                    {
                        //player.PlayerHP = CurrentPlayerHP; // Drags the current player's HP across other battles, instead of having it reset.
                        Victory();
                    }
                }

                else if (PlayerRageAttack == true)
                {
                    float CalculatedPlayerAtk;
                    CalculatedPlayerAtk = player.PlayerAtk * 2 - EnemyDef;

                    EnemyHP -= CalculatedPlayerAtk;
                    //Console.WriteLine("You have dealt {0} damage! Enemy now has {1} HP!", CalculatedPlayerAtk, EnemyHP);

                    Console.Write("You have dealt ");
                    Console.ForegroundColor = PlayerAttacks;
                    Console.Write("{0} ", MathF.Round(CalculatedPlayerAtk, 2, MidpointRounding.ToEven));
                    Console.ForegroundColor = DefaultColour;
                    Console.Write("damage! The {0} now has ", EnemyName);
                    Console.ForegroundColor = EnemyAttacks;
                    Console.Write("{0}", MathF.Round(EnemyHP, 2, MidpointRounding.ToEven));
                    Console.ForegroundColor = DefaultColour;
                    Console.Write(" HP!");
                    Console.WriteLine();

                    player.PlayerRage -= 100;

                    Console.WriteLine("Your Rage level is now: 0.\n");

                    PlayerRageAttack = false;

                    if (EnemyHP <= 0)
                    {
                        //player.PlayerHP = CurrentPlayerHP; // Drags the current player's HP across other battles, instead of having it reset.
                        Victory();
                    }
                }

                /*while (player.PlayerRage < player.PlayerRageMax)
                {
                    player.PlayerRage++;
                    Console.WriteLine("Rage is now: {0}", player.PlayerRage);
                    Thread.Sleep(1000);
                }*/

                if (PlayerAttack == false && PlayerRageAttack == false && player.PlayerHP > 0 && EnemyHP > 0)
                {
                    EnemyAttack();
                }
            };

            void EnemyAttack()
            {
                bool EnemyAttack = true;
                int WaitTime = 0;
                Random GeneratedWaitTime = new Random();

                if (player.PlayerHP > 0 && EnemyHP > 0 && EnemyAttack == true)
                {
                    //System.Threading.Thread.Sleep(750);

                    WaitTime = GeneratedWaitTime.Next(500, 1000);

                    System.Threading.Thread.Sleep(WaitTime);

                    float CalculatedEnemyAtk;
                    
                    CalculatedEnemyAtk = EnemyAtk - player.PlayerDef;
                    player.PlayerHP -= CalculatedEnemyAtk;

                    //Console.WriteLine("The {0} has dealt you {1} damage. Your HP is now {2}.", EnemyName, EnemyAtk, player.PlayerHP);

                    //      Console Printout        //
                    Console.Write("The {0} has dealt you ", EnemyName);
                    Console.ForegroundColor = EnemyAttacks;
                    Console.Write("{0} ", MathF.Round(CalculatedEnemyAtk, 2, MidpointRounding.ToEven));
                    Console.ForegroundColor = DefaultColour;
                    Console.Write("damage. Your HP is now ");
                    Console.ForegroundColor = PlayerAttacks;
                    Console.Write("{0}.\n", MathF.Round(player.PlayerHP, 2, MidpointRounding.ToEven));
                    Console.ForegroundColor = DefaultColour;

                    EnemyAttack = false;

                    /*      THERES NO TIME FOR THIS
                    if (EnemyName == "Black Rats")
                    {
                        Random poisonRND = new Random();
                        int poisonValue = poisonRND.Next(1, 2);

                        switch (poisonValue)
                        {
                            case 2:
                                Poisoned = true;
                                break;
                        }

                        if (Poisoned == true && PoisonTimer > 0) // NEED TO IMPLEMENT SOME SORT OF TIMER.
                        {
                            player.PlayerHP -= 0.13f;
                        }
                    }
                    */
                }

                if (player.PlayerHP <= 0f)
                {
                    Console.WriteLine("You have died. :(");
                    Console.ReadKey();
                    playGame1.MainGameInit();
                }
            }
        }

        public void Victory()
        {
            RandomisedRewards EnemyRewards = new RandomisedRewards();

            EnemyIsAliveBattle = false;
            Console.WriteLine("You have won against the {0}!\n", EnemyName);

            EnemyRewards.RandomEnemyReward();
        }
    }

    //      RANDOMISED REWARDS      //
    class RandomisedRewards
    {
        PlayGame Locations = new PlayGame();
        Player.PlayerInventory.ConsumableItems ConsumableItems = new Player.PlayerInventory.ConsumableItems();

        public Random RandomReward = new Random();
        public int RandomItem = 0;

        public void StorageBox1Rewards()
        {
            RandomItem = RandomReward.Next(1, 4);

            switch (RandomItem) 
            {
                case 1:
                    ConsumableItems.Chocolates++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You have found a chocolate.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                    Locations.OutSide1A();
                    break;

                case 2:
                    ConsumableItems.BottleOfWater++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You have found a Bo'le of Wa'er.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                    Locations.OutSide1A();
                    break;
                
                case 3:
                    ConsumableItems.HolyTapWater++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You have found the blessed Holy Tap Water.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                    Locations.OutSide1A();
                    break;
            }
        }

        public void RandomEnemyReward()
        {
            RandomItem = RandomReward.Next(1, 5);

            switch (RandomItem)
            {
                case 1:
                    ConsumableItems.BottleOfWater++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("The enemy has dropped a Bo'le of Wa'er behind!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                    break;

                case 2:
                    ConsumableItems.Coke++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("The enemy has dropped a Can of Coke behind!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                    break;

                case 3:
                    ConsumableItems.Chocolates++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("The enemy has dropped a Box of Cheap Chocolates behind!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                    break;

                case 4:
                    ConsumableItems.HolyTapWater++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("The enemy has dropped a container filled with the mighty Holy Tap Water behind!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                    break;
            }
        }

        /*
        public void SecretStaticVoidOfRickRollRewards()
        {
            You're anyways going to see this. -_-
        }
        */
    }

    // SCRAPPED
    /*class Store
    {

    }*/
    

    //      MAY BE USED TO GATHER INFORMATION IN A TXT FILE, ABOUT HOW THE VARIABLES ARE BEING UPDATED AT EACH FUNCTION        //
    /*class WriteInfoToFile
    {
 
    }*/
}

//      TO DO LIST      //
/*
 * Add randomised rewards - W.I.P, but VERY BLAND.
 * Add functions to allow player to equip weapons - Done
 * Add functions to allow player to equip outfits/armour - Scrapped
 * Implement Poison function - Scrapped
 * Implement Advanced Enemy Randomiser [E.g., Put enemies in categories, some enemies can be triggered if the player is playing the game at a certain time of day] - Scrapped
 * Add SoundPlayer to the game to play sounds - Done, PixaBay provided Royalty-free sounds.
 * Randomise the amount of time the enemy takes to decide an attack on the player, to make it more "natural" with how the combat works - Done
 * Implement Rage Meter & Functionality - Scrapped
 * Implement Fear Meter & Functionality - Scrapped
 * Implement Store Currency - Scrapped
 * Store - Scrapped
 * Implement decisions that impact the story and gameplay - Entirely scrapped
 */