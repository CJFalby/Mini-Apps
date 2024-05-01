using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
//this uses time which i will be using to create pauses in the program 
//this will make the program more readable to the user as they're not
//spammed with loads of text all at once
using System.Threading;

namespace ComputerScience
{
    class Coursework
    {
        //the main method calls the menu system
        public static void Main()
        {
            menu_System();

            //end of the program
            Console.WriteLine("\nQuitting\n");
        }
        //the menu system
        public static void menu_System()
        {
            //sets invalidChoice to true so the menu system will be re-displayed and 
            //the user will be prompted to enter a choice until they choose something valid (1-5)
            bool invalidChoice = true;
            int userChoice = 0;

            Console.Clear();
            Console.WriteLine("\nP4CS Mini Applications");
            Console.WriteLine("----------------------");

            while (invalidChoice == true)
            {
                //catches user input errors
                try
                {
                    //this pauses the program for 0.5 seconds
                    Thread.Sleep(500);

                    Console.WriteLine("Please select an option:");
                    Console.WriteLine(" 1: Keep Counting \n 2: Sqaure Root Calculator \n 3: Encrypt Text - Caesar Cipher \n 4: Decrypt Text - Caesar Cipher \n 5: Quit");
                    Console.WriteLine("----------------------\n");
                    Console.Write("Please enter option: ");
                    userChoice = int.Parse(Console.ReadLine());

                    //a switch case which will take the user another method, based on what number they choose
                    //if they input an invalid option they will be asked to input a correct number by the
                    //default case
                    switch (userChoice)
                    {
                        case 1: keepCounting(); break;
                        case 2: sqrRootCalc(); break;
                        case 3: encryptCaesar(); break;
                        case 4: decryptCaesar(); break;
                        case 5: Console.Write(""); break;
                        default: Console.WriteLine("\nPlease input a number in the range 1-5 corresponding to your choice.\n"); break;
                    }
                    //i had some issues here with after the program has run one of the applications once it kept userChoice
                    //as the previous value even though it should've been reset by the new user-input, to overcome this as
                    //case 5 and the default case are the only part of the switch that reach this pint i made it so if the user input
                    //a 1-5 invalidChoice was false, so the loop would be exited and quit like it should, without affecting the default case
                    if (userChoice >= 1 || userChoice <= 5)
                    {
                        invalidChoice = false;
                    }
                    else
                    {
                        invalidChoice = true;
                    }
                }
                //this catches an error which would break the program if anything other than a number is input         
                catch (System.FormatException)
                {
                    Console.WriteLine("\nError! Please input a number 1-5 corresponding to your choice.\n");
                }
            }
        }

        //method for Keep Counting Application
        public static void keepCounting()
        {
            //variables i need for this application
            int questionsAsked = 0;
            int questNum = 1;
            string randOper = "";
            int correctAns = 0;
            //this is a list to keep track of how many correct answers the user has input
            List<string> correct = new List<string>();

            Thread.Sleep(500);
            Console.WriteLine("\nKeep Counting");
            Console.WriteLine("-------------");
            Console.WriteLine("You will be presented with 10 arithmetic questions.\nAfter the first question, the left-hand operand is\nthe result of the previous addition. ");

            //this do while will repeat until 10 questions have been asked
            do
            {
                //this is to decide randomly if the operator will be + or -
                Random rnd = new Random();
                int addOrSub = rnd.Next(1, 3); //this generates 1 or 2 as the second position isn't inclusive
                if (addOrSub == 1)
                {
                    randOper = "+";
                }
                else if (addOrSub == 2)
                {
                    randOper = "-";
                }

                if (questionsAsked == 0)
                {
                    //method for first question
                    correctAns = keepCounting1(randOper, correct);
                }
                else
                {
                    //method for questions 2-10
                    correctAns = keepCounting2(randOper, questNum, correctAns, correct);
                }
                questionsAsked++;
                questNum++;
            } while (questionsAsked <= 9);

            //sets the variable correctNum (the amount of corrects answers) to the amount of 
            //values inside of the list 'correct '
            int correctNum = correct.Count;
            Console.WriteLine("\nYou got " + correctNum + " out of 10 questions right!\n");

            //returns to the menu
            Thread.Sleep(500);
            Console.Write("Press enter to return to menu system!");
            Console.ReadLine();
            menu_System();
        }

