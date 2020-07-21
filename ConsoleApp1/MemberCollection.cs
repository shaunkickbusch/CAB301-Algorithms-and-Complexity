using System;

namespace ConsoleApp1
{
    class MemberCollection
    {

        public Member[] Members { get; set; }
        public int AmountOfMembers { get; set; }


        public void addMember(Member myMember)
        {
            this.Members[this.AmountOfMembers] = myMember;
            this.AmountOfMembers++;

        }

        public MemberCollection()
        {
            this.Members = new Member[10];
            this.AmountOfMembers = 0;
        }

        public int authentication()
        {
            string input = "";
            bool inMemberLoginMenu = true;
            bool enteringUsername = true;
            bool enteringPassword = true;
            int memberIndexNumber = -1;

            while (inMemberLoginMenu == true)
            {
                Console.WriteLine("===========Member Login=============\n");
                while (enteringUsername == true)
                {
                    Console.Write("Username: ");
                    input = Console.ReadLine();
                    for (int i = 0; i < AmountOfMembers; i++)
                    {
                        if (input == Members[i].getUserName())
                        {
                            memberIndexNumber = i;
                            enteringUsername = false;
                            break;
                        }
                    }
                    if (enteringUsername == false)
                    {
                        enteringUsername = false;
                        break;
                    }

                    //Else we have an invalid username
                    Console.WriteLine("\nError: invalid username.\n");
                }
                while (enteringPassword == true)
                {
                    Console.Write("Password: ");
                    input = Console.ReadLine();

                    if (input == Members[memberIndexNumber].Password.ToString())
                    {
                        enteringPassword = false;
                        break;
                    }

                    //Else we have an invalid password
                    Console.WriteLine("\nError: Invalid Password.\n");
                }
                Console.WriteLine("\nSuccessfully Authenticated.\n");
                inMemberLoginMenu = false;
            }
            return memberIndexNumber;
        }
    }
}
