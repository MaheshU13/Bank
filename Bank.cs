using System;

class BankAccount
{
    private string name;
    private int accountNum;
    private int type;
    private float balance;
    private float dep;
    private float wd;

    public int AccountNum
    {
        get { return accountNum; }
        set { accountNum = value; }
    }

    public BankAccount(string name, int accountNum, int type, float balance)
    {
        this.name = name;
        this.accountNum = accountNum;
        this.type = type;
        this.balance = balance;
        this.dep = 0;
        this.wd = 0;
    }

    public void Input()
    {
        Console.Write("Please enter your name: ");
        name = Console.ReadLine();

        Console.Write("Please enter your account type (1 for Savings, 2 for Current): ");
        type = int.Parse(Console.ReadLine());
        while (type != 1 && type != 2)
        {
            Console.Write("\aInvalid Input! Please enter 1 for Savings or 2 for Current: ");
            type = int.Parse(Console.ReadLine());
        }

        Console.Write("Please enter your balance: ");
        balance = float.Parse(Console.ReadLine());
    }

    public void Deposit()
    {
        Console.Write("Enter amount to be Deposited: ");
        dep = float.Parse(Console.ReadLine());
        balance += dep;
    }

    public void Withdraw()
    {
        Console.Write("Enter amount to be withdrawn: ");
        wd = float.Parse(Console.ReadLine());
        if (wd > balance)
        {
            Console.WriteLine("Insufficient Balance!");
            wd = 0;
        }
        else
        {
            balance -= wd;
        }
    }

    public void Display()
    {
        Console.WriteLine("\n\n\t**************************************************");
        Console.WriteLine("\t\tName: " + name);
        Console.WriteLine("\t\tAccount number: " + accountNum);
        Console.WriteLine("\t\tAccount type (1: Savings, 2: Current): " + type);
        Console.WriteLine("\t\tAmount deposited: " + dep);
        Console.WriteLine("\t\tAmount withdrawn: " + wd);
        Console.WriteLine("\t\tFinal Balance: " + balance);
    }

    public void DeleteAccount()
    {
        // Resetting all fields to default values
        name = "";
        accountNum = 0;
        type = 0;
        balance = 0;
        dep = 0;
        wd = 0;

        Console.WriteLine("Account deleted successfully.");
    }

    public bool IsDeleted()
    {
        // Checking if account has been deleted (using a simple check on name)
        return string.IsNullOrEmpty(name);
    }
}

class Bank
{
    static BankAccount[] accounts = new BankAccount[10];
    static int accountCount = 0;

    public static void Main()
    {
        Console.WriteLine("---------------WELCOME TO VISHAL BANK ------------------\n\n");

        // Initialize dummy accounts
        InitializeDummyAccounts();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("1. Create or Access an Account");
            Console.WriteLine("2. Delete an Account");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter your account number: ");
                    int accNum = int.Parse(Console.ReadLine());

                    BankAccount account = FindOrCreateAccount(accNum);

                    // Interact with the account
                    if (account != null)
                    {
                        bool accountExit = false;
                        while (!accountExit)
                        {
                            Console.WriteLine("\nAccount Options:");
                            Console.WriteLine("1. Deposit");
                            Console.WriteLine("2. Withdraw");
                            Console.WriteLine("3. Balance Enquiry");
                            Console.WriteLine("4. Back to Main Menu");

                            Console.Write("Enter your choice: ");
                            int accountChoice = int.Parse(Console.ReadLine());

                            switch (accountChoice)
                            {
                                case 1:
                                    account.Deposit();
                                    account.Display();
                                    break;

                                case 2:
                                    account.Withdraw();
                                    account.Display();
                                    break;

                                case 3:
                                    account.Display();
                                    break;

                                case 4:
                                    accountExit = true;
                                    break;

                                default:
                                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                                    break;
                            }
                        }
                    }
                    break;

                case 2:
                    Console.Write("Enter account number to delete: ");
                    int accNumToDelete = int.Parse(Console.ReadLine());

                    BankAccount accToDelete = FindAccount(accNumToDelete);
                    if (accToDelete != null)
                    {
                        accToDelete.DeleteAccount();
                    }
                    else
                    {
                        Console.WriteLine("Account with number {0} not found.",accNumToDelete);
                    }
                    break;

                case 3:
                    exit = true;
                    Console.WriteLine("Exiting program. Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                    break;
            }
        }
    }

    static void InitializeDummyAccounts()
    {
        // Dummy account 1
        BankAccount dummyAccount1 = new BankAccount(" Raja ", 123456789, 1, 150000);
        accounts[accountCount++] = dummyAccount1;

        // Dummy account 2
        BankAccount dummyAccount2 = new BankAccount("Pandit ", 987654321, 2, 180000);
        accounts[accountCount++] = dummyAccount2;
    }

    static BankAccount FindOrCreateAccount(int accountNum)
    {
        BankAccount foundAccount = FindAccount(accountNum);

        if (foundAccount == null)
        {
            Console.WriteLine("Account with number {0} does not exist. Creating new account.",accountNum);

            Console.Write("Please enter your name: ");
            string name = Console.ReadLine();

            Console.Write("Please enter your account type (1 for Savings, 2 for Current): ");
            int type = int.Parse(Console.ReadLine());
            while (type != 1 && type != 2)
            {
                Console.Write("\aInvalid Input! Please enter 1 for Savings or 2 for Current: ");
                type = int.Parse(Console.ReadLine());
            }

            Console.Write("Please enter your balance: ");
            float balance = float.Parse(Console.ReadLine());

            foundAccount = new BankAccount(name, accountNum, type, balance);
            accounts[accountCount++] = foundAccount;
        }

        return foundAccount;
    }

    static BankAccount FindAccount(int accountNum)
    {
        foreach (BankAccount acc in accounts)
        {
            if (acc != null && acc.AccountNum == accountNum)
            {
                return acc;
            }
        }
        return null;
    }
}