        //method for keep counting question 1
        public static int keepCounting1(string operatorR, List<string> correct)
        {
            //generates two random numbers from 1 to 10
            Random rnd = new Random();
            int randNum1 = rnd.Next(1, 11);
            int randNum2 = rnd.Next(1, 11);
            bool invalidChoice = true;
            int userAnswer = 0;

            while (invalidChoice == true)
            {
                try
                {
                    Console.Write("\nQuestion 1: {0} {1} {2} = ", randNum1, operatorR, randNum2);
                    userAnswer = int.Parse(Console.ReadLine());
                    Console.WriteLine(" ");
                    invalidChoice = false;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("\nError! Please enter a number!");
                }
            }
            int number1 = keepCountingOper(userAnswer, randNum1, operatorR, randNum2, correct);

            //returns the correct answer to the question back to keepCounting to be used as the first
            //operand of the next question
            return number1;
        }

        //smethod for keep counting main questions 
        public static int keepCounting2(string randOper, int questNum, int number1, List<string> correct)
        {
            bool invalidChoice = true;
            Random rnd = new Random();
            int randNum2 = rnd.Next(1, 11);
            int userAnswer = 0;

            while (invalidChoice == true)
            {
                try
                {
                    Console.Write("\nQuestion {0}: {1} {2} {3} = ", questNum, number1, randOper, randNum2);
                    userAnswer = int.Parse(Console.ReadLine());
                    Console.WriteLine(" ");
                    invalidChoice = false;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("\nError! Please enter a number!");
                }
            }
            int correctAns = keepCountingOper(userAnswer, number1, randOper, randNum2, correct);
            return correctAns;
        }

        //method for calculating the correct answer
        public static int keepCountingOper(int userAnswer, int Num1, string operatorR, int Num2, List<string> correct)
        {
            int correctAns = 0;
            if (operatorR == "+")
            {
                correctAns = Num1 + Num2;
                keepCountingAns(userAnswer, correctAns, correct);
            }
            else
            {
                correctAns = Num1 - Num2;
                keepCountingAns(userAnswer, correctAns, correct);
            }
            return correctAns;
        }

        //method for checking if the users answer is correct
        public static List<string> keepCountingAns(int userAnswer, int correctAns, List<string> correct)
        {
            if (correctAns == userAnswer)
            {
                Console.WriteLine("!Correct!");
                //adds a random value of "a" so at the end of the app the length/ amount of
                //values in correct can be counted to give the number of correct values
                correct.Add("a");
            }
            else
            {
                Console.WriteLine("INCORRECT: Answer is " + correctAns);
            }
            Thread.Sleep(500);
            return correct;
        }

        // -Test Plan-
        //Positive Integer| Decimal Places|       Expected Outputs       |        Actual Outputs        |    Testing Because     | Outcome
        // Testing the Positive Integer
        //       8        |       3       |            2.829             |            2.828             |      +ve Integer       | Failed

        // '-> this test failed because i was always finding the value to 6 decimal places and then printing off the
        //     result to the specified number of decimal places, meaning i was getting a value that was incorrect for 3 d.p
        //     as it had iterated through the do while that compares the previous and current average too many times.
        //     To fix this i used 'Math.Round(preAvg, decPla)' where decPla replaced 6
        
        //       8        |       3       |            2.829             |            2.829             |      +ve Integer       |Successful
        //      -6        |      N/A      |   "Error! Make sure to..."   | "How many decimal places..." |      -ve Integer       | Failed

        // '-> this test failed because i thought the try catch would catch this error, however i realised as it was an
        //     integer, it was the correct datatype and i had no pre-caution to stop negative numbers from being input.
        //     To fix it i created an 'if' loop where if posNum is less than 0 an error message is displayed

