using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "";
            string mainMenu = "Welcome to the Community Library\n";
            mainMenu += "===========Main Menu============\n";
            mainMenu += "1. Staff Login\n";
            mainMenu += "2. Member Login\n";
            mainMenu += "0. Exit\n";
            mainMenu += "================================\n";
            string selection = "Please make a selection (1-2, or 0 to exit): ";

            string staffMenu = "===========Staff Menu=============\n";
            staffMenu += "1. Add a new movie DVD\n";
            staffMenu += "2. Remove a movie DVD\n";
            staffMenu += "3. Register a new Member\n";
            staffMenu += "4. Find a registered member's phone number\n";
            staffMenu += "0. Return to main menu\n";
            staffMenu += "=======================================\n";
            string selectionStaff = "Please make a selection (1-4 or 0 to return to main menu): ";

            string memberMenu = "===========Member Menu=============\n";
            memberMenu += "1. Display all movies\n";
            memberMenu += "2. Borrow a movie DVD\n";
            memberMenu += "3. Return a movie DVD\n";
            memberMenu += "4. List current borrowed movie DVDs\n";
            memberMenu += "5. Display top 10 most popular movies\n";
            memberMenu += "0. Return to main menu\n";
            memberMenu += "=======================================\n";
            string selectionMember = "Please make a selection (1-5 or 0 to return to main menu): ";

            bool inMainMenu = true;
            bool pickingMainMenuSelection = true;
            bool inStaffMenu = false;
            bool staffMemberLoggedIn = false;

            bool pickingStaffMenuSelection = false;
            bool inMemberMenu = false;
            bool pickingMemberMenuSelection = false;
            bool memberLoggedIn = false;
            int memberNum = -1;

            StaffCollection myStaffCollection = new StaffCollection();
            Staff myStaffMember = new Staff("staff", "today123");
            myStaffCollection.addStaffMember(myStaffMember);

            MovieCollection myBinaryTree = new MovieCollection();
            MemberCollection myMemberCollection = new MemberCollection();

            //TEST FUNCTIONS
            myMemberCollection.addMember(new Member("Shaun", "Kickbusch", "21", "Daisy", "Street", "Brisbane", "4000", "QLD", "000000000", "0000"));
            myBinaryTree.Insert(new Movie("D", new string[3] { "Element 1", "Element 2", "Element 3" }, "A", "A", "A", "A", "A", 2));
            myBinaryTree.Insert(new Movie("C", new string[3] { "Element 1", "Element 2", "Element 3" }, "A", "A", "A", "A", "A", 2));
            myBinaryTree.Insert(new Movie("I", new string[3] { "Element 1", "Element 2", "Element 3" }, "A", "A", "A", "A", "A", 2));
            myBinaryTree.Insert(new Movie("J", new string[3] { "Element 1", "Element 2", "Element 3" }, "A", "A", "A", "A", "A", 2));
            myBinaryTree.Insert(new Movie("F", new string[3] { "Element 1", "Element 2", "Element 3" }, "A", "A", "A", "A", "A", 2));
            myBinaryTree.Insert(new Movie("E", new string[3] { "Element 1", "Element 2", "Element 3" }, "A", "A", "A", "A", "A", 2));
            myBinaryTree.Insert(new Movie("G", new string[3] { "Element 1", "Element 2", "Element 3" }, "A", "A", "A", "A", "A", 2));
            myBinaryTree.Insert(new Movie("H", new string[3] { "Element 1", "Element 2", "Element 3" }, "A", "A", "A", "A", "A", 2));

            //This while loop controls the whole flow of the program. E.g. if a member exits their member menu to return to the main menu it runs back to the
            //top of this loop
            while (true)
            {
                //This while loop controls the entire main menu. It sets bool values depending whether the user wants to progress to the staff or member menus
                while (inMainMenu == true)
                {
                    Console.WriteLine(mainMenu);
                    while (pickingMainMenuSelection == true)
                    {
                        Console.Write(selection);
                        input = Console.ReadLine();
                        if (input == "0")
                        {
                            Console.Write("Bye.");
                            System.Environment.Exit(1);
                        }
                        else if (input == "1" || input == "2")
                        {
                            pickingMainMenuSelection = false;
                            break;

                        }
                        Console.WriteLine("\nError: invalid option entered.\n");
                    }
                    if (input == "1")
                    {
                        inStaffMenu = true;
                        pickingStaffMenuSelection = true;
                    }
                    else if (input == "2")
                    {
                        pickingMemberMenuSelection = true;
                        inMemberMenu = true;
                    }
                    inMainMenu = false;
                }

                //This while loop controls the entire staff menu, from authentication to option selection
                while (inStaffMenu == true)
                {
                    while (staffMemberLoggedIn == false)
                    {
                        myStaffCollection.authentication();
                        staffMemberLoggedIn = true;
                    }

                    Console.WriteLine(staffMenu);
                    while (pickingStaffMenuSelection == true)
                    {
                        Console.Write(selectionStaff);
                        input = Console.ReadLine();
                        if (input == "0")
                        {
                            inMainMenu = true;
                            pickingMainMenuSelection = true;
                            inStaffMenu = false;
                            staffMemberLoggedIn = false;
                            pickingStaffMenuSelection = false;
                            break;
                        }
                        //Check if our input is a valid option
                        else if (input == "1" || input == "2" || input == "3" || input == "4")
                        {
                            pickingStaffMenuSelection = false;
                            break;
                        }
                        Console.WriteLine("\nError: Invalid Option Entered.\n");
                    }
                    //The staff member has selected option 1 which is "Add a new movie DVD"
                    if (input == "1")
                    {
                        Console.WriteLine("===========Add Movie=============");
                        Movie myMovie = myStaffMember.addMovie(myBinaryTree);
                        //If the function returned null, we updated the DVD copies for an existing movie
                        if (myMovie == null)
                        {

                        }
                        //If it returned true we created a brand new movie
                        else if (myMovie != null)
                        {
                            //Insert the new movie into the BST
                            myBinaryTree.Insert(myMovie);
                            Console.WriteLine("\nMovie Successfully Added.\n");
                        }
                    }
                    else if (input == "2")
                    {
                        myStaffMember.removeMovie(myBinaryTree, myMemberCollection);
                    }
                    else if (input == "3")
                    {
                        Member myMember = myStaffMember.registerMember(myMemberCollection);
                        //If the function returned null, that member already existed
                        if (myMember == null)
                        {
                            Console.WriteLine("\nError: Member already exists\n");
                        }
                        else if (myMember != null)
                        {
                            //Insert the new member into the member collection
                            myMemberCollection.addMember(myMember);
                            Console.WriteLine("\nMember Successfully Added.\n");
                        }
                    }
                    else if (input == "4")
                    {
                        myStaffMember.findMemberPhoneNumber(myMemberCollection);
                    }
                    pickingStaffMenuSelection = true;
                }

                while (inMemberMenu == true)
                {
                    while (memberLoggedIn == false)
                    {
                        //Store the index where the member appears to ensure we can access that same member later
                        memberNum = myMemberCollection.authentication();
                        memberLoggedIn = true;
                    }

                    while (pickingMemberMenuSelection == true)
                    {
                        Console.WriteLine(memberMenu);
                        Console.Write(selectionMember);
                        input = Console.ReadLine();
                        if (input == "0")
                        {
                            inMainMenu = true;
                            pickingMainMenuSelection = true;
                            inMemberMenu = false;
                            memberLoggedIn = false;
                            pickingMemberMenuSelection = false;
                            break;
                        }
                        else if (input == "1")
                        {
                            if (myBinaryTree.GetNumNodes() == 0)
                            {
                                Console.WriteLine("There's currently no movies in the library.\n");
                            }
                            //There's at least 1 movie in the library
                            else
                            {
                                //Display all movies
                                myBinaryTree.InOrderTraversal();
                            }
                        }
                        else if (input == "2")
                        {
                            //Borrow a movie
                            Console.WriteLine(myMemberCollection.Members[memberNum].borrowMovie(myBinaryTree));
                        }
                        else if (input == "3")
                        {
                            //Return a movie
                            Console.WriteLine(myMemberCollection.Members[memberNum].returnMovie(myBinaryTree));
                        }
                        else if (input == "4")
                        {
                            //List borrowed movies
                            myMemberCollection.Members[memberNum].currentlyBorrowed();
                        }
                        else if (input == "5")
                        {
                            //Create an array of movies with the size being the number of movies in the tree
                            Movie[] myMovies = new Movie[myBinaryTree.GetNumNodes()];
                            //Flatten the binary tree into the myMovies array
                            myBinaryTree.FlattenBST(myBinaryTree.Root, myMovies, 0);
                            //List top 10 movies
                            myBinaryTree.Top10MostPopularMovies(myMovies);
                        }
                        else
                        {
                            Console.WriteLine("\nError: Invalid Option Entered.\n");
                        }
                    }
                    inMemberMenu = false;
                }
            }
        }
    }
}
