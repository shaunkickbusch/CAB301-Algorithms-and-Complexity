using System;

namespace ConsoleApp1
{
    class MovieCollection
    {
        public Movie Root { get; set; }

        public void Insert(Movie myMovie)
        {
            //if the root is not null then we call the Insert method on the root node
            if (Root != null)
            {
                Root.Insert(myMovie);
            }
            else
            {
                Root = myMovie;
            }
        }

        public void InOrderTraversal()
        {
            if (Root != null)
            {
                Root.InOrderTraversal();
            }
        }

        public void MemberCurrentRented()
        {
            if (Root != null)
            {
                Root.MemberCurrentRented();
            }
        }

        public void Top10MostPopularMovies(Movie[] myArray)
        {
            //Sort the array based on number of times borrowed
            MergeSort(myArray, 0, myArray.Length - 1);

            //Iterate over the array and print out the movies
            for (int i = 0; i < myArray.Length; i++)
            {
                Console.WriteLine("Title: {0} Num Times Borrowed: {1}", myArray[i].Title.PadRight(20), myArray[i].NumTimesBorrowed);
                //When we've printed 10 movies we break
                if (i == 9)
                {
                    break;
                }
            }
            //There are no movies in the library
            if (myArray.Length == 0)
            {
                Console.WriteLine("There's currently no movies in the library");
            }
        }

        static public void MergeSort(Movie[] input, int startIndex, int endIndex)
        {
            int mid;

            if (endIndex > startIndex)
            {
                mid = (endIndex + startIndex) / 2;
                //Sort the 1st half of the array
                MergeSort(input, startIndex, mid);
                //Sort the second half of the array
                MergeSort(input, mid + 1, endIndex);
                Merge(input, startIndex, (mid + 1), endIndex);
            }
        }

        static public void Merge(Movie[] input, int left, int mid, int right)
        {
            Movie[] temp = new Movie[input.Length];
            int tmpPos;
            int leftEnd = mid - 1;
            tmpPos = left;
            int numElem = right - left + 1;

            while ((left <= leftEnd) && (mid <= right))
            {
                //Merge in decending order
                if (input[left].NumTimesBorrowed >= input[mid].NumTimesBorrowed)
                {
                    temp[tmpPos++] = input[left++];
                }
                else
                {
                    temp[tmpPos++] = input[mid++];
                }
            }
            while (left <= leftEnd)
            {
                temp[tmpPos++] = input[left++];
            }

            while (mid <= right)
            {
                temp[tmpPos++] = input[mid++];
            }

            for (int i = 0; i < numElem; i++)
            {
                input[right] = temp[right];
                right--;
            }
        }

        public int FlattenBST(Movie currentNode, Movie[] myArray, int i)
        {
            if (currentNode == null)
            {
                return i;
            }
            //Add the movie object to the array
            myArray[i] = currentNode;
            //increment to go to a new position
            i++;
            if (currentNode.LeftNode != null)
            {
                //Call the function recusively therefore adding the left node to the myArray array
                i = FlattenBST(currentNode.LeftNode, myArray, i);
            }

            if (currentNode.RightNode != null)
            {
                //Call the function recusively therefore adding the right node to the myArray array
                i = FlattenBST(currentNode.RightNode, myArray, i);
            }
            return i;
        }

        public int GetNumNodes()
        {
            if (Root != null)
            {
                return Root.GetNumNodes();
            }
            else
            {
                return 0;
            }
        }

        public Movie Find(string movieTitle)
        {
            //if the root is not null then we call the find method on the root
            if (Root != null)
            {

                return Root.Find(movieTitle);
            }
            else
            {
                return null;
            }
        }

        // Function to delete node from the BST
        public void Remove(string movieTitle)
        {
            Movie currentNode = this.Root;
            Movie parent = null;

            //Loop through until a movie is not found (null) or until we've found the movie with that title we want
            while (currentNode != null && currentNode.Title != movieTitle)
            {
                parent = currentNode;

                //if the movie title we are looking for is lexographically less than the current node, we'll look at its left child
                if (string.Compare(movieTitle, currentNode.Title) == -1)
                {
                    currentNode = currentNode.LeftNode;
                }
                //Else the movie title we are looking for is lexographically more than the current node, we'll look at its right child
                else
                {
                    currentNode = currentNode.RightNode;
                }
            }
            //Root is null so return nothing
            if (currentNode == null)
            {
                return;
            }
            //We have a node that has a left and a right child
            if ((currentNode.LeftNode != null) && (currentNode.RightNode != null))
            {
                if (currentNode.LeftNode.RightNode == null)
                {
                    currentNode = currentNode.LeftNode;
                    currentNode.LeftNode = currentNode.LeftNode.LeftNode;
                }
                else
                {
                    //Assign the left child
                    Movie p = currentNode.LeftNode;
                    //parent of the left child
                    Movie pp = currentNode;
                    while (p.RightNode != null)
                    {
                        pp = p;
                        p = p.RightNode;
                    }   
                    currentNode = p;
                    //Set the right node on the parent node of the child node (p) to the left child node of p in case it has one
                    pp.RightNode = p.LeftNode;
                }
            }
            //If we get to this point, the node has a left child and no right child, a right child and no left child, or no children at all
            else
            {
                Movie c;
                if (currentNode.LeftNode != null)
                {
                    c = currentNode.LeftNode;
                }
                else
                {
                    c = currentNode.RightNode;
                }
   
                if (currentNode == this.Root)
                { 
                    this.Root = c;
                }
                else
                {
                    if (currentNode == parent.LeftNode)
                    {
                        parent.LeftNode = c;
                    }
                    else
                    {
                        parent.RightNode = c;
                    }
                }
            }
        }
    }
}