        //      -6        |      N/A      | "Invalid Choice: Number..."  | "Invalid Choice: Number..."  |      -ve Integer       |Successful
        //       25       |       6       |           5.000000           |           5.000000           |    Square Number       |Successful
        //       abc      |      N/A      |   "Error! Make sure to..."   |   "Error! Make sure to..."   |        String          |Successful
        //      9999      |       4       |            99.9950           |            99.9950           |    Large +ve Integer   |Successful
        //      ';#       |      N/A      |   "Error! Make sure to..."   |   "Error! Make sure to..."   |   Special Characters   |Successful
        //      4.2       |      N/A      |   "Error! Make sure to..."   |   "Error! Make sure to..."   |      Non-Integer       |Successful
        //    [enter]     |      N/A      |   "Error! Make sure to..."   |   "Error! Make sure to..."   |        No Input        |Successful
        //Testing the Decimal Places
        //      103       |       5       |           10.14889           |           10.14889           |    In-Range Integer    |Successful
        //     2003       |      /@       | "Error! Please make sure..." | "Error! Please make sure..." |   Special Characters   |Successful
        //      40        |       0       | "How many decimal places..." | "How many decimal places..." |  Out-of-Range Integer  |Successful
        //      69        |      abc      | "Error! Please make sure..." | "Error! Please make sure..." |        String          |Successful
        //      7         |      786      | "How many decimal places..." | "How many decimal places..." |  Out-of-Range Integer  |Successful
        //      85        |      3.5      | "Error! Please make sure..." | "Error! Please make sure..." |      Non-Integer       |Successful
        //      3         |       1       |             1.7              |             1.7              |   In-Range Integer     |Successful
        //      56        |    [enter]    | "Error! Please make sure..." | "Error! Please make sure..." |        No Input        |Successful
        //      92        |       6       |           9.591663           |           9.591663           |   In-Range Integer     |Successful


        //method for Square Root Calculator Application 
        public static void sqrRootCalc()
        {
            double upperB = 0;
            double lowerB = 0;
            double a = 0;
            int posNum = 0;
            int decPla = 0;
            bool invalidChoice = true;

            Thread.Sleep(500);
            Console.WriteLine("");
            Console.WriteLine("Square Root Calculator");
            Console.WriteLine("----------------------");

            //takes the +ve intger from the user, the user will keep being prompted
            //until they put in a valid option 
            while (invalidChoice == true)
            {
                try
                {
                    Console.Write("Please enter a positive intger: ");
                    posNum = int.Parse(Console.ReadLine());
                    Console.WriteLine(" ");
                    if (posNum < 0)
                    {
                        Console.WriteLine("Invalid Choice: Number must be positive to find the square root!\n");
                        invalidChoice = true;
                    }
                    else
                    {
                        invalidChoice = false;
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("Error! Make sure to enter a positive intger!");
                    Console.WriteLine(" ");
                }
            }

            invalidChoice = true;

            //takes a number 1-6 from the user, the user will keep being prompted 
            //until they put in a valid option
            while (invalidChoice == true)
            {
                try
                {
                    do
                    {
                        Console.Write("How many decimal places do you want the solution calculated to (1-6): ");
                        decPla = int.Parse(Console.ReadLine());
                        Console.WriteLine(" ");
                        invalidChoice = false;
                    } while (decPla < 1 || decPla > 6);
                }
                catch (System.FormatException)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("Error! Please make sure to enter an integer 1-6!");
                    Console.WriteLine(" ");
                }
            }

            //calculates the first square number that is higher than the user input
            //and finds the upper bound by taking the square root of that number, which is 'i'
            int i = 0;
            for (i = 1; a < posNum; i++)
            {
                a = i * i;
                upperB = i;
            }
            //the lower bound is the upper bound -1 as the square root of the user input must be between
            //the lower and upper bounds, unless the input itself was a square number
            lowerB = upperB - 1;

            double preAvg = 0;
            //finds the average of the lower and upper bounds
            double averageUpLo = (upperB + lowerB) / 2;

            //this do while repeats until the previous average and the new average are the same to the
            //amount of decimal places specified by the user
            do
            {
                //if the square of the average is more than the user input the upper bound is updated
                if ((averageUpLo * averageUpLo) > posNum)
                {
                    preAvg = averageUpLo;
                    upperB = averageUpLo;
                }
                //if the square of the average is less than the user input the lower bound is updated
                else if ((averageUpLo * averageUpLo) < posNum)
                {
                    preAvg = averageUpLo;
                    lowerB = averageUpLo;
                }
                //updates the average
                averageUpLo = (upperB + lowerB) / 2;
            } while (Math.Round(preAvg, decPla) != Math.Round(averageUpLo, decPla));

            Console.WriteLine("The square root of " + posNum + " to " + decPla + " decimal places is {0:F" + decPla + "}", averageUpLo);
            Thread.Sleep(500);
            Console.Write("Press enter to return to menu system!");
            Console.ReadLine();
            menu_System();
        }


