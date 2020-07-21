using System;

namespace ConsoleApp1
{
    class Movie
    {
        public string Title { get; set; }
        public string[] Starring { get; set; }
        public string Director { get; set; }
        public string Duration { get; set; }
        public string Genre { get; set; }
        public string Classification { get; set; }
        public string ReleaseDate { get; set; }
        public int NumberOfCopiesAvailable { get; set; }
        public int NumberOfCopiesThatExist { get; set; }
        public int NumberOfCopiesRentedByThisUser { get; set; }
        public Movie RightNode { get; set; }
        public Movie LeftNode { get; set; }
        public int NumTimesBorrowed { get; set; }

        //Node constructor
        public Movie(string title, string[] starring, string director, string duration, string genre, string classification, string releaseDate, int numberOfCopiesThatExist)
        {
            this.Title = title;
            this.Starring = starring;
            this.Director = director;
            this.Duration = duration;
            this.Genre = genre;
            this.Classification = classification;
            this.ReleaseDate = releaseDate;
            this.NumberOfCopiesThatExist = numberOfCopiesThatExist;
            this.NumberOfCopiesAvailable = numberOfCopiesThatExist;
            this.NumberOfCopiesRentedByThisUser = 0;
        }

        public void Insert(Movie myMovie)
        {
            //Lexographically, if the new node is equal to or greater than the current node
            if (string.Compare(myMovie.Title, this.Title) == 0 || string.Compare(myMovie.Title, this.Title) == 1)
            {
                if (RightNode == null)
                {
                    RightNode = myMovie;
                }
                else
                {
                    RightNode.Insert(myMovie);
                }
            }
            else
            {
                if (LeftNode == null)
                {
                    LeftNode = myMovie;
                }
                else
                {
                    LeftNode.Insert(myMovie);
                }
            }
        }

        public int GetNumNodes()
        {
            int leftSize = 0;
            int rightSize = 0;
            if (this.LeftNode != null)
            {
                //Call recusively down the left hand side of the tree
                leftSize = this.LeftNode.GetNumNodes();
            }
            if (this.RightNode != null)
            {
                //Call recusively down the right hand side of the tree
                rightSize = this.RightNode.GetNumNodes();
            }
            //Return 1, to account for the parent, plus the amount of left and right nodes found
            return 1 + leftSize + rightSize;
        }

        public void InOrderTraversal()
        {
            if (LeftNode != null)
            {
                LeftNode.InOrderTraversal();
            }

            //Then we print the root node 
            Console.WriteLine("\nTitle: {0}", this.Title);
            Console.WriteLine("Staring: {0}", string.Join(", ", this.Starring));
            Console.WriteLine("Director: {0}", this.Director);
            Console.WriteLine("Genre: {0}", this.Genre);
            Console.WriteLine("Classification: {0}", this.Classification);
            Console.WriteLine("Duration: {0} minutes", this.Duration);
            Console.WriteLine("Release Date: {0}", this.ReleaseDate);
            Console.WriteLine("Copies That Exist: {0}", this.NumberOfCopiesThatExist);
            Console.WriteLine("Copies Available: {0}", this.NumberOfCopiesAvailable);
            Console.WriteLine("Times Borrowed: {0}\n", this.NumTimesBorrowed);

            //Then we go to the right node which will print itself as both its children are null
            if (RightNode != null)
            {
                RightNode.InOrderTraversal();
            }
        }

        public void MemberCurrentRented()
        {
            if (LeftNode != null)
            {
                LeftNode.MemberCurrentRented();
            }

            //Then we print the root node 
            Console.WriteLine("Title: {0} Copies Currently Borrowed: {1}", this.Title.PadRight(20), this.NumberOfCopiesRentedByThisUser);

            //Then we go to the right node which will print itself as both its children are null
            if (RightNode != null)
            {
                RightNode.MemberCurrentRented();
            }
        }

        public Movie Find(string movieTitle)
        {
            //this node is the starting current node
            Movie currentNode = this;

            //loop through this node and all of the children of this node
            while (currentNode != null)
            {
                if (movieTitle == currentNode.Title)
                {
                    return currentNode;
                }
                else if (string.Compare(movieTitle, currentNode.Title) == 1)
                {
                    currentNode = currentNode.RightNode;
                }
                else
                {
                    currentNode = currentNode.LeftNode;
                }
            }
            //Node not found
            return null;
        }
    }
}

