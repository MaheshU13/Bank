using System;
using System.Collections;

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

    public string Name
    {
        get { return name; }
        set { name = value; }
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

    public void Deposit()
    {
        Console.Write("Enter amount to be deposited: ");
        dep = float.Parse(Console.ReadLine());
        balance += dep;
        Console.WriteLine("Deposit successful.");
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
            Console.WriteLine("Withdrawal successful.");
        }
    }

    public void Display()
    {
        Console.WriteLine("\n\n\t**************************************************");
        Console.WriteLine("\t\tName: "+name);
        Console.WriteLine("\t\tAccount number: "+accountNum);
        Console.WriteLine("\t\tAccount type (1: Savings, 2: Current): "+type);
        Console.WriteLine("\t\tAmount deposited: "+dep);
        Console.WriteLine("\t\tAmount withdrawn: "+wd);
        Console.WriteLine("\t\tFinal Balance: "+balance);
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
}

class Bank
{
    static ArrayList accounts = new ArrayList;
    static int accountCount = 0;

    public static void Main()
    {
        Console.WriteLine("---------------WELCOME TO VISHAL BANK ------------------\n\n");

        // Initialize dummy accounts
        InitializeDummyAccounts();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Select Login Type:");
            Console.WriteLine("1. Admin");
            Console.WriteLine("2. Customer");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AdminMenu();
                    break;

                case 2:
                    CustomerMenu();
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

    static void AdminMenu()
    {
        Console.WriteLine("\n--- Admin Login ---");
        // You can add a real password check here for admin authentication.
        bool adminExit = false;
        while (!adminExit)
        {
            Console.WriteLine("\nAdmin Options:");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Delete Account");
            Console.WriteLine("3. View All Accounts");
            Console.WriteLine("4. Back to Main Menu");
            Console.Write("Enter your choice: ");
            int adminChoice = int.Parse(Console.ReadLine());

            switch (adminChoice)
            {
                case 1:
                    CreateAccount();
                    break;

                case 2:
                    DeleteAccount();
                    break;

                case 3:
                    ViewAllAccounts();
                    break;

                case 4:
                    adminExit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                    break;
            }
        }
    }

    static void CustomerMenu()
    {
        Console.WriteLine("\n--- Customer Login ---");
        Console.Write("Enter your account number: ");
        int accNum = int.Parse(Console.ReadLine());

        BankAccount account = FindAccount(accNum);

        if (account == null)
        {
            Console.WriteLine("Account not found. Please contact the bank.");
            return;
        }

        bool customerExit = false;
        while (!customerExit)
        {
            Console.WriteLine("\nCustomer Options:");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Balance Enquiry");
            Console.WriteLine("4. Back to Main Menu");
            Console.Write("Enter your choice: ");
            int customerChoice = int.Parse(Console.ReadLine());

            switch (customerChoice)
            {
                case 1:
                    account.Deposit();
                    break;

                case 2:
                    account.Withdraw();
                    break;

                case 3:
                    account.Display();
                    break;

                case 4:
                    customerExit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                    break;
            }
        }
    }

    static void CreateAccount()
    {
        Console.Write("Please enter the customer's name: ");
        string name = Console.ReadLine();

        Console.Write("Please enter a unique 9-digit account number: ");
        int accountNum = int.Parse(Console.ReadLine());

        if (FindAccount(accountNum) != null)
        {
            Console.WriteLine("Account number already exists. Please try another.");
            return;
        }

        Console.Write("Please enter the account type (1 for Savings, 2 for Current): ");
        int type = int.Parse(Console.ReadLine());
        while (type != 1 && type != 2)
        {
            Console.Write("\aInvalid Input! Please enter 1 for Savings or 2 for Current: ");
            type = int.Parse(Console.ReadLine());
        }

        Console.Write("Please enter the initial balance: ");
        float balance = float.Parse(Console.ReadLine());

        BankAccount newAccount = new BankAccount(name, accountNum, type, balance);
        accounts[accountCount++] = newAccount;
        Console.WriteLine("Account created successfully.");
    }

    static void DeleteAccount()
    {
        Console.Write("Enter account number to delete: ");
        int accNumToDelete = int.Parse(Console.ReadLine());

        BankAccount accToDelete = FindAccount(accNumToDelete);
        if (accToDelete != null)
        {
            accToDelete.DeleteAccount();
        }
        else
        {
            Console.WriteLine("Account with number {0} not found.", accNumToDelete);
        }
    }

    static void ViewAllAccounts()
    {
        Console.WriteLine("\n--- Viewing All Accounts ---");
        foreach (BankAccount acc in accounts)
        {
            if (acc != null && !string.IsNullOrEmpty(acc.Name))
            {
                acc.Display();
            }
        }
    }

    static void InitializeDummyAccounts()
    {
        // Dummy account 1
        BankAccount dummyAccount1 = new BankAccount("Raja", 123456789, 1, 150000);
        accounts[accountCount++] = dummyAccount1;

        // Dummy account 2
        BankAccount dummyAccount2 = new BankAccount("Pandit", 987654321, 2, 180000);
        accounts[accountCount++] = dummyAccount2;
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