        //method for Encrypting Text Application
        public static void encryptCaesar()
        {
            string upperText = "";
            string userText = "";
            bool invalidChoice = true;

            Thread.Sleep(500);
            Console.WriteLine("");
            Console.WriteLine("Encrypt Text");
            Console.WriteLine("------------");
            //array of characters the user can input + what is use to encypt/decrypt text
            char[] useableChar = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ' };

            do
            {
                Console.Write("Please enter text to Encrypt: ");
                userText = Console.ReadLine();
                upperText = upperConv(userText);

                //if each character in the uppercase user input is in the array it is accpected -
                //if not the user will have to re-enter their text
                foreach (char x in upperText)
                {
                    if (useableChar.Contains(x))
                    {
                        invalidChoice = false;
                    }
                    else
                    {
                        invalidChoice = true;
                        Console.WriteLine("\nOnly enter letters, numbers and/or spaces!\n");
                        break;
                    }
                }
            } while (invalidChoice == true);

            Console.WriteLine(" ");
            int shift = shift1();
            en_de_crypt(upperText, shift, 1, useableChar);
        }
        //method for Decrypting Text Application
        public static void decryptCaesar()
        {
            string upperText = "";
            string userText = "";
            bool invalidChoice = true;

            Thread.Sleep(500);
            Console.WriteLine("");
            Console.WriteLine("Decrypt Text");
            Console.WriteLine("------------");
            char[] useableChar = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ' };

            do
            {
                Console.Write("Please enter text to Decrypt: ");
                userText = Console.ReadLine();
                upperText = upperConv(userText);

                foreach (char x in upperText)
                {
                    if (useableChar.Contains(x))
                    {
                        invalidChoice = false;
                    }
                    else
                    {
                        invalidChoice = true;
                        Console.WriteLine("\nOnly enter letters, numbers and/or spaces!\n");
                        break;
                    }
                }
            } while (invalidChoice == true);

            Console.WriteLine(" ");
            int shift = shift1();
            en_de_crypt(upperText, shift, 2, useableChar);
        }

        //converts user input to uppercase
        public static string upperConv(string text)
        {
            string UPtext = text.ToUpper();
            return UPtext;
        }

        //gets the shift
        public static int shift1()
        {
            bool invalid = true;
            int shift = 0;
            //keeps on prompting the user for a shift until they enter an integer 1-36
            do
            {
                try
                {
                    do
                    {
                        Console.Write("Please enter shift(1-36): ");
                        shift = int.Parse(Console.ReadLine());
                        Console.WriteLine(" ");
                    } while (shift < 1 || shift > 36);
                    invalid = false;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("Error! Please enter a number 1-36!");
                    Console.WriteLine(" ");
                }
            } while (invalid == true);
            return shift;
        }

        //encyption/decryption method
        private static void en_de_crypt(string text, int shift, int encr_Or_Decr, char[] useableChar)
        {
            //creates two empty strings that the newly shifted charcter will be appended to
            string encrypted = string.Empty;
            string decrypted = string.Empty;

            //every character of the user input is checked against each value in the array until it is found
            foreach (char x in text)
            {
                for (int y = 0; y <= 36; y++)
                {
                    if (useableChar[y] == x)
                    {
                        //if it is to be encrypted, the character's position in the array is is added to by 
                        //the shift, then modulo 37 is used to find the remainder. If there is no remainder then 
                        //it is not affected and the character is just shifted by the shift amount.
                        //if there is a remainder this means it reached then end of the array so the modulous 37 will make 
                        //it wrap back round to the beginning of the array and continue the rest of the shift
                        if (encr_Or_Decr == 1)
                        {
                            encrypted += useableChar[(y + shift) % 37];
                        }
                        //if it is to be decrypted, the character's position in the array is subtracted from by
                        //the shift amount. if the subtraction will require the array to wrap around to the end
                        //the IndexOutOfRangeException is caught and the same operation is done but added to by 37
                        //which gives the same result as what wrapping around to the end of the array and continuing
                        //the subtraction would do
                        else
                        {
                            try
                            {
                                decrypted += useableChar[(y - shift)];
                            }
                            catch (System.IndexOutOfRangeException)
                            {
                                decrypted += useableChar[(y - shift) + 37];
                            }
                        }
                    }
                }
            }
            if (encr_Or_Decr == 1)
            {
                Console.WriteLine("Encoded string is: " + encrypted);
            }
            else
            {
                Console.WriteLine("Decoded string is: " + decrypted);
            }
            Thread.Sleep(500);
            Console.Write("\nPress enter to return to menu system!");
            Console.ReadLine();
            menu_System();
        }
    }
}