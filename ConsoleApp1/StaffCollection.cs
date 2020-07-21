using System;

namespace ConsoleApp1
{
    class StaffCollection
    {
        public Staff[] StaffMembers { get; set; }
        public int NumOfStaffMembers { get; set; }
    
        public StaffCollection()
        {
            this.StaffMembers = new Staff[1];
            this.NumOfStaffMembers = 0;
        }

        public void addStaffMember(Staff myStaff)
        {
            this.StaffMembers[this.NumOfStaffMembers] = myStaff;
            this.NumOfStaffMembers++;
        }

        public void authentication()
        {
            string input = "";
            string staffLoginMenu = "===========Staff Login=============\n";
            string staffLoginUser = "Username: ";
            string staffLoginPassword = "Password: ";
            bool inStaffLoginMenu = true;
            bool enteringUsername = true;
            bool enteringPassword = true;
            int staffIndexNumber = -1;

            while (inStaffLoginMenu == true)
            {
                Console.WriteLine(staffLoginMenu);
                while (enteringUsername == true)
                {
                    Console.Write(staffLoginUser);
                    input = Console.ReadLine();
                    for (int i = 0; i < StaffMembers.Length; i++)
                    {
                        if (input == StaffMembers[i].UserName)
                        {
                            staffIndexNumber = i;
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
                    Console.Write(staffLoginPassword);
                    input = Console.ReadLine();

                    if (input == StaffMembers[staffIndexNumber].Password)
                    {
                        enteringPassword = false;
                        break;
                    }

                    //Else we have an invalid password
                    Console.WriteLine("\nError: Invalid Password.\n");
                }
                Console.WriteLine("\nSuccessfully Authenticated.\n");
                inStaffLoginMenu = false;
            }
        }
    }
}
