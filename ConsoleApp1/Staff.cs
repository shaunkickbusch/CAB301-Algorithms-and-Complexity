using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Staff
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        //Constructor the staff
        public Staff()
        {
            this.UserName = null;
            this.Password = null;
        }

        //Constructor the staff
        public Staff(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }

        public Member registerMember(MemberCollection myMemberCollection)
        {
            Member myMember;
            string firstName = "";
            string lastName = "";
            string streetNumber;
            string streetName = "";
            string streetType = "";
            string suburb = "";
            string postcode;
            string state = "";
            string phoneNumber;
            string password;

            Console.WriteLine("===========Register Member============\n");
            Console.Write("First Name: ");
            firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            lastName = Console.ReadLine();

            //Iterate through the member collection and see if this member already exists
            for (int i = 0; i < myMemberCollection.Members.Length; i++)
            {
                //If there exists members and if that member exists
                if (myMemberCollection.AmountOfMembers > 0 && myMemberCollection.Members[i].getUserName() == (lastName + firstName))
                {
                    return null;
                }
            }
            //Parse street number
            while (true)
            {
                Console.Write("Street Number: ");
                if (Int32.TryParse(Console.ReadLine(), out int streetNumberInt))
                {
                    streetNumber = streetNumberInt.ToString();
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Enter a valid digit");
                }
            }
            Console.Write("Street Name: ");
            streetName = Console.ReadLine();
            Console.Write("Street Type: ");
            streetType = Console.ReadLine();
            Console.Write("Suburb: ");
            suburb = Console.ReadLine();
            //Parse postcode
            while (true)
            {
                Console.Write("Postcode: ");
                if (Int32.TryParse(Console.ReadLine(), out int postcodeInt))
                {
                    postcode = postcodeInt.ToString();
                    break;
                }

                Console.WriteLine("Error: That's not a valid postcode.");

            }
            Console.Write("State: ");
            state = Console.ReadLine();
            //Parse phone Number
            while (true)
            {
                Console.Write("Phone Number: ");
                string input = Console.ReadLine();
                //Ensures the phone number is an int
                if (Int64.TryParse(input, out long phoneNumberLong))
                {
                    phoneNumber = input;
                    break;
                }
                else
                {
                    Console.WriteLine("Error: That's not a valid phone number.");
                }
            }
            //Enter password
            while (true)
            {
                while (true)
                {
                    Console.Write("Password: ");
                    string input = Console.ReadLine();
                    //Covers the edge case if the password is 0000 and also covers seeing if the password is digits and 4 long
                    if (input == "0000" || (Int32.TryParse(input, out int passwordInt) && Math.Floor(Math.Log10(passwordInt) + 1) == 4))
                    {
                        //Convert the int to a string to store
                        password = input;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nError: Password was not valid. Trying entering a valid integer that's 4 digits long.\n");
                    }
                }

                Console.Write("Confirm Password: ");
                //Is the user input the same as before?
                if (Console.ReadLine() == password)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Passwords don't match. Try again.");
                }
            }

            myMember = new Member(firstName, lastName, streetNumber, streetName, streetType, suburb, postcode, state, phoneNumber, password);
            return myMember;
        }


        public Movie addMovie(MovieCollection myBinaryTree)
        {
            string input = "";
            string movieTitle = "";
            List<string> movieStarring = new List<string>();
            string movieDirector = "";
            string movieDuration = "";
            string movieGenre = "";
            string movieClassification = "";
            string movieReleaseDate = "";
            bool addStar = true;
            bool addGenre = true;
            bool addClassification = true;
            int numCopies;
            Movie myMovie;

            Console.Write("Title: ");
            input = Console.ReadLine();

            //Check if that movie already exists
            if (myBinaryTree.Find(input) != null)
            {
                Console.WriteLine("Error: A movie with that title already exists");
                while (true)
                {
                    //Since the movie already exists, ask the user if they'd like to update the number of DVD copies
                    Console.Write("Would you like to change the number of DVDs? (y or n): ");
                    string input2 = Console.ReadLine();
                    //The user wants to change the number of copies
                    if (input2 == "y")
                    {
                        while (true)
                        {
                            Console.Write("Copies: ");
                            if (Int32.TryParse(Console.ReadLine(), out int numTemp))
                            {
                                //If the number they entered is 0, we want to remove the movie
                                if (numTemp == 0)
                                {
                                    myBinaryTree.Remove(input);
                                    Console.WriteLine("Successfully removed the movie");
                                    //Exit the function
                                    return null;
                                }
                                //The user entered a negative number
                                else if (numTemp < 0)
                                {
                                    Console.WriteLine("Error: you cannot enter a negative number.");
                                    //Go back to the start of the while loop
                                    continue;
                                }
                                int oldNumCopies = myBinaryTree.Find(input).NumberOfCopiesThatExist;
                                myBinaryTree.Find(input).NumberOfCopiesThatExist = numTemp;
                                Console.WriteLine("Successfully changed the num of copies for {0} from {1} to {2}.", input, oldNumCopies, myBinaryTree.Find(input).NumberOfCopiesThatExist);
                                return null;
                            }
                            Console.WriteLine("Error: That's not a valid integer.");
                        }
                    }
                    else if (input2 == "n")
                    {
                        return null;
                    }
                    else
                    {
                        Console.WriteLine("Error: {0} is not a valid option", input2);
                    }
                }
            }
            //We have a movie that doesn't exist
            else if (myBinaryTree.Find(input) == null)
            {
                movieTitle = input;
            }


            while (addStar == true)
            {
                Console.Write("Star: ");
                movieStarring.Add(Console.ReadLine());
                while (true)
                {
                    Console.Write("Add another star? (y or n): ");
                    input = Console.ReadLine();
                    if (input == "y")
                    {
                        break;
                    }
                    else if (input == "n")
                    {
                        addStar = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid Input.");
                    }
                }
            }
            Console.Write("Director: ");
            movieDirector = Console.ReadLine();

            while (addGenre == true)
            {
                Console.WriteLine("Select the genre: ");
                Console.WriteLine("1. Drama\n2. Adventure\n3. Family\n4. Action\n5. Sci-Fi\n6. Comedy\n7. Animated\n8. Thriller\n9. Other");
                Console.Write("Make selection (1-9): ");
                if (Int32.TryParse(Console.ReadLine(), out int genreSelection) && genreSelection >= 1 && genreSelection <= 9)
                {
                    switch (genreSelection)
                    {
                        case 1:
                            movieGenre = "Drama";
                            break;
                        case 2:
                            movieGenre = "Adventure";
                            break;
                        case 3:
                            movieGenre = "Family";
                            break;
                        case 4:
                            movieGenre = "Action";
                            break;
                        case 5:
                            movieGenre = "Sci-Fi";
                            break;
                        case 6:
                            movieGenre = "Comedy";
                            break;
                        case 7:
                            movieGenre = "Animated";
                            break;
                        case 8:
                            movieGenre = "Thriller";
                            break;
                        case 9:
                            movieGenre = "Other";
                            break;
                    }
                    addGenre = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Invalid Genre.");
                }
            }
            while (addClassification == true)
            {
                Console.WriteLine("Select the classification:");
                Console.WriteLine("1. General (G)\n2. Parental Guidance (PG)\n3. Mature (M15+)\n4. Mature Accompanied (MA15+)");
                Console.Write("Make selection (1-4): ");
                if (Int32.TryParse(Console.ReadLine(), out int classificationSelection) && classificationSelection >= 1 && classificationSelection <= 4)
                {
                    switch (classificationSelection)
                    {
                        case 1:
                            movieClassification = "G";
                            break;
                        case 2:
                            movieClassification = "PG";
                            break;
                        case 3:
                            movieClassification = "M15+";
                            break;
                        case 4:
                            movieClassification = "MA15+";
                            break;
                    }
                    addClassification = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Invalid Classification.");
                }
            }
            Console.Write("Duration (minutes): ");
            movieDuration = Console.ReadLine();

            Console.Write("Release Date (year): ");
            movieReleaseDate = Console.ReadLine();

            while (true)
            {
                Console.Write("Enter the number of copies available: ");
                if (Int32.TryParse(Console.ReadLine(), out numCopies))
                {
                    break;
                }
                Console.WriteLine("Error: Invalid number");
            }

            //Convert the list to an array
            string[] myArray = movieStarring.ToArray();
            myMovie = new Movie(movieTitle, myArray, movieDirector, movieDuration, movieGenre, movieClassification, movieReleaseDate, numCopies);
            return myMovie;
        }

        public void removeMovie(MovieCollection myMovieCollection, MemberCollection myMemberCollection)
        {
            string input = "";
            string removeMovieMenu = "===========REMOVE MOVIE=============\n";
            Console.WriteLine(removeMovieMenu);
            while (true)
            {
                Console.Write("Movie Title: ");
                input = Console.ReadLine();

                //Checks to see if we have that movie. If the find function returns null, that movie doesn't exist
                if (myMovieCollection.Find(input) == null)
                {
                    Console.WriteLine("The movie '{0}' currently doesn't exist in the library.", input);
                    break;
                }
                int numCopies = myMovieCollection.Find(input).NumberOfCopiesThatExist;
                //If there's only 1 instance, DVD, of the movie we instantly remove it
                if (numCopies == 1)
                {
                    myMovieCollection.Remove(input);
                    Console.WriteLine("The movie '{0}' was successfully removed.", input);
                    break;
                }
                //We get to this point if there's more than 1 DVD for that same movie. While loop is used to keep prompting
                //The user with the same Console.Write message if they enter an invalid input
                while (true)
                {
                    //If there's more than 1 DVD, we have to ask how many copies they'd like to remove
                    Console.Write("There's currently {0} DVD copies for '{1}.' How many would you like to remove? ", numCopies, input);

                    //See if the user input is an int
                    string temp = Console.ReadLine();
                    if (Int32.TryParse(temp, out int wantedAmount))
                    {
                        //See if the num DVD copies of that movie- the amount the user wants to remove is possible
                        if ((numCopies - wantedAmount) < 0)
                        {
                            Console.WriteLine("Error: You can't remove more copies of a movie than there exists.");
                        }
                        //If there's no copies of the movie left, we remove it
                        else if ((numCopies - wantedAmount) == 0)
                        {
                            myMovieCollection.Remove(input);
                            Console.WriteLine("The movie '{0}' was successfully removed.", input);
                            break;
                        }
                        //Else we have existing copies left so we update the movie in the tree
                        else
                        {
                            myMovieCollection.Find(input).NumberOfCopiesThatExist -= wantedAmount;
                            Console.WriteLine("Successfully removed {0} DVD copies for '{1}.' The updated amount of copies that exist is now {2}.", wantedAmount, input, myMovieCollection.Find(input).NumberOfCopiesThatExist);
                            break;
                        }
                    }
                    //We get to this point if the user input was unable to be parsed to an int
                    else
                    {
                        Console.WriteLine("Error: {0} is not a valid number.", temp);
                    }
                }
                break;
            }
        }
        public void findMemberPhoneNumber(MemberCollection myMemberCollection)
        {
            string input = "";
            string findMemberPhoneNumberMenu = "===========FIND MEMBER PHONE NUMBER=============\n";
            Console.WriteLine(findMemberPhoneNumberMenu);
            bool foundMatch = false;
            string phoneNumber = "";

            while (foundMatch == false)
            {
                Console.Write("Username: ");
                input = Console.ReadLine();
                //Search through the whole collection
                for (int i = 0; i < myMemberCollection.AmountOfMembers; i++)
                {
                    if (myMemberCollection.AmountOfMembers == 0)
                    {
                        break;
                    }
                    //Search through the members in the collection and get their username
                    if (input == myMemberCollection.Members[i].getUserName())
                    {
                        phoneNumber = myMemberCollection.Members[i].PhoneNumber;
                        foundMatch = true;
                        break;
                    }
                }
                if (foundMatch == false)
                {
                    Console.WriteLine("\nError: A member with that username could not be found.\n");
                    break;
                }

            }
            if (foundMatch == true)
            {
                Console.WriteLine("Member with username {0} has the phone number {1}\n", input, phoneNumber);
            }
        }
    }
}
