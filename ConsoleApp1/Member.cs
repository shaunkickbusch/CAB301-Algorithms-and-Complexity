using System;
using System.Collections;
using System.Text;
using System.Transactions;

namespace ConsoleApp1
{
    class Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string StreetType { get; set; }
        public string Suburb { get; set; }
        public string Postcode { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public MovieCollection BorrowedMovies { get; set; }

        public Member(string firstName, string lastName, string streetNumber, string streetName, string streetType, string suburb, string postcode, string state, string phoneNumber, string password)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.StreetNumber = streetNumber;
            this.StreetName = streetName;
            this.StreetType = streetType;
            this.Suburb = suburb;
            this.Postcode = postcode;
            this.State = state;
            this.PhoneNumber = phoneNumber;
            this.Password = password;
            this.BorrowedMovies = new MovieCollection();
        }

        public string borrowMovie(MovieCollection myBinaryTree)
        {
            string input = "";
            bool foundMovie = false;
            Console.Write("Enter movie title: ");
            input = Console.ReadLine();

            //If the user input is a valid movie name but there are no dvd copies available
            if (myBinaryTree.Find(input) != null && myBinaryTree.Find(input).NumberOfCopiesAvailable == 0)
            {
                return string.Format("Unfortunately, there are no DVD copies of the movie '{0}' in stock.\n", input);
            }
            //If we find a movie that has the title the user gave with available copies
            else if (myBinaryTree.Find(input) != null && myBinaryTree.Find(input).NumberOfCopiesAvailable >= 1)
            {

                //If the member already has that movie borrowed
                if (BorrowedMovies.Find(input) != null)
                {
                    //Increment the amount of copies that specific member has borrowed;
                    BorrowedMovies.Find(input).NumberOfCopiesRentedByThisUser++;
                    foundMovie = true;
                }


                //If foundMovie is still equal to false even after traversing through the user's currently borrowed movies, we know they don't currently
                //Have it borrowed
                if (!foundMovie)
                {
                    //Copy the data from the movies BST node into tempMovie
                    Movie tempMovie = new Movie
                    (
                        myBinaryTree.Find(input).Title,
                        myBinaryTree.Find(input).Starring,
                        myBinaryTree.Find(input).Director,
                        myBinaryTree.Find(input).Duration,
                        myBinaryTree.Find(input).Genre,
                        myBinaryTree.Find(input).Classification,
                        myBinaryTree.Find(input).ReleaseDate,
                        myBinaryTree.Find(input).NumberOfCopiesThatExist
                    );
                    tempMovie.NumberOfCopiesRentedByThisUser++;
                    BorrowedMovies.Insert(tempMovie);
                }

                //Decrement the total number of copies available
                myBinaryTree.Find(input).NumberOfCopiesAvailable--;
                //Incremet the total number of times borrowed for that specific movie
                myBinaryTree.Find(input).NumTimesBorrowed++;
                return string.Format("You have successfully borrowed '{0}'.\n", input);
            }
            //The user gave a movie title which doesn't exist in the library
            else
            {
                return string.Format("A movie with the title '{0}' doesn't exist.\n", input);
            }
        }

        public string returnMovie(MovieCollection myBinaryTree)
        {
            string input = "";
            Console.Write("Enter movie title: ");
            input = Console.ReadLine();

            //Iterate through the members borrowed movies
            if (BorrowedMovies.Find(input) != null)
            {
                BorrowedMovies.Find(input).NumberOfCopiesRentedByThisUser--;
            }
            else
            {
                return string.Format("You don't currently have any DVDs borrowed with the name '{0}'\n", input);
            }

            //If a member returned a movie that is present in the system.
            //Keep in mind if they returned a movie that wasn't present in the system, it's because staff deleted it from the BST
            if (myBinaryTree.Find(input) != null)
            {
                //Incrememt the number of copies for that movie that are available again
                myBinaryTree.Find(input).NumberOfCopiesAvailable++;
            }

            //This member has no copies of that movie left
            if (BorrowedMovies.Find(input).NumberOfCopiesRentedByThisUser == 0)
            {
                //Remove the movie entirely from the members borrowed movies
                BorrowedMovies.Remove(input);
                return string.Format("You successfully returned your only DVD copy of '{0}'.\n", input);
            }
            //We only get to this point of the user has multiple copies of the same movie still in their possession
            return string.Format("You successfully returned a DVD copy of '{0}'. You've still got {1} copies.\n", input, BorrowedMovies.Find(input).NumberOfCopiesRentedByThisUser);
        }

        public void currentlyBorrowed()
        {
            if (BorrowedMovies.GetNumNodes() == 0)
            {
                Console.WriteLine("You don't have any DVDs currently borrowed.\n");
            }
            //There's at least 1 movie in the library
            else
            {
                //Display all movies
                BorrowedMovies.MemberCurrentRented();
            }
        }

        public string getUserName()
        {
            return this.LastName + this.FirstName;
        }

        public string getFullAddress()
        {
            return StreetNumber + ", " + StreetName + ", " + StreetType + ", " + Suburb + ", " + Postcode + ", " + State;
        }
    }
}
